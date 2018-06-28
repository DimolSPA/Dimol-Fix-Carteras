

Create Procedure Update_Dias_Festivos(@dif_codemp integer, @dif_difid integer, @dif_dia smallint, @dif_mes smallint, 
                                                                   @dif_repetir char (1), @dif_diaesp datetime) as  
  UPDATE dias_festivos  
     SET dif_dia = @dif_dia,   
         dif_mes = @dif_mes,   
         dif_repetir = @dif_repetir,   
         dif_diaesp = @dif_diaesp  
   WHERE ( dias_festivos.dif_codemp = @dif_codemp ) AND  
         ( dias_festivos.dif_difid = @dif_difid )
