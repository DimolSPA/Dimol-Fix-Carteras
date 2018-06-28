
/*==============================================================*/
/* View: VIEW_CABECERA_COMPROBANTES                             */
/*==============================================================*/
create view VIEW_CABECERA_COMPROBANTES as
 SELECT cabacera_comprobantes.cbc_codemp,   
         cabacera_comprobantes.cbc_sucid,   
         cabacera_comprobantes.cbc_tpcid,   
         cabacera_comprobantes.cbc_numero,   
         cabacera_comprobantes.cbc_numprovcli,   
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
         provcli.pcl_rut,   
         provcli.pcl_nombre,   
         provcli.pcl_apepat,   
         provcli.pcl_apemat,   
         provcli.pcl_nomfant,   
         formas_pago_idiomas.fpi_nombre,   
         tipos_cpbtdoc_idiomas.tci_nombre,   
         idiomas.idi_nombre,   
         monedas.mon_nombre,
         idi_idid,
         cbt_tntid,
         cbt_tgdid,
          cbt_ttlid,
         cbc_exento,
         cbc_pcsid, 
         pcl_girid,
         cabacera_comprobantes.cbc_feccont,
         tpc_codigo,
         cabacera_comprobantes.cbc_fecoc
    FROM cabacera_comprobantes,   
         provcli,   
         tipos_cpbtdoc_idiomas,   
         idiomas,   
         monedas,   
         formas_pago_idiomas,
         tipos_cpbtdoc  
   WHERE ( provcli.pcl_codemp = cabacera_comprobantes.cbc_codemp ) and  
         ( provcli.pcl_pclid = cabacera_comprobantes.cbc_pclid ) and  
         ( cabacera_comprobantes.cbc_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( cabacera_comprobantes.cbc_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid ) and  

         (  tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid) and  


         ( tipos_cpbtdoc_idiomas.tci_idid = idiomas.idi_idid ) and  
         ( monedas.mon_codemp = cabacera_comprobantes.cbc_codemp ) and  
         ( monedas.mon_codmon = cabacera_comprobantes.cbc_codmon ) and  
         ( cabacera_comprobantes.cbc_codemp = formas_pago_idiomas.fpi_codemp ) and  
         ( cabacera_comprobantes.cbc_frpid = formas_pago_idiomas.fpi_frpid ) and  
         ( formas_pago_idiomas.fpi_idid = idiomas.idi_idid )
