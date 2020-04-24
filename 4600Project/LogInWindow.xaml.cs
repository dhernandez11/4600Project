using Microsoft.Win32;
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

namespace _4600Project
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private CalendarWindow calendar;
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
           int userId = Database.GetUserById(txtbxLogInName.Text, pwrdbxPassword.Password);
           if(userId > 0)
            {
                //open appropriate calendar window here
                if(calendar == null)
                {
                    calendar = new CalendarWindow();
                }
                this.Close();

                calendar.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Calendar Name or Password");
                txtbxLogInName.Clear();
                pwrdbxPassword.Clear();
            }
        }
    }
}
