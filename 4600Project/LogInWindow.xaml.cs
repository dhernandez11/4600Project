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
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewCalendar cal = new CreateNewCalendar();
            if(txtbxLogInName.Text != cal.getTitle())
            {
                MessageBox.Show("Calendar Name or Password Incorrect");
            }
            else if(pwrdbxPassword.Password != cal.getPassword())
            {
                MessageBox.Show("Calendar Name or Password Incorrect");
            }
            else
            {
                //open appropriate calendar window here
                this.Hide();
            }
        }
    }
}
