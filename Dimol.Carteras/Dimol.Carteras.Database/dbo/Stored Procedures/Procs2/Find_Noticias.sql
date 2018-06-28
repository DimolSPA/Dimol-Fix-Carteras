

Create Procedure Find_Noticias(@nte_codemp integer, @nte_nteid integer) as

  SELECT count(noticias.nte_nteid)  
    FROM noticias  
   WHERE ( noticias.nte_codemp = @nte_codemp ) AND  
         ( noticias.nte_nteid = @nte_nteid )
