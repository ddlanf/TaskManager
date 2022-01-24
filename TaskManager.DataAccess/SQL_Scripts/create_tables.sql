USE master;
USE TaskManager;  

CREATE TABLE Projects (
	ID int PRIMARY KEY, 
	ProjectName nvarchar(50) NOT NULL
);

CREATE TABLE Employees (
	MID int PRIMARY KEY, 
	FirstName nvarchar(100) NOT NULL, 
	LastName nvarchar(100) NOT NULL, 
	EmploymentType nvarchar(50) 
);

CREATE TABLE Tasks (
	ID int PRIMARY KEY, 
	Description nvarchar(200) NOT NULL,
	StartDate datetimeoffset NOT NULL, 
	DueDate datetimeoffset NOT NULL,
	ProjectID int, 
	FOREIGN KEY (ProjectID) REFERENCES Projects(ID),
	EmployeeID int, 
	FOREIGN KEY (EmployeeID) REFERENCES Employees(MID)
);





