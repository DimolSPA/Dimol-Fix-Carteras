

Create Procedure Update_Maestra_Productos_ProvCli_CodBar(@pdt_codemp integer, @pdt_prodid integer, @pcl_pclid integer, @mpc_codbarimg image) as
  UPDATE maestra_productos_provcli  
     SET mpc_codbarimg = @mpc_codbarimg  
   WHERE ( maestra_productos_provcli.pdt_codemp = @pdt_codemp ) AND  
         ( maestra_productos_provcli.pdt_prodid = @pdt_prodid ) AND  
         ( maestra_productos_provcli.pcl_pclid = @pcl_pclid )
