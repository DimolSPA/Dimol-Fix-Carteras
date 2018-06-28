

Create Procedure Update_Tipos_Descripcion_Producto_Idiomas(@tpi_codemp integer, @tpi_tpdid integer, @tpi_idiid integer, @tpi_nombre varchar(250)) as
   UPDATE tipos_descripcion_producto_idioma  
     SET tpi_nombre = @tpi_nombre  
   WHERE ( tipos_descripcion_producto_idioma.tpi_codemp = @tpi_codemp ) AND  
         ( tipos_descripcion_producto_idioma.tpi_tpdid = @tpi_tpdid ) AND  
         ( tipos_descripcion_producto_idioma.tpi_idiid = @tpi_idiid )
