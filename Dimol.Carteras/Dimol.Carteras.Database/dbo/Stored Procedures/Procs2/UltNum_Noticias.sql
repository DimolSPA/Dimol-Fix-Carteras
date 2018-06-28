

Create Procedure UltNum_Noticias(@nte_codemp integer) as

  SELECT IsNull(Max(nte_nteid)+1, 1) 
    FROM noticias  
   WHERE ( noticias.nte_codemp = @nte_codemp )
