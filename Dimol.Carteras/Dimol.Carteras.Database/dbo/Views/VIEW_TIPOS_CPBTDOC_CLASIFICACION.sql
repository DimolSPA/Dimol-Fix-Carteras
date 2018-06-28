
/*==============================================================*/
/* View: VIEW_TIPOS_CPBTDOC_CLASIFICACION                       */
/*==============================================================*/
create view VIEW_TIPOS_CPBTDOC_CLASIFICACION as
SELECT tipos_cpbtdoc.tpc_codemp,   
         tipos_cpbtdoc.tpc_tpcid,   
         tipos_cpbtdoc.tpc_nombre,   
         tipos_cpbtdoc.tpc_talonario,   
         tipos_cpbtdoc.tpc_ultnum,   
         tipos_cpbtdoc.tpc_lineas,   
         clasificacion_cpbtdoc.clb_tipcpbtdoc,   
         clasificacion_cpbtdoc.clb_tipprod,   
         clasificacion_cpbtdoc.clb_costos,   
         clasificacion_cpbtdoc.clb_selcpbt,   
         clasificacion_cpbtdoc.clb_cartcli,   
         clasificacion_cpbtdoc.clb_contable,   
         clasificacion_cpbtdoc.clb_selapl,   
         clasificacion_cpbtdoc.clb_aplica,   
         clasificacion_cpbtdoc.clb_cptoctbl,   
         clasificacion_cpbtdoc.clb_findeuda,   
         clasificacion_cpbtdoc.clb_cancela,   
         clasificacion_cpbtdoc.clb_libcompra,   
         clasificacion_cpbtdoc.clb_cambiodoc,   
         clasificacion_cpbtdoc.clb_remesa,
         clb_clbid,
         tpc_tipdig,
         tpc_codigo  
    FROM tipos_cpbtdoc,   
         clasificacion_cpbtdoc  
   WHERE ( clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid )
