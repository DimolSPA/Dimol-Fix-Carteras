

create procedure UltNum_Detalle_Comprobantes_Item(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero integer) as
  SELECT IsNull(Max(dcc_item)+1, 1) 
    FROM detalle_comprobantes  
   WHERE ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero )
