

Create Procedure Find_Negociacion(@neg_codemp integer, @neg_anio smallint, @neg_negid integer) as
  SELECT count(negociacion.neg_codemp)  
    FROM negociacion  
   WHERE ( negociacion.neg_codemp = @neg_codemp ) AND  
         ( negociacion.neg_anio = @neg_anio ) AND  
         ( negociacion.neg_negid = @neg_negid )
