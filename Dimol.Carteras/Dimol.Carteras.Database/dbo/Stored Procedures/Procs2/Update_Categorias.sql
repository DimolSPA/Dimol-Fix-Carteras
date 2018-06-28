

Create Procedure Update_Categorias(@cat_codemp integer, @cat_catid integer, @cat_nombre varchar(50), @cat_utilizacion integer) as
   UPDATE categorias  
     SET cat_nombre = @cat_nombre,   
         cat_utilizacion = @cat_utilizacion  
   WHERE ( categorias.cat_codemp = @cat_codemp ) AND  
         ( categorias.cat_catid = @cat_catid )
