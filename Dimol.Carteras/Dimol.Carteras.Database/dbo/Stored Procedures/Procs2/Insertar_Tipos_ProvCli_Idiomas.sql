

Create Procedure Insertar_Tipos_ProvCli_Idiomas(@tpi_codemp integer, @tpi_tpcid integer, @tpi_idid integer, @tpi_nombre varchar (60)) as
  INSERT INTO tipos_provcli_idiomas  
         ( tpi_codemp,   
           tpi_tpcid,   
           tpi_idid,   
           tpi_nombre )  
  VALUES ( @tpi_codemp,   
           @tpi_tpcid,   
           @tpi_idid,   
           @tpi_nombre )
