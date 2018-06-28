-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Fecha_Cuaderno_PJ] 
(
	@id_causa int,
	@id_cuaderno int
)
RETURNS datetime
AS
BEGIN
	-- Declare the return variable here
	DECLARE @fecha datetime

	-- Add the T-SQL statements to compute the return value here
	set @fecha = (
	select top 1 FECHA_TRAMITE from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_HISTORIAL with (nolock) where ID_CAUSA = @id_causa and ID_CUADERNO = @id_cuaderno
	order by FECHA_TRAMITE asc
	)

	-- Return the result of the function
	RETURN @fecha

END
