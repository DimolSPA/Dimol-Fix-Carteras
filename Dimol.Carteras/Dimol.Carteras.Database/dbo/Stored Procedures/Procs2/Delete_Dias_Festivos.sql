

Create Procedure Delete_Dias_Festivos(@dif_codemp integer, @dif_difid integer) as 
  DELETE FROM dias_festivos  
   WHERE ( dias_festivos.dif_codemp = @dif_codemp ) AND  
         ( dias_festivos.dif_difid = @dif_difid )
