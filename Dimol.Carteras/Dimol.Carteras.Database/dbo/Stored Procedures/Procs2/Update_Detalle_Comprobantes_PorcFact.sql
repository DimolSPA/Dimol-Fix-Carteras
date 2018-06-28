

Create Procedure Update_Detalle_Comprobantes_PorcFact(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero numeric(15), @dcc_porcfact decimal(10,2)) as
  UPDATE detalle_comprobantes  
     SET dcc_porcfact = @dcc_porcfact  
   WHERE ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero )
