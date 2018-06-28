-- =============================================
-- Author:		César León
-- Create date: 22/01/2018
-- Description:	Elimina registros de la tabla MUTUAL_LEY_INTERESES por Fecha_Pago
-- =============================================
CREATE PROCEDURE [dbo].[_Eliminar_Mutual_Ley_Intereses_PorFechaPago]
	@Mes int,
	@Anio int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

	DELETE FROM [dbo].[MUTUAL_LEY_INTERESES] 
	WHERE MONTH(FECHA_PAGO) = @Mes AND YEAR(FECHA_PAGO) = @Anio
END
