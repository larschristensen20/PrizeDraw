using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeDrawingLibrary
{
    public class ValidateUserInput
    {
        WriterReader writerReader = new WriterReader();


        /// <summary>
        /// check if a string is empty
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>false if empty, else true</returns>
        public bool CheckIfFirstNameIsValid(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return true;
        }
        /// <summary>
        /// check if a string is empty
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>false if empty, else true</returns>
        public bool CheckIfSurNameIsValid(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return true;
        }
        /// <summary>
        /// checks if an string is a valid email using EmailAddressAttribute.
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>false if not a correct email, else true</returns>
        public bool CheckIfEmailIsValid(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
                else if (!new EmailAddressAttribute().IsValid(str))
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return true;
        }

        /// <summary>
        /// checks if a string is a number
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>false if empty or not a number, else true</returns>
        public bool CheckIfPhoneNumberIsValid(string str)
        {
            try
            {
                var isNumeric = int.TryParse(str, out var n);
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
                if (!isNumeric)
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return true;
        }
        /// <summary>
        /// checks if a string is a valid date, follows YYYY-MM-DD.
        /// </summary>
        /// <param name="str">the string to check</param>
        /// <returns>false if empty or not a valid date, else true </returns>
        public bool CheckIfDateOfBirthIsValid(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
                else if (!DateTime.TryParse(str, out DateTime dateTime))
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return true;
        }

        /// <summary>
        /// checks if a string is a valid key in the generated key list.
        /// </summary>
        /// <param name="keystring">the string to check</param>
        /// <param name="keys">the list of generated keys</param>
        /// <returns>false if keystring not in list, else true</returns>
        public bool CheckIfSerialIsValid(string keystring, List<string> keys)
        {
            try
            {
                foreach (String key in keys)
                {
                    if (keystring == key)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {

                Debug.Write(e);
            }
            return false;
        }

    }

}
