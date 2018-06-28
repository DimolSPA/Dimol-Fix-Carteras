

Create Procedure Insertar_cabacera_comprobantes_ReImputacion(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric(15), @cbc_numeroNew numeric(15), @ast_numeroNew numeric(15), @ase_usrid integer, @ast_glosa varchar(1000)) as
insert into cabacera_comprobantes
  SELECT cabacera_comprobantes.cbc_codemp,   
         cabacera_comprobantes.cbc_sucid,   
         cabacera_comprobantes.cbc_tpcid,   
         @cbc_numeroNew,   
         @cbc_numeroNew,   
         cabacera_comprobantes.cbc_pclid,   
         cabacera_comprobantes.cbc_fecemi,   
         cabacera_comprobantes.cbc_feccpbt,   
         cabacera_comprobantes.cbc_fecvenc,   
         cabacera_comprobantes.cbc_fecent,   
         cabacera_comprobantes.cbc_codmon,   
         cabacera_comprobantes.cbc_tipcambio,   
         cabacera_comprobantes.cbc_frpid,   
         cabacera_comprobantes.cbc_anio,   
         cabacera_comprobantes.cbc_mes,   
         cabacera_comprobantes.cbc_glosa,   
         cabacera_comprobantes.cbc_porcdesc,   
         cabacera_comprobantes.cbc_neto,   
         cabacera_comprobantes.cbc_impuestos,   
         cabacera_comprobantes.cbc_retenido,   
         cabacera_comprobantes.cbc_descuentos,   
         cabacera_comprobantes.cbc_final,   
         cabacera_comprobantes.cbc_saldo,   
         cabacera_comprobantes.cbc_ordcomp,   
         cabacera_comprobantes.cbt_gastjud,   
         cabacera_comprobantes.cbt_vdeid,   
         cabacera_comprobantes.cbt_estado,   
         cabacera_comprobantes.cbt_tntid,   
         cabacera_comprobantes.cbt_tgdid,   
         cabacera_comprobantes.cbt_ttlid,   
         cabacera_comprobantes.cbc_exento,   
         cabacera_comprobantes.cbc_pcsid,   
         cabacera_comprobantes.cbc_feccont,   
         cabacera_comprobantes.cbc_fecoc  
    FROM cabacera_comprobantes  
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )   


insert into detalle_comprobantes
  SELECT detalle_comprobantes.dcc_codemp,   
         detalle_comprobantes.dcc_sucid,   
         detalle_comprobantes.dcc_tpcid,   
         @cbc_numeroNew,      
         detalle_comprobantes.dcc_item,   
         detalle_comprobantes.dcc_insid,   
         detalle_comprobantes.dcc_prodid,   
         detalle_comprobantes.dcc_pclid,   
         detalle_comprobantes.dcc_ctcid,   
         detalle_comprobantes.dcc_ccbid,   
         detalle_comprobantes.dcc_prereal,   
         detalle_comprobantes.dcc_precio,   
         detalle_comprobantes.dcc_cantidad,   
         detalle_comprobantes.dcc_saldo,   
         detalle_comprobantes.dcc_neto,   
         detalle_comprobantes.dcc_impuesto,   
         detalle_comprobantes.dcc_retenido,   
         detalle_comprobantes.dcc_total,   
         detalle_comprobantes.dcc_interes,   
         detalle_comprobantes.dcc_honorario,   
         detalle_comprobantes.dcc_gastpre,   
         detalle_comprobantes.dcc_gastjud,   
         detalle_comprobantes.dcc_porcfact,   
         detalle_comprobantes.dcc_porchon,   
         detalle_comprobantes.dcc_bodid,   
         detalle_comprobantes.dcc_bdsid,   
         detalle_comprobantes.dcc_posicion,   
         detalle_comprobantes.dcc_tpcidpad,   
         detalle_comprobantes.dcc_numeropad,   
         detalle_comprobantes.dcc_itempad,   
         detalle_comprobantes.dcc_bodiddes,   
         detalle_comprobantes.dcc_bdsiddes,   
         detalle_comprobantes.dcc_posiciondes,   
         detalle_comprobantes.dcc_numserie,   
         detalle_comprobantes.dcc_numserieprov,   
         detalle_comprobantes.dcc_cantebj,   
         detalle_comprobantes.dcc_ltpid,   
         detalle_comprobantes.dcc_bscid,   
         detalle_comprobantes.dcc_bsciddes,   
         detalle_comprobantes.dcc_anio,   
         detalle_comprobantes.dcc_numapl,   
         detalle_comprobantes.dcc_itemapl,   
         detalle_comprobantes.dcc_valrem,   
         detalle_comprobantes.dcc_comentario,   
         detalle_comprobantes.dcc_subitem,
         detalle_comprobantes.dcc_exento  
    FROM detalle_comprobantes  
   WHERE ( detalle_comprobantes.dcc_codemp = @cbc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @cbc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @cbc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @cbc_numero )   
           

