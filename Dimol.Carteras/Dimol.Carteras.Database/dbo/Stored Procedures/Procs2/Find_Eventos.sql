

Create Procedure Find_Eventos(@eve_codemp integer, @eve_eveid integer) as
   
      SELECT count(eventos.eve_eveid)  
    FROM eventos  
   WHERE ( eventos.eve_codemp = @eve_codemp ) AND  
         ( eventos.eve_eveid = @eve_eveid )
