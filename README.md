# library-borrowing-system
## Description
The program aims to provide a tool borrowing system using c# which can be divided into two parts. One is menu, the other is classes which are Tool, ToolCollection, Member and MemberCollection class. The menu provides a simple interface for users interacting with the program. Turning to classes, the ToolCollection class uses an array of Tool object to store the tool objects. For MemberCollection, I use a hastable of Member class to store the member objects. 

![image](https://user-images.githubusercontent.com/115144351/203486106-122b800f-fb77-4860-bb9f-a2a832f84cc1.png)

(The picture illustrates a rough idea of some main elements and interaction in the program.)

## Algorithm Design & Analysis
### Tool
A 3-dimensional array is used for storing tool objects in ToolCollection class. By using a 3-D array, the program can access each tool object immediately by giving it the location which make it easier for storing, removing, and so on. Comparing to 1-D and 2-D array which have to traverse more for loops to access the tool object and need to have more variable to store needed data, 3-D array can do any manipulation in a glimpse and with less storage needed. It is far more efficient in time and space for using a 3-D array to store and use tool objects in the system. In a nutshell, 3-D array is best suit for the system. 
For example, if the program tried to access the first tool in the first category and in the first type, it can access like this toolCollec[0, 0, 0] which is quite simple and efficient. And, I only need to initiate name, number, available number, borrowed times, and member collection in my tool class which would be less than using 1-D and 2-D array. I have to create more variables to store category, type, and so on in 1-D and 2-D array.

![image](https://user-images.githubusercontent.com/115144351/203486832-4383902f-e667-4f45-a52c-069c71f76acc.png)

(pseudo code: 3-D array)

As a result of using 3-D array, the best case of time efficiency is Cbest(n) = ϴ(1) and the worst case is Cworst(n) = ϴ(n) in Add a tool function. Apparently, it would be lot more time and space efficient comparing to using 1-D and 2-D array with nested for loop and if-else condition.

![image](https://user-images.githubusercontent.com/115144351/203486871-2b2f297f-be8c-420d-b727-32bb7917828d.png)

(pseudo code: Add_tool)

### Display top three most borrowing tools
Heap sort is implemented for Display top three. The detailed design pseudo codes are listed in the following pictures. They can be divided into 3 parts as Check_Top3_Borrowing, HeapSort and Heapify. Check_Top3_Borrowing is to get the result from heap sort function and display the top 3.

![image](https://user-images.githubusercontent.com/115144351/203486954-8633e917-d598-4152-8fef-3505fcd7e1ff.png)

(pseudo code: Check_Top3_Borrowing)

HeapSort is to sort the array into descending order. In HeapSort, there are 2 main function that are Heap Bottom Up and Minimum Key Deletion.

![image](https://user-images.githubusercontent.com/115144351/203486983-299986a7-6df9-487a-b68d-c8198fff0825.png)

(pseudo code: HeapSort)

Heapify is to convert tree or subtree into heap.

![image](https://user-images.githubusercontent.com/115144351/203487019-51332920-816e-4109-abdc-50c9d40c7a39.png)

(pseudo code: Heapify)

The efficiency of Heap Bottom Up is O(n). Additionally, the efficiency of Minimum Key Deletion is O(nlogn). As a result, the efficiency of Heap sort is O(n) + O(nlogn) = O(nlogn). 
Heap sort is a compared based sorting algorithm. Comparing to other advanced sorting algorithm taught in class, heap sort has the best time efficiency which is the most significant advantage for the program. As for space efficiency, heap sort doesn’t need further temporary space which is a good benefit for the program. As for stability, heap sort is not stable. But, the program doesn’t need to care about stability. Hence, it is totally perfect for the program. Considering the advantages are more than disadvantages, heap sort is the best algorithm for Display top3 tools.


## Screenshots

![image](https://user-images.githubusercontent.com/115144351/203486441-192e3050-6d1e-47c8-b99a-d245eb5528f7.png)
(Menu)

![image](https://user-images.githubusercontent.com/115144351/203486468-b0686704-d637-42d7-8776-834e267a27e8.png)
(Staff login)

![image](https://user-images.githubusercontent.com/115144351/203486497-28d5310b-ac45-4462-ba53-5e5c69bac8ee.png)
(Staff menu)

![image](https://user-images.githubusercontent.com/115144351/203486536-7f258dc7-19a3-4faf-b96c-09a7e98b1be5.png)
(Member login)

![image](https://user-images.githubusercontent.com/115144351/203486653-b8ca76a9-7b84-4152-a6e6-2ba3cc3519ca.png)
(Member meun)
