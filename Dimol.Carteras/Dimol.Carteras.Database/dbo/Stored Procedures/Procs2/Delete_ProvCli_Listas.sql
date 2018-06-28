

Create Procedure Delete_ProvCli_Listas(@plp_codemp integer, @plp_pclid integer, @plp_ltpid integer) as
   DELETE FROM provcli_listas  
   WHERE ( provcli_listas.plp_codemp = @plp_codemp ) AND  
         ( provcli_listas.plp_pclid = @plp_pclid ) AND  
         ( provcli_listas.plp_ltpid = @plp_ltpid )
