CREATE PROCEDURE [dbo].[_Listar_Proceso_Itinerarios]

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @time time(3)
	SET @time = GETDATE()--'23:00:00.000'

	SELECT
		PROCESO_ITINERARIOS.CODEMP,	
		PROCESO_ITINERARIOS.PROCESO,
		PROCESO_ITINERARIOS.DIASEMANA,
		PROCESO_ITINERARIOS.DIA,
		PROCESO.NOMBRE,
		PROCESO.SERVIDOR, 'Running' ESTATUS,
		convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) As INICIO,
		convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) As TERMINO
	FROM PROCESO_ITINERARIOS
	INNER JOIN PROCESO
	  ON PROCESO_ITINERARIOS.PROCESO = PROCESO.PROCESO
	WHERE PROCESO_ITINERARIOS.DIA = DATEPART(Weekday, GETDATE())
	AND (
			(convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) <= convert(varchar(5), @time, 108)
				AND	convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) >= convert(varchar(5), @time, 108)
			)
			OR
			(convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) <= convert(varchar(5), @time, 108)
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) > convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108)
			)
			OR
			(convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) >= convert(varchar(5), @time, 108)
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) > convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108)
			)
			OR
			((convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) <= convert(varchar(5), @time, 108) 
						AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) <= convert(varchar(5), @time, 108))
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) = convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108)
			)
		)
	UNION 
	SELECT
		PROCESO_ITINERARIOS.CODEMP,	
		PROCESO_ITINERARIOS.PROCESO,
		PROCESO_ITINERARIOS.DIASEMANA,
		PROCESO_ITINERARIOS.DIA,
		PROCESO.NOMBRE,
		PROCESO.SERVIDOR, 'Stop' ESTATUS,
		convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) As INICIO,
		convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) As TERMINO
	FROM PROCESO_ITINERARIOS
	INNER JOIN PROCESO
	  ON PROCESO_ITINERARIOS.PROCESO = PROCESO.PROCESO
	WHERE PROCESO_ITINERARIOS.DIA = DATEPART(Weekday, GETDATE())
	AND (	----Procesos para horas inicial mayor a la actual o Procesos ya finalizaron
			(convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) > convert(varchar(5), @time, 108)
			--No Termina al siguiente dia 
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) < convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108))
			OR
			(convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) < convert(varchar(5), @time, 108)
			--No Termina al siguiente dia
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) < convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108))
			OR
			(convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) > convert(varchar(5), @time, 108)
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) < convert(varchar(5), @time, 108))
		)
	UNION 
	--Seleccionar Procesos que corren todo el dia del dia anterior para detenerlo
	SELECT
		PROCESO_ITINERARIOS.CODEMP,	
		PROCESO_ITINERARIOS.PROCESO,
		PROCESO_ITINERARIOS.DIASEMANA,
		PROCESO_ITINERARIOS.DIA,
		PROCESO.NOMBRE,
		PROCESO.SERVIDOR, 'Stop' ESTATUS,
		convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) As INICIO,
		convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) As TERMINO
	FROM PROCESO_ITINERARIOS
	INNER JOIN PROCESO
	  ON PROCESO_ITINERARIOS.PROCESO = PROCESO.PROCESO
	WHERE PROCESO_ITINERARIOS.DIA = DATEPART(Weekday, GETDATE()-1)
	--Hora actual mayor que la hora de inicio y mayor que la hora actual, hora inicio igual a la hora final
	AND ((convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108) <= convert(varchar(5), @time, 108) 
						AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) <= convert(varchar(5), @time, 108))
				AND convert(varchar(5), PROCESO_ITINERARIOS.HORAINICIO, 108) = convert(varchar(5), PROCESO_ITINERARIOS.HORAFIN, 108)
			)

END
