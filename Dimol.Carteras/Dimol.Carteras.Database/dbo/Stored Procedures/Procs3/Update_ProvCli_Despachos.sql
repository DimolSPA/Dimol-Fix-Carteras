

Create Procedure Update_ProvCli_Despachos(@pcd_codemp integer, @pcd_pclid numeric(15), @pcd_pcdid integer, @pcd_nombre varchar(200), @pcd_comid integer, @pcd_direccion varchar(200), @pcd_codigo varchar(10)) as
   UPDATE provcli_despachos  
     SET pcd_nombre = @pcd_nombre,   
         pcd_comid = @pcd_comid,   
         pcd_direccion = @pcd_direccion,
         pcd_codigo = @pcd_codigo  
   WHERE ( provcli_despachos.pcd_codemp = @pcd_codemp ) AND  
         ( provcli_despachos.pcd_pclid = @pcd_pclid ) AND  
         ( provcli_despachos.pcd_pcdid = @pcd_pcdid )
