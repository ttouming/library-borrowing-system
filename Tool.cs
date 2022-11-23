using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Collections;

namespace library_borrowing_system
{
    class Tool
    {
        public string name { get; set; }

        public int num { get; set; }

        public int avail_num { get; set; }

        public int borrowed_times { get; set; }

        public MemberCollection memlib = new MemberCollection();

        public Tool(string toolName, int toolNum)
        {
            name = toolName;
            num = toolNum;
            avail_num = toolNum;
            borrowed_times = 0;
        }

        public Tool(string toolName, int toolNum, int borrow)
        {
            name = toolName;
            num = toolNum;
            avail_num = toolNum;
            borrowed_times = borrow;
        }

        public Tool() { }
    }

    class ToolCollection
    {
        public Tool[,,] toolCollect = new Tool[9, 10, 20];

        public string[] Category = { "Gardening tools", "Flooring tools", "Fencing tools",
                "Measuring tools", "Cleaning tools", "Painting tools",
                "Electronic tools", "Electricity tools", "Automotive tools"};
        // for showing category

        public string[,] Type = { { "Line Trimmers", "Lawn Mowers", "Hand Tools",
                "Wheelbarrows", "Garden Power Tools", "", "" }, { "Scrapers", "Floor Lasers",
                "Floor Levelling Tools", "Floor Levelling Materials", "Floor Hand Tools",
                "Tiling Tools" , ""}, { "Hand Tools", "Electric Fencing", "Steel Fencing Tools",
                "Power Tools", "Fencing Accessories" ,"", ""},  { "Distance Tools", "Laser Measurer",
                "Measuring Jugs", "Temperature & Humidity Tools", "Levelling Tools", "Markers" , ""},
                { "Draining", "Car Cleaning", "Vaccum", "Pressure Cleaners", "Pool Cleaning",
                "Floor Cleaning" , ""}, { "Sanding Tools", "Brushes", "Rollers", "Paint Removal Tools",
                "Paint Scrapers", "Sprayers" , ""}, {"Voltage Tester", "Oscilloscopes", "Thermal Imaging",
                "Data Test Tool", "Insulation Testers", "", "" }, { "Test Equipment", "Safety Equipment",
                "Basic Hand tools", "Circuit Protection", "Cable Tools" ,"", ""},{ "Jacks",
                "Air Compressors", "Battery Chargers", "Socket Tools", "Braking", "Drivetrain", "" } };
        // for showing type

