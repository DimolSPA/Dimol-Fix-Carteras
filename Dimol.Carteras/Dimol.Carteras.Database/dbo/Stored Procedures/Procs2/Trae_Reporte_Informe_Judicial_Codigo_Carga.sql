

CREATE procedure [dbo].[Trae_Reporte_Informe_Judicial_Codigo_Carga](@rol_codemp integer, @rol_pclid integer, @eci_idid integer, @pcc_codid integer) as
  SELECT distinct view_rol_datos.rol_pclid,   
         view_rol_datos.pcl_rut,   
         view_rol_datos.pcl_nomfant,   
         view_rol_datos.ctc_rut,   
         view_rol_datos.ctc_nomfant,   
         view_rol_datos.trb_nombre,   
         view_rol_datos.tci_nombre,   
         view_rol_datos.rol_rolid,   
         view_rol_datos.rol_numero,   
         view_rol_datos.rol_fecrol,   
         substring(rle_comentario, 1, 1000) as rle_comentario,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         view_rol_datos.trb_direccion,   
         materia_judicial_idiomas.mji_nombre,   
         estados_cartera_idiomas.eci_nombre, 
         estados_cartera_idiomas.eci_nombre ect_nombre,   
         view_rol_datos.tipdoc,   
         view_rol_datos.ccb_numero,   
         view_rol_datos.ccb_fecdoc,   
         view_rol_datos.ccb_fecvenc,   
         view_rol_datos.ccb_asignado,   
         view_rol_datos.ccb_monto,   
         view_rol_datos.ccb_saldo,   
         view_rol_datos.ccb_gastjud,   
         view_rol_datos.ccb_gastotro,   
         view_rol_datos.ccb_intereses,   
         view_rol_datos.ccb_honorarios,   
         view_rol_datos.bco_nombre,   
         view_rol_datos.ccb_numesp,   
         view_rol_datos.ccb_numagrupa,   
         view_rol_datos.sbc_rut,   
         view_rol_datos.sbc_nombre,   
         view_rol_datos.mon_nombre,   
         view_rol_datos.pcc_nombre,   
         view_rol_datos.mci_nombre,   
         view_rol_datos.rol_fecjud,   
         rol_estados.rle_fecha,
         ctc_numero, 
	    ctc_digito,
         ccb_codmon 	  
    FROM view_rol_datos,   
         rol_estados,   
         materia_judicial_idiomas,   
         estados_cartera_idiomas,   
         tribunales,   
         view_datos_geograficos,
         materia_estados  
   WHERE ( view_rol_datos.rol_codemp = rol_estados.rle_codemp ) and  
         ( view_rol_datos.rol_rolid = rol_estados.rle_rolid ) and  
         ( view_rol_datos.rol_fecjud = rol_estados.rle_fecjud ) and  
         
           ( view_rol_datos.rol_esjid = rol_estados.rle_esjid ) and  
         ( view_rol_datos.rol_estid = rol_estados.rle_estid ) and  
         
               ( view_rol_datos.rol_esjid = mej_esjid ) and  
         ( view_rol_datos.rol_estid = mej_estid ) and  


         
         ( rol_estados.rle_codemp = materia_judicial_idiomas.mji_codemp ) and  
         ( rol_estados.rle_esjid = materia_judicial_idiomas.mji_esjid ) and  
         ( rol_estados.rle_codemp = estados_cartera_idiomas.eci_codemp ) and  
         ( rol_estados.rle_estid = estados_cartera_idiomas.eci_estid ) and  
         ( view_rol_datos.rol_codemp = tribunales.trb_codemp ) and  
         ( view_rol_datos.rol_trbid = tribunales.trb_trbid ) and  
         ( tribunales.trb_comid = view_datos_geograficos.com_comid ) and  
         ( view_rol_datos.eci_idid = estados_cartera_idiomas.eci_idid ) and  
         ( view_rol_datos.eci_idid = materia_judicial_idiomas.mji_idid ) and  
         ( ( view_rol_datos.rol_codemp = @rol_codemp ) AND  
         ( view_rol_datos.rol_pclid = @rol_pclid ) AND  
         ( view_rol_datos.ccb_estcpbt = 'J' ) AND  
         ( view_rol_datos.eci_idid = @eci_idid and pcc_codid = @pcc_codid )   
         )   

union

  SELECT cartera_clientes_documentos_cpbt_doc.ccb_pclid,   
         cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         ctc_rut,
         ctc_nomfant,
         '',
         '',
         0,
         '',
         getdate(),
         '',
          view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         '',
         '', 
         eci_nombre,
         eci_nombre ect_nombre,
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc, 
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,     
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios, 
         bco_nombre,  
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.ccb_numagrupa,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,  
         mci_nombre,   
         getdate(),
         getdate(),
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,
         ccb_codmon  
    FROM cartera_clientes_documentos_cpbt_doc, view_datos_geograficos  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @rol_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @rol_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt = 'J' ) AND  
         ( tci_idid = @eci_idid and pcc_codid = @pcc_codid )   and
         ( ctc_comid = com_comid ) AND
         ( convert(varchar, ccb_pclid) + '_' + convert(varchar, ccb_ctcid) + '_' + convert(varchar, ccb_ccbid) not in (  SELECT convert(varchar, rdc_pclid) + '_' + convert(varchar, rdc_ctcid) + '_' + convert(varchar, rdc_ccbid)  
                                                                                                                           FROM rol_documentos  
                                                                                                                          WHERE ( rol_documentos.rdc_codemp =@rol_codemp ) AND  
                                                                                                                                ( rol_documentos.rdc_pclid = @rol_pclid )   
                                                                                                                                 )) 
order by ctc_numero, ccb_fecvenc
