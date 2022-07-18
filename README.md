# University-Timetable

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/9eb1e244b7174953aa7a68edda609c2a)](https://www.codacy.com/gh/valerii-martell/University-Timetable/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=valerii-martell/University-Timetable&amp;utm_campaign=Badge_Grade)

Web service for displaying the schedule of classes at the National Technical University of Ukraine "Kyiv Polytechnic Institute". Before the beginning of each semester, the university prepares a schedule for all groups of students in a very large and inconvenient to read Excel file. The project is designed to provide students with the convenience of staying online exactly the schedule they need, indicating only the name of their group.

The project consists of two subprojects:
1. _**UniversalTimetable**_ - a web service for displaying a convenient schedule of student classes by the specified number of their group.
Implemented by using C#, .NET Framework, ASP.NET.

**Main page:**
![image](https://user-images.githubusercontent.com/19497575/161455447-2dd39b72-f34d-4838-9fec-d40276bb13bd.png)

**Timetable example:**
![image](https://user-images.githubusercontent.com/19497575/161455465-52bd2863-13d5-4731-bb3b-e829c254eb29.png)

2. _**FormTable**_ - a simple desktop program for converting the excel file published by the university with class schedules of all groups of students into a set of html files according to one established sample. It must be run manually every six months in a separate directory on the web server.
Implemented by using C#, .NET Framework, WinForms, Microsoft Excel API and HTML.

**Example:**

![image](https://user-images.githubusercontent.com/19497575/161455638-b22cf0d9-2df7-4ab8-bf98-2a53ecb4e36d.png)
