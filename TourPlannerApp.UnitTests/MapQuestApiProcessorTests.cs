using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TourPlannerApp.DataAccessLayer;
using TourPlannerApp.Models;

namespace TourPlannerApp.UnitTests
{
    [TestFixture]
    class MapQuestApiProcessorTests
    {
        [Test]
        public void CreateDirectionApiUrl_Test()
        {
            // ARANGE
            MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            string actualUrl = "http://www.mapquestapi.com/directions/v2/route?key=&from=fromTest&to=toTest";

            // ACT
            string testUrl = mapQuestApiProcessor.CreateDirectionApiUrl("fromTest", "toTest", "tourNameTest");

            // ASSERT
            Assert.AreEqual(actualUrl, testUrl);
        }

        [Test]
        public void CreateStaticMapApiUrl_Test()
        {
            // ARANGE
            MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            string testSessionId = "abc123";
            string testBoundingBox = "123456";
            string actualUrl = "https://www.mapquestapi.com/staticmap/v5/map?key=&size=640,480&zoom=11&session=abc123&boundingBox=123456";

            // ACT
            string testUrl = mapQuestApiProcessor.CreateStaticMapApiUrl(testSessionId, testBoundingBox);

            // ASSERT
            Assert.AreEqual(actualUrl, testUrl);
        }

        [Test]
        public void CreateImageFilePath_Test()
        {
            // ARANGE
            MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            string actualPath = "\\testName.png";

            // ACT
            string testPath = mapQuestApiProcessor.CreateImageFilePath("testName");

            // ASSERT
            Assert.AreEqual(testPath, actualPath);
        }

        [Test]
        public void CreateImageFilePath_Wrong_Test()
        {
            // ARANGE
            MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            string actualPath = "\\testName.png";

            // ACT
            string testPath = mapQuestApiProcessor.CreateImageFilePath("test");

            // ASSERT
            Assert.AreNotEqual(testPath, actualPath);
        }

        [Test] public void ExtractInfoFromRootObj()
        {
            // ARANGE
            MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            Root testObj = new Root();
            Route testRouteObj = new Route();
            BoundingBox testBoundingBoxObj = new BoundingBox();
            Ul testUlObj = new Ul();
            Lr testLrObj = new Lr();
            testObj.route = testRouteObj;
            testObj.route.boundingBox = testBoundingBoxObj;
            testObj.route.boundingBox.ul = testUlObj;
            testObj.route.boundingBox.lr = testLrObj;
            testObj.route.sessionId = "testSessionId";
            testObj.route.boundingBox.ul.lat = 123;
            testObj.route.boundingBox.ul.lng = 123;
            testObj.route.boundingBox.lr.lat = 123;
            testObj.route.boundingBox.lr.lng = 123;
            string testBoundingBox = "123,123,123,123";
            Tuple<string, string> actualTuple = new Tuple<string, string>(testObj.route.sessionId, testBoundingBox);

            // ACT
            Tuple<string, string> t = mapQuestApiProcessor.ExtractInfoFromRootObj(testObj);

            // ASSERT
            Assert.AreEqual(t, actualTuple);
        }
    }
}
