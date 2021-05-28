using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlannerApp.ViewModels;

namespace TourPlannerApp
{
    /// <summary>
    /// Interaction logic for CreateTourWindow.xaml
    /// </summary>
    public partial class CreateTourWindow : Window
    {
        public CreateTourWindow()
        {
            InitializeComponent();
            this.DataContext = new CreateTourViewModel();
        }
    }
}
