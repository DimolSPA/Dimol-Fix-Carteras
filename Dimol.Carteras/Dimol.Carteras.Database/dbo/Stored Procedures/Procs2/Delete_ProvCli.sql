

Create Procedure Delete_ProvCli(@pcl_codemp integer, @pcl_pclid numeric (15)) as  
  DELETE FROM provcli_sucursal_contacto  
   WHERE ( provcli_sucursal_contacto.psc_codemp = @pcl_codemp ) AND  
         ( provcli_sucursal_contacto.psc_pclid = @pcl_pclid )   
           

  DELETE FROM provcli_sucursal  
   WHERE ( provcli_sucursal.pcs_codemp = @pcl_codemp) AND  
         ( provcli_sucursal.pcs_pclid = @pcl_pclid )   
           

  DELETE FROM provcli_impuestos  
   WHERE ( provcli_impuestos.pci_codemp = @pcl_codemp ) AND  
         ( provcli_impuestos.pci_pclid = @pcl_pclid )   
           

  DELETE FROM provcli_ctacte  
   WHERE ( provcli_ctacte.pct_codemp = @pcl_codemp ) AND  
         ( provcli_ctacte.pct_pclid = @pcl_pclid )   
           


  DELETE FROM provcli  
   WHERE ( provcli.pcl_codemp = @pcl_codemp ) AND  
         ( provcli.pcl_pclid = @pcl_pclid )
