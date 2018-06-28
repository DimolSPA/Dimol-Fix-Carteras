

Create Procedure Delete_Tipos_ProvCli(@tpc_codemp integer, @tpc_tpcid integer) as
  DELETE FROM tipos_provcli_idiomas  
   WHERE ( tipos_provcli_idiomas.tpi_codemp = @tpc_codemp ) AND  
         ( tipos_provcli_idiomas.tpi_tpcid = @tpc_tpcid )   


  DELETE FROM tipos_provcli  
   WHERE ( tipos_provcli.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_provcli.tpc_tpcid = @tpc_tpcid )
