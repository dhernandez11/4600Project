using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _4600Project
{
    [ValueConversion(typeof(ObservableCollection<Appointment>), typeof(ObservableCollection<Appointment>))]

    public class AppointmentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime date = (DateTime)values[1];

            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment appointment in ((ObservableCollection<Appointment>)values[0]))
            {
                if (appointment.Date.Date == date)
                {
                    appointments.Add(appointment);
                }
            }

            return appointments;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
