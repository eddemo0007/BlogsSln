using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class WriterViewModel
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public WriterViewModel(string name, string lastName, string email)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
        }
    }
}
