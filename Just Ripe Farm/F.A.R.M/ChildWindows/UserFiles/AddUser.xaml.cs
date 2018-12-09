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
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();

            //Call Method for filling user data grid
            FillUserList();
        }



        private void SubmitUserClick(object sender, RoutedEventArgs e)
        {
            DatabaseConnection.DataConn.AddUserToDB(10,"bob", "dylan", "bDylan", "password", 1);
        }


        private void DataManagementClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void FillUserList()
        {
            //Bind each row as a source
            _addUserGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetUserList() });
            //_dmListGridUser.ItemsSource = DatabaseConnection.DataConn.GetUserList().DefaultView;
        }
    }
}
