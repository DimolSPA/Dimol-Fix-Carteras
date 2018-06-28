

Create Procedure Find_ProvCli_Despachos(@pcd_codemp integer, @pcd_pclid numeric(15), @pcd_pcdid integer) as
  SELECT count(provcli_despachos.pcd_pcdid)  
    FROM provcli_despachos  
   WHERE ( provcli_despachos.pcd_codemp = @pcd_codemp ) AND  
         ( provcli_despachos.pcd_pclid = @pcd_pclid ) AND  
         ( provcli_despachos.pcd_pcdid = @pcd_pcdid )
