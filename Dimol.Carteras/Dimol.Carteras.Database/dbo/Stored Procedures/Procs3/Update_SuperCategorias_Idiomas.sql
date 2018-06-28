

Create Procedure Update_SuperCategorias_Idiomas(@sci_codemp integer, @sci_spcid integer, @sci_idiid integer, @sci_nombre varchar(150)) as
   UPDATE supercategorias_idioma  
     SET sci_nombre = @sci_nombre 
   WHERE ( supercategorias_idioma.sci_codemp = @sci_codemp ) AND  
         ( supercategorias_idioma.sci_spcid = @sci_spcid ) AND  
         ( supercategorias_idioma.sci_idiid = @sci_idiid )
