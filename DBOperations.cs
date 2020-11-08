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

        public static List<Contact> GetEmployeesByDate(string startDate,string endDate)
        {
            List<Contact> book = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=AddressBook;Integrated Security=True");
                Contact employee = new Contact();
                using (connection)
                {
                    string query = @"SELECT * FROM AddressBook where date_added between ('"+ startDate + "') and ('" + endDate + "')";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    book = new List<Contact>();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        Console.WriteLine("\nContacts added between " + startDate + " and " + endDate+" are-");
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
    }
}
