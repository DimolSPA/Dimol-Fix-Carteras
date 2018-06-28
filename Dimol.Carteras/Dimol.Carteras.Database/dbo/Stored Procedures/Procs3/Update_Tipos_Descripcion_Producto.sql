

Create Procedure Update_Tipos_Descripcion_Producto(@tdp_codemp integer, @tdp_tpdid integer, @tpd_nombre varchar(50)) as
   UPDATE tipos_descripcion_producto  
     SET tpd_nombre = @tpd_nombre  
   WHERE ( tipos_descripcion_producto.tdp_codemp = @tdp_codemp ) AND  
         ( tipos_descripcion_producto.tdp_tpdid = @tdp_tpdid )
