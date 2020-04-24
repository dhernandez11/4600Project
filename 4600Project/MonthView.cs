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

            //calls function when listbox item is double clicked
            appointmentsLbx_SelectedItem();
        }
        public void appointmentsLbx_SelectedItem()
        {
           //for now I just have a series of messageboxes to ask the user if they want to edit/delete appointment (listbox item) when they double click
           //the appointment and the current var lb is just a place holder, the findname() returns null
            var lb = (ListBox)FindName("appointmentsLbx");

            if (lb.SelectedValue.ToString() == "")
            { 
            DialogResult dr = System.Windows.Forms.MessageBox.Show("Would you like to edit or delete this appointment?", "Appointment Editor", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    DialogResult dr2 = System.Windows.Forms.MessageBox.Show("Would you like to edit the appointment?", "Appointment Editor", MessageBoxButtons.YesNo);
                    if (dr2 == DialogResult.Yes)
                    {
                        lb.Items.RemoveAt(lb.Items.IndexOf(lb.SelectedItem));

                        CreateAppointmentWindow appointmentWindow = new CreateAppointmentWindow((Appointment appointment) =>
                            {
                                Appointments.Add(appointment);

                                if (PropertyChanged != null)
                                {
                                    PropertyChanged(this, new PropertyChangedEventArgs("Appointments"));

                                }
                            }

                        );
                        appointmentWindow.Show();
                    }
                    else if (dr2 == DialogResult.No)
                    {
                        DialogResult dr3 = System.Windows.Forms.MessageBox.Show("Would you like to delete the appointment?", "Appointment Editor", MessageBoxButtons.YesNo);

                        if (dr3 == DialogResult.Yes)
                        {
                            lb.Items.RemoveAt(lb.Items.IndexOf(lb.SelectedItem));
                        }
                        else
                        {
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }

            }
           
        }
    }
   
}