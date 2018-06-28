

Create Procedure Delete_Transporte(@tra_codemp integer, @tra_traid integer) as
 
   DELETE FROM transporte  
   WHERE ( transporte.tra_codemp = @tra_codemp ) AND  
         ( transporte.tra_traid = @tra_traid )
