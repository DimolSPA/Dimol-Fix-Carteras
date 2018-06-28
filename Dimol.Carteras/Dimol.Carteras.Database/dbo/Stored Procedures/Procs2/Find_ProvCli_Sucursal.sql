

Create Procedure Find_ProvCli_Sucursal(@pcs_codemp integer, @pcs_pclid integer, @pcs_pcsid integer) as
  SELECT count(provcli_sucursal.pcs_pcsid)  
    FROM provcli_sucursal  
   WHERE ( provcli_sucursal.pcs_codemp = @pcs_codemp ) AND  
         ( provcli_sucursal.pcs_pclid = @pcs_pclid ) AND  
         ( provcli_sucursal.pcs_pcsid = @pcs_pcsid )
