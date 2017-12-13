using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeDrawLibrary
{
   public class Person
    {
        private string _firstName, _surName, _email, _phoneNumber, _dateOfBirth, _serial;

        /// <summary>
        /// Constructor for Person, sets each local varible to the parameter received when initialized.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="serial"></param>
        public Person(string firstName, string surName, string email, string phoneNumber, string DateOfBirth, string serial)
        {
            this._firstName = firstName;
            this._surName = surName;
            this._email = email;
            this._phoneNumber = phoneNumber;
            this._dateOfBirth = DateOfBirth;
            this._serial = serial;
        }

        /// <summary>
        /// Get and set
        /// </summary>
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string SurName { get => _surName; set => _surName = value; }
        public string Email { get => _email; set => _email = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public string Serial { get => _serial; set => _serial = value; }

        public override bool Equals(object obj)
        {
            var person = obj as Person;
            return person != null &&
                   _firstName == person._firstName &&
                   _surName == person._surName &&
                   _email == person._email &&
                   _phoneNumber == person._phoneNumber &&
                   _dateOfBirth == person._dateOfBirth &&
                   FirstName == person.FirstName &&
                   SurName == person.SurName &&
                   Email == person.Email &&
                   PhoneNumber == person.PhoneNumber &&
                   DateOfBirth == person.DateOfBirth;
        }

        public override int GetHashCode()
        {
            var hashCode = -653018400;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_firstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_surName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_phoneNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_dateOfBirth);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_serial);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SurName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DateOfBirth);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Serial);
            return hashCode;
        }
    }
}