using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlannerApp.BusinessLayer;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;
using TourPlannerApp.ViewModels;

namespace TourPlannerApp.UnitTests
{
    [TestFixture]
    public class MainViewModelTests
    {

        [Test]
        public void FillListView_Test()
        {
            // ARANGE
            var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            MainViewModel _sut = new MainViewModel(mockTourEntryDAO.Object);
            ITourPlannerAppFactory tourPlannerAppFactoryImp = TourPlannerAppFactory.GetInstance(mockTourEntryDAO.Object);
            ObservableCollection<TourEntry> testTours = new ObservableCollection<TourEntry>();
            testTours.Add(new TourEntry(1, "testName", "testDescription", 100));
            mockTourEntryDAO.Setup(mock => mock.GetTours()).Returns(testTours);

            // ACT
            _sut.FillListView();

            // ASSERT
            Assert.AreEqual(_sut.Tours, testTours);
        }


        [Test]
        public void InitListView_Test()
        {
            // ARANGE
            var mockTourEntryDAO = new Mock<ITourEntryDAO>();
            MainViewModel _sut2 = new MainViewModel(mockTourEntryDAO.Object);
            ObservableCollection<TourEntry> testTours2 = new ObservableCollection<TourEntry>();
            testTours2.Add(new TourEntry(1, "testName", "testDescription", 100));
            mockTourEntryDAO.Setup(mock => mock.GetTours()).Returns(testTours2);

            // ACT
            _sut2.InitListView();

            // ASSERT
            Assert.IsNotEmpty(_sut2.Tours);
        }
    }
}