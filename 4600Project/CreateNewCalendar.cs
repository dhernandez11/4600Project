using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4600Project
{
    public class CreateNewCalendar
    {
        private string title;
        private List<Members> members = new List<Members>();
        private string password;

        public CreateNewCalendar()
        {

        }
        public CreateNewCalendar(string title, string password)
        {
            this.title = title;
            this.password = password;
        }

        public string getTitle()
        {
            return title;
        }
        public string getPassword()
        {
            return password;
        }
        public List<Members> getList()
        {
            return members;
        }
        public void addMemberToCalendar(Members member)
        {
            this.members.Add(member);
        }
        public void deleteMemberFromCalendar(Members member)
        {
            members.Remove(member);
        }

    }
}
