using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    public class FileIO
    {
        public static void LoadFromTxt(string path)
        {
            FileStream fileStream = new FileStream(path,FileMode.Open);
            if (fileStream.Length > 6)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Shelf deserializedShelf = (Shelf)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                AddressBookMain.shelf = deserializedShelf;
                Console.WriteLine("File loaded Successfully from " + path);
            }
            else
                Console.WriteLine("Load File Empty, creating new Shelf");

        }

        public static void SaveToText(Shelf shelf, string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, shelf);
            fileStream.Close();
            Console.WriteLine("File saved Successfully to " + path);
        }

        public static void SaveToCSV(AddressBook addressBook, string path)
        {
            StreamWriter writer = new StreamWriter(path);
            CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(addressBook.addressBook);
            writer.Close();
        }

        public static List<Contact> LoadFromCSV(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                reader.Close();
                List<Contact> list = csv.GetRecords<Contact>().ToList();
                return list;
            }
        }

        public static void SaveToJSON(Shelf shelf,string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            using(StreamWriter sw = new StreamWriter(path))
            using(JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, shelf.shelf);
            }
        }

        public static void LoadFromJSON(Shelf shelf, string path)
        {
            shelf.shelf = JsonConvert.DeserializeObject<Dictionary<string, AddressBook>>(File.ReadAllText(path));
        }
    }
}
