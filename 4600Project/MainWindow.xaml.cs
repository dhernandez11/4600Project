using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _4600Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreateCalendarWindow NewCalendar;
        private LogInWindow LogInCalendar;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnNewCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (NewCalendar == null)
            {
                NewCalendar = new CreateCalendarWindow();
            }
            NewCalendar.Show();
        }

        private void btnJoinCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (LogInCalendar == null)
            {
                LogInCalendar = new LogInWindow();
            }
            LogInCalendar.Show();
            this.Close();
        }
        
    }

}
