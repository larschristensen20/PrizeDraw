using PrizeDrawingLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PrizeDraw
{
    /// <summary>
    /// The mainpage of the application. 
    /// Gui related logic found here.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        KeyGenerator generator = new KeyGenerator();
        WriterReader writerReader = new WriterReader();
        TestData testData = new TestData();
        ValidateUserInput validateUserInput = new ValidateUserInput();
        private ObservableCollection<Person> personsEnteredInDraw = new ObservableCollection<Person>();
        private List<string> strList;
        
        
        

        /// <summary>
        /// Initialize mainpage
        /// Listens for SuspendingEvents
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            LoadInstructionsIntoAppAsync();
            testData.LoadTestDataPersonsFromXMLFile();
            

            void App_Suspending(Object sender, Windows.ApplicationModel.SuspendingEventArgs e)
            {
                writerReader.SaveSubmittedPersonsToXMLFile(personsEnteredInDraw);
            }
            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
        }

        /// <summary>
        /// Generates keys and saves it to a file.
        /// </summary>
        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            writerReader.WriteKeysToFile(generator.GenerateKeys());
        }

        /// <summary>
        /// checks if userinput is correct in enter form.
        /// </summary>
        /// <returns>false if wrong, else true</returns>
        private bool ValidateGUIUserInput()
        {
            if (!validateUserInput.CheckIfFirstNameIsValid(FirstNameBox.Text))
            {
                DisplayIncorrectFirstNameDialog();
                return false;
            }
            else if (!validateUserInput.CheckIfSurNameIsValid(SurnameBox.Text))
            {
                DisplayIncorrectSurNameDialog();
                return false;
            }
            else if (!validateUserInput.CheckIfEmailIsValid(EmailBox.Text))
            {
                DisplayIncorrectEmailDialog();
                return false;
            }
            else if (!validateUserInput.CheckIfPhoneNumberIsValid(PhoneNumberBox.Text))
            {
                DisplayIncorrectPhoneNumberDialog();
                return false;
            }
            else if (!validateUserInput.CheckIfDateOfBirthIsValid(DateOfBirthBox.Text))
            {
                DisplayIncorrectDateOfBirthDialog();
                return false;
            }
            else if (!validateUserInput.CheckIfSerialIsValid(SerialBox.Text, strList))
            {
                DisplayIncorrectSerialDialog();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if user input is correct,
        /// if correct adds the Person to a list,
        /// checks if person is already entered,
        /// removes the key the Person used to enter
        /// </summary>
        private void EnterDrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateGUIUserInput())
            {
                Person p = new Person(FirstNameBox.Text, SurnameBox.Text, EmailBox.Text,
                    PhoneNumberBox.Text, DateOfBirthBox.Text, SerialBox.Text);
                if (!personsEnteredInDraw.Contains(p))
                {
                    personsEnteredInDraw.Add(p);
                    strList.Remove(SerialBox.Text);
                } else
                {
                    DisplayDuplicatePersonDialog();
                }
            }
            
        }

        /// <summary>
        /// Clears the user input textboxes
        /// </summary>
        private void ClearAllTextBoxes()
        {
            FirstNameBox.Text = string.Empty;
            SurnameBox.Text = string.Empty;
            EmailBox.Text = string.Empty;
            PhoneNumberBox.Text = string.Empty;
            DateOfBirthBox.Text = string.Empty;
            SerialBox.Text = string.Empty;
        }
        /// <summary>
        /// Displays a content dialog if first name is empty
        /// </summary>
        private async void DisplayIncorrectFirstNameDialog()
        {
            ContentDialog incorrectName = new ContentDialog()

            {
                Content = "Please input a first name",
                CloseButtonText = "Ok"
            };

            await incorrectName.ShowAsync();
        }
        /// <summary>
        /// Displays a content dialog if surname is empty
        /// </summary>
        private async void DisplayIncorrectSurNameDialog()
        {
            ContentDialog incorrectSurName = new ContentDialog()

            {
                Content = "Please input a surname",
                CloseButtonText = "Ok"
            };

            await incorrectSurName.ShowAsync();
        }
        /// <summary>
        /// Displays a content dialog if email is not a valid email
        /// </summary>
        private async void DisplayIncorrectEmailDialog()
        {
            ContentDialog incorrectEmail = new ContentDialog()

            {
                Content = "Please input a correct email",
                CloseButtonText = "Ok"
            };

            await incorrectEmail.ShowAsync();
        }
        /// <summary>
        /// Displays a content dialog if phone number is not a number
        /// </summary>
        private async void DisplayIncorrectPhoneNumberDialog()
        {
            ContentDialog incorrectPhoneNumber = new ContentDialog()

            {
                Content = "Please input a phone number",
                CloseButtonText = "Ok"
            };

            await incorrectPhoneNumber.ShowAsync();
        }
        /// <summary>
        /// Displays a content dialog if date of birth is not a valid date.
        /// </summary>
        private async void DisplayIncorrectDateOfBirthDialog()
        {
            ContentDialog incorrectDateOfBirth = new ContentDialog()

            {
                Content = "Please input a date of birth in yyyy/mm/dd format",
                CloseButtonText = "Ok"
            };

            await incorrectDateOfBirth.ShowAsync();
        }
        /// <summary>
        /// Displays a content dialog if serial is not a valid serial key
        /// </summary>
        private async void DisplayIncorrectSerialDialog()
        {
            ContentDialog incorrectSerial = new ContentDialog()

            {
                Content = "Please input a valid serial to enter",
                CloseButtonText = "Ok"
            };

            await incorrectSerial.ShowAsync();
        }
        /// <summary>
        /// Displays a content dialog if you try to enter same person in draw twice
        /// </summary>
        private async void DisplayDuplicatePersonDialog()
        {
            ContentDialog duplicatePerson = new ContentDialog()

            {
                Content = "You cannot add a person twice",
                CloseButtonText = "Ok"
            };

            await duplicatePerson.ShowAsync();
        }
        /// <summary>
        /// Button click to clear all user input text boxes.
        /// </summary>
        private void ClearAllTextBoxesButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllTextBoxes();
        }
        /// <summary>
        /// Button click to generate 10 persons
        /// </summary>
        private void GeneratePersonsFromTestDataButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Person p in testData.TestDataPersonsReadFromXMLFile)
            {
                personsEnteredInDraw.Add(p);
            }
        }
        /// <summary>
        /// Button click to load serial keys from file system.
        /// </summary>
        private void LoadKeysFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            writerReader.ReadKeysFromFile();
        }

        /// <summary>
        /// Button click to save persons entered in draw to XML file
        /// </summary>
        private void SaveSubmissonsButton_Click(object sender, RoutedEventArgs e)
        {
            writerReader.SaveSubmittedPersonsToXMLFile(personsEnteredInDraw);
        }

        /// <summary>
        /// Button click to load persons entered in draw from file system
        /// </summary>
        private void LoadSubmissionsButton_Click(object sender, RoutedEventArgs e)
        {

            writerReader.LoadSubmittedPersonsFromXMLFile();
        }

        /// <summary>
        /// Button click to clear the persons from the enteredInDraw list
        /// This is to clear the gridview.
        /// </summary>
        private void ClearSubmissionsInGridViewButton_Click(object sender, RoutedEventArgs e)
        {
            personsEnteredInDraw.Clear();
        }

        /// <summary>
        /// Button click to show loaded persons in gridview.
        /// </summary>
        private void PopulateGridViewWithXMLPersonsButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Person p in writerReader.SubmittedPersonsReadFromXMLFile)
            { 
            personsEnteredInDraw.Add(p);
            }
        }
        /// <summary>
        /// Button click to show loaded keys in listview
        /// </summary>
        private void ShowKeysInListView_Click(object sender, RoutedEventArgs e)
        {
            strList = writerReader.KeysReadFromFile;
            foreach (string str in strList) { GeneratedKeyListView.Items.Add(str); }
        }
        /// <summary>
        /// Button click to clear the list view
        /// </summary>
        private void ClearListView_Click(object sender, RoutedEventArgs e)
        {
            GeneratedKeyListView.ItemsSource = null;
            GeneratedKeyListView.Items.Clear();
        }

        /// <summary>
        /// Button click to copy selected serial key to serial box
        /// </summary>
        private void GeneratedKeyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SerialBox.Text = e.ClickedItem.ToString();
        }

        /// <summary>
        /// Task to load instructions from .txt file into InstructionsTextBox.
        /// </summary>
        private async void LoadInstructionsIntoAppAsync()
        {
            try
            {
                string fname = @"Assets\instructions.txt";
                StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFile file = await InstallationFolder.GetFileAsync(fname);
                InstructionsTextBox.Text = File.ReadAllText(file.Path);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
                ContentDialog instructionFileNotFound = new ContentDialog()

                {
                    Content = "Instructions were not found, you have to figure it out yourself!",
                    CloseButtonText = "Ok"
                };

                await instructionFileNotFound.ShowAsync();
            }
            
        }
    }

}
