CREATE Procedure [dbo].[_Trae_Reporte_Informe_PreJudicial_Ochenta](@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart integer, @idi_idid integer, @pcc_codid integer) as
if @pcc_codid = 0
begin
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecultgest,   
         cartera_clientes_documentos_cpbt_doc.eci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios,   
         isnull(cartera_clientes_documentos_cpbt_doc.ccb_numesp,'') ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,
         ccb_ctcid,
         ccb_pclid,
         ccb_ccbid,
		 (select sii from(
			 select top 1 
			 case  
			 WHEN TIPO_DOCUMENTO IN (14,44,49,11,53,35,36,13,10) THEN 'B/' + CAST(ANIO AS VARCHAR(4))
			 WHEN TIPO_DOCUMENTO IN (20,41,52,18,19,9,6,12,28,54) THEN 'F/' + CAST(ANIO AS VARCHAR(4))
			 ELSE '' END AS SII, anio   
			 from SII..TIMBRAJE with(nolock)
			 WHERE CTCID = CCB_CTCID 
			 order by 2 desc 
			 ) as t) Sii, 
		 (select count(*) terreno from VISITA_TERRENO with(nolock) where ID_ESTATUS = 5 and CTCID = CCB_CTCID) Terreno	 
    FROM cartera_clientes_documentos_cpbt_doc with(nolock),   
         view_datos_geograficos with(nolock),   
         idiomas with(nolock)  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos.com_comid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND   
         ( cartera_clientes_documentos_cpbt_doc.ccb_fecvenc <= getdate() ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt='V' and idi_idid = @idi_idid )   
         )
order by ctc_numero,ccb_fecvenc 
end
else
begin
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecultgest,   
         cartera_clientes_documentos_cpbt_doc.eci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios,   
         isnull(cartera_clientes_documentos_cpbt_doc.ccb_numesp,'') ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,
         ccb_ctcid,
         ccb_pclid,
         ccb_ccbid,
		 (select sii from(
			 select top 1 
			 case  
			 WHEN TIPO_DOCUMENTO IN (14,44,49,11,53,35,36,13,10) THEN 'B/' + CAST(ANIO AS VARCHAR(4))
			 WHEN TIPO_DOCUMENTO IN (20,41,52,18,19,9,6,12,28,54) THEN 'F/' + CAST(ANIO AS VARCHAR(4))
			 ELSE '' END AS SII, anio   
			 from SII..TIMBRAJE with(nolock)
			 WHERE CTCID = CCB_CTCID 
			 order by 2 desc 
			 ) as t) Sii, 
		 (select count(*) terreno from VISITA_TERRENO with(nolock) where ID_ESTATUS = 5 and CTCID = CCB_CTCID) Terreno	 
    FROM cartera_clientes_documentos_cpbt_doc with(nolock),   
         view_datos_geograficos with(nolock),   
         idiomas with(nolock)  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos.com_comid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND  
		 ( cartera_clientes_documentos_cpbt_doc.pcc_codid = @pcc_codid ) AND 
         ( cartera_clientes_documentos_cpbt_doc.ccb_fecvenc <= getdate() ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt='V' and idi_idid = @idi_idid )   
         )
order by ctc_numero,ccb_fecvenc 
end
