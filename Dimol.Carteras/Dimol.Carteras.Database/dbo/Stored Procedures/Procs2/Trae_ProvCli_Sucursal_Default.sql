

Create Procedure Trae_ProvCli_Sucursal_Default(@pcs_codemp integer, @pcs_pclid integer) as
  SELECT top 1 view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         provcli_sucursal.pcs_direccion,   
         provcli_sucursal.pcs_telefono,   
         provcli_sucursal.pcs_fax,   
         provcli_sucursal.pcs_mail,
         pcl_nomfant  
     FROM provcli,   
         provcli_sucursal,   
         view_datos_geograficos  
   WHERE ( provcli_sucursal.pcs_codemp = provcli.pcl_codemp ) and  
         ( provcli_sucursal.pcs_pclid = provcli.pcl_pclid ) and  
         ( provcli_sucursal.pcs_comid = view_datos_geograficos.com_comid ) and  
         ( ( provcli.pcl_codemp = @pcs_codemp ) AND  
         ( provcli.pcl_pclid = @pcs_pclid ) AND  
         ( provcli_sucursal.pcs_casamatriz = 'S' )   
         )
