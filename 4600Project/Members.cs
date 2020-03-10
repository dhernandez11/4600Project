using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4600Project
{
    public class Members
    { 
        private string name;
        private string emailAddress;

        public Members()
        {

        }
        public Members(string name, string emailAddress)
        {
            this.name = name;
            this.emailAddress = emailAddress;
        }
        public string getName()
        {
            return name;
        }

        public string getEmailAddress()
        {
            return emailAddress;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.name, this.emailAddress);

        }

    }
}
