using MC.DAL;
using System.Collections.Generic;

namespace MC.BLL
{
    public interface IContactManager
    {
        Contact this[int index] { get; }
        void AddContact(Contact contact);
        void DisplayContacts();
        void DeleteContact(string name);
        void SaveContacts();
        void LoadContacts();
    }
}
