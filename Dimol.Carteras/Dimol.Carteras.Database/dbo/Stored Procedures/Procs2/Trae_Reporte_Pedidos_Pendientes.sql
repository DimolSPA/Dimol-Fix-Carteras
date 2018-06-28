

Create Procedure Trae_Reporte_Pedidos_Pendientes(@cbc_codemp integer, @idi_idid integer, @desde datetime, @hasta datetime) as
 SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         view_cabecera_comprobantes.fpi_nombre,   
         view_cabecera_comprobantes.cbc_ordcomp,   
         view_cabecera_comprobantes.mon_nombre,   
         productos.pdt_codfisico,   
         productos.pdt_nombre,   
         detalle_comprobantes.dcc_precio,   
         detalle_comprobantes.dcc_cantidad,   
         detalle_comprobantes.dcc_saldo,   
         productos_stock.pst_stock_total,   
         view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc  
    FROM {oj productos_stock RIGHT OUTER JOIN productos ON productos_stock.pst_codemp = productos.pdt_codemp AND productos_stock.pst_prodid = productos.pdt_prodid},   
         view_cabecera_comprobantes,   
         detalle_comprobantes,   
         view_tipos_cpbtdoc_clasificacion  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( productos.pdt_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( productos.pdt_prodid = detalle_comprobantes.dcc_prodid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid ) AND  
         ( view_cabecera_comprobantes.cbc_feccpbt >=@desde ) AND  
         ( view_cabecera_comprobantes.cbc_feccpbt <=@hasta ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_contable = 'N' ) AND  
         ( view_cabecera_comprobantes.cbt_estado = 'A' ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = 'V' )   
         )
