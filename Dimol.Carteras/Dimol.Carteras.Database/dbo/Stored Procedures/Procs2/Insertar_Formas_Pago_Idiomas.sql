

Create Procedure Insertar_Formas_Pago_Idiomas(@fpi_codemp integer, @fpi_frpid integer, @fpi_idid integer, @fpi_nombre varchar(200) ) as
  INSERT INTO formas_pago_idiomas  
         ( fpi_codemp,   
           fpi_frpid,   
           fpi_idid,   
           fpi_nombre )  
  VALUES ( @fpi_codemp,   
           @fpi_frpid,   
           @fpi_idid,   
           @fpi_nombre )
