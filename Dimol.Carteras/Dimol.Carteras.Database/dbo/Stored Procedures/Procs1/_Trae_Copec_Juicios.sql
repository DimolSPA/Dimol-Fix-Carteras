
CREATE procedure [dbo].[_Trae_Copec_Juicios] (@pclid int) as 

	SELECT isnull(cd.ccb_numero, ' ') Cuenta_Interna, 
	isnull(d.CTC_RUT, ' ') Rut_Cliente, 
	case d.CTC_PARTEMP when 'P' then 1 when 'E' then 2 else ' ' end as Tipo_Persona, 
	case d.CTC_PARTEMP when 'E' then d.CTC_NOMFANT else ' ' end as Razon_Social, 
	case d.CTC_PARTEMP when 'P' then d.CTC_NOMBRE else ' ' end as Nombre, 
	case d.CTC_PARTEMP when 'P' then d.CTC_APEPAT else ' ' end as Apellido_Paterno, 
	case d.CTC_PARTEMP when 'P' then d.CTC_APEMAT else ' ' end as Apellido_Materno, 
	isnull(cd.CCB_IDCUENTA, ' ') Tipo_Cliente, 
	isnull(cast(cd.CCB_MONTO as decimal(18,0)),0) Total_Deuda, 
	case cd.ccb_codmon when 1 then 'Pesos' when 2 then 'UF' when 3 then 'Dolar' else ' ' end as Moneda_Total_Deuda, 
	isnull(convert(varchar, cd.CCB_FECING, 103), ' ') Fecha_Envio, 
	isnull(ctc.CCD_CCDID, ' ') Tipo_Cobranza_Documento, 
	--isnull(ctj.CTJ_CTJID, ' ') Tipo_Juicio, 
	case cd.CCB_ESTCPBT when 'J' then isnull((SELECT top 1 case r.ROL_ESJID when 9 then case d.CTC_PARTEMP when 'P' then 29 else 30 end else CTJ_CTJID end FROM COPEC_TIPO_JUICIO ctj WHERE ctj.CTJ_CODEMP = r.ROL_CODEMP and ctj.CTJ_ESJID = r.ROL_ESJID), ' ') else ' ' end AS Tipo_Juicio, 
	isnull(r.ROL_NUMERO, ' ') Rol, 
	isnull(t.TRC_COBID, ' ') Tribunal, 
	isnull(cg.CGS_CETID, ' ') Etapa_Juicio, 
	isnull(cej.CES_NOMBRE, ' ') Estado_Juicio, 
	isnull(cg.CGS_CGSID, ' ') Ultima_Gestion_Judicial, 
	isnull((select top 1 convert(varchar, re.RLE_FECHA , 103) from ROL_ESTADOS re where re.RLE_CODEMP = r.ROL_CODEMP AND re.RLE_ROLID = r.ROL_ROLID order by RLE_FECHA desc), ' ') Fecha_Ultima_Gestion_Judicial, 
	'779448606' Rut_Abogado, 
	'Dimol SpA' Nombre_Abogado, 
	isnull((SELECT top 1 convert(varchar, re.RLE_FECHA , 103) FROM ROL_ESTADOS re WHERE re.RLE_CODEMP = r.ROL_CODEMP AND re.RLE_ROLID = r.ROL_ROLID and RLE_COMENTARIO LIKE '%Creación de Rol%' order by RLE_FECHA desc), ' ') Fecha_Inicio_Juicio, 
	' ' Fecha_Notificacion, 
	' ' Fecha_Embargo, 
	' ' Fecha_Remate, 
	' ' Fecha_Incitacion, 
	' ' Fecha_Fin_Juicio, 
	' ' Rut_Ejecutivo, 
	' ' Nombre_Ejecutivo, 
	' ' Observaciones_Copec, 
	' ' Observaciones_Oficina, 
	isnull((select top 1 cc.CPC_COMID from COPEC_COMUNA cc WHERE cc.COM_COMID = TRB.TRB_COMID), ' ') Localidad, 
	isnull(cd.CCB_CCBID, ' ') Numero_Cuota, 
	' ' Sociedad, 
	' ' Anio,
	' ' Numero_Documento, 
	' ' Rol_Padre, 
	' ' Tribunal_Padre 
	FROM DEUDORES d with(nolock) 
	INNER JOIN CARTERA_CLIENTES_CPBT_DOC cd with(nolock)
	ON d.CTC_CTCID = cd.CCB_CTCID 
	AND d.CTC_CODEMP = cd.CCB_CODEMP 
	LEFT JOIN ROL_DOCUMENTOS rd with(nolock)
	ON rd.RDC_CODEMP = cd.CCB_CODEMP 
	AND rd.RDC_PCLID = cd.CCB_PCLID 
	AND rd.RDC_CTCID = cd.CCB_CTCID 
	AND rd.RDC_CCBID = cd.CCB_CCBID 
	LEFT JOIN ROL r with(nolock)
	ON r.ROL_CODEMP = rd.RDC_CODEMP 
	AND r.ROL_ROLID = rd.RDC_ROLID 
	LEFT JOIN COPEC_TIPO_COBRANZA_DOC ctc with(nolock)
	ON ctc.CCD_CODEMP = cd.CCB_CODEMP 
	AND ctc.CCD_TPCID = cd.CCB_TPCID  
	LEFT JOIN COBRALEX_TRIBUNALES t with(nolock)
	ON r.ROL_CODEMP = t.TRC_CODEMP 
	AND t.TRC_TRBID = r.ROL_TRBID 
	AND t.TRC_PCLID = r.ROL_PCLID 
	LEFT JOIN COPEC_GESTIONES cg with(nolock) 
	ON cg.CGS_CODEMP = cd.CCB_CODEMP 
	AND cg.CGS_ESTID = cd.CCB_ESTID 
	LEFT JOIN COPEC_ESTADO_JUICIO cej with(nolock) 
	ON cej.CES_CESID = cg.CGS_CESID 
	INNER JOIN TRIBUNALES trb with(nolock) 
	ON trb.TRB_CODEMP = r.ROL_CODEMP 
	AND trb.TRB_TRBID = r.ROL_TRBID 
	/*LEFT JOIN COPEC_TIPO_JUICIO ctj 
	ON ctj.CTJ_CODEMP = r.ROL_CODEMP 
	AND ctj.CTJ_ESJID = r.ROL_ESJID */ 
	--AND 
	WHERE cd.CCB_ESTCPBT IN ('V','J') 
	AND	cd.CCB_PCLID = @pclid 
	AND r.ROL_ESJID <> 0 
