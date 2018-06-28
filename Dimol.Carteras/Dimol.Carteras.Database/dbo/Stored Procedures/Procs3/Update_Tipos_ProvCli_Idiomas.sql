

Create Procedure Update_Tipos_ProvCli_Idiomas(@tpi_codemp integer, @tpi_tpcid integer, @tpi_idid integer, @tpi_nombre varchar (60)) as  
  UPDATE tipos_provcli_idiomas  
     SET tpi_nombre = @tpi_nombre  
   WHERE ( tipos_provcli_idiomas.tpi_codemp = @tpi_codemp ) AND  
         ( tipos_provcli_idiomas.tpi_tpcid = @tpi_tpcid ) AND  
         ( tipos_provcli_idiomas.tpi_idid = @tpi_idid )
