﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Sql;
using F.A.R.M.Properties;
using System.Data;
using FarmControl;

namespace F.A.R.M.ChildWindows
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddVehicle : Window
    {
        public AddVehicle()
        {
            InitializeComponent();

            //FullVehicleList();
        }

        //private void FullVehicleList()
        //{
        //    throw new NotImplementedException();
        //}

        private void DataManagementClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}