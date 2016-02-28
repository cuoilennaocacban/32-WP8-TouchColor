using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using TouchColorWP8.ViewModel;

namespace TouchColorWP8.Utilities
{
    public class StaticMethod
    {
        public static void SaveFavs()
        {
            string favKey = "favCollection";

            //Serialize
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (
                    IsolatedStorageFileStream stream = new IsolatedStorageFileStream("fav.xml",
                        FileMode.OpenOrCreate, isoStorage))
                {
                    XmlSerializer serializer = new XmlSerializer(StaticData.FavColorCollection.GetType());
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                    {
                        serializer.Serialize(xmlWriter, StaticData.FavColorCollection);
                    }
                }
            }
        }

        public static void GetFavs()
        {
            string favKey = "favCollection";

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Create a file stream to open or create file
                using (
                    IsolatedStorageFileStream stream = new IsolatedStorageFileStream("fav.xml",
                        FileMode.OpenOrCreate, isoStorage))
                {
                    XmlSerializer serializer = new XmlSerializer(StaticData.FavColorCollection.GetType());

                    //StreamReader reader = new StreamReader(stream);
                    //reader.ReadToEnd();

                    try
                    {
                        StaticData.FavColorCollection = (ObservableCollection<Color>)serializer.Deserialize(stream);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
