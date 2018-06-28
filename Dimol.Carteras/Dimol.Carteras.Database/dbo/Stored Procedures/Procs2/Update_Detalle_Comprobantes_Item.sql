

Create Procedure Update_Detalle_Comprobantes_Item(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero numeric(15), @dcc_item integer, @dcc_itemnew integer) as
  UPDATE detalle_comprobantes  
     SET dcc_item = @dcc_itemnew  
   WHERE ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero ) AND  
         ( detalle_comprobantes.dcc_item = @dcc_item )
