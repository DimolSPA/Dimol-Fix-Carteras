

 Create Procedure Insertar_Dias_Festivos(@dif_codemp integer, @dif_difid integer, @dif_dia smallint, @dif_mes smallint, @dif_repetir char (1), @dif_diaesp datetime) as
  INSERT INTO dias_festivos  
         ( dif_codemp,   
           dif_difid,   
           dif_dia,   
           dif_mes,   
           dif_repetir,   
           dif_diaesp )  
  VALUES ( @dif_codemp,   
           @dif_difid,   
           @dif_dia,   
           @dif_mes,   
           @dif_repetir,   
           @dif_diaesp )
