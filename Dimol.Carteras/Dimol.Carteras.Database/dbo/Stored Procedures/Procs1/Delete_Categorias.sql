

Create Procedure Delete_Categorias(@cai_codemp integer, @cai_catid integer) as
 
  DELETE FROM categorias_idiomas  
   WHERE ( categorias_idiomas.cai_codemp = @cai_codemp ) AND  
         ( categorias_idiomas.cai_catid = @cai_catid ) 
           

  DELETE FROM categorias  
   WHERE ( categorias.cat_codemp = @cai_codemp ) AND  
         ( categorias.cat_catid = @cai_catid )
