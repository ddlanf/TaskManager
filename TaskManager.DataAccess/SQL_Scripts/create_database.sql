USE master;  

/*If the database is in use, restrict connections temporarily*/
ALTER DATABASE TaskManager
SET RESTRICTED_USER WITH ROLLBACK AFTER 5 SECONDS
  
DROP DATABASE IF EXISTS TaskManager;
CREATE DATABASE TaskManager

