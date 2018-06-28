

Create Procedure UltNum_ProvCli_Despachos(@pcd_codemp integer, @pcd_pclid numeric(15)) as
  SELECT IsNull(Max(pcd_pcdid)+1, 1)
    FROM provcli_despachos  
   WHERE ( provcli_despachos.pcd_codemp = @pcd_codemp ) AND  
         ( provcli_despachos.pcd_pclid = @pcd_pclid )
