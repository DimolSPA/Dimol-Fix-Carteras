﻿CREATE PROCEDURE [dbo].[_Trae_Visita_Terreno_Credenciales_GeoGestion]
AS
BEGIN
	select top 1 USERGEO, PASSGEO from VISITA_TERRENO_CREDENCIALES_GEOGESTION
		
END
