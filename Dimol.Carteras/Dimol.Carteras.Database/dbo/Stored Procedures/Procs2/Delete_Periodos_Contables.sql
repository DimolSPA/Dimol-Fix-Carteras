

Create Procedure Delete_Periodos_Contables(@pec_codemp integer, @pec_anio integer) as 
  DELETE FROM periodos_contables  
   WHERE ( periodos_contables.pec_codemp = @pec_codemp ) AND  
         ( periodos_contables.pec_anio = @pec_anio )
