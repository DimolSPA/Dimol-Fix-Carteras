-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE  FUNCTION [dbo].[_Trae_Interes_Mutual_Ley] 
(
	@fecha datetime
)
RETURNS decimal(15,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @interes  decimal(15,2)

	-- Add the T-SQL statements to compute the return value here
	select @interes = tasa_interes from mutual_ley_intereses where fecha_documento = DATEADD(m, DATEDIFF(m, 0, @fecha), 0)  and fecha_pago = CAST(convert(varchar(10),GETDATE(),112) AS date) 
	

	-- Return the result of the function
	RETURN @interes

END