        public void Add_Tool(int toolCategory, int toolType, string toolName, int toolNum)
        {
            (bool check, int tool_x, int tool_y, int tool_z) = Check_SelectedCateType_Tool_Existency_Location(toolCategory, toolType, toolName);

            if (Check_SelectedCategoryType_Tool_Number(toolCategory, toolType) == 20 && !check)
            {
                WriteLine("The tool collection in Category: {0}, Type: {1} is fulled.", Category[toolCategory], Type[toolCategory, toolType]);
                WriteLine("Please remove to add new tools");
            }
            else
            {
                if (!check) // if tool name not existed
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (toolCollect[toolCategory, toolType, i] == null)
                        {
                            toolCollect[toolCategory, toolType, i] = new Tool(toolName, toolNum);
                            break;
                        }// There is no this tool name, initiate a new tool in an null place.
                    }
                }
                else // if tool name existed
                {
                    toolCollect[tool_x, tool_y, tool_z].num += toolNum;
                    toolCollect[tool_x, tool_y, tool_z].avail_num = toolCollect[tool_x, tool_y, tool_z].num;
                }
            }
        }

        public void Add_Tool2(int tool_x, int tool_y, int tool_z, string toolName, int toolNum, int borrowTimes)
        {
            toolCollect[tool_x, tool_y, tool_z] = new Tool(toolName, toolNum, borrowTimes);
        }// this is for hardcoding input to create dumb data to verify the function in the system

        public void Member_Add_Tool(int tool_x, int tool_y, int tool_z, string toolName, int toolNum)
        {
            toolCollect[tool_x, tool_y, tool_z] = new Tool(toolName, toolNum);
        }// this is for adding tools into member's ToolCollection

        public void Remove_Tool(int toolCategory, int toolType, string toolName, MemberCollection b)
        {
            (bool check, int tool_x, int tool_y, int tool_z) = Check_SelectedCateType_Tool_Existency_Location(toolCategory, toolType, toolName);

            if (check)// tool name exist
            {
                if (toolCollect[tool_x, tool_y, tool_z].num == toolCollect[tool_x, tool_y, tool_z].avail_num)
                {
                    toolCollect[tool_x, tool_y, tool_z] = null;
                    WriteLine("Tools named {0} are removed successfully.", toolName);
                    // set toolCollect[i, j, z] = null, if total num = avail num (all in lib)
                }
                else
                {
                    foreach (DictionaryEntry entry in b.memberCollec)
                    {
                        if (((Member)entry.Value).memberToolCollec.toolCollect[tool_x, tool_y, tool_z] != null)
                        {
                            if (((Member)entry.Value).memberToolCollec.toolCollect[tool_x, tool_y, tool_z].name == toolName)
                            {
                                WriteLine("Some of the tools are still being borrowed.");
                                WriteLine("| Member: " + entry.Key + "| Tool Name: " + toolName + "| Number :" + ((Member)entry.Value).memberToolCollec.toolCollect[tool_x, tool_y, tool_z].num);
                            }
                        }
                        //break;
                    }
                }
            }
            else
            {
                WriteLine("there is no such tool");
            }
        }

        public void Remove_SelectedNumber_Tool(int toolCategory, int toolType, string toolName, int toolNum)
        {
            for (int i = 0; i < 20; i++)
            {
                if (toolCollect[toolCategory, toolType, i] != null)
                {
                    if (toolCollect[toolCategory, toolType, i].name == toolName)
                    {
                        toolCollect[toolCategory, toolType, i].num -= toolNum;
                        toolCollect[toolCategory, toolType, i].avail_num = toolCollect[toolCategory, toolType, i].num;

                        if (toolCollect[toolCategory, toolType, i].num == 0)
                        {
                            toolCollect[toolCategory, toolType, i] = null;
                        }
                        break;
                    }

                }
                else if (i == 20 && toolCollect[toolCategory, toolType, i] == null)
                {
                    WriteLine("there is no such tool");
                    break;
                }
            }
        }

        public bool Check_ToolCollection()// check if there is any tool
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            return true;
                            // return true if there is any tool in the tool collection
                        }
                    }
                }
            }
            return false;
        }

        public int Check_ToolCollection_Number()// check how many tools in the toolCollection
        {
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            count += toolCollect[i, j, z].num;
                            // count+= toolCollect[i,j,z].num, if there is a tool
                        }
                    }
                }
            }
            return count;
        }

        public int Check_SelectedCateType_ToolCollection_Number(int x, int y)// check how many tools in the toolCollection
        {
            int count = 0;

            for (int z = 0; z < 20; z++)
            {
                if (toolCollect[x, y, z] != null)
                {
                    count += toolCollect[x, y, z].num;
                    // count+= toolCollect[i,j,z].num, if there is a tool
                }
            }

            return count;
        }

        public int Check_ToolCollection_Borrowtimes()// check how many tools in the toolCollection
        {
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null && toolCollect[i, j, z].borrowed_times > 0)
                        {
                            count++;
                            // count+= toolCollect[i,j,z].num, if there is a tool
                        }
                    }
                }
            }
            return count;
        }

        public bool Check_Tool_Existency(string toolName)// this is to check if tool name exists
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            if (toolCollect[i, j, z].name == toolName)
                            {
                                return true;
                                // return true if tool existed
                            }
                        }
                    }
                }
            }

            return false;
            // return false if tool didn't exsit
        }

        public (bool, int, int, int) Check_Tool_Existency_Location(string toolName)// this is to check if tool name exists and the loaction
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            if (toolCollect[i, j, z].name == toolName)
                            {
                                return (true, i, j, z);
                                // return true if tool existed, return the loaction of the tool with toolName
                            }
                        }
                    }
                }
            }

            return (false, 0, 0, 0);
            // return false if tool didn't exsit
        }

        public (bool, int, int, int) Check_SelectedCateType_Tool_Existency_Location(int tool_x, int tool_y, string toolName)// this is to check if tool name exists and the loaction
        {
            for (int i = 0; i < 20; i++)
            {
                if (toolCollect[tool_x, tool_y, i] != null)
                {
                    if (toolCollect[tool_x, tool_y, i].name == toolName)
                    {
                        return (true, tool_x, tool_y, i);
                        // return true if tool existed, return the loaction of the tool with toolName
                    }
                }
            }
            return (false, 0, 0, 0);
            // return false if tool didn't exsit
        }

        public bool Check_Tool_Availability(string toolName) // this is to check tool if avail_num > 0
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            if (toolCollect[i, j, z].name == toolName)
                            {
                                if (toolCollect[i, j, z].avail_num > 0)
                                {
                                    return true;
                                    // return true if tool avail num > 0
                                }
                            }
                        }
                    }
                }
            }

            return false;
            // return false if tool avail num <= 0;
        }

        public int Check_SelectedCategoryType_Tool_Number(int tool_x, int tool_y) // this is to check tool if avail_num > 0
        {
            int count = 0;

            for (int i = 0; i < 20; i++)
            {
                if (toolCollect[tool_x, tool_y, i] != null)
                {
                    count++;
                }
            }

            return count;
        }

        public void Borrow_Tool(Member m1, int tool_x, int tool_y, int tool_z, int toolNum)
        {
            toolCollect[tool_x, tool_y, tool_z].avail_num -= toolNum;
            toolCollect[tool_x, tool_y, tool_z].borrowed_times += toolNum;

            if (toolCollect[tool_x, tool_y, tool_z].memlib.Check_MemberfName(m1.fname))
            {
                toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname] = m1;
            }
            else
            {
                toolCollect[tool_x, tool_y, tool_z].memlib.Add_Member(m1);
            }
            //((Member)toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname]).memberToolCollec.toolCollect[tool_x, tool_y, tool_z].num += toolNum;
            //((Member)toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname]).memberToolCollec.Add_Tool(tool_x, tool_y, toolCollect[tool_x, tool_y, tool_z].name, toolNum);
            // this is will add the number to m1 
        }

        public void Return_Tool(Member m1, int tool_x, int tool_y, int tool_z, int toolNum)
        {
            toolCollect[tool_x, tool_y, tool_z].avail_num += toolNum;
            toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname] = m1;
            if (m1.memberToolCollec.toolCollect[tool_x, tool_y, tool_z] == null)
            {
                WriteLine("You've returned all the tool named {0}.", toolCollect[tool_x, tool_y, tool_z].name);
            }
            //ReadLine();

            //((Member)toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname]).memberToolCollec.toolCollect[tool_x, tool_y, tool_z].num += toolNum;
            //((Member)toolCollect[tool_x, tool_y, tool_z].memlib.memberCollec[m1.fname]).memberToolCollec.Add_Tool(tool_x, tool_y, toolCollect[tool_x, tool_y, tool_z].name, toolNum);
            // this is will add the number to m1 
        }

        public void Display_Tool()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            WriteLine("Tool: {0}, Avail Num: {1}, Total Num: {2}.",
                                toolCollect[i, j, z].name, toolCollect[i, j, z].avail_num, toolCollect[i, j, z].num);
                        }
                    }
                }
            }
        }


        public void DisplayToolOfSelectedCategoryType(int cate, int type)
        {
            for (int i = 0; i < 20; i++)
            {
                if (toolCollect[cate, type, i] != null)
                {
                    WriteLine("Tool Name: {0}, Availiable Number: {1}, Total Num: {2}.",
                        toolCollect[cate, type, i].name, toolCollect[cate, type, i].avail_num, toolCollect[cate, type, i].num);
                }
            }
        }

        public void Check_Top3_Borrowing()
        {
            Tool[] totalTools = AllTools();
            Tool[] top3Tools;
            HeapSort(totalTools);

            if (totalTools.Length > 3) // if totalTools.Length > 3, top3Tools = new Tools[3]
            {
                top3Tools = new Tool[3];
            }
            else // if totalTools.Length <= 3, top3Tools = new Tool[totalTools.Length]
            {
                top3Tools = new Tool[totalTools.Length];
            }

            for (int i = 0; i < top3Tools.Length; i++)
            {
                top3Tools[i] = totalTools[i];
            }
            //DisplayArray(top3Tools);
            for (int i = 0; i < top3Tools.Length; i++)
            {
                WriteLine("| Tool: {0}, | Borrow times: {1}",
                    top3Tools[i].name, top3Tools[i].borrowed_times);
            }
            ReadKey();
        }

        public void HeapSort(Tool[] totalTools)
        {
            int count = totalTools.Length;

            for (int i = count / 2 - 1; i >= 0; i--)
            {
                Heapify(totalTools, count, i);
            }// building a max heap

            for (int i = count - 1; i >= 0; i--)
            {
                //---max key deletion---
                Tool temp = totalTools[0];
                totalTools[0] = totalTools[i];
                totalTools[i] = temp;
                //---max key deletion---
                Heapify(totalTools, i, 0);
            }
        }

        public void Heapify(Tool[] totalTools, int heapSize, int rootNode)
        {
            int smallest = rootNode;
            int left = (2 * rootNode) + 1;
            int right = (2 * rootNode) + 2;

            if (left < heapSize && totalTools[left].borrowed_times < totalTools[smallest].borrowed_times)
                smallest = left;
            if (right < heapSize && totalTools[right].borrowed_times < totalTools[smallest].borrowed_times)
                smallest = right;
            if (smallest != rootNode)
            {
                Tool exchange = totalTools[rootNode];
                totalTools[rootNode] = totalTools[smallest];
                totalTools[smallest] = exchange;
                Heapify(totalTools, heapSize, smallest);
            }

        }

        public Tool[] AllTools()
        {
            int totalLength = Check_ToolCollection_Borrowtimes();

            Tool[] allTools = new Tool[totalLength];

            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null && toolCollect[i, j, z].borrowed_times > 0)
                        {
                            allTools[count] = toolCollect[i, j, z];
                            count++;
                        }
                    }
                }
            }

            return allTools;
        }// this is to collect tool which has borrowed_times > 0 (i.e. tool has been borrowed)

        public string[] Check_Borrowed()
        {
            string[] a = new string[5];
            int c = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (toolCollect[i, j, z] != null)
                        {
                            a[c] = toolCollect[i, j, z].name + "*" + toolCollect[i, j, z].num;
                            c++;
                        }
                    }
                }
            }

            return a;
        }// this is to check if tool had been borrowed or not
    }
}

