using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW3
{
    class UserProfile
    {
        private string username;
        private string email;
        private string status;

        public string Username { get { return username; } set { username = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Status { get { return status; } set { status = value; } }
        public UserProfile()
        {
            this.username = "Anonymous";
            this.email = "unknown@example.com";
            this.status = "Active";
        }
        public UserProfile(string NewName, string NewEmail, string NewStatus)
        {   
                this.username = NewName;
                this.email = NewEmail;
                this.status = NewStatus;
                if (!(string.Equals(NewStatus,"Active") || string.Equals(NewStatus,"Blocked"))) { this.status = "Active"; }
        }
        public virtual void SendMessage(string Mes)
        {
            Console.WriteLine(this.Username + " sent a message: " + Mes);
        }

        public virtual void DeleteAccount()
        {   
            Console.WriteLine("Account " + this.Username + " was deleted!");
             this.username = "Deleted user";
            this.email = "";
            this.Status = "Blocked";
        }
        public void BlockUser()
        {
            this.status = "Blocked";
            Console.WriteLine("user " + this.Username + " was blocked!");
        }
        public override string ToString()
        {
            return ("Username: " + this.Username + ", email adress: " + this.Email + ", Status: " + this.Status);
        }
    }
    class AdminProfile : UserProfile
    {
        private int adminlevel;

        public int Adminlevel { get { return adminlevel; } set { adminlevel = value; } }

        public AdminProfile(string NewName, string NewEmail, string NewStatus, int NewLevel) : base(NewName,NewEmail,NewStatus)
        {
            this.adminlevel = NewLevel;
        }

        public override void SendMessage(string Mes)
        {
            Console.WriteLine("Administrator " + this.Username + " sent a system message: " + Mes);
        }

        public override void DeleteAccount()
        {
            Console.WriteLine("Administrator account " + this.Username + " was deleted!");
            this.Username = "Deleted user";
            this.Email = "";
            this.Status = "Blocked";
            this.Adminlevel = 0;
        }
        public void BanUser(string UserName)
        {
            Console.WriteLine("Administrator blocked user" + UserName);
        }
        public void UnbanUser(string UserName)
        {
            Console.WriteLine("Administrator unblocked user" + UserName);
        }
    }
    class RegularProfile : UserProfile
    {
        private int friendCount = 0;
        public int FriendCount { get { return friendCount; } set { friendCount = value; } }
        public RegularProfile(string NewName, string NewEmail, string NewStatus, int NewFriends) : base(NewName, NewEmail, NewStatus)
        {
            this.friendCount = NewFriends;
        }
        public override void SendMessage(string Mes)
        {
            Console.WriteLine("User " + this.Username + " sent a personal message: " + Mes);
        }
        public override void DeleteAccount()
        {
            Console.WriteLine("User " + this.Username + " was deleted!");
            this.Username = "Deleted user";
            this.Email = "";
            this.Status = "Blocked";
            this.FriendCount = 0;

        }
        public void AddFriend()
        {
            FriendCount++;
            Console.WriteLine("User " + this.Username + " added a new friend! total friends count: " + this.friendCount);
        }
    }



    class Program
    {
        public static void Split()
        {
            Console.WriteLine("---================---");
        }
        public static string AskTester(string s)
        {
            Console.Write(s);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Testing started!");
            RegularProfile Reg;
            AdminProfile Adm;
            Split();
            Console.WriteLine("Registering regular profile");
            while (true)
            {
                try
                {

                    string name = AskTester("Enter username: ");
                    string email = AskTester("Enter email adress: "); 
                    string status = AskTester("Enter account status: ");
                    Reg = new RegularProfile(name, email, status, 0);
                    break;
                }
                catch
                {
                    Console.WriteLine("Wrong data! Please try again!");
                }
            }
            Split();
            Console.WriteLine("Testing user methods: ");
            Console.WriteLine(Reg.ToString());
            Reg.AddFriend();
            string mess = AskTester("Enter message: ");
            Reg.SendMessage(mess);
            Reg.BlockUser();
            Split();
            Console.WriteLine("Registering administrator profile");
            while (true)
            {
                try
                {

                    string name = AskTester("Enter username: ");
                    string email = AskTester("Enter email adress: ");
                    string status = AskTester("Enter account status: ");
                    int Level = Convert.ToInt32(AskTester("enter administration level: "));
                    Adm = new AdminProfile(name, email, status, Level);
                    break;
                }
                catch
                {
                    Console.WriteLine("Wrong data! Please try again!");
                }
            }
            Split();
            Console.WriteLine("Testing administrator methods: ");
            Console.WriteLine(Adm.ToString());
            string mess2 = AskTester("Enter message: ");
            Adm.SendMessage(mess2);
            string usertoban = AskTester("Enter user to ban: ");
            Adm.BanUser(usertoban);
            Adm.UnbanUser(usertoban);
            Split();
            Reg.DeleteAccount();
            Adm.DeleteAccount();
            Console.WriteLine("Testing finished! Press Enter to exit");
            Console.ReadLine();
        }
    }
}
