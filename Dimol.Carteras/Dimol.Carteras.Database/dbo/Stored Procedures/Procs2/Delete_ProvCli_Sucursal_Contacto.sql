

Create Procedure Delete_ProvCli_Sucursal_Contacto(@psc_codemp integer, @psc_pclid numeric (15), @psc_pcsid integer, @psc_pscid integer) as  
  DELETE FROM provcli_sucursal_contacto  
   WHERE ( provcli_sucursal_contacto.psc_codemp = @psc_codemp ) AND  
         ( provcli_sucursal_contacto.psc_pclid = @psc_pclid ) AND  
         ( provcli_sucursal_contacto.psc_pcsid = @psc_pcsid ) AND  
         ( provcli_sucursal_contacto.psc_pscid = @psc_pscid )
