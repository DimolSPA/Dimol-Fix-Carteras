

Create Procedure UltNum_Transporte(@tra_codemp integer) as
 
    SELECT IsNull(Max(tra_traid)+1, 1) 
    FROM transporte  
   WHERE ( transporte.tra_codemp = @tra_codemp )
