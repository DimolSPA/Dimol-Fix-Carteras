

Create Procedure Delete_Tipos_Descripcion_Producto(@tpi_codemp integer, @tpi_tpdid integer) as

DELETE FROM tipos_descripcion_producto_idioma  
   WHERE ( tipos_descripcion_producto_idioma.tpi_codemp = @tpi_codemp ) AND  
         ( tipos_descripcion_producto_idioma.tpi_tpdid = @tpi_tpdid ) 

  DELETE FROM tipos_descripcion_producto  
   WHERE ( tipos_descripcion_producto.tdp_codemp = @tpi_codemp ) AND  
         ( tipos_descripcion_producto.tdp_tpdid = @tpi_tpdid )
