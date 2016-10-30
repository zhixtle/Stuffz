using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pastebook.Models;
using Pastebook.Controllers;
using Pastebook.Managers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PastebookUnitTest
{
    [TestClass]
    public class SearchControllerUnitTest
    {
        [TestMethod]
        public void SearchResultsTestMethodViewNameReturn()
        {
            var controller = new SearchController();
            var result = controller.SearchResults("Liz") as PartialViewResult;
            Assert.AreEqual("SearchResults", result.ViewName);
        }

        [TestMethod]
        public void GetSearchResultsTestMethodViewDataReturn()
        {
            var controller = new SearchController();
            var result = controller.GetSearchResults("Liz") as JsonResult;
            Assert.AreEqual("{ results = Liz }", result.Data.ToString());
        }

        [TestMethod]
        public void SearchResultsTestMethodViewDataReturn()
        {
            var controller = new SearchController();
            var result = controller.SearchResults("Liz") as PartialViewResult;
            var searchResult = (List<UserProfileModel>)result.ViewData.Model;
            Assert.AreEqual("Tester", searchResult[0].FirstName);
        }

        [TestClass]
        public class UserManagerSearchUnitTest
        {
            [TestMethod]
            public void GetUsersSearchResultsTestMethod()
            {
                var manager = new UserManager();
                var searchResult = manager.GetUsersSearchResults("Liz");
                Assert.AreEqual("Tester", searchResult[0].FirstName);
            }
        }
    }
}
