USE master;  
USE TaskManager;
GO  

DROP PROCEDURE IF EXISTS dbo.GetEmployeesByProjectId;
DROP PROCEDURE IF EXISTS dbo.GetProjects;

Go

CREATE PROCEDURE dbo.GetEmployeesByProjectId   
    @Id int
AS   
    SET NOCOUNT ON;  
    SELECT 
	   [MId]
      ,[FirstName]
      ,[LastName]
      ,[EmploymentType]
      ,[ProjectId]
	FROM [TaskManager].[dbo].[Employees] 
    WHERE ProjectId = @Id
GO  

CREATE PROCEDURE dbo.GetProjects   
AS   
    SET NOCOUNT ON;  
    SELECT TOP (1000) [Id]
      ,[ProjectName]
    FROM [TaskManager].[dbo].[Projects]
GO  

