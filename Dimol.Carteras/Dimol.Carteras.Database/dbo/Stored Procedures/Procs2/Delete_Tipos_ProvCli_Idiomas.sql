

Create Procedure Delete_Tipos_ProvCli_Idiomas(@tpi_codemp integer, @tpi_tpcid integer, @tpi_idid integer) as  
  DELETE FROM tipos_provcli_idiomas  
   WHERE ( tipos_provcli_idiomas.tpi_codemp = @tpi_codemp ) AND  
         ( tipos_provcli_idiomas.tpi_tpcid = @tpi_tpcid ) AND  
         ( tipos_provcli_idiomas.tpi_idid = @tpi_idid )
