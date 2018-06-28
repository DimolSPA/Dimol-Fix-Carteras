

Create Procedure Delete_SuperCategorias_Idiomas(@sci_codemp integer, @sci_spcid integer, @sci_idiid integer) as

  DELETE FROM supercategorias_idioma  
   WHERE ( supercategorias_idioma.sci_codemp = @sci_codemp ) AND  
         ( supercategorias_idioma.sci_spcid = @sci_spcid ) AND  
         ( supercategorias_idioma.sci_idiid = @sci_idiid )
