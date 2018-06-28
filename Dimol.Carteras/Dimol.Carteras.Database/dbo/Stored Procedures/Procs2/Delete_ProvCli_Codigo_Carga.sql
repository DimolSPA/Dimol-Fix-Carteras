

Create Procedure Delete_ProvCli_Codigo_Carga(@pcc_codemp integer, @pcc_pclid numeric (15), @pcc_codid integer) as  
  DELETE FROM provcli_codigo_carga  
   WHERE ( provcli_codigo_carga.pcc_codemp = @pcc_codemp ) AND  
         ( provcli_codigo_carga.pcc_pclid = @pcc_pclid ) AND  
         ( provcli_codigo_carga.pcc_codid = @pcc_codid )
