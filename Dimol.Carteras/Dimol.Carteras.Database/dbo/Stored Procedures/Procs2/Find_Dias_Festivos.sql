

Create Procedure Find_Dias_Festivos(@dif_codemp integer, @dif_difid integer) as
  SELECT count(dias_festivos.dif_difid)  
    FROM dias_festivos  
   WHERE ( dias_festivos.dif_codemp = @dif_codemp ) AND  
         ( dias_festivos.dif_difid = @dif_difid )
