

Create Procedure Update_Noticias_Idiomas(@nei_codemp integer, @nei_nteid integer, @nei_idid integer, @nei_titulo varchar(800), @nei_texto text) as
  UPDATE noticias_idiomas  
     SET nei_titulo = @nei_titulo,   
         nei_texto = @nei_texto  
   WHERE ( noticias_idiomas.nei_codemp = @nei_codemp ) AND  
         ( noticias_idiomas.nei_nteid = @nei_nteid ) AND  
         ( noticias_idiomas.nei_idid = @nei_idid )
