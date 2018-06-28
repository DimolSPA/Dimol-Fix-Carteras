

Create Procedure UltNum_Cabacera_Comprobantes(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer) as
  SELECT IsNull(Max(cbc_numero)+1, 1)
    FROM cabacera_comprobantes  
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid )
