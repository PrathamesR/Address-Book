using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    class DBOperations
    {
        /// <summary>
        /// Loads from database.
        /// </summary>
        /// <returns></returns>
        public static List<Contact> LoadFromDB()
        {
            List<Contact> book = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                Contact employee = new Contact();
                using (connection)
                {
                    string query = @"SELECT * FROM AddressBook";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    book = new List<Contact>();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Contact contact = new Contact(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetDecimal(5).ToString(), dataReader.GetDecimal(6).ToString(), dataReader.GetString(7));
                            book.Add(contact);
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return book;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return book;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateContact()
        {
            Console.Write("\nEnter the name of the contact to edit: ");
            string name = Console.ReadLine();

            Console.WriteLine("Select The property to edit");
            Console.WriteLine("1.First Name\n2.Last Name\n3.Address\n4.City\n5.State\n6.ZIP Code\n7.Phone Number\n8.Email Address");
            string[] properties = { "firstName", "lastName", "address", "city", "state", "zip", "phoneNumber", "email" };
            int choice = int.Parse(Console.ReadLine());
            Console.Write("Enter New Value: ");
            string newInput = Console.ReadLine();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                Contact employee = new Contact();
                using (connection)
                {
                    string query = null;
                    if (choice == 6 || choice == 7)
                        query = "Update AddressBook set " + properties[choice-1] + " = " + newInput + " where firstName = '" + name + "';";
                    else
                        query = "Update AddressBook set " + properties[choice-1] + " = '" + newInput + "' where firstName = '" + name + "';";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                }
                Console.WriteLine("Successfully Edited " + name + "'s " + properties[choice - 1] + " property");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Contact> GetEmployeesByDate(string startDate, string endDate)
        {
            List<Contact> book = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                Contact employee = new Contact();
                using (connection)
                {
                    string query = @"SELECT * FROM AddressBook where date_added between ('" + startDate + "') and ('" + endDate + "')";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    book = new List<Contact>();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        Console.WriteLine("\nContacts added between " + startDate + " and " + endDate + " are-");
                        while (dataReader.Read())
                        {
                            Contact contact = new Contact(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetDecimal(5).ToString(), dataReader.GetDecimal(6).ToString(), dataReader.GetString(7));
                            book.Add(contact);
                            Console.WriteLine(contact.ToString());
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return book;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return book;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int GetCountByCity(string city)
        {
            SqlConnection connection = null;
            int count = 0;

            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                Contact employee = new Contact();
                using (connection)
                {
                    string query = @"SELECT count(firstName) FROM AddressBook where city='" + city + "';";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            count = dataReader.GetInt32(0);
                            Console.WriteLine("There are " + count + " contacts in " + city + " City");
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int GetCountByState(string state)
        {
            SqlConnection connection = null;
            int count = 0;

            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                Contact employee = new Contact();
                using (connection)
                {
                    string query = @"SELECT count(firstName) FROM AddressBook where state='" + state + "';";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            count = dataReader.GetInt32(0);
                            Console.WriteLine("There are " + count + " contacts in " + state);
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool AddMultipleContacts(List<Contact> contacts)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                using (connection)
                {
                    Parallel.ForEach(contacts, contact => {
                        connection.Open();
                        Contact employee = new Contact();
                        SqlCommand command = new SqlCommand("CreateNewContact", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@firstName", contact.FirstName);
                        command.Parameters.AddWithValue("@lastName", contact.LastName);
                        command.Parameters.AddWithValue("@address", contact.Address);
                        command.Parameters.AddWithValue("@city", contact.City);
                        command.Parameters.AddWithValue("@state", contact.state);
                        command.Parameters.AddWithValue("@zip", decimal.Parse(contact.Zip));
                        command.Parameters.AddWithValue("@phoneNumber", decimal.Parse(contact.PhoneNo));
                        command.Parameters.AddWithValue("@email", contact.Email);

                        SqlDataReader dr = command.ExecuteReader();
                        connection.Close();
                    });
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
