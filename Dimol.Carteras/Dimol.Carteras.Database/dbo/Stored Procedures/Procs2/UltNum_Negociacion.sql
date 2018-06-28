

Create Procedure UltNum_Negociacion(@neg_codemp integer, @neg_anio smallint) as
  SELECT IsNull(Max(neg_negid)+1, 1)
    FROM negociacion  
   WHERE ( negociacion.neg_codemp = @neg_codemp ) AND  
         ( negociacion.neg_anio = @neg_anio )