insert into despachos_documentos
  SELECT despachos_documentos.dcd_codemp,   
         despachos_documentos.dcd_sucid,   
         despachos_documentos.dcd_dpcid,   
         despachos_documentos.dcd_tpcid,   
         @cbc_numeroNew,    
         despachos_documentos.dcd_edpid,   
         despachos_documentos.dcd_fecdesp,   
         despachos_documentos.dcd_fecent,   
         despachos_documentos.dcd_recibido  
    FROM despachos_documentos  
   WHERE ( despachos_documentos.dcd_codemp = @cbc_codemp ) AND  
         ( despachos_documentos.dcd_sucid = @cbc_sucid ) AND  
         ( despachos_documentos.dcd_tpcid = @cbc_tpcid ) AND  
         ( despachos_documentos.dcd_numero = @cbc_numero )   
           

insert into cabacera_comprobantes_motivos_castigo
  SELECT cabacera_comprobantes_motivos_castigo.cbm_codemp,   
         cabacera_comprobantes_motivos_castigo.cbm_sucid,   
         cabacera_comprobantes_motivos_castigo.cbm_tpcid,   
         @cbc_numeroNew,    
         cabacera_comprobantes_motivos_castigo.cbm_tmcid  
    FROM cabacera_comprobantes_motivos_castigo  
   WHERE ( cabacera_comprobantes_motivos_castigo.cbm_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_numero = @cbc_numero )   


           
insert into cabacera_comprobantes_estados
  SELECT cabacera_comprobantes_estados.cbe_codemp,   
         cabacera_comprobantes_estados.cbe_sucid,   
         cabacera_comprobantes_estados.cbe_tpcid,   
         @cbc_numeroNew,      
         cabacera_comprobantes_estados.cbe_estado,   
         getdate(),   
         cabacera_comprobantes_estados.cbe_usrid,   
         cabacera_comprobantes_estados.cbe_ippc,   
         cabacera_comprobantes_estados.cbe_ipred,   
         cabacera_comprobantes_estados.cbe_comentario  
    FROM cabacera_comprobantes_estados  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbc_numero and cbe_estado not in ('A','X') )   
           

insert into cabacera_comprobantes_envio_doc
  SELECT cabacera_comprobantes_envio_doc.cbv_codemp,   
         cabacera_comprobantes_envio_doc.cbv_sucid,   
         cabacera_comprobantes_envio_doc.cbv_tpcid,   
         @cbc_numeroNew,     
         cabacera_comprobantes_envio_doc.cbv_pclid,   
         cabacera_comprobantes_envio_doc.cbv_ctcid,   
         cabacera_comprobantes_envio_doc.cbv_tdeid  
    FROM cabacera_comprobantes_envio_doc  
   WHERE ( cabacera_comprobantes_envio_doc.cbv_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_envio_doc.cbv_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_envio_doc.cbv_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_envio_doc.cbv_numero = @cbc_numero )   
           

