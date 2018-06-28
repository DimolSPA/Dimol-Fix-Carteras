

Create Procedure Update_Detalle_Comprobantes_Totales(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero numeric(15), @dcc_item integer,
																		@dcc_precio decimal(15,2), @dcc_neto decimal(15,2), @dcc_impuesto decimal(15,2), @dcc_total decimal(15,2),
																		@dcc_interes decimal(15,2), @dcc_honorario decimal(15,2), @dcc_gastpre decimal(15,2), @dcc_gastjud decimal(15,2), @dcc_retenido char(1), @dcc_exento numeric(10,2)) as
  UPDATE detalle_comprobantes  
     SET dcc_precio = @dcc_precio,   
         dcc_neto = @dcc_neto,   
         dcc_impuesto = @dcc_impuesto,   
         dcc_total = @dcc_total,   
         dcc_interes = @dcc_interes,   
         dcc_honorario = @dcc_honorario,   
         dcc_gastpre = @dcc_gastpre,   
         dcc_gastjud = @dcc_gastjud,
         dcc_retenido  = @dcc_retenido,
         dcc_exento = @dcc_exento
   WHERE ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero ) AND  
         ( detalle_comprobantes.dcc_item = @dcc_item )
