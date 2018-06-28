-- =============================================
-- Author:		César León
-- Create date: 12/01/2018
-- Description:	Inserta un registro en la tabla MUTUAL_LEY_INTERESES
-- =============================================
CREATE PROCEDURE [dbo].[_Insertar_Mutual_Ley_Intereses]
	@FechaPago datetime,
	@FechaDocumento datetime,
	@TasaInteres decimal(15,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

	INSERT INTO [dbo].[MUTUAL_LEY_INTERESES]
				([FECHA_PAGO]
				,[FECHA_DOCUMENTO]
				,[TASA_INTERES])
			VALUES
				(@FechaPago, @FechaDocumento, @TasaInteres)
END
