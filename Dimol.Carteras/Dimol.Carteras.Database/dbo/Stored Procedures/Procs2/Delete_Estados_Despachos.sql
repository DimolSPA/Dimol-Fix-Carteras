

Create Procedure Delete_Estados_Despachos(@edp_codemp integer, @edp_edpid integer) as

  DELETE FROM estados_despachos_idiomas  
   WHERE ( estados_despachos_idiomas.edi_codemp = @edp_codemp ) AND  
         ( estados_despachos_idiomas.edi_edpid = @edp_edpid ) 

   DELETE FROM estados_despachos  
   WHERE ( estados_despachos.edp_codemp = @edp_codemp ) AND  
         ( estados_despachos.edp_edpid = @edp_edpid )
