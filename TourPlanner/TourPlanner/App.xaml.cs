﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//using TourPlanner.;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //data context needs to be set here => otherwise fail
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // BIZ-layer => maybe factory should be in here 
            //var searchEngine = new StandardSearchEngine();

            // MVVM:
            //var searchBarViewModel = new SearchBarViewModel();
            var subWindowTour = new SubWindowViewTour();

            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(subWindowTour),
                //SubWindowViewTour = { DataContext = subWindowTour}
                //
                //subWindowTour = { DataContext = subWindowTour }
            };
            
            wnd.Show();
        }
        
    }
}
