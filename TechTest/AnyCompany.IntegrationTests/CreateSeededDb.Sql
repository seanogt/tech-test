CREATE DATABASE [Customers]
GO

CREATE TABLE [Customers].[dbo].[Customer](
	[CustomerId] [int] NOT NULL,
	[Country] [nvarchar](3) NOT NULL,
	[DateOfBirth] [date]  NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [Customers].[dbo].[Customer]
([CustomerId],[Country],[DateOfBirth],[Name])
VALUES 
(1, 'ZA', '1980-04-16', 'bob'),
(2, 'ZA', '1991-09-23', 'jill'),
(3, 'UK', '1985-03-01', 'sarah');


CREATE DATABASE [Orders]
GO

CREATE TABLE [Orders].[dbo].[Orders](
	[OrderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[VAT] [float] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [Orders].[dbo].[Orders] 
([OrderId] ,[CustomerId] ,[Amount] ,[VAT])
VALUES 
(1, 1, 50, 0),
(2, 1, 60, 0),
(3, 2, 70, 0),
(4, 2, 80, 0),
(5, 3, 90, 0.2),
(6, 4, 100, 0.2);