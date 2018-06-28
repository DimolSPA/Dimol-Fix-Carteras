

Create Procedure Delete_Bancos(@bco_codemp integer, @bco_bcoid integer) as
  DELETE FROM bancos  
   WHERE ( bancos.bco_codemp = @bco_codemp ) AND  
         ( bancos.bco_bcoid = @bco_bcoid )
