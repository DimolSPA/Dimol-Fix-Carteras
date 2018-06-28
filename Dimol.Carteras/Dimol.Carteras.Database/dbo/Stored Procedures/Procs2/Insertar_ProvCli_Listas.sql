

Create Procedure Insertar_ProvCli_Listas(@plp_codemp integer, @plp_pclid integer, @plp_ltpid integer) as
  INSERT INTO provcli_listas  
         ( plp_codemp,   
           plp_pclid,   
           plp_ltpid )  
  VALUES ( @plp_codemp,   
           @plp_pclid,   
           @plp_ltpid )
