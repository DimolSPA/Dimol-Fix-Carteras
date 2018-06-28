
CREATE PROCEDURE [dbo].[_Job_Cargar_Datos_Poder_Judicial_Monitoreo_Interno_Cabecera]
AS
BEGIN
	INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza datos de la cabecera a mostar en el monitoreo de Demonio Interno', 'Comienzo', 'Job 5 AM',0 )
	
	DELETE PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA
	
	INSERT INTO PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA(ID,ITEM,VALOR)
	--Nº Causas internas actualizadas
	select '1' ID, 'CantCausasDiaAnterior' ITEM, COUNT(DISTINCT H.ID_CAUSA)Valor
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
	where h.FECHA_HISTORIAL > (DATEADD(DD, -1, GETDATE()))

	UNION ALL
	--Nº Deudores
	SELECT '2' ID, 'CantDeudoresDiaAnterior' ITEM, count(distinct v.rol_ctcid)Valor 
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
	inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
	on R.ID_CAUSA = h.ID_CAUSA
	inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	ON r.TRIBUNAL = K.ID_TRIBUNAL
	inner join [Iluvatar].[dbo].VIEW_ROL v
	  on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
	where h.FECHA_HISTORIAL > (DATEADD(DD, -1, GETDATE()))

	UNION ALL
	--Monto
	select '3' ID, 'SaldoDiaAnterior' ITEM, sum(saldo) Valor from
			(SELECT sum(rd.RDC_SALDO) saldo
			from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
			inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
			on R.ID_CAUSA = h.ID_CAUSA
			inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
			ON r.TRIBUNAL = K.ID_TRIBUNAL
			inner join [Iluvatar].[dbo].VIEW_ROL v
			  on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
			join ROL_DOCUMENTOS rd
			on v.rol_rolid = rd.RDC_ROLID
			where h.FECHA_HISTORIAL > (DATEADD(DD, -1, GETDATE()))
			group by v.rol_rolid) SubTotal

	UNION ALL
	--Nº Deudores Judicializados
	SELECT '4' ID, 'CantDeudoresJudicializado' ITEM, count(distinct v.rol_ctcid) Valor --v.ctc_rut,v.ctc_nomfant,count(h.id_causa) 
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
	inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
	on R.ID_CAUSA = h.ID_CAUSA
	inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	ON r.TRIBUNAL = K.ID_TRIBUNAL
	inner join [Iluvatar].[dbo].VIEW_ROL v
	  on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
	UNION ALL
	--Nº Causas Judicializadas
	select '5' ID, 'CantCausasJudicializadas' ITEM, COUNT(DISTINCT H.ID_CAUSA) Valor
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h

	UNION ALL
	--Monto Judicializado
	select '6' ID, 'SaldoJudicializado' ITEM, sum(saldo) Valor from
			(SELECT sum(rd.RDC_SALDO) saldo
			from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
			inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
			on R.ID_CAUSA = h.ID_CAUSA
			inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
			ON r.TRIBUNAL = K.ID_TRIBUNAL
			inner join [Iluvatar].[dbo].VIEW_ROL v
			  on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
			join ROL_DOCUMENTOS rd
			on v.rol_rolid = rd.RDC_ROLID
			group by v.rol_rolid) SubTotal

	UNION ALL
	--Deudores Con Causas Activas
	SELECT '7' ID, 'CantDeudoresCausasActivas' ITEM, count(distinct v.rol_ctcid) Valor --v.ctc_rut,v.ctc_nomfant,count(h.id_causa) 
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
	inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
	on R.ID_CAUSA = h.ID_CAUSA
	inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	ON r.TRIBUNAL = K.ID_TRIBUNAL
	inner join [Iluvatar].[dbo].VIEW_ROL v
	on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
	where 
		CASE v.rol_pclid
			WHEN 86 THEN CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178,62) THEN 1 ELSE 0 END 
			ELSE 
				CASE v.rol_pclid
					WHEN 424 THEN 
						CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178,62) THEN 1 ELSE 0 END 
					ELSE 
						CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178) THEN 1 ELSE 0 END
				END
		END = 1
	AND 
		 CASE v.ctc_quiebra
			 WHEN 'S' THEN 
					 CASE WHEN v.rol_esjid not in(3,5,10) THEN 1 ELSE 0 END
			 ELSE 
					 CASE WHEN v.rol_estid not in(5,10) THEN 1 ELSE 0 END
		 END = 1
	AND v.rol_bloqueo = 'N'

	UNION ALL
	--Nº Causas Activas
	select '8' ID, 'CantCausasActivas' ITEM, COUNT(DISTINCT H.ID_CAUSA) Valor
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
	inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
	on R.ID_CAUSA = h.ID_CAUSA
	inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	ON r.TRIBUNAL = K.ID_TRIBUNAL
	inner join [Iluvatar].[dbo].VIEW_ROL v
	on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
	where 
		CASE v.rol_pclid
			WHEN 86 THEN CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178,62) THEN 1 ELSE 0 END 
			ELSE 
				CASE v.rol_pclid
					WHEN 424 THEN 
						CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178,62) THEN 1 ELSE 0 END 
					ELSE 
						CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178) THEN 1 ELSE 0 END
				END
		END = 1
	AND 
		 CASE v.ctc_quiebra
			 WHEN 'S' THEN 
					 CASE WHEN v.rol_esjid not in(3,5,10) THEN 1 ELSE 0 END
			 ELSE 
					 CASE WHEN v.rol_estid not in(5,10) THEN 1 ELSE 0 END
		 END = 1
	AND v.rol_bloqueo = 'N'

	UNION ALL
	--Monto Causa Activas
	select '9' ID, 'SaldoCausaActiva' ITEM, sum(saldo) Valor from
			(SELECT sum(rd.RDC_SALDO) saldo
			from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
			inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
			on R.ID_CAUSA = h.ID_CAUSA
			inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
			ON r.TRIBUNAL = K.ID_TRIBUNAL
			inner join [Iluvatar].[dbo].VIEW_ROL v
			  on v.rol_numero = CONVERT(varchar, R.NUMERO)+'-'+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
			join ROL_DOCUMENTOS rd
			on v.rol_rolid = rd.RDC_ROLID
			where 
				CASE v.rol_pclid
					WHEN 86 THEN CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178,62) THEN 1 ELSE 0 END 
					ELSE 
						CASE v.rol_pclid
							WHEN 424 THEN 
								CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178,62) THEN 1 ELSE 0 END 
							ELSE 
								CASE WHEN v.rol_estid not in(265,249,165,69,74,75,178) THEN 1 ELSE 0 END
						END
				END = 1
			AND 
				 CASE v.ctc_quiebra
					 WHEN 'S' THEN 
							 CASE WHEN v.rol_esjid not in(3,5,10) THEN 1 ELSE 0 END
					 ELSE 
							 CASE WHEN v.rol_estid not in(5,10) THEN 1 ELSE 0 END
				 END = 1
			AND v.rol_bloqueo = 'N'
			group by v.rol_rolid) SubTotal

	UNION ALL
	--Nº Deudores con causas archivadas
	Select '10' ID, 'CantDeudoresCausasArchivadas' ITEM, count(distinct v.ctc_rut) Valor
	FROM [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO  R WITH (NOLOCK)
	  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
	  ON R.ID_CAUSA = T.ID_CAUSA
	  inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	  ON T.TRIBUNAL = K.ID_TRIBUNAL
	  inner join [Iluvatar].[dbo].VIEW_ROL v
	  on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL

	UNION ALL  
	--Causas Archivadas Actuales
	select '11' ID, 'CantCausasArchivadas' ITEM, count(distinct id_causa) Valor 
	from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO

	UNION ALL
	--Monto con causas archivadas
	select '12' ID, 'SaldoCausaArchivada' ITEM, sum(saldo) Valor from
	(Select sum(rd.RDC_SALDO) saldo
	FROM [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO  R WITH (NOLOCK)
	  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
	  ON R.ID_CAUSA = T.ID_CAUSA
	  inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	  ON T.TRIBUNAL = K.ID_TRIBUNAL
	  inner join [Iluvatar].[dbo].VIEW_ROL v
	  on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL
	  join ROL_DOCUMENTOS rd
	  on v.rol_rolid = rd.RDC_ROLID
	  group by v.rol_rolid) SubTotal

	UNION ALL
	-- Cantidad de Deudores con causas archivadas los ultimos 7 dias
	Select '13' ID, 'CantDeudoresCausasArchivadas7dias' ITEM, count(distinct v.ctc_rut) Valor
	FROM (SELECT TOP 1 WITH TIES 
			FECHA_ENVIO, ID_CAUSA
		FROM 
			[10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO WITH (NOLOCK)
		WHERE FECHA_ENVIO > (DATEADD(DD, -7, GETDATE()))
		ORDER BY
			ROW_NUMBER() OVER(PARTITION BY FECHA_ENVIO, ID_CAUSA ORDER BY FECHA_ENVIO DESC)) R 
	  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
	  ON R.ID_CAUSA = T.ID_CAUSA
	  inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	  ON T.TRIBUNAL = K.ID_TRIBUNAL
	  inner join [Iluvatar].[dbo].VIEW_ROL v
	  on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL

	UNION ALL
	--Cantidad de causas archivadas los utimos 7 dias
	select 
		'14' ID, 'CantCausaArchivada7Dias' ITEM,
		count(ID_CAUSA) Valor
	from (SELECT TOP 1 WITH TIES 
			FECHA_ENVIO, ID_CAUSA
		FROM 
			[10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO WITH (NOLOCK)
		WHERE FECHA_ENVIO > (DATEADD(DD, -7, GETDATE()))
		ORDER BY
			ROW_NUMBER() OVER(PARTITION BY FECHA_ENVIO, ID_CAUSA ORDER BY FECHA_ENVIO DESC)) Archivadas

	UNION ALL
	--Monto con causas archivadas
	select '15' ID, 'SaldoCausaArchivada7Dias' ITEM, sum(saldo) Valor from
	(Select sum(rd.RDC_SALDO) saldo
	FROM (SELECT TOP 1 WITH TIES 
			FECHA_ENVIO, ID_CAUSA
		FROM 
			[10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO WITH (NOLOCK)
		WHERE FECHA_ENVIO > (DATEADD(DD, -7, GETDATE()))
		ORDER BY
			ROW_NUMBER() OVER(PARTITION BY FECHA_ENVIO, ID_CAUSA ORDER BY FECHA_ENVIO DESC)) R
	  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
	  ON R.ID_CAUSA = T.ID_CAUSA
	  inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
	  ON T.TRIBUNAL = K.ID_TRIBUNAL
	  inner join [Iluvatar].[dbo].VIEW_ROL v
	  on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL
	  join ROL_DOCUMENTOS rd
	  on v.rol_rolid = rd.RDC_ROLID
	  group by v.rol_rolid) SubTotal


	  INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza datos de la cabecera a mostar en el monitoreo de Demonio Interno', 'Termino', 'Job 5 AM',0 )
END


