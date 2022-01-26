USE master;
USE TaskManager;  

DROP TABLE IF EXISTS [EmployeeTasks]
DROP TABLE IF EXISTS [Tasks]
DROP TABLE IF EXISTS [Employees]
DROP TABLE IF EXISTS [Projects]

CREATE TABLE [Projects] (
	[Id] int NOT NULL IDENTITY(1,1), 
	[ProjectName] nvarchar(50) NOT NULL
	CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
);

CREATE TABLE Employees (
	[MId] int NOT NULL IDENTITY(1,1), 
	[FirstName] nvarchar(100) NOT NULL, 
	[LastName] nvarchar(100) NOT NULL, 
	[EmploymentType] nvarchar(50),
	[ProjectId] int NOT NULL, 
	CONSTRAINT [PK_Employees] PRIMARY KEY ([MID]),
	CONSTRAINT [FK_Employees] FOREIGN KEY ([ProjectId]) REFERENCES [Projects]([Id])
);

CREATE TABLE Tasks (
	[Id] int PRIMARY KEY IDENTITY(1,1), 
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





