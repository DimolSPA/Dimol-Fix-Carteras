
-- =============================================
-- Author:		César León
-- Create date: <22/02/2018>
-- Description:	<Selecciona los clientes filtrando por si es previsional o no>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rut_Nombre_Cliente_FiltradoEsPrevisional] (
	@texto varchar(200),
	@esPrevisional bit = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE 
		@nombre varchar(250) = '%' + @texto + '%',
		@rut varchar(20) = @texto + '%'
	
	SELECT PCL_RUT + ' - ' + PCL_NOMFANT, PCL_PCLID
	FROM PROVCLI p
	WHERE
	(
		p.PCL_NOMFANT LIKE @nombre
		OR p.PCL_RUT LIKE @rut
	)
	-- Esta condicion permite filtrar entre Civil/Previsional
	AND (@esPrevisional = 0 OR PCL_TIPCLI = 'P')
	AND (@esPrevisional = 1 OR PCL_TIPCLI != 'P')
END