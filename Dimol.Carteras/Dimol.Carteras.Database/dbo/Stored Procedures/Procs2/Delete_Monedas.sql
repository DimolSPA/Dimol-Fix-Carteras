

Create Procedure Delete_Monedas(@mon_codemp integer, @mon_codmon integer) as 
DELETE FROM monedas  
   WHERE ( monedas.mon_codemp = @mon_codemp ) AND  
         ( monedas.mon_codmon = @mon_codmon )
