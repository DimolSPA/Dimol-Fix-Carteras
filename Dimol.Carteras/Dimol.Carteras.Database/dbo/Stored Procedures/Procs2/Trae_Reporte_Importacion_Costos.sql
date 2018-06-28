

Create Procedure Trae_Reporte_Importacion_Costos(@imp_codemp integer, @imp_impid integer, @idi_idid integer) as
SELECT importacion.imp_numero,   
         importacion.imp_fecing,   
         insumos.ins_nombre,   
         detalle_comprobantes.dcc_cantidad,   
         detalle_comprobantes.dcc_precio,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         detalle_comprobantes.dcc_total,   
         tipos_costo_idiomas.tci_nombre,   
         importacion.imp_impid,   
         view_cabecera_comprobantes.pcl_nomfant,   
         insumos.ins_codigo,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         insumos.ins_cubicaje,   
         insumos.ins_pack,   
         insumos.ins_packint,   
         importacion.imp_nombre,   
         view_cabecera_comprobantes.cbc_tpcid,   
         view_tipos_cpbtdoc_clasificacion.clb_costos,  
         view_cabecera_comprobantes.tci_nombre TipCpbt,
         tco_agrupa
    FROM importacion,   
         importacion_cpbtdoc,   
         detalle_comprobantes,   
         tipos_costo,   
         tipos_costo_idiomas,   
         insumos,   
         view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion  
   WHERE ( importacion_cpbtdoc.ipc_codemp = importacion.imp_codemp ) and  
         ( importacion_cpbtdoc.ipc_impid = importacion.imp_impid ) and  
         ( tipos_costo.tco_codemp = importacion_cpbtdoc.ipc_codemp ) and  
         ( tipos_costo.tco_tcoid = importacion_cpbtdoc.ipc_tcoid ) and  
         ( tipos_costo_idiomas.tci_codemp = tipos_costo.tco_codemp ) and  
         ( tipos_costo_idiomas.tci_tcoid = tipos_costo.tco_tcoid ) and  
         ( insumos.ins_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( insumos.ins_insid = detalle_comprobantes.dcc_insid ) and  
         ( importacion_cpbtdoc.ipc_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( importacion_cpbtdoc.ipc_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( importacion_cpbtdoc.ipc_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( importacion_cpbtdoc.ipc_numero = view_cabecera_comprobantes.cbc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( ( importacion.imp_codemp = @imp_codemp ) AND  
         ( importacion.imp_impid = @imp_impid ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','B','F' ) ) AND  
         ( tipos_costo_idiomas.tci_idid = @idi_idid ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_costos = 'S' )   
         )
