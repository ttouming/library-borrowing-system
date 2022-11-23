using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Collections;


namespace library_borrowing_system
{
    class Member
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string phone { get; set; }

        public string password { get; set; }

        public ToolCollection memberToolCollec = new ToolCollection();

        public Member(string f_nm, string l_nm, string phnum, string pwnum)
        {
            fname = f_nm;
            lname = l_nm;
            phone = phnum;
            password = pwnum;
        }

    }// end class Member

    class MemberCollection
    {
        public Hashtable memberCollec = new Hashtable();

        public bool Check_MemberfName(string memName)// check if there is a member with memName
        {
            if (memberCollec[memName] != null)
            {
                return true; // member with memName exists return true
            }
            else
            {
                return false; // member with memName doesn't exist return false
            }
        }// verify first name when member login

        public bool Check_MemberlName(string memfName, string memlName)
        {
            if (((Member)memberCollec[memfName]).lname == memlName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }// verify last name when member login


        public bool Check_MemberPassword(string memfName, string mempwd)
        {
            if (((Member)memberCollec[memfName]).password == mempwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }// verify password when member login


        public void Add_Member(Member newMember)
        {
            memberCollec.Add(key: newMember.fname, value: newMember);
        }// this is to add member in to MemberCollection

        public void Remove_Member(string fname)
        {
            if (((Member)memberCollec[fname]).memberToolCollec.Check_ToolCollection())// if member with fname has tool in its memberToolCollec
            {
                WriteLine("{0} still has tools.", fname);
                WriteLine("Return all tools. Then be removed.");
            }
            else
            {
                memberCollec.Remove(fname);
            }
        }

        public void Display_Member_All() // display fisrt name, last name, phone number, password
        {
            WriteLine("---Member Info---");

            foreach (DictionaryEntry entry in memberCollec)
            {
                WriteLine(entry.Key + ": { First Name: " + ((Member)entry.Value).fname +
                    "| Last Name: " + ((Member)entry.Value).lname + "| Phone Number: " +
                    ((Member)entry.Value).phone + "| Password: " + ((Member)entry.Value).password);
            }

        }

        public void FindMemberPhoneNum(string fname)
        {
            if (Check_MemberfName(fname)) // if member with fname exists
            {
                WriteLine(((Member)memberCollec[fname]).phone);
            }
            else // member with fname not exists
            {
                WriteLine("No such member with first name {0}.", fname);
            }
        }


    }// end class MemberCollection


}

