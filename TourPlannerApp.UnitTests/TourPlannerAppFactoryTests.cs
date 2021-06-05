using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerApp.BusinessLayer;
using NUnit.Framework;
using Moq;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;
using System.Linq;

namespace TourPlannerApp.UnitTests
{
    
    [TestFixture]
    public class TourPlannerAppFactoryTests
    {
        public Mock<ITourEntryDAO> mockTourEntryDAO;
        public Mock<ILogEntryDAO> mockLogEntryDAO;

        [SetUp]
        public void setUp()
        {
           mockTourEntryDAO = new Mock<ITourEntryDAO>();
           mockLogEntryDAO = new Mock<ILogEntryDAO>();
        }

        [TearDown]
        public void teardown()
        {
            mockTourEntryDAO = null;
            mockLogEntryDAO = null;
        }

        [Test]
        public void GetTours_Test()
        {
            // ARANGE
            Mock<ITourEntryDAO> mockTourEntryDAO;
            mockTourEntryDAO = new Mock<ITourEntryDAO>();
            List<TourEntry> testTourList = new List<TourEntry>();
            testTourList.Add(new TourEntry(1, "testName", "testDescription", 100));

            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            mockTourEntryDAO.Setup(mock => mock.GetTours()).Returns(testTourList);

            // ACT
            IEnumerable<TourEntry> actualTourList = tourPlannerAppFactoryImp.GetTours();

            // ASSERT
            Assert.AreEqual(testTourList[0].Name, actualTourList.ToList()[0].Name);
            Assert.AreEqual(testTourList[0].Description, actualTourList.ToList()[0].Description);
            Assert.AreEqual(testTourList[0].Distance, actualTourList.ToList()[0].Distance);

        }

        [Test]
        public void Search_Test_WithFoundItems()
        {
            // ARANGE
            // var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            List<TourEntry> testTourList = new List<TourEntry>();
            testTourList.Add(new TourEntry(1, "testName", "testDescription", 100));
            mockTourEntryDAO.Setup(mock => mock.GetTours()).Returns(testTourList);

            // ACT
            List<TourEntry> foundTourList = (List<TourEntry>)tourPlannerAppFactoryImp.Search("test");

            // ASSERT
            Assert.IsNotEmpty(foundTourList);
        }


        [Test]
        public void Search_Test_WithoutFoundItems()
        {
            // ARANGE
            // var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            List<TourEntry> testTourList = new List<TourEntry>();
            testTourList.Add(new TourEntry(1, "testName", "testDescription", 100));
            mockTourEntryDAO.Setup(mock => mock.GetTours()).Returns(testTourList);

            // ACT
            List<TourEntry> foundTourList = (List<TourEntry>)tourPlannerAppFactoryImp.Search("abc");

            // ASSERT
            Assert.AreNotEqual(testTourList, foundTourList);
        }
        

        [Test]
        public void CreateTour_Test()
        {
            // ARANGE
            // var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            TourEntry testTourEntry = new TourEntry(1, "testName", "testDescription", 1234);
            mockTourEntryDAO.Setup(mock => mock.AddNewTour("testName", "testDescription", 1234)).Returns(testTourEntry);
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);

            // ACT
            TourEntry addedTour = tourPlannerAppFactoryImp.CreateTour("wrongName", "testDescription", 1234);

            // ASSERT
            Assert.AreNotEqual(testTourEntry, addedTour);
        }

        [Test]
        public void UpdateTour_Test()
        {
            // ARANGE
            TourEntry testTourEntry = new TourEntry(1, "testName", "testDescription", 1234);
            mockTourEntryDAO.Setup(mock => mock.UpdateTour(testTourEntry, "testName", "testDescription", 123)).Verifiable();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);

            // ACT
            tourPlannerAppFactoryImp.UpdateTour(testTourEntry, "testName", "testDescription", 123);

