﻿CREATE PROCEDURE [dbo].[_Listar_Panel_Demandas_Control_Gestion](
@codemp int)
AS 
BEGIN


	--Total Demandas
	SELECT 1 As ID, count(PANEL_ID) As TOTAL, 'Total Meses Anteriores' As ITEM, NULL As PARENT
	FROM PANEL_DEMANDA with (nolock)
	WHERE /*FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-2, 0)  
	AND FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -2, GETDATE())-2, -1))
	AND PROCESADA = 'N' 
	OR FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0) and */
	FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
	--AND PROCESADA = 'N'
	AND (CURSODEMANDA is NULL or CURSODEMANDA = 'NO')
	union
	SELECT 2 As ID, count(PANEL_ID) As TOTAL, 'Total Mes Actual' As ITEM, 1 As PARENT
	FROM PANEL_DEMANDA with (nolock)
	WHERE FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
	FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	union
	SELECT 3 As ID, count(PANEL_ID)As TOTAL, 'Total Demandas' As ITEM, 2 As PARENT 
	FROM PANEL_DEMANDA with (nolock)
	WHERE --FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND --Mes anterior
	FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (CURSODEMANDA is NULL or CURSODEMANDA = 'NO')
	OR FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
	FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	union 
	Select 4 As ID, count(PD.PANEL_ID)As TOTAL, 'Demandas no Asignadas' As ITEM, 3 As PARENT 
	from PANEL_DEMANDA PD with (nolock)
	left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
	on PD.PANEL_ID = PDD.PANEL_ID
	WHERE --PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND 
	PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
	AND PDD.USRID_ENCARGADO IS NULL 
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.USRID_ENCARGADO IS NULL 
	union 
	Select 5 As ID, count(PD.PANEL_ID)As TOTAL, 'Demandas Asignadas' As ITEM, 3 As PARENT 
	from PANEL_DEMANDA PD with (nolock)
	left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
	on PD.PANEL_ID = PDD.PANEL_ID
	WHERE --PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND 
	PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO') 
	AND PDD.USRID_ENCARGADO IS NOT NULL
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.USRID_ENCARGADO IS NOT NULL
	union 
	Select 6 As ID, count(PD.PANEL_ID)As TOTAL, 'Demandas sin Confeccionar' As ITEM, 5 As PARENT 
	from PANEL_DEMANDA PD with (nolock)
	Join PANEL_DEMANDA_DETALLE PDD with (nolock)
	on PD.PANEL_ID = PDD.PANEL_ID
	WHERE --PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND 
	PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
	AND PDD.FEC_ENTREGA is null 
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.FEC_ENTREGA is null
	union 
	Select 7 As ID, count(PD.PANEL_ID)As TOTAL, 'Demandas Confeccionadas' As ITEM, 5 As PARENT 
	from PANEL_DEMANDA PD with (nolock)
	Join PANEL_DEMANDA_DETALLE PDD with (nolock)
	on PD.PANEL_ID = PDD.PANEL_ID
	WHERE --PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND 
	PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO') 
	AND PDD.FEC_ENTREGA IS NOT NULL
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.FEC_ENTREGA IS NOT NULL
	union 
	Select 8 As ID, count(PD.PANEL_ID)As TOTAL, 'Demandas Ingresadas a Tribunal' As ITEM, 7 As PARENT 
	from PANEL_DEMANDA PD with (nolock)
	Join PANEL_DEMANDA_DETALLE PDD with (nolock)
	on PD.PANEL_ID = PDD.PANEL_ID
	WHERE --PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND 
	PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
	AND PDD.FEC_ENTREGA IS NOT NULL 
	AND PDD.FEC_INGRESO_TRIBUNAL IS NOT NULL
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.FEC_ENTREGA IS NOT NULL
	AND PDD.FEC_INGRESO_TRIBUNAL IS NOT NULL 
	union 
	Select 9 As ID, count(PD.PANEL_ID)As TOTAL, 'Demandas No Ingresadas a Tribunal' As ITEM, 7 As PARENT 
	from PANEL_DEMANDA PD with (nolock)
	Join PANEL_DEMANDA_DETALLE PDD with (nolock)
	on PD.PANEL_ID = PDD.PANEL_ID
	WHERE --PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  AND 
	PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
	AND PDD.FEC_ENTREGA IS NOT NULL
    AND PDD.FEC_INGRESO_TRIBUNAL IS NULL
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.FEC_ENTREGA IS NOT NULL
	AND PDD.FEC_INGRESO_TRIBUNAL IS NULL
END