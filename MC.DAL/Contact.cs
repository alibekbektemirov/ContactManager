using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.DAL
{
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Phone: {Phone}, Email: {Email}";
        }
    }
}
