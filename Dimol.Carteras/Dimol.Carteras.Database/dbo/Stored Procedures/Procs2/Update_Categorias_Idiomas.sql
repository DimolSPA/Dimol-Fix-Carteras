

Create Procedure Update_Categorias_Idiomas(@cai_codemp integer, @cai_catid integer, @cai_idiid integer, @cai_nombre varchar(200)) as
   UPDATE categorias_idiomas  
     SET cai_nombre = @cai_nombre  
   WHERE ( categorias_idiomas.cai_codemp = @cai_codemp ) AND  
         ( categorias_idiomas.cai_catid = @cai_catid ) AND  
         ( categorias_idiomas.cai_idiid = @cai_idiid )
