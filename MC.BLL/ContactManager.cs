using MC.DAL;
using System;
using System.Collections.Generic;
using System.IO;

namespace MC.BLL
{
    public class ContactManager : IContactManager
    {
        private List<Contact> contacts = new List<Contact>();
        private string filePath = @"C:\Temp\contacts.txt";
        private readonly IOutputWriter outputWriter;

        public ContactManager(IOutputWriter outputWriter)
        {
            this.outputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
        }

        public Contact this[int index]
        {
            get
            {
                if (index >= 0 && index < contacts.Count)
                {
                    return contacts[index];
                }
                else
                {
                    outputWriter.Write("Индекс вне диапазона.");
                    return null;
                }
            }
        }

        public void AddContact(Contact contact)
        {
            try
            {
                if (contact == null)
                {
                    throw new ArgumentNullException(nameof(contact), "Контакт не может быть null.");
                }

                contacts.Add(contact);
                outputWriter.Write("Контакт успешно добавлен.");
            }
            catch (Exception ex)
            {
                outputWriter.Write($"Ошибка при добавлении контакта: {ex.Message}");
            }
        }

        public void DisplayContacts()
        {
            try
            {
                if (contacts.Count == 0)
                {
                    outputWriter.Write("Контактов нет.");
                }
                else
                {
                    foreach (var contact in contacts)
                    {
                        outputWriter.Write(contact.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                outputWriter.Write($"Ошибка при отображении контактов: {ex.Message}");
            }
        }

        public void DeleteContact(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Имя контакта не может быть пустым.");
                }

                Contact contactToRemove = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (contactToRemove != null)
                {
                    contacts.Remove(contactToRemove);
                    outputWriter.Write("Контакт успешно удален.");
                }
                else
                {
                    outputWriter.Write("Контакт не найден.");
                }
            }
            catch (Exception ex)
            {
                outputWriter.Write($"Ошибка при удалении контакта: {ex.Message}");
            }
        }

        public void SaveContacts()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var contact in contacts)
                    {
                        writer.WriteLine($"{contact.Name},{contact.Phone},{contact.Email}");
                    }
                }
                outputWriter.Write("Контакты успешно сохранены.");
            }
            catch (Exception ex)
            {
                outputWriter.Write($"Ошибка при сохранении контактов: {ex.Message}");
            }
        }

        public void LoadContacts()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (var line in lines)
                    {
                        string[] data = line.Split(',');
                        Contact loadedContact = new Contact
                        {
                            Name = data[0],
                            Phone = data[1],
                            Email = data[2]
                        };
                        contacts.Add(loadedContact);
                    }
                    outputWriter.Write("Контакты успешно загружены.");
                }
            }
            catch (Exception ex)
            {
                outputWriter.Write($"Ошибка при загрузке контактов: {ex.Message}");
            }
        }
    }
}
