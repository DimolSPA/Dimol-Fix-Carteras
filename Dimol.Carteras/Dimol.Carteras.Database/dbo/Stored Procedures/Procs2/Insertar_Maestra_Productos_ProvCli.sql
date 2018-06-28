

Create Procedure Insertar_Maestra_Productos_ProvCli(@pdt_codemp integer, @pdt_prodid numeric(15), @pcl_pclid numeric(15), @mpc_codigo varchar(20), @mpc_nombre varchar(300), @mpc_codbarra varchar(30), @mpc_pack smallint) as
  INSERT INTO maestra_productos_provcli  
         ( pdt_codemp,   
           pdt_prodid,   
           pcl_pclid,   
           mpc_codigo,   
           mpc_nombre,   
           mpc_codbarra,   
           mpc_pack )  
  VALUES ( @pdt_codemp,   
           @pdt_prodid,   
           @pcl_pclid,   
           @mpc_codigo,   
           @mpc_nombre,   
           @mpc_codbarra,   
           @mpc_pack )
