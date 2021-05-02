using Assignment2Project.Models;
using Assignment2Project.Repository;
using Assignment2Project.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestAssignment2Project
{
    public class UnitTestAssetController
    {
       [Fact]
       public async Task Index_ReturnsAViewResult_WithAListOfAssets()
        {
            //Arrange - Set up connections and instantiate any objects.
            var mockRepo = new MockUnitOfWork();
            var controller = new AssetController(mockRepo);

            //Act - Populate the variable
            var result = await controller.Index(""); // As Index() requires a string for the search an empty string was required to return all data without being filtered.

            //Assert - Check everything is being returned correctly.
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AssetModel>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfSearchedAssets()
        {
            //Arrange
            var mockRepo = new MockUnitOfWork();
            var controller = new AssetController(mockRepo);

            //Act
            var result = await controller.Index("2"); // passes a search parameter to return a list of results filtered by the search parameter.

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AssetModel>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]

        public async Task EditPost_ReturnsNotFoundResult_WhenModelIdIsIncorrect()
        {
            //Arrange
            var mockRepo = new MockUnitOfWork();
            var controller = new AssetController(mockRepo);
            var asset = new AssetModel()
            {
                AssetId = 4,
                AssetName = "AssetTest4"
            };

            //Act
            var result = await controller.Edit(-1, asset); // Passes an int id and aseet to result.

            //Assert
            Assert.IsType<NotFoundResult>(result); //checks if the id (-1) is not same as the asset AssetId (4) and returns not found.
        }

        [Fact]
        public async Task EditPost_ReturnsRedirectToIndexAction_WhenModelIsValid()
        {
            //Arrange
            var mockRepo = new MockUnitOfWork();
            var controller = new AssetController(mockRepo);
            var asset = new AssetModel()
            {
                AssetId = 4,
                AssetName = "AssetTest4"
            };

            //Act
            var result = await controller.Edit(4, asset);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result); //Checks if there is a redirect to action.
            Assert.Equal("Index", redirectToActionResult.ActionName); //Checks the correct redirect.
        }
    }
}
