# Refactoring Assessment

This repository contains a terribly written Web API project. It's terrible on purpose, so you can show us how we can improve it.

## Getting Started

Fork this repository, and make the changes you would want to see if you had to maintain this api. To set up the project:

 - Open in Visual Studio (2015 or later is preferred)
 - Restore the NuGet packages and rebuild
 - Run the project
 
 Once you are satisied, replace the contents of the readme with a summary of what you have changed, and why. If there are more things that could be improved, list them as well.

The api is composed of the following endpoints:

| Verb     | Path                                   | Description
|----------|----------------------------------------|--------------------------------------------------------
| `GET`    | `/api/Accounts`                        | Gets the list of all accounts
| `GET`    | `/api/Accounts/{id:guid}`              | Gets an account by the specified id
| `POST`   | `/api/Accounts`                        | Creates a new account
| `PUT`    | `/api/Accounts/{id:guid}`              | Updates an account
| `DELETE` | `/api/Accounts/{id:guid}`              | Deletes an account
| `GET`    | `/api/Accounts/{id:guid}/Transactions` | Gets the list of transactions for an account
| `POST`   | `/api/Accounts/{id:guid}/Transactions` | Adds a transaction to an account, and updates the amount of money in the account

Models should conform to the following formats:

**Account**
```
{
    "Id": "01234567-89ab-cdef-0123-456789abcdef",
	"Name": "Savings",
	"Number": "012345678901234",
	"Amount": 123.4
}
```	

**Transaction**
```
{
    "Date": "2018-09-01",
    "Amount": -12.3
}
```

# Refactoring Assessment
 

##### Room for Optimization
![image](https://user-images.githubusercontent.com/51988941/101119659-b1035000-3650-11eb-81e7-45ad25a03683.png)
In this piece of code, we are hitting the database five times. First to get all of the accounts data and 4 times to get the individual accounts data. I believe there is room for improvement. 

![image](https://user-images.githubusercontent.com/51988941/101119871-27a04d80-3651-11eb-864f-3e34664faff4.png)

I hit the database once and add all the accounts data into list and return.

I took `private Account Get(Guid id)` from Controller to Model class and make it public. Reason for doing is that we must not have sql connections in controllers. That's not a good practice. We must have business logic in Controllers. All the data related stuff must be kept in models.

![image](https://user-images.githubusercontent.com/51988941/101121783-d47cc980-3655-11eb-844c-bdf0937fd1c8.png)



And i got few errors for doing this :sweat_smile:

![image](https://user-images.githubusercontent.com/51988941/101120663-17896d80-3653-11eb-91b4-d77380e7c467.png)

I fixed them

![image](https://user-images.githubusercontent.com/51988941/101120934-ba41ec00-3653-11eb-8c90-ed157c77ece7.png)
![image](https://user-images.githubusercontent.com/51988941/101121250-8915eb80-3654-11eb-9a0f-300c2d3abfac.png)

In Delete API, we are doing the same thing here again. We are hitting the database two times. First to get Id and then to delete it. I just passed the parameter (id) to delete from Model

![image](https://user-images.githubusercontent.com/51988941/101121508-325ce180-3655-11eb-98ad-2e1640249ff1.png)

I try to do the same with Transaction Model and Controller

![image](https://user-images.githubusercontent.com/51988941/101123736-5f5fc300-365a-11eb-8af8-c5efdf8d04ce.png)

> My expertise are in SQL Server. What I do is use Entity Framework DB First approach after designing database. It takes care of encapsulation and abstraction. I have done this much because I know how thing works in EF.