insert asientos_contables
  SELECT asientos_contables.ast_codemp,   
         asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         @ast_numeroNew,   
         @ast_numeroNew,   
         asientos_contables.ast_mes,   
         getdate(),   
         asientos_contables.ast_fecperiodo,   
         asientos_contables.ast_estado,   
         @ast_glosa,   
         asientos_contables.ast_tot_debe,   
         asientos_contables.ast_tot_haber  
    FROM asientos_contables_cpbtdoc_apl,   
         asientos_contables  
   WHERE ( asientos_contables.ast_codemp = asientos_contables_cpbtdoc_apl.ada_codemp ) and  
         ( asientos_contables.ast_anio = asientos_contables_cpbtdoc_apl.ada_anio ) and  
         ( asientos_contables.ast_tipo = asientos_contables_cpbtdoc_apl.ada_tipo ) and  
         ( asientos_contables.ast_numero = asientos_contables_cpbtdoc_apl.ada_numero ) and  
         ( ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = @cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero )   
         )    



insert into asientos_contables_detalle
SELECT asientos_contables_detalle.acd_codemp,   
         asientos_contables_detalle.acd_anio,   
         asientos_contables_detalle.acd_tipo,   
         @ast_numeroNew,     
         asientos_contables_detalle.acd_item,   
         asientos_contables_detalle.acd_pctid,   
         asientos_contables_detalle.acd_ccsid,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         asientos_contables_detalle.acd_glosa  
    FROM asientos_contables_cpbtdoc_apl,   
         asientos_contables_detalle  
   WHERE ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables_detalle.acd_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables_detalle.acd_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables_detalle.acd_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables_detalle.acd_numero ) and  
         ( ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = @cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero )   
         )    



insert into asientos_contables_estados
  SELECT asientos_contables_estados.ase_codemp,   
         asientos_contables_estados.ase_anio,   
         asientos_contables_estados.ase_tipo,   
         @ast_numeroNew,     
         asientos_contables_estados.ase_estado,   
         asientos_contables_estados.ase_fecha,   
         asientos_contables_estados.ase_comentario,   
         asientos_contables_estados.ase_usrid  
    FROM asientos_contables_cpbtdoc_apl,   
         asientos_contables,   
         asientos_contables_estados  
   WHERE ( asientos_contables.ast_codemp = asientos_contables_cpbtdoc_apl.ada_codemp ) and  
         ( asientos_contables.ast_anio = asientos_contables_cpbtdoc_apl.ada_anio ) and  
         ( asientos_contables.ast_tipo = asientos_contables_cpbtdoc_apl.ada_tipo ) and  
         ( asientos_contables.ast_numero = asientos_contables_cpbtdoc_apl.ada_numero ) and  
         ( asientos_contables_estados.ase_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_estados.ase_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_estados.ase_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_estados.ase_numero = asientos_contables.ast_numero ) and  
         ( ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = @cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero and ase_estado not in ('V','X') )   
         )    


insert into asientos_contables_cpbtdoc_apl
  SELECT asientos_contables_cpbtdoc_apl.ada_codemp,   
         asientos_contables_cpbtdoc_apl.ada_anio,   
         asientos_contables_cpbtdoc_apl.ada_tipo,   
        @ast_numeroNew,     
         asientos_contables_cpbtdoc_apl.ada_item,   
         asientos_contables_cpbtdoc_apl.ada_sucid,   
         asientos_contables_cpbtdoc_apl.ada_tpcid,   
         @cbc_numeroNew,   
         asientos_contables_cpbtdoc_apl.ada_aniodoc,   
         asientos_contables_cpbtdoc_apl.ada_numdoc,   
         asientos_contables_cpbtdoc_apl.ada_anioapl,   
         asientos_contables_cpbtdoc_apl.ada_numapl  
    FROM asientos_contables_cpbtdoc_apl  
   WHERE ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = @cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero )   


