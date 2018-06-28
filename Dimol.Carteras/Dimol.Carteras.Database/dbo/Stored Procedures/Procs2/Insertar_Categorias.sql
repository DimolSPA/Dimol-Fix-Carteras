

Create Procedure Insertar_Categorias(@cat_codemp integer, @cat_catid integer, @cat_nombre varchar(50), @cat_utilizacion integer) as
  INSERT INTO categorias  
         ( cat_codemp,   
           cat_catid,   
           cat_nombre,   
           cat_utilizacion )  
  VALUES ( @cat_codemp,   
           @cat_catid,   
           @cat_nombre,   
           @cat_utilizacion )
