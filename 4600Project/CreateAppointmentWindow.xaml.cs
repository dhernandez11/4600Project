using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _4600Project
{
    /// <summary>
    /// Interaction logic for CreateAppointmentWindow.xaml
    /// </summary>
    public partial class CreateAppointmentWindow : Window
    {
        private Action<Appointment> saveApp;

        public CreateAppointmentWindow(Action<Appointment> saveApp)
        {
            InitializeComponent();

            this.saveApp = saveApp;
        }
        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Subject = txtbxAppointmentTitle.Text;
            appointment.Date = datePicker.SelectedDate.Value;

            appointment.Time = cmbxHour.Text + ":" + cmbxMinute.Text + " " + cmbxAMorPM.Text;
            appointment.Location = txtbxAppointmentLocation.Text;

            saveApp(appointment);

            Close();
        }

        private void btnAddCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            //for some reason the month shows up 00 always. not sure why
            //also the xmal labels in the designer look fine but when the appointment window comes up at execution, they look bad
            //i'll try and work on it and see what up though

            txtbxAppointmentDate.Text = Convert.ToDateTime(datePicker.SelectedDate, CultureInfo.GetCultureInfo("en-US")).ToString("mm/dd/yyyy");
        }
    }
}

