using System;
using static System.Console;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace library_borrowing_system
{
    class Program
    {
        static void Main(string[] args)
        {
            ToolCollection a = new ToolCollection();

            // ---create tool manually as mock up data---
            a.Add_Tool(0, 4, "tool_a", 13);
            a.Add_Tool(0, 1, "tool_b", 15);
            // ---create tool manually as mock up data---

            // ---create member manually as mock up data---
            WriteLine("-------Member tool-----");
            Member b = new Member("steve", "liao", "12345", "111");
            Member b2 = new Member("john", "lee", "23456", "111");
            Member b3 = new Member("shane", "neil", "11223", "111");
            // ---create member manually as mock up data---

            // ---add member into membercollection manually as mock up data---
            WriteLine("-------Add Member to member collec-----");
            MemberCollection b1 = new MemberCollection();
            b1.Add_Member(b);
            b1.Add_Member(b2);
            b1.Add_Member(b3);
            b1.Display_Member_All();
            // ---add member into membercollection manually as mock up data---
            MainMenu(a, b1);

        }

        static void MainMenu(ToolCollection tCollec, MemberCollection mCollec)
        {
            MainMenu_Display();

            string useropt;

            useropt = ReadLine();

            switch (useropt)
            {
                case "1":
                    StaffLogin();
                    StaffMenu(tCollec, mCollec);
                    break;
                case "2":
                    MemberLogin(tCollec, mCollec);
                    break;
                case "0":
                    Exit();
                    break;
            }

            MainMenu(tCollec, mCollec);
        }// Main Menu

        // ---Staff Menu Display---
        static void StaffMenu_Display()
        {
            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("  ");
            WriteLine("==========Staff Menu==========");
            WriteLine("1. Add a tool");
            WriteLine("2. Remove a tool");
            WriteLine("3. Register a new member");
            WriteLine("4. Remove a member");
            WriteLine("5. Display all the members who are borrowing a tool");
            WriteLine("6. Find a member's phone number");
            WriteLine("0. Return to mian menu");
            WriteLine("==============================");
            WriteLine("   Select option from menu (0 to exit) - ");
        }

        // ---Memebr Menu Display---
        static void MemberMenu_Display(Member m1)
        {
            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("Member Name: {0}", m1.fname);
            WriteLine("==========Member Menu==========");
            WriteLine("1. Display tools of a type");
            WriteLine("2. Borrow a tool");
            WriteLine("3. Return a tool");
            WriteLine("4. List tools I'm borrowing");
            WriteLine("5. Displaytop three most frequently borrowed tools");
            WriteLine("0. Return to main menu");
            WriteLine("===============================");
            WriteLine("   Select option from menu (0 to exit) - ");
        }

        static void StaffMenu(ToolCollection tCollec, MemberCollection mCollec)
        {
            StaffMenu_Display();

            string useropt;

            useropt = ReadLine();

            switch (useropt)
            {
                case "1":
                    SelectCategory(tCollec, mCollec, null, 1);
                    break;
                case "2":
                    SelectCategory(tCollec, mCollec, null, 2);
                    break;
                case "3":
                    RegisterMember(mCollec);
                    break;
                case "4":
                    RemoveMember(mCollec);
                    break;
                case "5":
                    DisplayBorrowing(mCollec);
                    break;
                case "6":
                    FindMemberPhoneNumber(mCollec);
                    break;
                case "0":
                    MainMenu(tCollec, mCollec);
                    break;
            }
            StaffMenu(tCollec, mCollec);
        }// Staff Menu

        // is member m1 needed for SelectCategory()?
        static void SelectCategory(ToolCollection tCollec, MemberCollection mCollec, Member b1, int selection)
        {
            SelectCategory_Display(tCollec, selection);

            string useropt = ReadLine();

            switch (useropt)
            {
                case "1":
                    SelectType(tCollec, mCollec, b1, 0, selection);
                    break;
                case "2":
                    SelectType(tCollec, mCollec, b1, 1, selection);
                    break;
                case "3":
                    SelectType(tCollec, mCollec, b1, 2, selection);
                    break;
                case "4":
                    SelectType(tCollec, mCollec, b1, 3, selection);
                    break;
                case "5":
                    SelectType(tCollec, mCollec, b1, 4, selection);
                    break;
                case "6":
                    SelectType(tCollec, mCollec, b1, 5, selection);
                    break;
                case "7":
                    SelectType(tCollec, mCollec, b1, 6, selection);
                    break;
                case "8":
                    SelectType(tCollec, mCollec, b1, 7, selection);
                    break;
                case "9":
                    SelectType(tCollec, mCollec, b1, 8, selection);
                    break;
                case "0":
                    //Exit();
                    break;
            }
        }// Select Category

        static void SelectType(ToolCollection tCollec, MemberCollection mCollec, Member b1, int cate, int selection)
        {
            int countType = SelectType_Display(tCollec, cate, selection);
            // countType is the number of type.

            string useropt = ReadLine();
            int useroptInt = Convert.ToInt32(useropt);

            while (useroptInt > countType || useroptInt < 0)
            {
                WriteLine("Please enter valid number.");
                countType = SelectType_Display(tCollec, cate, selection);

                useropt = ReadLine();
                useroptInt = Convert.ToInt32(useropt);
                if (useroptInt == 0)
                {
                    StaffMenu(tCollec, mCollec);
                }
                else if (useroptInt <= countType && useroptInt >= 1 && selection != 999 && selection != 888)
                {
                    StaffAddRemoveTool(tCollec, mCollec, cate, useroptInt - 1, selection);
                }
                else if (selection == 999)
                {
                    tCollec.DisplayToolOfSelectedCategoryType(cate, useroptInt - 1);
                }
            }

            if (useroptInt == 0)
            {
                StaffMenu(tCollec, mCollec);
            }
            else if (useroptInt <= countType && useroptInt >= 1 && selection != 999 && selection != 888)
            {
                StaffAddRemoveTool(tCollec, mCollec, cate, useroptInt - 1, selection);
            }
            else if (selection == 999)
            {
                if (tCollec.Check_SelectedCateType_ToolCollection_Number(cate, useroptInt - 1) > 0)
                {
                    tCollec.DisplayToolOfSelectedCategoryType(cate, useroptInt - 1);
                }
                else
                {
                    WriteLine("Nothing in Category: {0}, Type: {1} yet.", tCollec.Category[cate], tCollec.Type[cate, useroptInt - 1]);
                }

            }

        }// Select Type

        static void StaffAddRemoveTool_Display(ToolCollection tCollec, int cate, int type, int selection)
        {
            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("  ");
            WriteLine("Category: {0}", tCollec.Category[cate]);
            WriteLine("Type    : {0}", tCollec.Type[cate, type]);
            if (selection == 1)
            {
                WriteLine("==========Add Tool==========");
            }
            else
            {
                WriteLine("==========Remove Tool==========");
            }
        }

        static void StaffAddRemoveTool(ToolCollection tCollec, MemberCollection mCollec, int cate, int type, int selection)
        {
            string name;
            int number;
            string useropt;
            int checkNumber = tCollec.Check_SelectedCategoryType_Tool_Number(cate, type);

            if (selection == 1)
            {
                Clear();
                StaffAddRemoveTool_Display(tCollec, cate, type, selection);
                WriteLine("Enter Tool Name: ");

                useropt = ReadLine().ToLower();
                name = useropt;

                while (name == "")
                {
                    SetCursorPosition(0, 5);
                    WriteLine("Please enter a Name: ");
                    name = ReadLine();
                }
                Clear();
                StaffAddRemoveTool_Display(tCollec, cate, type, selection);
                WriteLine("Enter Tool Name: {0}", name);
                WriteLine("Enter Number :");

                int n;
                string num = ReadLine();
                bool result = Int32.TryParse(num, out n);
                while (!result)
                {
                    SetCursorPosition(0, 6);
                    WriteLine("Please enter a Number: ");
                    WriteLine("                          ");
                    SetCursorPosition(0, 7);
                    num = ReadLine();
                    result = Int32.TryParse(num, out n);
                }// check if input was number 
                number = n;

                tCollec.Add_Tool(cate, type, name, number);
                // add tool
                WriteLine("Tools in Category: {0}, Type: {1}", tCollec.Category[cate], tCollec.Type[cate, type]);
                tCollec.DisplayToolOfSelectedCategoryType(cate, type);
                // display tool


            }
            else
            {
                if (checkNumber == 0)
                {
                    Clear();
                    WriteLine("\nThe tools in the Category: {0}, Type: {1} are empty.", tCollec.Category[cate], tCollec.Type[cate, type]);
                    WriteLine("There is no tool can be removed.");
                }
                else
                {
                    Clear();
                    StaffAddRemoveTool_Display(tCollec, cate, type, selection);
                    WriteLine("Tools currently in the Category: {0}, Type: {1}", tCollec.Category[cate], tCollec.Type[cate, type]);
                    for (int i = 0; i < 20; i++)
                    {
                        if (tCollec.toolCollect[cate, type, i] != null)
                        {
                            WriteLine("Tool Name: {0}, Tool Total Number: {1}, Tool Avail Number: {2} ",
                                tCollec.toolCollect[cate, type, i].name, tCollec.toolCollect[cate, type, i].num,
                                tCollec.toolCollect[cate, type, i].avail_num);
                        }
                    }

                    WriteLine("Enter Tool Name: ");
                    useropt = ReadLine();
                    name = useropt;
                    tCollec.Remove_Tool(cate, type, name, mCollec);
                    // Remove whole tool
                    //tCollec.Display_Tool();
                    // Display tool
                }
            }

            WriteLine("Press any key to move to staff menu.");
            ReadLine();
        }

        static void RegisterMember(MemberCollection mCollec)
        {
            string fname, lname, no, pw;
            bool checkFname;
            Clear();
            WriteLine("Enter fname: ");
            fname = ReadLine().ToLower();
            checkFname = mCollec.Check_MemberfName(fname);

            while (checkFname)
            {
                Clear();
                WriteLine("The first name has been used, please enter a unique first name.");
                WriteLine("Enter fname: ");
                fname = ReadLine().ToLower();
                checkFname = mCollec.Check_MemberfName(fname);
            }

            WriteLine("Enter lname: ");
            lname = ReadLine().ToLower();
            WriteLine("Enter Phone: ");
            no = ReadLine();
            WriteLine("Enter Password: ");
            pw = ReadLine().ToLower();

            Member newMember = new Member(fname, lname, no, pw);

            mCollec.Add_Member(newMember);
            mCollec.Display_Member_All();
            ReadLine();
        }

        static void RemoveMember(MemberCollection mCollec)
        {
            Clear();
            mCollec.Display_Member_All();
            WriteLine("Enter fname: ");
            string fname;
            fname = ReadLine().ToLower();

            while (!mCollec.Check_MemberfName(fname))
            {
                Clear();
                WriteLine("The member {0} doesn't exist.", fname);
                WriteLine("Enter fname: ");
                fname = ReadLine().ToLower();
            }

            mCollec.Remove_Member(fname);
            mCollec.Display_Member_All();
            ReadLine();
        }

        static void DisplayBorrowing(MemberCollection mCollec)
        {
            int count = 0;
            foreach (DictionaryEntry entry in mCollec.memberCollec)
            {
                if (((Member)entry.Value).memberToolCollec.Check_ToolCollection())
                {
                    WriteLine("Member: {0}", entry.Key);
                    ((Member)entry.Value).memberToolCollec.Display_Tool();
                    count++;
                }
            }

            if (count == 0)
            {
                WriteLine("No one is borrowing tools.");
            }
            ReadLine();
        }

        static void FindMemberPhoneNumber(MemberCollection mCollec)
        {
            Clear();
            mCollec.Display_Member_All();
            WriteLine("Enter fname: ");
            string fname;
            fname = ReadLine().ToLower();

            while (!mCollec.Check_MemberfName(fname))
            {
                Clear();
                WriteLine("You are entering an invalid name, please try again.");
                WriteLine("Enter fname: ");
                fname = ReadLine().ToLower();
            }

            mCollec.FindMemberPhoneNum(fname);
            ReadLine();
        }

        static int SelectType_Display(ToolCollection tCollect, int cate, int selection)
        {
            int countType = 0;
            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("  ");
            WriteLine("Category: {0}", tCollect.Category[cate]);
            if (selection == 1)
            {
                WriteLine("==========Add Tool==========");
            }
            else
            {
                WriteLine("==========Remove Tool==========");
            }
            WriteLine("Input Type:");
            for (int i = 0; i < 7; i++)
            {
                if (tCollect.Type[cate, i] != "")
                {
                    countType++;
                    WriteLine("{0}. {1}", i + 1, tCollect.Type[cate, i]);
                }
            }
            WriteLine("==============================");
            WriteLine("   Select option from menu (0 to exit) - ");
            return countType;
        } // display for selecting type

        static void SelectCategory_Display(ToolCollection tCollect, int selection)
        {
            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("  ");
            if (selection == 1)
            {
                WriteLine("==========Add Tool==========");
            }
            else
            {
                WriteLine("==========Remove Tool==========");
            }
            WriteLine("Input Category:");
            for (int i = 0; i < 9; i++)
            {
                WriteLine("{0}. {1}", i + 1, tCollect.Category[i]);
            }// displaying category
            WriteLine("==============================");
            WriteLine("   Select option from menu (0 to exit) - ");
        }

        static void MainMenu_Display()
        {
            Clear();
            WriteLine("Welcome to the Tool Library System");
            WriteLine("  ");
            WriteLine("==========Main Menu==========");
            WriteLine("1. Staff login");
            WriteLine("2. Member login");
            WriteLine("0. Exit");
            WriteLine("=============================");
            WriteLine("   Select option from menu (0 to exit) - ");
        }

        static void StaffLogin()
        {
            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("  ");
            WriteLine("==========Staff Login==========");
            Write("Please enter account name: ");
            string account = ReadLine();

            // staff account name = staff
            while (account != "staff")
            {
                Clear();
                WriteLine("Welcome to the Tool Library");
                WriteLine("  ");
                WriteLine("==========Staff Login==========");
                Write("Your account name is invalid, Please try again: ");
                account = ReadLine();
            }

            Write("Please enter your password: ");
            string password = ReadLine();

            // password = today123
            while (password != "today123")
            {
                Clear();
                WriteLine("Welcome to the Tool Library");
                WriteLine("  ");
                WriteLine("==========Staff Login==========");
                WriteLine("Account name: {0}", account);
                Write("Your password is invalid, Please try again: ");
                password = ReadLine();
            }

            WriteLine("Login Successfully. Press any key to move to staff menu.");
            ReadKey();

        }

        static void MemberLogin(ToolCollection tCollect, MemberCollection mCollec)
        {
            string fname, lname, password;

            Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("  ");
            WriteLine("==========Member Login==========");
            Write("Please enter your first name: ");
            fname = ReadLine().ToLower();

            while (!mCollec.Check_MemberfName(fname))
            {
                Clear();
                WriteLine("Welcome to the Tool Library");
                WriteLine("  ");
                WriteLine("==========Member Login==========");
                Write("No such member with first name {0}, Please enter a valid first name: ", fname);
                fname = ReadLine().ToLower();
            }

            Write("Please enter your last name: ");
            lname = ReadLine().ToLower();

            while (!mCollec.Check_MemberlName(fname, lname))
            {
                Clear();
                WriteLine("Welcome to the Tool Library");
                WriteLine("  ");
                WriteLine("==========Member Login==========");
                WriteLine("First Name :{0}", fname);
                Write("No such member with last name {0}, Please enter a valid first name: ", lname);
                lname = ReadLine().ToLower();
            }

            Write("Please enter your password: ");
            password = ReadLine();

            while (!mCollec.Check_MemberPassword(fname, password))
            {
                Clear();
                WriteLine("Welcome to the Tool Library");
                WriteLine("  ");
                WriteLine("==========Member Login==========");
                WriteLine("First Name : {0}", fname);
                WriteLine("Last Name : {0}", lname);
                Write("Password is incorrect, please try again: ");
                password = ReadLine();
            }

            Member m1 = ((Member)mCollec.memberCollec[fname]);
            WriteLine("Login Successfully. Press any key to move to member menu.");
            ReadLine();
            MemberMenu(tCollect, mCollec, m1);
        }


        static void MemberMenu(ToolCollection tCollec, MemberCollection mCollec, Member m1)
        {
            /*Clear();
            WriteLine("Welcome to the Tool Library");
            WriteLine("Member Name: {0}", m1.fname);
            WriteLine("==========Member Menu==========");
            WriteLine("1. Display tools of a type"); 
            WriteLine("2. Borrow a tool");
            WriteLine("3. Return a tool");
            WriteLine("4. List tools I'm borrowing");
            WriteLine("5. Displaytop three most frequently borrowed tools");
            WriteLine("0. Return to main menu");
            WriteLine("===============================");
            WriteLine("   Select option from menu (0 to exit) - ");
            */

            MemberMenu_Display(m1);

            string useropt;

            useropt = ReadLine();

            switch (useropt)
            {
                case "1":
                    SelectCategory(tCollec, mCollec, m1, 999);
                    ReadLine();
                    break;
                case "2":
                    BorrowTools(tCollec, mCollec, m1);
                    break;
                case "3":
                    ReturnTools(tCollec, mCollec, m1);
                    break;
                case "4":
                    DisplayBorrowingTools(m1);
                    break;
                case "5":
                    DisplayTop3FrequentBorrowedTools(tCollec);
                    break;
                case "0":
                    MainMenu(tCollec, mCollec);
                    break;
            }

            MemberMenu(tCollec, mCollec, m1);
        }// member menu

        static void BorrowTools(ToolCollection tCollec, MemberCollection mCollec, Member m1)
        {
            string tName;
            int tNum = 0;
            int tool_x, tool_y, tool_z;
            bool check1, check2;
            int m1ToolNum = m1.memberToolCollec.Check_ToolCollection_Number();

            if (m1ToolNum >= 5)
            {
                Clear();
                WriteLine("You have excced the maximum tools you can borrow.");
                WriteLine("Please return tools to borrow others.");
                WriteLine("{0} is currently holding. ", m1.fname);
                m1.memberToolCollec.Display_Tool();
            }
            else
            {
                Clear();
                WriteLine("You still can borrow {0} tools.", 5 - m1ToolNum);
                WriteLine("Please enter tool name: ");
                tName = ReadLine().ToLower();
                (check1, tool_x, tool_y, tool_z) = tCollec.Check_Tool_Existency_Location(tName);
                check2 = tCollec.Check_Tool_Availability(tName);

                while (!check1 || !check2)// check if tool exits and if it's available 
                {
                    Clear();
                    if (!check1)
                    {
                        WriteLine("There is no such tool {0}, please enter a valid tool name: ", tName);
                    }
                    else if (!check2)
                    {
                        WriteLine("The tool {0} is not available, please try other tool name: ", tName);
                    }
                    tName = ReadLine().ToLower();
                    (check1, tool_x, tool_y, tool_z) = tCollec.Check_Tool_Existency_Location(tName);
                    check2 = tCollec.Check_Tool_Availability(tName);
                }

                WriteLine("Please enter the number of tool you want to borrow: ");
                tNum = Convert.ToInt32(ReadLine());

                while (tNum > tCollec.toolCollect[tool_x, tool_y, tool_z].avail_num || m1ToolNum + tNum > 5)// check if borrow number is valid
                {
                    Clear();
                    if (tNum > tCollec.toolCollect[tool_x, tool_y, tool_z].avail_num)
                    {
                        WriteLine("Tool Name: {0}", tName);
                        WriteLine("Tool is not enough, please enter a valid number of tool you want to borrow: ");
                    }
                    else
                    {
                        WriteLine("You've exceed the maximum number you can borrow at the same time.");
                        WriteLine("Please enter a valid number:");
                    }
                    tNum = Convert.ToInt32(ReadLine());
                }

                //m1.memberToolCollec.Add_Tool(tool_x, tool_y, tName, tNum); // m1 add tool
                m1.memberToolCollec.Member_Add_Tool(tool_x, tool_y, tool_z, tName, tNum);
                tCollec.Borrow_Tool(m1, tool_x, tool_y, tool_z, tNum); // toolcollection borrow tools
                mCollec.memberCollec[m1.fname] = m1; // refresh member collection
                //(Member)mCollec.memberCollec[m1.fname] = m1;
                //WriteLine("tool's member collection");
                //((Member)tCollec.toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname]).memberToolCollec.Display_Tool();
                //WriteLine("---");
                WriteLine("\n{0} is currently borrowing.", m1.fname);
                ((Member)mCollec.memberCollec[m1.fname]).memberToolCollec.Display_Tool();
            }
            WriteLine("Press any key to move to member menu.");
            ReadLine();

        }

        static void ReturnTools(ToolCollection tCollec, MemberCollection mCollec, Member m1)
        {
            string tName;
            int tNum;
            int tool_x, tool_y, tool_z;
            bool check1;

            if (m1.memberToolCollec.Check_ToolCollection_Number() <= 0)
            {
                Clear();
                WriteLine("You currently don't have any tool in your collection.");
                WriteLine("Press any key to go back to member menu.");
            }
            else
            {
                Clear();
                WriteLine("Please enter tool name: ");
                tName = ReadLine().ToLower();
                (check1, tool_x, tool_y, tool_z) = m1.memberToolCollec.Check_Tool_Existency_Location(tName);

                while (!check1)// check if tool exits and if it's available 
                {
                    Clear();
                    WriteLine("There is no such tool {0} in your collection, please enter a valid tool name: ", tName);
                    tName = ReadLine().ToLower();
                    (check1, tool_x, tool_y, tool_z) = m1.memberToolCollec.Check_Tool_Existency_Location(tName);
                }

                WriteLine("Please enter the number of tool you want to return: ");
                tNum = Convert.ToInt32(ReadLine());

                while (tNum > m1.memberToolCollec.toolCollect[tool_x, tool_y, tool_z].num)// check if return number is valid
                {
                    Clear();
                    WriteLine("You have Tool: {0} Number: {1} in your collection.", tName, m1.memberToolCollec.toolCollect[tool_x, tool_y, tool_z].num);
                    WriteLine("Please enter a valid number of tool you want to return: ");
                    tNum = Convert.ToInt32(ReadLine());
                }

                m1.memberToolCollec.Remove_SelectedNumber_Tool(tool_x, tool_y, tName, tNum);
                tCollec.Return_Tool(m1, tool_x, tool_y, tool_z, tNum); // toolcollection return tools
                mCollec.memberCollec[m1.fname] = m1; // refresh member collection
                WriteLine("\n{0} is currently borrowing.", m1.fname);
                ((Member)mCollec.memberCollec[m1.fname]).memberToolCollec.Display_Tool();
            }
            WriteLine("Press any key to move to member menu.");
            ReadLine();

        }

        static void DisplayBorrowingTools(Member m1)
        {
            if (m1.memberToolCollec.Check_ToolCollection_Number() > 0)
            {
                m1.memberToolCollec.Display_Tool();
            }
            else
            {
                WriteLine("You currently don't have any tool in your collection.");
                WriteLine("Press any key to go back to member menu.");
            }
            ReadLine();
        }

        static void DisplayTop3FrequentBorrowedTools(ToolCollection tCollect)
        {
            if (tCollect.AllTools().Length == 0)
            {
                WriteLine("No one is currently borrowing tools.");
                ReadKey();
            }
            else
            {
                tCollect.Check_Top3_Borrowing();
            }
        }

        public static void Exit()
        {
            System.Environment.Exit(1);
        }// exit
    }
}
