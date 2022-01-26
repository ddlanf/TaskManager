USE master;
USE TaskManager2;  

CREATE TABLE [Projects] (
	[Id] int NOT NULL, 
	[ProjectName] nvarchar(50) NOT NULL
	CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
);

CREATE TABLE Employees (
	[MId] int NOT NULL, 
	[FirstName] nvarchar(100) NOT NULL, 
	[LastName] nvarchar(100) NOT NULL, 
	[EmploymentType] nvarchar(50),
	CONSTRAINT [PK_Employees] PRIMARY KEY ([MID])
);

CREATE TABLE Tasks (
	[Id] int PRIMARY KEY, 
	[Description] nvarchar(200) NOT NULL,
	[StartDate] datetimeoffset NOT NULL, 
	[DueDate] datetimeoffset NOT NULL,
	[ProjectID] int NOT NULL, 
	CONSTRAINT [FK_Tasks] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
	ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE EmployeeTasks (
	[EmployeeId] int NOT NULL, 
	[TaskId] int NOT NULL,
	CONSTRAINT [PK_EmployeeTasks] PRIMARY KEY ([EmployeeId], [TaskId]),
	CONSTRAINT [FK_EmployeeTasks_Employees_Id] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([MId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EmployeeTasks_Tasks_Id] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);





