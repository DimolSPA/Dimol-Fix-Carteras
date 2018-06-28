

Create Procedure Insertar_Tipos_Descripcion_Producto_Idioma(@tpi_codemp integer, @tpi_tpdid integer, @tpi_idiid integer, @tpi_nombre varchar (80)) as
  INSERT INTO tipos_descripcion_producto_idioma  
         ( tpi_codemp,   
           tpi_tpdid,   
           tpi_idiid,   
           tpi_nombre )  
  VALUES ( @tpi_codemp,   
           @tpi_tpdid,   
           @tpi_idiid,   
           @tpi_nombre )
