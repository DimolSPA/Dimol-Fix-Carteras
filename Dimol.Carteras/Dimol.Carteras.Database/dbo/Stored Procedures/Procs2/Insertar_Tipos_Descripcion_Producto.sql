

Create Procedure Insertar_Tipos_Descripcion_Producto(@tdp_codemp integer, @tdp_tpdid integer, @tpd_nombre varchar(50)) as
  INSERT INTO tipos_descripcion_producto  
         ( tdp_codemp,   
           tdp_tpdid,   
           tpd_nombre )  
  VALUES ( @tdp_codemp,   
           @tdp_tpdid,   
           @tpd_nombre )
