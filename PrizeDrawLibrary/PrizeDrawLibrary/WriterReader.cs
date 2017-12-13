using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;

namespace PrizeDrawLibrary
{
    public class WriterReader
    {

        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private List<string> _keysReadFromFile = new List<string>();
        private ObservableCollection<Person> _submittedPersonsReadFromXMLFile = new ObservableCollection<Person>();

        /// <summary>
        /// Writes serial keys to a txt file using StreamWriter.
        /// </summary>
        /// <param name="strList">A list of keys</param>
        public async void WriteKeysToFile(List<string> strList)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var towriter = new StreamWriter(new FileStream(storageFolder.Path + @"\generatedKeys.txt", FileMode.Create), new UTF8Encoding()))
                    {
                        foreach (String str in strList)
                        {
                            towriter.WriteLine(str);
                        }
                        towriter.Flush();
                    }
                }
                catch (Exception e)
                {
                    Debug.Write(e);

                }
            });
        }

        /// <summary>
        /// Reads keys from the storageFolder, and adds them to a List.
        /// </summary>
        public async void ReadKeysFromFile()
        {
            await Task.Run(async () =>
            {
                try
                {
                    var file = await storageFolder.GetFileAsync(@"\generatedKeys.txt");
                    IList<string> readKeys = await FileIO.ReadLinesAsync(file);

                    foreach (string key in readKeys)
                    {
                        _keysReadFromFile.Add(key);
                    }
                }
                catch (FileNotFoundException e)
                {

                    Debug.Write(e);
                }

            });

        }

        /// <summary>
        /// Saves persons to an xml file using a StreamWriter and LINQ to populate each xml element.
        /// </summary>
        /// <param name="personCollection">The ObservableCollection of submitted persons</param>
        public async void SaveSubmittedPersonsToXMLFile(ObservableCollection<Person> personCollection)
        {

            await Task.Run(() =>
            {
                try
                {
                    XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                            new XElement("SubmittedPersons",
                                from person in personCollection
                                select
                                    new XElement("Person",
                                    new XElement("FirstName", person.FirstName),
                                    new XElement("SurName", person.SurName),
                                    new XElement("Email", person.Email),
                                    new XElement("PhoneNumber", person.PhoneNumber),
                                    new XElement("DateOfBirth", person.DateOfBirth),
                                    new XElement("Key", person.Serial))));
                    using (var writer = new StreamWriter(new FileStream(storageFolder.Path + @"\submittedPersons.xml", FileMode.Create), new UTF8Encoding()))
                    {
                        xdoc.Save(writer);
                        writer.Flush();
                    }

                }
                catch (Exception e)
                {
                    Debug.Write(e);
                }

            });
        }

        /// <summary>
        /// Loads persons from XML file, and adds each person to an ObservableCollection
        /// </summary>
        public async void LoadSubmittedPersonsFromXMLFile()
        {
            await Task.Run(() =>
            {
                try
                {
                    var xmlDoc = XDocument.Load(storageFolder.Path + @"\submittedPersons.xml");
                    foreach (XElement xe in xmlDoc.Root.Elements("Person"))
                    {
                        _submittedPersonsReadFromXMLFile.Add(new Person
                               (xe.Element("FirstName").Value,
                               xe.Element("SurName").Value,
                               xe.Element("Email").Value,
                               xe.Element("PhoneNumber").Value,
                               xe.Element("DateOfBirth").Value,
                               xe.Element("Key").Value));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Debug.Write(e);
                }

            });
        }

        /// <summary>
        /// Get and set
        /// </summary>
        public List<string> KeysReadFromFile { get => _keysReadFromFile; set => _keysReadFromFile = value; }
        public ObservableCollection<Person> SubmittedPersonsReadFromXMLFile { get => _submittedPersonsReadFromXMLFile; set => _submittedPersonsReadFromXMLFile = value; }

    }
}
