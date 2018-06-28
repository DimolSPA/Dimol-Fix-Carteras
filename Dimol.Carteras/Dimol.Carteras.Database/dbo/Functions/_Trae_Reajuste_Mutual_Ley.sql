-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Reajuste_Mutual_Ley] 
(
	@fecha datetime
)
RETURNS decimal(15,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @interes  decimal(15,2)

	-- Add the T-SQL statements to compute the return value here
	select @interes = reajuste from mutual_ley_reajuste where  fecha_pago = DATEADD(m, DATEDIFF(m, 0, GETDATE()), 0)     -- CAST(convert(varchar(10),GETDATE(),112) AS date) 
	and @fecha >= fecha_inicial and @fecha <= fecha_final

	-- Return the result of the function
	RETURN @interes

END