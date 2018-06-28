

Create Procedure UltNum_Eventos(@eve_codemp integer) as
   
      SELECT  IsNull(Max(eve_eveid)+1, 1)
    FROM eventos  
   WHERE ( eventos.eve_codemp = @eve_codemp )
