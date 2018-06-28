

Create Procedure Delete_Eventos_Fotos(@evf_codemp integer, @evf_eveid integer, @evf_evfid integer) as
   
   DELETE FROM eventos_fotos  
   WHERE ( eventos_fotos.evf_codemp = @evf_codemp ) AND  
         ( eventos_fotos.evf_eveid = @evf_eveid ) AND  
         ( eventos_fotos.evf_evfid = @evf_evfid )
