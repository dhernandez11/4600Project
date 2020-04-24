using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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
        private List<Member> membersList = new List<Member>();


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

        private void chkbxReminder_Checked(object sender, RoutedEventArgs e)
        {
            rReminder.Visibility = Visibility.Visible;
            txtblReminder.Visibility = Visibility.Visible;
            lbxReminder.Visibility = Visibility.Visible;
            btnSendReminder.Visibility = Visibility.Visible;
            lblReminderMembers.Visibility = Visibility.Visible;


            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dvwvi\Source\Repos\4600Project\4600Project\Database1.mdf;Integrated Security=True");
            using (SqlCommand command = new SqlCommand("SELECT name, emailaddress FROM [Members] WHERE username=@username", connection))
            {
                command.Parameters.AddWithValue("@username", "calendar");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = Convert.ToString(reader["name"]);
                    string email = Convert.ToString(reader["emailaddress"]);

                    lbxReminder.Items.Add(name + ": " + email);
                    Member member = new Member(name, email);

                    addMemb(member);

                }
                connection.Close();
            }


        }
        public void addMemb(Member member)
        {
            this.membersList.Add(member);
        }

        private void btnSendReminder_Click(object sender, RoutedEventArgs e)
        {
          

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.outlook.com");

            mail.From = new MailAddress("vDayCalendar@outlook.com");
            mail.Body = "This is a reminder for your appointment: " + txtbxAppointmentTitle.Text + " at " + cmbxHour.Text + ":" + cmbxMinute.Text + " " + cmbxAMorPM.Text + " located at " + txtbxAppointmentLocation.Text + " on " + datePicker.SelectedDate.Value.Date.ToShortDateString() + ".";
            mail.Subject = "vDay Reminder";

            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("vDayCalendar@outlook.com", "v8Day8Cal");


            foreach (Member member in membersList)
            {
                if(lbxReminder.SelectedItem.ToString() == member.getName() + ": " + member.getEmailAddress())
                {
                    MailAddress To = new MailAddress(member.getEmailAddress());
                    mail.To.Add(member.getEmailAddress());
                    smtp.Send(mail);
                }
              

            }

            rReminder.Visibility = Visibility.Hidden;
            txtblReminder.Visibility = Visibility.Hidden;
            lbxReminder.Visibility = Visibility.Hidden;
            btnSendReminder.Visibility = Visibility.Hidden;
            lblReminderMembers.Visibility = Visibility.Hidden;
        }

    }
}

