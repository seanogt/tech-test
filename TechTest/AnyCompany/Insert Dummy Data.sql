USE [AnyCompany]
GO

INSERT INTO [dbo].[Customer]
           ([Country]
           ,[Name]
           ,[DateOfBirth])
     VALUES
           ('KK'
           ,'King Kunta'
           ,'1977-11-03'),
		         ('UK'
           ,'UK Dude'
           ,'1977-11-03'),
		   	         ('NU'
           ,'NON UK Dude'
           ,'1977-11-03')
GO

INSERT INTO [dbo].[Orders]
           ([Amount]
           ,[VAT]
           ,[CustomerId])
     VALUES
           (111
           ,3332
           ,2),
		           (500
           ,45
           ,2),
		         (250
           ,23
           ,1),
		           (500
           ,45
           ,1)
		   ,
		         (250
           ,23
           ,1),
		           (4500
           ,3
           ,1)
		    ,
		         (650
           ,27
           ,3),
		           (4500
           ,14
           ,1)
GO


