﻿using Microsoft.Win32;
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
            calendar.addMemberToCalendar(member);
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
            //still to do:
            //save created calendar in a file with name and password
            //encrypt/save password and calendar name
            //figure out how to use multiple email senders; this as of right now, will send only from outlook/hotmail accounts to any email account
            //possibly create a generic outlook email account to send out invites rather than use user emails to combat this easily

            calendar = new CreateNewCalendar(txtbxCalendarName.Text, txtbxPasswordCreate.Text);

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.outlook.com");

            mail.From = new MailAddress(txtbxCreatorEmail.Text);
            mail.Body = "You have been invited to join " + txtbxCreatorName.Text + "'s vDay calendar.\n To log in, use this calendar username and password.\n\nCalendar Name: " + txtbxCalendarName.Text + "\nPassword: " + txtbxPasswordCreate.Text;
            mail.Subject = "Join vDay Calendar";

            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(txtbxCreatorEmail.Text, pwdbxCreatorEmailPassword.Password);


            foreach (Member member in membersList)
            {
                MailAddress To = new MailAddress(member.getEmailAddress());
                mail.To.Add(member.getEmailAddress());
                smtp.Send(mail);

            }

            Database.AddUser(txtbxCalendarName.Text, txtbxPasswordCreate.Text);
            this.Close();


        }

    }
}
