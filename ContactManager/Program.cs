using MC.BLL;
using MC.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal class Program : IOutputWriter
    {
        static void Main()
        {
            MC.BLL.ContactManager contactManager = new MC.BLL.ContactManager(new Program());
            contactManager.LoadContacts();

            while (true)
            {
                Console.WriteLine("1. Добавить контакт");
                Console.WriteLine("2. Просмотреть контакты");
                Console.WriteLine("3. Удалить контакт");
                Console.WriteLine("4. Выход");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddContact(contactManager);
                            break;
                        case 2:
                            contactManager.DisplayContacts();
                            break;
                        case 3:
                            DeleteContact(contactManager);
                            break;
                        case 4:
                            contactManager.SaveContacts();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                }
            }
        }

        static void AddContact(MC.BLL.ContactManager contactManager)
        {
            Contact newContact = new Contact();

            Console.Write("Введите имя: ");
            newContact.Name = Console.ReadLine();

            Console.Write("Введите телефон: ");
            newContact.Phone = Console.ReadLine();

            Console.Write("Введите электронную почту: ");
            newContact.Email = Console.ReadLine();

            contactManager.AddContact(newContact);
        }

        static void DeleteContact(MC.BLL.ContactManager contactManager)
        {
            Console.Write("Введите имя контакта для удаления: ");
            string nameToDelete = Console.ReadLine();

            contactManager.DeleteContact(nameToDelete);
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
