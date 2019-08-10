USE [Orders]
GO

-- base table
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Orders'))
BEGIN
    CREATE TABLE [dbo].[Orders]
	(
		[OrderId] INT NOT NULL PRIMARY KEY, 
		[Amount] DECIMAL(10, 2) NOT NULL,
		[VAT] DECIMAL(10, 2) NOT NULL
	)
END

-- first migration
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'CustomerId'
          AND Object_ID = Object_ID(N'dbo.Orders'))
BEGIN
    ALTER TABLE dbo.Orders
	ADD CustomerId INT NOT NULL DEFAULT 0;
END