
CREATE Procedure [dbo].[Trae_Reporte_Rol_Demandas_Para_PanelDemandaPrevisional]
(
	@codEmp integer,
	@panelId integer
) AS

	SELECT 
		--view_rol.rol_numero,   -- ELiminado
		--view_rol.rol_total,   -- ELiminado
		provcli.pcl_rut,   
		provcli.pcl_nomfant,   
		--view_rol.ctc_numero,   
		deudores.CTC_NUMERO,
		--view_rol.ctc_digito,   
		deudores.CTC_DIGITO,
		--view_rol.ctc_nomfant,   
		deudores.CTC_NOMFANT,
		view_datos_geograficos_a.pai_nombre,   
		view_datos_geograficos_a.reg_nombre,   
		view_datos_geograficos_a.ciu_nombre,   
		view_datos_geograficos_a.com_nombre,   
		view_datos_geograficos_a.com_codpost,   
		deudores.ctc_direccion,   
		--empleados.epl_rut,   
		--empleados.epl_nombre,   
		--empleados.epl_apepat,   
		--empleados.epl_apemat,   
		--view_rol.trb_nombre,   -- ELiminado
		--empleados.epl_telefono,   
		--empleados.epl_mail,   
		--rol_avedem.rad_fecdem,   -- ELiminado
		--rol_avedem.rad_cuodem,   -- ELiminado
		--rol_avedem.rad_mondem,   -- ELiminado
		--rol_avedem.rad_monucoudem,   -- ELiminado
		--rol_avedem.rad_fecpcoudem,   -- ELiminado
		--rol_avedem.rad_fecucoudem,   -- ELiminado
		--rol_avedem.rad_intdem,   -- ELiminado
		provcli.pcl_nombre,   
		provcli.pcl_apepat,   
		provcli.pcl_apemat,   
		--view_rol.eci_nombre,   
		EC.ECI_NOMBRE eci_nombre,
		--view_rol.tci_nombre,   
		TCI.TCI_NOMBRE tci_nombre,
		--view_rol.mji_nombre,   
		MJI.MJI_NOMBRE mji_nombre,
		provcli.pcl_replegal,   
		provcli.pcl_rutlegal,   
		CC.ccb_numero,   
		CC.ccb_fecvenc,   
		--rol_documentos.rdc_saldo,   -- ELiminado
		-- view_datos_geograficos_b.pai_nombre,  -- ELiminado  
		-- view_datos_geograficos_b.reg_nombre,  -- ELiminado
		-- view_datos_geograficos_b.ciu_nombre,  -- ELiminado
		-- view_datos_geograficos_b.com_nombre,  -- ELiminado
		-- empresa_sucursal.esu_direccion,   -- ELiminado
		-- empresa_sucursal.esu_telefono,-- ELiminado
		-- rad_monpcuodem, -- ELiminado
		--view_rol.ctc_nombre,
		deudores.CTC_NOMBRE,
		--view_rol.ctc_apepat,
		deudores.CTC_APEPAT,
		--view_rol.ctc_apemat,
		deudores.CTC_APEMAT,
		CC.CCB_IDCUENTA NumContrato,
	
		CC_EXT.CCB_PCLID,
		CC_EXT.CCB_CTCID,
		CC_EXT.CCB_CCBID,
		CC_EXT.NUM_RESOLUCION NumResolucion,
		CC_EXT.FEC_RESOLUCION FecResolucion,
		CC_EXT.RUT_REPRESENTANTE_1 RutRepresentante1,
		CC_EXT.NOM_REPRESENTANTE_1 NombRepresentante1,
		CC_EXT.RUT_REPRESENTANTE_2 RutRepresentante2,
		CC_EXT.NOM_REPRESENTANTE_2 NombRepresentante2,
		CC_EXT.RUT_REPRESENTANTE_3 RutRepresentante3,
		CC_EXT.NOM_REPRESENTANTE_3 NombRepresentante3,
		CC_EXT.FEC_CARGA
		
	FROM
		PANEL_DEMANDA_PREVISIONAL PDP
		INNER JOIN PANEL_DEMANDA_PREVISIONAL_DOCUMENTOS PDP_DOC on 
			PDP_DOC.PANEL_ID = PDP.PANEL_ID
		INNER JOIN cartera_clientes_cpbt_doc CC on
			CC.ccb_codemp = 1 AND  
			CC.ccb_pclid = PDP.PCLID AND  
			CC.ccb_ctcid = PDP.CTCID AND  
			CC.ccb_ccbid = PDP_DOC.CCBID
		LEFT JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO CC_EXT on
			CC.CCB_CODEMP = CC_EXT.CCB_CODEMP AND 
			CC.CCB_PCLID = CC_EXT.CCB_PCLID AND 
			CC.CCB_CTCID = CC_EXT.CCB_CTCID AND 
			CC.CCB_CCBID = CC_EXT.CCB_CCBID
		INNER JOIN provcli on
			provcli.pcl_codemp = 1 AND 
			provcli.pcl_pclid = PDP.PCLID
		INNER JOIN deudores on
			deudores.ctc_codemp = 1 AND 
			deudores.ctc_ctcid = CC.CCB_CTCID
		INNER JOIN view_datos_geograficos view_datos_geograficos_a on
			deudores.ctc_comid = view_datos_geograficos_a.com_comid
		INNER JOIN estados_cartera_idiomas EC on 
			EC.ECI_CODEMP = 1 AND 
			EC.ECI_ESTID = CC.CCB_ESTID
		INNER JOIN materia_judicial_idiomas MJI on 
			MJI.MJI_CODEMP = 1 AND 
			MJI.MJI_ESJID = 15 -- Siempre es: "COBRO DE COTIZACIONES LEY 16.744"
		INNER JOIN tipos_causa_idiomas TCI on 
			TCI.TCI_CODEMP = 1 AND 
			TCI.TCI_TCAID = 21 -- Siempre es: "Juicio Ejecutivo Previsional"

		-- Mientras no tenga tribunal asignado estos datos no se podrán obtener
		--LEFT JOIN entes_judicial on
		--	entes_judicial.etj_codemp = 1 
		--	AND entes_judicial.etj_etjid = CC.CCB_ESTIDJ
		--LEFT JOIN empleados on
		--	empleados.epl_codemp = 1 
		--	AND empleados.epl_emplid = entes_judicial.etj_emplid
		--LEFT JOIN empresa_sucursal on
		--	empresa_sucursal.esu_codemp = 1 AND  
		--	empresa_sucursal.esu_sucid = empleados.epl_sucid
		--LEFT JOIN view_datos_geograficos view_datos_geograficos_b on
		--	empresa_sucursal.esu_comid = view_datos_geograficos_b.com_comid

	WHERE 
		PDP.CODEMP = @codEmp AND
		PDP.PANEL_ID = @panelId
		-- AND entes_judicial.etj_abogado = 'S'  -- ELiminado