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
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Window
    {
        public ModifyUser()
        {
            InitializeComponent();
        }

        private void ModifyUserClick(object sender, RoutedEventArgs e)
        {
            int _userID = Convert.ToInt32(userIDInput.Text);
            string _userFirstName = userFirstNameInput.Text;
            string _userSurname = userSurnameInput.Text;
            string _userUname = userUnameInput.Text;
            //string _userPword = userPassowrdInput.Text;
            int _userPriv = Convert.ToInt32(userPrivInput.Text);
            DatabaseConnection.DataConn.EditUserInDB(_userID, _userFirstName, _userSurname, _userUname, _userPriv);
        }
        private void DataManagementClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
