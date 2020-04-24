using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
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
    /// Interaction logic for CreateCalendarWindow.xaml
    /// </summary>
    public partial class CreateCalendarWindow : Window
    {
        private List<Member> membersList = new List<Member>();
        CreateNewCalendar calendar;


        public CreateCalendarWindow()
        {
            InitializeComponent();
        }

        public void addMemb(Member member)
        {
            this.membersList.Add(member);
        }
        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            calendar = new CreateNewCalendar(txtbxCalendarName.Text, txtbxPasswordCreate.Text);
            Member member = new Member(txtbxAddMembersName.Text, txtbxAddMembersEmail.Text);
            //calendar.addMemberToCalendar(member);
            this.addMemb(member);
            lstbxAddMember.Items.Add(member); 
            txtbxAddMembersName.Clear();
            txtbxAddMembersEmail.Clear();


        }

        private void btnRemoveMember_Click(object sender, RoutedEventArgs e)
        {
            lstbxAddMember.Items.RemoveAt(lstbxAddMember.SelectedIndex);
            //delete member from list
        }

        private void btnStartNew_Click(object sender, RoutedEventArgs e)
        {
            Creator creator = new Creator(txtbxCreatorName.Text, txtbxCreatorEmail.Text);
            this.addMemb(creator);

            calendar = new CreateNewCalendar(txtbxCalendarName.Text, txtbxPasswordCreate.Text);

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.outlook.com");

            mail.From = new MailAddress("vDayCalendar@outlook.com");
            mail.Body = "You have been invited to join " + txtbxCreatorName.Text + "'s vDay calendar.\n To log in, use this calendar username and password.\n\nCalendar Name: " + txtbxCalendarName.Text + "\nPassword: " + txtbxPasswordCreate.Text;
            mail.Subject = "Join vDay Calendar";

            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("vDayCalendar@outlook.com", "v8Day8Cal");


            foreach (Member member in membersList)
            {
                
                MailAddress To = new MailAddress(member.getEmailAddress());
                mail.To.Add(member.getEmailAddress());
                smtp.Send(mail);

                Database.AddMember(txtbxCalendarName.Text, member.getName(), member.getEmailAddress());
                calendar.addMemberToCalendar(member);


            }

            Database.AddUser(txtbxCalendarName.Text, txtbxPasswordCreate.Text);

            this.Close();


        }

    }
}
