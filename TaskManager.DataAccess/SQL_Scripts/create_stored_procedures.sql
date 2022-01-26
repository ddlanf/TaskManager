USE master;  
USE TaskManager;
GO  

DROP PROCEDURE IF EXISTS dbo.GetEmployeeByProjectId;
DROP PROCEDURE IF EXISTS dbo.GetProjects;
Go

CREATE PROCEDURE dbo.GetEmployeeByProjectId   
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
GO  
