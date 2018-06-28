

Create Procedure Delete_Tipos_Descripcion_Producto_Idioma(@tpi_codemp integer, @tpi_tpdid integer, @tpi_idiid integer) as
  DELETE FROM tipos_descripcion_producto_idioma  
   WHERE ( tipos_descripcion_producto_idioma.tpi_codemp = @tpi_codemp ) AND  
         ( tipos_descripcion_producto_idioma.tpi_tpdid = @tpi_tpdid ) AND  
         ( tipos_descripcion_producto_idioma.tpi_idiid = @tpi_idiid )
