USE master
USE TaskManager

DELETE FROM dbo.EmployeeTasks;
DELETE FROM dbo.Tasks;
DBCC CHECKIDENT ('dbo.Tasks', RESEED, 0)
DELETE FROM dbo.Employees;
DBCC CHECKIDENT ('dbo.Employees', RESEED, 0)
DELETE FROM dbo.Projects;
DBCC CHECKIDENT ('dbo.Projects', RESEED, 0)

INSERT INTO dbo.Projects
(ProjectName)

Values 
('Azure Capacity Management'),
('Azure Machine Learning'),
('Amazon Billing'),
('Amazon Fraud Investigation'),
('Salesforce Credit Department');

INSERT INTO dbo.Employees
(FirstName, LastName, EmploymentType, ProjectId)

Values
('Anderson', 'Fuller', 'Contract', 1),
('Amy', 'Jones', 'Full Time', 1),
('John', 'Power', 'Part Time', 1),
('Dean', 'Shin', 'Part Time', 1),
('Brock', 'Keeth', 'Part Time', 1),
('Hector', 'Moe', 'Contract', 2),
('Hanna', 'Heller', 'Full Time', 2),
('Joe', 'Ray', 'Full Time', 2),
('Paul', 'Galliard', 'Full Time', 2),
('Hanson', 'Lee', 'Full Time', 2),
('Holly', 'Gomez', 'Full Time', 3),
('Paul', 'Rogers', 'Contract', 3),
('Satoshi', 'Joe', 'Full Time', 3),
('Nick', 'Arms', 'Contract', 3),
('Cole', 'Keller', 'Contract', 3),
('Andy', 'Lee', 'Contract', 4),
('Daniel', 'Soleman', 'Contract', 4),
('Noah', 'Cheng', 'Full Time', 4),
('Serra', 'Mendoza', 'Full Time', 4),
('Hank', 'Choi', 'Full Time', 5),
('Beth', 'Joy', 'Full Time', 5);

INSERT INTO dbo.Tasks
([Description], StartDate, DueDate, ProjectID)

Values 
('Identify and report possible capacity shortage in Azure resources', '1/10/2021', '2/20/2023', 1),
('Assess customer rating and reviews', '4/10/2020', '6/29/2024', 1),
('Draft a software archtecure diagram', '1/1/2022', '8/30/2022', 2),
('Track invoices for enterprise customers', '2/2/2020', '11/11/2023', 3),
('Hire a new team member', '1/10/2020', '5/7/2022', 3),
('Report fradulent transaction on AWS', '10/9/2023', '8/7/2025', 4);



INSERT INTO dbo.EmployeeTasks
(EmployeeId, TaskId)

Values 
(1, 1),
(1, 2),
(2, 1),
(2, 2),
(3, 1),
(3, 2),
(4, 1),
(4, 2),
(5, 1),
(6, 3), 
(7, 3), 
(8, 3), 
(9, 3),
(11, 4), 
(11, 5), 
(12, 4), 
(13, 4), 
(13, 5), 
(14, 4),
(16, 6),
(17, 6),
(18, 6),
(19, 6);
