

Create Procedure Trae_ProvCli_Sucursal(@pcs_codemp integer, @pcs_pclid integer, @pcs_pcsid integer) as
  SELECT view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         provcli_sucursal.pcs_direccion,   
         provcli_sucursal.pcs_telefono,   
         provcli_sucursal.pcs_fax,   
         provcli_sucursal.pcs_mail  
    FROM provcli_sucursal,   
         view_datos_geograficos  
   WHERE ( provcli_sucursal.pcs_comid = view_datos_geograficos.com_comid ) and  
         ( ( provcli_sucursal.pcs_codemp = @pcs_codemp ) AND  
         ( provcli_sucursal.pcs_pclid = @pcs_pclid ) AND  
         ( provcli_sucursal.pcs_pcsid = @pcs_pcsid ) )
