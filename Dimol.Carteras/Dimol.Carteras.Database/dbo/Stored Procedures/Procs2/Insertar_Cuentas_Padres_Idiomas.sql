

Create Procedure Insertar_Cuentas_Padres_Idiomas(@cpi_codemp integer, @cpi_ctpid integer,@cpi_idid integer,  @cpi_nombre varchar(200)) as
  INSERT INTO cuentas_padres_idiomas  
         ( cpi_codemp,   
           cpi_ctpid,   
           cpi_idid,   
           cpi_nombre )  
  VALUES ( @cpi_codemp,   
           @cpi_ctpid,   
           @cpi_idid,   
           @cpi_nombre )
