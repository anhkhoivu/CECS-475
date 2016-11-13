using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace PhoneBookRESTJSONService
{
   public class PhoneBookRESTJSONService : IPhoneBookRESTJSONService
   {
      // create a dbcontext object to access PhoneBook database
      private PhoneBookEntities dbcontext = new PhoneBookEntities();

      // add an entry to the phone book database
      public void AddEntry(string lastName, string firstName,
         string phoneNumber)
      {
            // create PhoneBook entry to be inserted in database
            PhoneBook newEntry = new PhoneBook()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            // insert PhoneBook entry in database
            dbcontext.PhoneBooks.Add(newEntry);
            dbcontext.SaveChanges();
      } // end method AddEntry

      // retrieve phone book entries with a given last name
      public PhoneBookEntry[] GetEntries(string lastName)
      {
            List<PhoneBookEntry> entries = new List<PhoneBookEntry>();
            PhoneBookEntry[] allEntries;
            foreach (PhoneBook phonebook in dbcontext.PhoneBooks)
            {
                PhoneBookEntry newEntry = new PhoneBookEntry
                {
                    FirstName = phonebook.FirstName,
                    LastName = phonebook.LastName,
                    PhoneNumber = phonebook.PhoneNumber
                };
                newEntry.LastName = Regex.Replace(newEntry.LastName, @"\s", "");
                if (newEntry.LastName == lastName)
                {
                    entries.Add(newEntry);
                }
            }

            allEntries = entries.ToArray();
            return allEntries;
      } // end method GetEntries
   }
}

