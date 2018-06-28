

Create Procedure Insertar_ProvCli_Despachos(@pcd_codemp integer, @pcd_pclid numeric(15), @pcd_pcdid integer, @pcd_nombre varchar(200), @pcd_comid integer, @pcd_direccion varchar(200), @pcd_codigo varchar(10)) as
  INSERT INTO provcli_despachos  
         ( pcd_codemp,   
           pcd_pclid,   
           pcd_pcdid,   
           pcd_nombre,   
           pcd_comid,   
           pcd_direccion,
           pcd_codigo )  
  VALUES ( @pcd_codemp,   
           @pcd_pclid,   
           @pcd_pcdid,   
           @pcd_nombre,   
           @pcd_comid,   
           @pcd_direccion,
           @pcd_codigo )
