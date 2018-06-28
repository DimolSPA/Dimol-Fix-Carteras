

Create Procedure Delete_ProvCli_Sucursal(@pcs_codemp integer, @pcs_pclid numeric (15), @pcs_pcsid integer) as  
  DELETE FROM provcli_sucursal  
   WHERE ( provcli_sucursal.pcs_codemp = @pcs_codemp ) AND  
         ( provcli_sucursal.pcs_pclid = @pcs_pclid ) AND  
         ( provcli_sucursal.pcs_pcsid = @pcs_pcsid )
