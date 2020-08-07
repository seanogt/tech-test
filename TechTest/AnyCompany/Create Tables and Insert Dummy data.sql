USE [AnyCompany]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](max) NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[VAT] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customer_CustomerId]
GO



INSERT INTO [dbo].[Customers]
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


