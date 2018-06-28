

Create Procedure UltNum_Eventos_Fotos(@evf_codemp integer, @evf_eveid integer) as
   
      SELECT IsNull(Max(evf_evfid)+1, 1) 
    FROM eventos_fotos  
   WHERE ( eventos_fotos.evf_codemp = @evf_codemp ) AND  
         ( eventos_fotos.evf_eveid = @evf_eveid )
