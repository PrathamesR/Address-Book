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
    }
}
