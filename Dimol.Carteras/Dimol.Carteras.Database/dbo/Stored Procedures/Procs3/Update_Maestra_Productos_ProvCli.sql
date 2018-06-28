

Create Procedure Update_Maestra_Productos_ProvCli(@pdt_codemp integer, @pdt_prodid numeric(15), @pcl_pclid numeric(15), @mpc_codigo varchar(20), @mpc_nombre varchar(300), @mpc_codbarra varchar(30), @mpc_pack smallint) as
  UPDATE maestra_productos_provcli  
     SET mpc_codigo = @mpc_codigo,   
         mpc_nombre = @mpc_nombre,   
         mpc_codbarra = @mpc_codbarra,   
         mpc_pack = @mpc_pack  
   WHERE ( maestra_productos_provcli.pdt_codemp = @pdt_codemp ) AND  
         ( maestra_productos_provcli.pdt_prodid = @pdt_prodid ) AND  
         ( maestra_productos_provcli.pcl_pclid = @pcl_pclid )
