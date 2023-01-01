<h1 align="center">
  Library MVC
  
  ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
  ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
  ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
  <br>
</h1>

<p align="center">
  <a href="#introduction">Introduction</a> •
  <a href="#installation-guide">Installation Guide</a> •
  <a href="#screenshots">Screenshots</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a> •
  <a href="#contributors">Contributors</a> 
</p>

<div align="center">

![GitHub Repo stars](https://img.shields.io/github/stars/Ctere1/LibraryMVC?style=social)
![GitHub forks](https://img.shields.io/github/forks/Ctere1/LibraryMVC?style=social)
![GitHub watchers](https://img.shields.io/github/watchers/Ctere1/LibraryMVC?style=social)

</div>

## ℹ️Introduction
Library application that you can manage the books and it's users using Microsoft's ASP-NET MVC architecture. It has 2 panels which are `Admin` and `User`. 

It uses **MS-SQL** | **EntityFramework** on DB side. Authentication and authorization processes are performed using `Web Cookies` (aspNet form-auth).

| Admin Actions                               | User Actions                       |                                     
| :----------------------------------------   | :-------------------------------   |        
| `Create, Update and Delete a User`          | `Borrow a Book`                    |         
| `Create, Update, Delete and Return a book`  | `Return the Book`                  |        
| `Get book(s)`                               | `Get book(s)`                      |        
| `Get and Delete all auth Logs`              | `Get own profile auth Logs`        |        
| `Update own profile credentials`            | `Update own profile credentials`   |        


## 📃Installation Guide

To clone and run this application, you'll need [Git](https://git-scm.com), [ASP NET](https://dotnet.microsoft.com/en-us/apps/aspnet), [MS-SQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and [Visual Studio](https://visualstudio.microsoft.com/downloads/) installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/Ctere1/LibraryMVC
# Go into the repository
$ cd LibraryMVC
```
> After these steps,  you should be able to open the project/solution with Visual Studio, build it and run it from there.

## 🪟Screenshots
### **Login-Signup Pages**
> * User can login on this page.
>
>   ![Screenshot_20221226_032707](https://user-images.githubusercontent.com/62745858/209548931-5e1b52e7-7585-4937-bf5e-19db82124885.png)
>
> * User can signup on this page.
>
>   ![ss2](https://user-images.githubusercontent.com/62745858/208377244-46e82e5a-9d17-4f58-8a61-b11cc3b467ca.png)
>
> * Admin can login on this page.
>
>   ![ss3](https://user-images.githubusercontent.com/62745858/208377236-033cc98c-44d8-4b03-8e04-b100d8c8033e.png)

### **User Pages**
> * User can see information about all **own** books in a table on this page (e.g. borrowed books and due date expired books).
>
>   ![user1](https://user-images.githubusercontent.com/62745858/208377113-98f4e78f-d3f3-4987-9af1-9a4d4316cd84.png) 
>
> * User can see all **own** book's details on this page (red lines means book's date expired).
> 
>   ![user2](https://user-images.githubusercontent.com/62745858/208377114-de644782-29b1-477d-99f9-f3775211a394.png)
>
> * User can see all library books and their details. Also user can `Borrow` the books on this page.
> 
>   ![user3](https://user-images.githubusercontent.com/62745858/208377116-4211df21-221c-45f0-9287-0cd45ccf678d.png)
>
> * For borrowing the book, user can choose a `IssuedTo` date on this page.
> 
>   ![user6](https://user-images.githubusercontent.com/62745858/208377112-5144dcd0-3c48-42a3-b042-4d20eecd10c8.png)
>
> * User can see all **own** logs and their details on this page.
> 
>   ![user4](https://user-images.githubusercontent.com/62745858/208377104-9fd1cc11-156f-4ece-a068-358aefc2686f.png)
>
> * User can change **own** account credentials on this page. 
> 
>   ![user5](https://user-images.githubusercontent.com/62745858/208377109-43c0f13c-c14b-443d-85fc-b6a9d266dbcd.png)


### **Admin Pages**
> * Admin can see information about **all** library books and users in a table on this page (e.g. all books, due date expired books, active books, all users).
>
>   ![admin1](https://user-images.githubusercontent.com/62745858/208377186-d93b4b14-1393-4f35-8da6-fdf556577e65.png)
>
> * Admin can `Edit`, `Create` and `Delete` information about **all** library users. Also admin can see user's books on this page.
>
>   ![admin2](https://user-images.githubusercontent.com/62745858/208377188-80248c8c-e6bd-4e1f-8ef1-3bd9c5e9c79a.png)
>
> * Admin can `Edit`, `Create` and `Delete` information about **all** library books on this page.
>
>   ![admin3](https://user-images.githubusercontent.com/62745858/208377192-8f489250-31be-47c3-afff-4c7e34d8ab8c.png)
>
> * Admin can see and `Delete` **all** auth logs on this page.
>
>   ![admin4](https://user-images.githubusercontent.com/62745858/208377195-c88763c9-b453-4bab-9c05-3466041d06a4.png)
>
> * Admin can change **own** account credentials on this page. 
>
>   ![admin5](https://user-images.githubusercontent.com/62745858/208377180-35838f01-010a-44fe-9b47-b036f57f5d46.png)


## 📝Credits

This software uses the following packages:

- Microsoft.AspNet.Mvc
- Bootstrap
- EntityFramework

## ©License
![GitHub](https://img.shields.io/github/license/Ctere1/LibraryMVC)

```
Copyright (c) 2022 Cemil TAN

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## 📌Contributors

<a href="https://github.com/Ctere1/">
  <img src="https://contrib.rocks/image?repo=Ctere1/Ctere1" />
</a>
