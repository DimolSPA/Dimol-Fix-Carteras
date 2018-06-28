

Create Procedure UltNum_Dias_Festivos(@dif_codemp integer) as
  SELECT IsNull(Max(dif_difid)+1, 1)  
    FROM dias_festivos  
   WHERE ( dias_festivos.dif_codemp = @dif_codemp )
