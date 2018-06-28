

Create Procedure UltNum_ProvCli_Sucursal(@pcs_codemp integer, @pcs_pclid integer) as
  SELECT IsNull(Max(pcs_pcsid)+1, 1)
    FROM provcli_sucursal  
   WHERE ( provcli_sucursal.pcs_codemp = @pcs_codemp ) AND  
         ( provcli_sucursal.pcs_pclid = @pcs_pclid )
