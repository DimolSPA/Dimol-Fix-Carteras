

Create Procedure Trae_ProvCli_Sucursales(@pcs_codemp integer, @pcs_pclid integer) as
  SELECT provcli_sucursal.pcs_pcsid,   
         provcli_sucursal.pcs_nombre,   
         provcli_sucursal.pcs_casamatriz  
    FROM provcli_sucursal  
   WHERE ( provcli_sucursal.pcs_codemp = @pcs_codemp ) AND  
         ( provcli_sucursal.pcs_pclid = @pcs_pclid )   
           
ORDER BY provcli_sucursal.pcs_casamatriz DESC,   
         provcli_sucursal.pcs_nombre ASC
