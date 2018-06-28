

Create Procedure Insertar_Eventos_Idiomas(@evi_codemp integer, @evi_eveid integer, @evi_idid integer, @evi_titulo varchar(200), @evi_texto text) as
   
  INSERT INTO eventos_idiomas  
         ( evi_codemp,   
           evi_eveid,   
           evi_idid,   
           evi_titulo,   
           evi_texto )  
  VALUES ( @evi_codemp,   
           @evi_eveid,   
           @evi_idid,   
           @evi_titulo,   
           @evi_texto )
