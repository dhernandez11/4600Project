using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;
using System.Runtime.Remoting.Channels;
using ListBox = System.Windows.Controls.ListBox;
using System.Xaml.Permissions;

namespace _4600Project
{
    public class MonthView : Calendar 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty AppointmentsProperty = DependencyProperty.Register
            (
                "Appointments",
                typeof(ObservableCollection<Appointment>),
                typeof(Calendar)
            );

        public ObservableCollection<Appointment> Appointments
        {
            get { return (ObservableCollection<Appointment>)GetValue(AppointmentsProperty); }
            set { SetValue(AppointmentsProperty, value); }
        }
        static MonthView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthView), new FrameworkPropertyMetadata(typeof(MonthView)));
        }
        public MonthView() : base()
        {
            SetValue(AppointmentsProperty, new ObservableCollection<Appointment>());
        }
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            FrameworkElement element = e.OriginalSource as FrameworkElement;

            if (element.DataContext is DateTime)
            {
                CreateAppointmentWindow appointmentWindow = new CreateAppointmentWindow
                (
                    (Appointment appointment) =>
                    {
                        Appointments.Add(appointment);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Appointments"));
                    }
                );

                appointmentWindow.Show();
                return;
            }

           
        }
       
    }
   
}