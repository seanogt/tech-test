-- Author:		Kgaugelo
-- Create date: 2020-08-15
-- Description:	Get Customer Details
-- =============================================
CREATE PROCEDURE spGetCustomer 
	@CustomerId INT	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT * FROM [Customer] 
	WHERE CustomerId = @CustomerId
END
