

Create Procedure Delete_Eventos(@eve_codemp integer, @eve_eveid integer) as
   
    DELETE FROM eventos_fotos  
   WHERE ( eventos_fotos.evf_codemp = @eve_codemp ) AND  
         ( eventos_fotos.evf_eveid = @eve_eveid ) 
     
          

  DELETE FROM eventos_idiomas  
   WHERE ( eventos_idiomas.evi_codemp = @eve_codemp ) AND  
         ( eventos_idiomas.evi_eveid = @eve_eveid )   
           

  DELETE FROM eventos  
   WHERE ( eventos.eve_codemp = @eve_codemp ) AND  
         ( eventos.eve_eveid = @eve_eveid )
