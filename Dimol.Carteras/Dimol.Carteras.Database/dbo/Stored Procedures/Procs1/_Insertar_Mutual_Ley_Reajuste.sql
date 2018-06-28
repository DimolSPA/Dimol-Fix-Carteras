
-- =============================================
-- Author:		César León
-- Create date: 19/01/2018
-- Description:	Inserta un registro en la tabla MUTUAL_LEY_REAJUSTE
-- =============================================
CREATE PROCEDURE [dbo].[_Insertar_Mutual_Ley_Reajuste]
	@FechaPago datetime,
	@Periodo varchar(150),
	@FechaInicial datetime,
	@FechaFinal datetime,
	@Reajuste decimal(15,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

	INSERT INTO [dbo].[MUTUAL_LEY_REAJUSTE]
				([FECHA_PAGO]
				  ,[PERIODO]
				  ,[FECHA_INICIAL]
				  ,[FECHA_FINAL]
				  ,[REAJUSTE])
			VALUES
				(@FechaPago, @Periodo, @FechaInicial, @FechaFinal, @Reajuste)
END
