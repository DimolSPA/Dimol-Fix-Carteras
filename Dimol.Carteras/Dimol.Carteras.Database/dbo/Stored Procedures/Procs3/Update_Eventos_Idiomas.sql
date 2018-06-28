

Create Procedure Update_Eventos_Idiomas(@evi_codemp integer, @evi_eveid integer, @evi_idid integer, @evi_titulo varchar(200), @evi_texto text) as
   
   UPDATE eventos_idiomas  
     SET evi_titulo = @evi_titulo,   
         evi_texto = @evi_texto  
   WHERE ( eventos_idiomas.evi_codemp = @evi_codemp ) AND  
         ( eventos_idiomas.evi_eveid = @evi_eveid ) AND  
         ( eventos_idiomas.evi_idid = @evi_idid )
