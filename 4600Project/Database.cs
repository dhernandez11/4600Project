﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;

namespace _4600Project
{
    public class Database
    {
        public static bool AddUser(string username, string password)
        {
            Guid userGuid = System.Guid.NewGuid();

            string hashedPass = HashPassword.hashPassword(password + userGuid.ToString());

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dvwvi\Source\Repos\4600Project\4600Project\Database1.mdf;Integrated Security=True");
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO [Users] VALUES (@username, @password, @userguid)", connection);

                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hashedPass);
                    command.Parameters.AddWithValue("@userguid", userGuid);

                    command.ExecuteNonQuery();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

            }

            return true;

        }

        public static bool AddMember(string username, string name, string emailaddress)
        {
            Guid userGuid = System.Guid.NewGuid();

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dvwvi\Source\Repos\4600Project\4600Project\Database1.mdf;Integrated Security=True");
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO [Members] VALUES (@username, @name, @emailaddress)", connection);

                    command.CommandType = CommandType.Text;
                    
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@emailaddress", emailaddress);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

            }

            return true;

        }

        public static bool addAppointment(string username, string subject, DateTime date, string time, string location)
        {
            Guid userGuid = System.Guid.NewGuid();

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dvwvi\Source\Repos\4600Project\4600Project\Database1.mdf;Integrated Security=True");
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO [Appointments] VALUES (@username, @subject, @date, @time, @location)", connection);

                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@subject", subject);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@location", location);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();

            }

            return true;
        
        }
        public static int GetUserById(string username, string password)
        {
            int iD = 0;

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dvwvi\Source\Repos\4600Project\4600Project\Database1.mdf;Integrated Security=True");
            using(SqlCommand command = new SqlCommand("SELECT Id, password, userguid FROM [Users] WHERE username=@username", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    int userID = Convert.ToInt32(reader["Id"]);
                    string pass = Convert.ToString(reader["password"]);
                    string guid = Convert.ToString(reader["userguid"]);

                    string hashPass = HashPassword.hashPassword(password + guid);

                    if(pass == hashPass)
                    {
                        iD = userID;
                    }
                }
                connection.Close();
            }
            return iD;
        }
    }
}
