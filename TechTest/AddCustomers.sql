USE [Customers]
GO

-- The initial customer table
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Customer'))
BEGIN
    CREATE TABLE [dbo].[Customer]
	(
		[CustomerId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
		[Name] NVARCHAR(100) NOT NULL,
		[Country] VARCHAR(2) NOT NULL,
		[DateOfBirth] DATETIME NOT NULL
	)
END