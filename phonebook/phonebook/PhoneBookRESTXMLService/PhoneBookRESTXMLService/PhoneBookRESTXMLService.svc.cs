using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace PhoneBookRESTXMLService
{
    public class PhoneBookRESTXMLService : IPhoneBookRESTXMLService
    {
        // create a dbcontext object to access PhoneBook database
        private PhoneBookEntities dbcontext = new PhoneBookEntities();

        // add an entry to the phone book database
        public void AddEntry(string lastName, string firstName, string phoneNumber)
        {
            // create PhoneBook entry to be inserted in database
            PhoneBook entry = new PhoneBook()
            {
                FirstName = firstName + " ",
                LastName = lastName + " ",
                PhoneNumber = phoneNumber
            };

            // insert PhoneBook entry in database
            dbcontext.PhoneBooks.Add(entry);
        } // end method AddEntry

        // retrieve phone book entries with a given last name
        public PhoneBookEntry[] RetrieveEntries(string lastName)
        {
            List<PhoneBookEntry> newPB = new List<PhoneBookEntry>();
            PhoneBookEntry[] allEntries;
          
            foreach (PhoneBook entry in dbcontext.PhoneBooks)
            {
                PhoneBookEntry newEntry = new PhoneBookEntry()
                {
                    FirstName = entry.FirstName,
                    LastName = entry.LastName,
                    PhoneNumber = entry.PhoneNumber
                };
                newEntry.LastName = Regex.Replace(newEntry.LastName, @"\s", "");
                if (newEntry.LastName == lastName)
                {
                    newPB.Add(newEntry);
                }
            }

            allEntries = newPB.ToArray();
            return allEntries;
        }
    }
}

