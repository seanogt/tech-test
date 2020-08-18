-- Author:		Kgaugelo
-- Create date: 2020-08-15
-- Description:	Get List of Customer and their orders
-- =============================================
CREATE PROCEDURE spGetOrders 	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT * FROM [Orders] O 
	INNER JOIN [Customers] C
	ON C.CustomerId = O.CustomerId
END