insert into asientos_contables_estados
  SELECT top 1 asientos_contables_estados.ase_codemp,   
         asientos_contables_estados.ase_anio,   
         asientos_contables_estados.ase_tipo,   
         @ast_numeroNew,     
         'V',   
         getdate(),   
         asientos_contables_estados.ase_comentario,   
         @ase_usrid  
    FROM asientos_contables_cpbtdoc_apl,   
         asientos_contables,   
         asientos_contables_estados  
   WHERE ( asientos_contables.ast_codemp = asientos_contables_cpbtdoc_apl.ada_codemp ) and  
         ( asientos_contables.ast_anio = asientos_contables_cpbtdoc_apl.ada_anio ) and  
         ( asientos_contables.ast_tipo = asientos_contables_cpbtdoc_apl.ada_tipo ) and  
         ( asientos_contables.ast_numero = asientos_contables_cpbtdoc_apl.ada_numero ) and  
         ( asientos_contables_estados.ase_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_estados.ase_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_estados.ase_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_estados.ase_numero = asientos_contables.ast_numero ) and  
         ( ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = @cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero )   
         )    


insert into asientos_contables_estados
  SELECT top 1 asientos_contables_estados.ase_codemp,   
         asientos_contables_estados.ase_anio,   
         asientos_contables_estados.ase_tipo,   
         ast_numero,     
         'N',   
         getdate(),   
         asientos_contables_estados.ase_comentario,   
         @ase_usrid  
    FROM asientos_contables_cpbtdoc_apl,   
         asientos_contables,   
         asientos_contables_estados  
   WHERE ( asientos_contables.ast_codemp = asientos_contables_cpbtdoc_apl.ada_codemp ) and  
         ( asientos_contables.ast_anio = asientos_contables_cpbtdoc_apl.ada_anio ) and  
         ( asientos_contables.ast_tipo = asientos_contables_cpbtdoc_apl.ada_tipo ) and  
         ( asientos_contables.ast_numero = asientos_contables_cpbtdoc_apl.ada_numero ) and  
         ( asientos_contables_estados.ase_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_estados.ase_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_estados.ase_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_estados.ase_numero = asientos_contables.ast_numero ) and  
         ( ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid =@cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero )   
         )    




insert into cabacera_comprobantes_estados
  SELECT top 1 cabacera_comprobantes_estados.cbe_codemp,   
         cabacera_comprobantes_estados.cbe_sucid,   
         cabacera_comprobantes_estados.cbe_tpcid,   
         @cbc_numeroNew,      
         'A',   
         getdate(),   
         @ase_usrid,   
         cabacera_comprobantes_estados.cbe_ippc,   
         cabacera_comprobantes_estados.cbe_ipred,   
         cabacera_comprobantes_estados.cbe_comentario  
    FROM cabacera_comprobantes_estados  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbc_tpcid  ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbc_numero )   


insert into cabacera_comprobantes_estados
  SELECT top 1 cabacera_comprobantes_estados.cbe_codemp,   
         cabacera_comprobantes_estados.cbe_sucid,   
         cabacera_comprobantes_estados.cbe_tpcid,   
         @cbc_numero,      
         'X',   
         getdate(),   
         @ase_usrid,   
         cabacera_comprobantes_estados.cbe_ippc,   
         cabacera_comprobantes_estados.cbe_ipred,   
         cabacera_comprobantes_estados.cbe_comentario  
    FROM cabacera_comprobantes_estados  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbc_numero )   


delete from detalle_comprobantes
  FROM detalle_comprobantes  
   WHERE ( detalle_comprobantes.dcc_codemp = @cbc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @cbc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @cbc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @cbc_numero )   


  UPDATE cabacera_comprobantes  
     SET cbt_estado = 'X',   
         cbc_saldo = 0  
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )   


UPDATE asientos_contables  
     SET ast_estado = 'X',   
         ast_tot_debe = 0,
        ast_tot_haber = 0    
  FROM asientos_contables_cpbtdoc_apl,   
         asientos_contables  
   WHERE ( asientos_contables.ast_codemp = asientos_contables_cpbtdoc_apl.ada_codemp ) and  
         ( asientos_contables.ast_anio = asientos_contables_cpbtdoc_apl.ada_anio ) and  
         ( asientos_contables.ast_tipo = asientos_contables_cpbtdoc_apl.ada_tipo ) and  
         ( asientos_contables.ast_numero = asientos_contables_cpbtdoc_apl.ada_numero ) and  
         ( ( asientos_contables_cpbtdoc_apl.ada_codemp = @cbc_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @cbc_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = @cbc_tpcid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = @cbc_numero )   
         )
