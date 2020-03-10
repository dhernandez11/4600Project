using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4600Project
{
    class Creator
    {
        private string creatorName;
        private string creatorEmailAddress;

        public Creator()
        {
            
        }

        public Creator(string creatorName, string creatorEmailAddress)
        {
            this.creatorName = creatorName;
            this.creatorEmailAddress = creatorEmailAddress;
        }
    }
}