            // ASSERT
            mockTourEntryDAO.Verify(mock => mock.UpdateTour(testTourEntry, "testName", "testDescription", 123), Times.Once());
        }


        [Test]
        public void GetRouteImgPath_Test()
        {
            // ARANGE
            // var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            string testTourName = "testName";
            string expectedPath = "\\testName.png";

            // ACT
            string actualPath = tourPlannerAppFactoryImp.GetRouteImgPath(testTourName);

            // ASSERT
            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void GetRouteImgPath_Wrong_Test()
        {
            // ARANGE
            // var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            string testTourName = "testWrongName";
            string expectedPath = "\\testName.png";

            // ACT
            string actualPath = tourPlannerAppFactoryImp.GetRouteImgPath(testTourName);

            // ASSERT
            Assert.AreNotEqual(expectedPath, actualPath);
        }

        [Test]
        public void CreateLog_Test()
        {
            // ARANGE
            TourEntry testTourEntry = new TourEntry(1, "testName", "testDescription", 1234);
            LogEntry testLogEntry = new LogEntry(1, testTourEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123);
            mockLogEntryDAO.Setup(mock => mock.AddNewLog(testTourEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123)).Returns(testLogEntry);
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockLogEntryDAO.Object);

            // ACT
            LogEntry addedLog = tourPlannerAppFactoryImp.CreateLog(testTourEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123);

            // ASSERT
            Assert.AreEqual(testLogEntry.Report, addedLog.Report);
        }

        [Test]
        public void GetLogsByTour_Test()
        {
            // ARANGE
            TourEntry testTourEntry = new TourEntry(1, "testName", "testDescription", 1234);
            List<LogEntry> testLogList = new List<LogEntry>();
            testLogList.Add(new LogEntry(1, testTourEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123));
            mockLogEntryDAO.Setup(mock => mock.GetLogsByTour(testTourEntry)).Returns(testLogList);
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockLogEntryDAO.Object);

            // ACT
            List<LogEntry> returnedLogList = (List<LogEntry>)tourPlannerAppFactoryImp.GetLogsByTour(testTourEntry);

            // ASSERT
            Assert.AreEqual(testLogList[0].Report, returnedLogList.ToList()[0].Report);
        }

        [Test]
        public void UpdateLog_Test()
        {
            // ARANGE
            TourEntry testTourEntry = new TourEntry(1, "testName", "testDescription", 1234);
            LogEntry testLogEntry = new LogEntry(1, testTourEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123);
            mockLogEntryDAO.Setup(mock => mock.UpdateLog(testLogEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123)).Verifiable();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockLogEntryDAO.Object);

            // ACT
            tourPlannerAppFactoryImp.UpdateLog(testLogEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123);

            // ASSERT
            mockLogEntryDAO.Verify(mock => mock.UpdateLog(testLogEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123), Times.Once());
        }

        [Test]
        public void DeleteLog_Test()
        {
            // ARANGE
            TourEntry testTourEntry = new TourEntry(1, "testName", "testDescription", 1234);
            LogEntry testLogEntry = new LogEntry(1, testTourEntry, new DateTime(), "testReport", 123, new TimeSpan(), 123);
            mockLogEntryDAO.Setup(mock => mock.DeleteLog(testLogEntry)).Verifiable();
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockLogEntryDAO.Object);

            // ACT
            tourPlannerAppFactoryImp.DeleteLog(testLogEntry);

            // ASSERT
            mockLogEntryDAO.Verify(mock => mock.DeleteLog(testLogEntry), Times.Once());
        }

        [Test]
        public void GetReportFolderPath_Test()
        {
            // ARANGE
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            string testTourName = "testName";
            string testFilePath = "\\testName.pdf";

            // ACT
            string returnedFilePath = tourPlannerAppFactoryImp.GetReportFolderPath(testTourName);

            // ASSERT
            Assert.AreEqual(returnedFilePath, testFilePath);

        }

        [Test]
        public void GetReportFolderPath_Wrong_Test()
        {
            // ARANGE
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            string testTourName = "testWrongName";
            string testFilePath = "\\testName.pdf";

            // ACT
            string returnedFilePath = tourPlannerAppFactoryImp.GetReportFolderPath(testTourName);

            // ASSERT
            Assert.AreNotEqual(returnedFilePath, testFilePath);

        }


    }
}
