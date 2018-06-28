

CREATE Procedure [dbo].[Trae_Reporte_Rol_Demandas]
(
	@rol_codemp integer,
	@rol_rolid integer
) AS

	SELECT 
		view_rol.rol_numero,   
		view_rol.rol_total,   
		view_rol.pcl_rut,   
		view_rol.pcl_nomfant,   
		view_rol.ctc_numero,   
		view_rol.ctc_digito,   
		view_rol.ctc_nomfant,   
		view_datos_geograficos_a.pai_nombre,   
		view_datos_geograficos_a.reg_nombre,   
		view_datos_geograficos_a.ciu_nombre,   
		view_datos_geograficos_a.com_nombre,   
		view_datos_geograficos_a.com_codpost,   
		deudores.ctc_direccion,   
		empleados.epl_rut,   
		empleados.epl_nombre,   
		empleados.epl_apepat,   
		empleados.epl_apemat,   
		view_rol.trb_nombre,   
		empleados.epl_telefono,   
		empleados.epl_mail,   
		rol_avedem.rad_fecdem,   
		rol_avedem.rad_cuodem,   
		rol_avedem.rad_mondem,   
		rol_avedem.rad_monucoudem,   
		rol_avedem.rad_fecpcoudem,   
		rol_avedem.rad_fecucoudem,   
		rol_avedem.rad_intdem,   
		provcli.pcl_nombre,   
		provcli.pcl_apepat,   
		provcli.pcl_apemat,   
		view_rol.eci_nombre,   
		view_rol.tci_nombre,   
		view_rol.mji_nombre,   
		provcli.pcl_replegal,   
		provcli.pcl_rutlegal,   
		CC.ccb_numero,   
		CC.ccb_fecvenc,   
		rol_documentos.rdc_saldo,   
		view_datos_geograficos_b.pai_nombre,   
		view_datos_geograficos_b.reg_nombre,   
		view_datos_geograficos_b.ciu_nombre,   
		view_datos_geograficos_b.com_nombre,   
		empresa_sucursal.esu_direccion,   
		empresa_sucursal.esu_telefono,
		rad_monpcuodem,
		view_rol.ctc_nombre,
		view_rol.ctc_apepat,
		view_rol.ctc_apemat,
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
	FROM view_rol
		INNER JOIN entejud_rol on
			view_rol.rol_codemp = entejud_rol.ejr_codemp and 
			view_rol.rol_rolid  = entejud_rol.ejr_rolid
		LEFT JOIN rol_avedem on
			view_rol.rol_codemp = rol_avedem.rad_codemp and 
			view_rol.rol_rolid = rol_avedem.rad_rolid
		INNER JOIN provcli on
			view_rol.rol_codemp = provcli.pcl_codemp and 
			view_rol.rol_pclid = provcli.pcl_pclid
		LEFT JOIN rol_documentos on
			view_rol.rol_codemp = rol_documentos.rdc_codemp and 
			view_rol.rol_rolid = rol_documentos.rdc_rolid
		LEFT JOIN cartera_clientes_cpbt_doc CC on
			CC.ccb_codemp = rol_documentos.rdc_codemp and  
			CC.ccb_pclid = rol_documentos.rdc_pclid and  
			CC.ccb_ctcid = rol_documentos.rdc_ctcid and  
			CC.ccb_ccbid = rol_documentos.rdc_ccbid
		INNER JOIN deudores on
			view_rol.rol_codemp = deudores.ctc_codemp and 
			view_rol.rol_ctcid = deudores.ctc_ctcid
		INNER JOIN view_datos_geograficos view_datos_geograficos_a on
			deudores.ctc_comid = view_datos_geograficos_a.com_comid
		INNER JOIN entes_judicial on
			entes_judicial.etj_codemp = entejud_rol.ejr_codemp and  
			entes_judicial.etj_etjid = entejud_rol.ejr_etjid
		INNER JOIN empleados on
			empleados.epl_codemp = entes_judicial.etj_codemp and  
			empleados.epl_emplid = entes_judicial.etj_emplid
		INNER JOIN empresa_sucursal on
			empresa_sucursal.esu_codemp = empleados.epl_codemp and  
			empresa_sucursal.esu_sucid = empleados.epl_sucid
		INNER JOIN view_datos_geograficos view_datos_geograficos_b on
			empresa_sucursal.esu_comid = view_datos_geograficos_b.com_comid
	
		LEFT JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO CC_EXT on
			CC.CCB_CODEMP = CC_EXT.CCB_CODEMP AND 
			CC.CCB_PCLID = CC_EXT.CCB_PCLID AND 
			CC.CCB_CTCID = CC_EXT.CCB_CTCID AND 
			CC.CCB_CCBID = CC_EXT.CCB_CCBID

	WHERE 
		view_rol.rol_codemp = @rol_codemp AND  
		view_rol.rol_rolid = @rol_rolid AND  
		entes_judicial.etj_abogado = 'S'