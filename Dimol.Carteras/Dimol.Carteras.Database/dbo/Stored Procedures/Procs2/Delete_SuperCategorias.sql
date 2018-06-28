

Create Procedure Delete_SuperCategorias(@sci_codemp integer, @sci_spcid integer) as

  DELETE FROM supercategorias_idioma  
   WHERE ( supercategorias_idioma.sci_codemp = @sci_codemp ) AND  
         ( supercategorias_idioma.sci_spcid = @sci_spcid ) 
          

  DELETE FROM supercategorias  
   WHERE ( supercategorias.spc_codemp = @sci_codemp ) AND  
         ( supercategorias.spc_spcid = @sci_spcid )
