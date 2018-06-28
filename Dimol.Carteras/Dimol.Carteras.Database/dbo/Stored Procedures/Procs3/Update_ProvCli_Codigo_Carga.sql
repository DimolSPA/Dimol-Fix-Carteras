

Create Procedure Update_ProvCli_Codigo_Carga(@pcc_codemp integer, @pcc_pclid numeric (15), @pcc_codid integer, 
                                                                                @pcc_codigo varchar (20), @pcc_nombre varchar (120)) as  
  UPDATE provcli_codigo_carga  
     SET pcc_codigo = @pcc_codigo,   
         pcc_nombre = @pcc_nombre  
   WHERE ( provcli_codigo_carga.pcc_codemp = @pcc_codemp ) AND  
         ( provcli_codigo_carga.pcc_pclid = @pcc_pclid ) AND  
         ( provcli_codigo_carga.pcc_codid = @pcc_codid )
