

Create Procedure Update_Cabecera_Comprobantes_Estado(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero decimal (15), @cbt_estado char(1), @cbc_saldo decimal(15,2)) as
  UPDATE cabacera_comprobantes  
     SET cbt_estado = @cbt_estado,   
         cbc_saldo = @cbc_saldo  
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )
