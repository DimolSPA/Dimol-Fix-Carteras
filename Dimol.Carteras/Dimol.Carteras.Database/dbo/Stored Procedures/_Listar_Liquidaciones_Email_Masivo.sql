﻿CREATE PROCEDURE [dbo].[_Listar_Liquidaciones_Email_Masivo]
AS
	SELECT [RPT_RPTID] AS Id
		,[RPT_NOMBRE] AS Nombre
	FROM [dbo].[REPORTES]
	WHERE [RPT_TRVID] = 355
	ORDER BY [RPT_NOMBRE] ASC
RETURN 0