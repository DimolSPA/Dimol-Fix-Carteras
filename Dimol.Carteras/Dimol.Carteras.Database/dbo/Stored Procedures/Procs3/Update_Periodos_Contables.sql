

Create Procedure Update_Periodos_Contables(@pec_codemp integer, @pec_anio integer, @pec_habilitado char (1), @pec_finalizado char (1)) as  
  UPDATE periodos_contables  
     SET pec_habilitado = @pec_habilitado,   
         pec_finalizado = @pec_finalizado 
   WHERE ( periodos_contables.pec_codemp = @pec_codemp ) AND  
         ( periodos_contables.pec_anio = @pec_anio )
