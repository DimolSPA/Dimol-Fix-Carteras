

Create Procedure Delete_Periodos_Contables_Meses(@pcm_codemp integer, @pcm_anio integer, @pcm_mes numeric (2)) as 
  DELETE FROM periodos_contables_meses  
   WHERE ( periodos_contables_meses.pcm_codemp = @pcm_codemp ) AND  
         ( periodos_contables_meses.pcm_anio = @pcm_anio ) AND  
         ( periodos_contables_meses.pcm_mes = @pcm_mes )
