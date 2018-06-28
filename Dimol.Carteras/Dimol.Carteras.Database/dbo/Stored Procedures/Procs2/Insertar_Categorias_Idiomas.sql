

Create Procedure Insertar_Categorias_Idiomas(@cai_codemp integer, @cai_catid integer, @cai_idiid integer, @cai_nombre varchar(200)) as
  INSERT INTO categorias_idiomas  
         ( cai_codemp,   
           cai_catid,   
           cai_idiid,   
           cai_nombre )  
  VALUES ( @cai_codemp,   
           @cai_catid,   
           @cai_idiid,   
           @cai_nombre )
