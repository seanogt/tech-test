-- Author:		Kgaugelo
-- Create date: 2020-08-15
-- Description:	Plece New Customer Order
-- =============================================
CREATE PROCEDURE spPlaceOrder 
	@OrderId INT,
	@Amount DECIMAL(18,2),
	@VAT DECIMAL(18,2),
	@CustomerId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Orders 
	VALUES (@Amount, @VAT, @CustomerId)
END
GO
