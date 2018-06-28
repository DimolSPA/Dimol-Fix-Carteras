

Create Procedure Find_Transporte(@tra_codemp integer, @tra_traid integer) as
 
    SELECT count(transporte.tra_traid)  
    FROM transporte  
   WHERE ( transporte.tra_codemp = @tra_codemp ) AND  
         ( transporte.tra_traid = @tra_traid )
