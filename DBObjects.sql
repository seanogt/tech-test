USE [Customers]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 2020/07/17 8:38:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Surname] [varchar](100) NOT NULL,
	[ContactNo] [varchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
	[Country] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerOrders](
	[CustomerOrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Collected] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO

ALTER TABLE [dbo].[CustomerOrders]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO

--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [float] NOT NULL,
	[VAT] [float] NOT NULL,
	[Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_Date]  DEFAULT (getdate()) FOR [Date]
GO


--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SaveOrder]
	@CustomerId INT,
	@Amount FLOAT,
	@VAT FLOAT
AS
BEGIN
	
	SET NOCOUNT ON;

	BEGIN TRANSACTION SaveOrder;  

	INSERT INTO Orders([Amount], [VAT], [Date])
	VALUES(@Amount, @VAT, GETDATE())

	IF @@ERROR <> 0
	BEGIN
		ROLLBACK
		RAISERROR ('Error in saving the order in the Orders table.', 16, 1)
		RETURN
	END

	DECLARE @OrderId INT = (SELECT SCOPE_IDENTITY());

	INSERT INTO CustomerOrders([OrderId], [CustomerId], [Date], [Collected])
	VALUES(@OrderId, @CustomerId, GETDATE(), 0)

	IF @@ERROR <> 0
	BEGIN
		ROLLBACK
		RAISERROR ('Error in saving the order in the CustomerOrders table.', 16, 1)
		RETURN
	END

	COMMIT TRANSACTION SaveOrder;  

END
GO