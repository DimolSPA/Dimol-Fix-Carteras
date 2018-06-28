

Create Procedure Delete_Maestra_Productos_ProvCli(@pdt_codemp integer, @pdt_prodid numeric(15), @pcl_pclid numeric(15)) as
   DELETE FROM maestra_productos_provcli  
   WHERE ( maestra_productos_provcli.pdt_codemp = @pdt_codemp ) AND  
         ( maestra_productos_provcli.pdt_prodid = @pdt_prodid ) AND  
         ( maestra_productos_provcli.pcl_pclid = @pcl_pclid )
