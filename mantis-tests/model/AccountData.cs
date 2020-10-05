using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData
    {
        private string name;
        private string password;
        public string Email { get; set; }
        public string Id { get; set; }

        public AccountData(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public AccountData()
        {
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        } 
    }
}