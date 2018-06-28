
-- =============================================
-- Author:		César León
-- Create date: 06-04-2018
-- Description:	Indica si la comuna pertenece a la región metropolitana
-- =============================================
CREATE PROCEDURE [dbo].[_ComunaPerteneceRegionMetropolitana] (
	@comunaBuscar varchar(100)
) AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @regionId int

	SET @regionId = (SELECT TOP(1) reg_regid FROM [dbo].[VIEW_DATOS_GEOGRAFICOS] WHERE com_nombre = @comunaBuscar)

	IF @regionId = 6
		BEGIN
			SELECT 1
		END
	ELSE
		BEGIN
			SELECT 0
		END
END
