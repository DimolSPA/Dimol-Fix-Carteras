

Create Procedure Find_Maestra_Productos_ProvCli(@pdt_codemp integer, @pcl_pclid numeric(15), @pdt_prodid numeric(15)) as
   SELECT count(maestra_productos_provcli.pdt_prodid)  
    FROM maestra_productos_provcli  
   WHERE ( maestra_productos_provcli.pdt_codemp = @pdt_codemp ) AND  
         ( maestra_productos_provcli.pcl_pclid = @pcl_pclid ) AND  
         ( maestra_productos_provcli.pdt_prodid = @pdt_prodid )
