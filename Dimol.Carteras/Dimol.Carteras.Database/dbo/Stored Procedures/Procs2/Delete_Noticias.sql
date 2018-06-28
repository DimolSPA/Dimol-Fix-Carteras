

Create Procedure Delete_Noticias(@nte_codemp integer, @nte_nteid integer) as

  DELETE FROM noticias_idiomas  
   WHERE ( noticias_idiomas.nei_codemp = @nte_codemp ) AND  
         ( noticias_idiomas.nei_nteid = @nte_nteid )   


  DELETE FROM noticias  
   WHERE ( noticias.nte_codemp = @nte_codemp ) AND  
         ( noticias.nte_nteid = @nte_nteid )
