

Create Procedure Delete_Categorias_Idiomas(@cai_codemp integer, @cai_catid integer, @cai_idiid integer) as
 
  DELETE FROM categorias_idiomas  
   WHERE ( categorias_idiomas.cai_codemp = @cai_codemp ) AND  
         ( categorias_idiomas.cai_catid = @cai_catid ) AND  
         ( categorias_idiomas.cai_idiid = @cai_idiid )
