

Create Procedure Insertar_Noticias(@nte_codemp integer, @nte_nteid integer, @nte_titulo varchar(800), @nte_fuente varchar(200), @nte_enlace varchar(800), @nte_foto varchar(100)) as
  INSERT INTO noticias  
         ( nte_codemp,   
           nte_nteid,   
           nte_titulo,   
           nte_fecha,   
           nte_fuente,   
           nte_enlace,   
           nte_foto )  
  VALUES ( @nte_codemp,   
           @nte_nteid,   
           @nte_titulo,   
           getdate(),   
           @nte_fuente,   
           @nte_enlace,   
           @nte_foto )
