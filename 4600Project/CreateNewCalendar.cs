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
        private List<Member> members = new List<Member>();
        private string password;

        public CreateNewCalendar(string title, string password)
        {
            this.title = title;
            this.password = password;
        }
        public void addMemberToCalendar(Member member)
        {
            this.members.Add(member);
        }
        public void deleteMemberFromCalendar(Member member)
        {
            members.Remove(member);
        }

    }
}
