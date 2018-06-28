-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Ultimo_Receptor_PJ] 
(
	@id_causa int
)
RETURNS datetime
AS
BEGIN
	-- Declare the return variable here
	DECLARE @fecha datetime

	-- Add the T-SQL statements to compute the return value here
	set @fecha = (
	select top 1 FECHA from PODER_JUDICIAL_RECEPTOR  with (nolock) where ID_CAUSA = @id_causa order by FECHA desc
	)

	-- Return the result of the function
	RETURN @fecha

END
