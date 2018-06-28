

Create Procedure Insertar_Noticias_Idiomas(@nei_codemp integer, @nei_nteid integer, @nei_idid integer, @nei_titulo varchar(800), @nei_texto text) as
  INSERT INTO noticias_idiomas  
         ( nei_codemp,   
           nei_nteid,   
           nei_idid,   
           nei_titulo,   
           nei_texto )  
  VALUES ( @nei_codemp,   
           @nei_nteid,   
           @nei_idid,   
           @nei_titulo,   
           @nei_texto )
