

Create Procedure Insertar_ProvCli_Codigo_Carga(@pcc_codemp integer, @pcc_pclid numeric (15), @pcc_codid integer, 
                                                                               @pcc_codigo varchar (20), @pcc_nombre varchar (120)) as 
  INSERT INTO provcli_codigo_carga  
         ( pcc_codemp,   
           pcc_pclid,   
           pcc_codid,   
           pcc_codigo,   
           pcc_nombre )  
  VALUES ( @pcc_codemp,   
           @pcc_pclid,   
           @pcc_codid,   
           @pcc_codigo,   
           @pcc_nombre )
