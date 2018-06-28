

Create Procedure Update_Noticias(@nte_codemp integer, @nte_nteid integer, @nte_titulo varchar(800), @nte_fuente varchar(200), @nte_enlace varchar(800), @nte_foto varchar(100)) as
  UPDATE noticias  
     SET nte_titulo = @nte_titulo,   
         nte_fuente = @nte_fuente,   
         nte_enlace = @nte_enlace,   
         nte_foto = @nte_foto  
   WHERE ( noticias.nte_codemp = @nte_codemp ) AND  
         ( noticias.nte_nteid = @nte_nteid )
