create procedure [dbo].[_Trae_Datos_Confeccion_Demanda] (@codemp integer, @idpanel integer) as 

select	 1 rol_numero,
		 ccb.CCB_MONTO rol_total, 
		 p.pcl_rut,   
         p.pcl_nomfant,   
         d.ctc_numero,   
         d.ctc_digito,   
         d.ctc_nomfant,   
         view_datos_geograficos_a.pai_nombre,   
         view_datos_geograficos_a.reg_nombre,   
         view_datos_geograficos_a.ciu_nombre,   
         view_datos_geograficos_a.com_nombre,   
         view_datos_geograficos_a.com_codpost,   
         d.ctc_direccion,   
         empleados.epl_rut,   
         empleados.epl_nombre,   
         empleados.epl_apepat,   
         empleados.epl_apemat,   
         t.trb_nombre,   
         empleados.epl_telefono,   
         empleados.epl_mail,   
         pdm.fecdem,   
         pdm.cuodem,   
         pdm.mondem,   
         pdm.monucoudem,   
         pdm.fecpcoudem,   
         pdm.fecucoudem,   
         pdm.intdem,   
         p.pcl_nombre,   
         p.pcl_apepat,   
         p.pcl_apemat,   
         estados_cartera_idiomas.eci_nombre,   
         tipos_causa_idiomas.tci_nombre,   
         materia_judicial_idiomas.mji_nombre,   
         p.pcl_replegal,   
         p.pcl_rutlegal,   
         ccb.ccb_numero,   
         ccb.ccb_fecvenc,   
         ccb.CCB_SALDO rdc_saldo,   
         view_datos_geograficos_b.pai_nombre,   
         view_datos_geograficos_b.reg_nombre,   
         view_datos_geograficos_b.ciu_nombre,   
         view_datos_geograficos_b.com_nombre,   
         empresa_sucursal.esu_direccion,   
         empresa_sucursal.esu_telefono, 
         pdm.monpcuodem,
         d.ctc_nombre,
         d.ctc_apepat,
         d.ctc_apemat 

from PANEL_DEMANDA_MASIVA PDM 
INNER JOIN TRIBUNALES T 
ON T.TRB_TRBID = PDM.TRBID 
AND T.TRB_CODEMP = PDM.CODEMP 
INNER JOIN PROVCLI P 
ON P.PCL_CODEMP = PDM.CODEMP 
AND P.PCL_PCLID = PDM.PCLID 
INNER JOIN DEUDORES D 
ON D.CTC_CODEMP = PDM.CODEMP 
AND D.CTC_CTCID = PDM.CTCID 
INNER JOIN PANEL_DEMANDA_MASIVA_DOCUMENTOS PDMD 
ON PDMD.CODEMP = PDM.CODEMP 
AND PDMD.ID_PANEL_MASIVO = PDM.ID_PANEL_MASIVO 
INNER JOIN CARTERA_CLIENTES_CPBT_DOC CCB 
	ON PDMD.CODEMP = CCB.CCB_CODEMP 
	AND PDMD.PCLID = CCB.CCB_PCLID 
	AND PDMD.CTCID = CCB.CCB_CTCID 
	AND PDMD.CCBID = CCB.CCB_CCBID 
LEFT OUTER JOIN materia_judicial_idiomas	
ON materia_judicial_idiomas.MJI_CODEMP = PDM.CODEMP,
empresa_sucursal,  
empleados,  
view_datos_geograficos view_datos_geograficos_a,   
tipos_causa_idiomas,   
estados_cartera_idiomas,   
view_datos_geograficos view_datos_geograficos_b  
where 
( d.ctc_comid = view_datos_geograficos_a.com_comid ) and  
( empresa_sucursal.esu_codemp = empleados.epl_codemp ) and  
( empresa_sucursal.esu_sucid = empleados.epl_sucid ) and  
( empresa_sucursal.esu_comid = view_datos_geograficos_b.com_comid ) and  
( pdm.codemp = tipos_causa_idiomas.tci_codemp ) and  
( tipos_causa_idiomas.tci_tcaid = 4 ) and  
( pdm.codemp = estados_cartera_idiomas.eci_codemp ) and  
( estados_cartera_idiomas.eci_estid = 99 ) and  
( empleados.EPL_EMPLID = 3 ) and  
( materia_judicial_idiomas.mji_codemp = pdm.CODEMP ) and 
materia_judicial_idiomas.MJI_ESJID = 2 AND
PDM.ID_PANEL_MASIVO = @idpanel AND 
PDM.CODEMP = @codemp
