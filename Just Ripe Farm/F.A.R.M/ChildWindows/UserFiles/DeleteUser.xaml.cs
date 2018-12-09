using FarmControl;
using System;
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

namespace F.A.R.M.ChildWindows
{
    /// <summary>
    /// Interaction logic for DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        private void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            //    string _userID = deleteUserIdInput.Text;
            string _userUname = deleteUserNameInput.Text;
            string _userPword = DeleteUserPasswordInput.Text;
            DatabaseConnection.DataConn.DeleteUserFromDB(_userUname, _userPword);
            this.Close();
        }

        private void DataManagementClick(object sender, RoutedEventArgs e)
         {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
         }
    }
}
