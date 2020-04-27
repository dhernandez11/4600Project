using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4600Project
{
    public class CreateNewCalendar
    {
        private static string title;
        private List<Member> members = new List<Member>();
        private string password;

        public CreateNewCalendar(string title1)
        {
            title = title1;
        }
        public CreateNewCalendar(string title1, string password)
        {
            title = title1;
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
        public static string getTitle()
        {
            return title;
        }
    }
}
