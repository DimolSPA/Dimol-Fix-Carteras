CREATE Procedure [dbo].[_Insertar_Categoria](@cat_codemp integer, @idioma integer, @cat_nombre varchar(80), @cat_utilizacion varchar(5)) as
declare @catid int

set @catid = (select IsNull(Max(cat_catid)+1, 1) from CATEGORIAS where cat_codemp = @cat_codemp)

  INSERT INTO CATEGORIAS  
         ( cat_codemp,   
           cat_catid,   
           cat_nombre,   
           cat_utilizacion )  
  VALUES ( @cat_codemp,
		   @catid,
           @cat_nombre,   
           @cat_utilizacion )
           
  INSERT INTO CATEGORIAS_IDIOMAS  
         ( cai_codemp,   
           cai_catid,   
           cai_idiid,   
           cai_nombre )  
  VALUES ( @cat_codemp,
		   @catid,
           @idioma,   
           @cat_nombre )
