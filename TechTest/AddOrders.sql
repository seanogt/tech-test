USE [Orders]
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Orders'))
BEGIN
    CREATE TABLE [dbo].[Orders]
	(
		[OrderId] INT NOT NULL PRIMARY KEY, 
		[Amount] DECIMAL NOT NULL,
		[VAT] DECIMAL NOT NULL
	)
END
