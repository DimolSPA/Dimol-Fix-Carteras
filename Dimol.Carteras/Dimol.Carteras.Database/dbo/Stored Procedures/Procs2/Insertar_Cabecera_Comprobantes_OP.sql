

Create Procedure Insertar_Cabecera_Comprobantes_OP(@cbo_codemp integer, @cbo_sucid integer, @cbo_tpcid integer, @cbo_numero integer) as
  DELETE FROM cabacera_comprobantes_op  
   WHERE ( cabacera_comprobantes_op.cbo_codemp = @cbo_codemp ) AND  
         ( cabacera_comprobantes_op.cbo_sucid = @cbo_sucid ) AND  
         ( cabacera_comprobantes_op.cbo_tpcid = @cbo_tpcid ) AND  
         ( cabacera_comprobantes_op.cbo_numero = @cbo_numero )   
           

insert into cabacera_comprobantes_op
  SELECT view_cabecera_comprobantes.cbc_codemp,   
         view_cabecera_comprobantes.cbc_sucid,   
         view_cabecera_comprobantes.cbc_tpcid,   
         view_cabecera_comprobantes.cbc_numero,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_pclid,   
         view_cabecera_comprobantes.cbc_fecemi,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         view_cabecera_comprobantes.cbc_fecent,   
         view_cabecera_comprobantes.cbc_codmon,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         view_cabecera_comprobantes.cbc_frpid,   
         view_cabecera_comprobantes.cbc_anio,   
         view_cabecera_comprobantes.cbc_mes,   
         view_cabecera_comprobantes.cbc_glosa,   
         view_cabecera_comprobantes.cbc_porcdesc,   
         view_cabecera_comprobantes.cbc_neto,   
         view_cabecera_comprobantes.cbc_impuestos,   
         view_cabecera_comprobantes.cbc_retenido,   
         view_cabecera_comprobantes.cbc_descuentos,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_saldo,   
         view_cabecera_comprobantes.cbc_ordcomp,   
         view_cabecera_comprobantes.cbt_gastjud,   
         view_cabecera_comprobantes.cbt_vdeid,   
         view_cabecera_comprobantes.cbt_estado,   
         view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nombre,   
         view_cabecera_comprobantes.pcl_apepat,   
         view_cabecera_comprobantes.pcl_apemat,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.fpi_nombre,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.idi_nombre,   
         view_cabecera_comprobantes.mon_nombre,   
         view_cabecera_comprobantes.idi_idid,   
         view_cabecera_comprobantes.cbt_tntid,   
         view_cabecera_comprobantes.cbt_tgdid,   
         view_cabecera_comprobantes.cbt_ttlid,   
         view_cabecera_comprobantes.cbc_exento,   
         view_cabecera_comprobantes.cbc_pcsid,   
         view_cabecera_comprobantes.pcl_girid,
         view_cabecera_comprobantes.cbc_feccont,
         view_cabecera_comprobantes.cbc_fecoc  
    FROM view_cabecera_comprobantes  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = @cbo_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_sucid = @cbo_sucid ) AND  
         ( view_cabecera_comprobantes.cbc_tpcid = @cbo_tpcid ) AND  
         ( view_cabecera_comprobantes.cbc_numero = @cbo_numero )
