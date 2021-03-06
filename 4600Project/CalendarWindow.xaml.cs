﻿using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {

        public CalendarWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }
        private async void populateWeather(int zip)
        {
            try
            {
                var weatherInfo = await WeatherProcessor.LoadWeatherInfo(zip);
                var nameInfo = await NameProcessor.LoadName(zip);
                CurrentTemp.Content = "The current temperature is " + weatherInfo.Temp.ToString() + " degrees";
                CurrentFeel.Content = "It currently feels like " + weatherInfo.Feels_like.ToString() + " degrees";
                LowTemp.Content = "The low today is " + weatherInfo.Temp_min.ToString();
                HighTemp.Content = "The high today is " + weatherInfo.Temp_max.ToString();
            }
            catch
            {
                MessageBox.Show("Please enter a real zip code");
            }
       
        }

        private void zipCodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (zipCodeBox.Text == "")
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(
                    new Uri(@"ZipCodeBoxBackground.png", UriKind.Relative));
                imageBrush.AlignmentX = AlignmentX.Left;
                imageBrush.Stretch = Stretch.None;
                zipCodeBox.Background = imageBrush;
            }
            else
            {
                zipCodeBox.Background = null;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (zipCodeBox.Text.Length < 5)
            {
                MessageBox.Show("Please enter a five digit numerical zip code to get the weather!");
            }
            else
            {
                try
                {
                    int num = -1;
                    if (!int.TryParse(zipCodeBox.Text, out num))
                    {
                        MessageBox.Show("Please enter a five digit numerical zip code to look up the weather!");
                    }
                    else
                    {
                        populateWeather(Convert.ToInt32(zipCodeBox.Text));
                    }
                }
                catch
                {
                    zipCodeBox.Text = "";
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            
            
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dvwvi\Source\Repos\4600Project\4600Project\Database1.mdf;Integrated Security=True");
    
                using (SqlCommand command = new SqlCommand("SELECT subject, date, time, location FROM [Appointments] WHERE username=@username", connection))
                {
                    command.Parameters.AddWithValue("@username", "cale");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string subject = Convert.ToString(reader["subject"]);
                        DateTime date = Convert.ToDateTime(reader["date"]);
                        string time = Convert.ToString(reader["time"]);
                        string location = Convert.ToString(reader["location"]);


                    }
                    connection.Close();
         
            }

        }
    }
}

