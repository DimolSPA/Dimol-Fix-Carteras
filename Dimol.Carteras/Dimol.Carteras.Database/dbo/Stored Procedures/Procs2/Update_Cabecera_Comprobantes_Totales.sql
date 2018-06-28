

Create Procedure Update_Cabecera_Comprobantes_Totales(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric(15),
																		    @cbc_neto decimal(15,2), @cbc_impuestos decimal(15,2), @cbc_retenido decimal(15,2),
																			 @cbc_descuentos decimal(15,2), @cbc_final decimal(15,2), @cbc_saldo decimal(15,2), @cbc_exento decimal(15,2)) as
  UPDATE cabacera_comprobantes  
     SET cbc_neto = @cbc_neto,   
         cbc_impuestos = @cbc_impuestos,   
         cbc_retenido = @cbc_retenido,   
         cbc_descuentos = @cbc_descuentos,   
         cbc_final = @cbc_final,   
         cbc_saldo = @cbc_saldo,
         cbc_exento  = @cbc_exento
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )
