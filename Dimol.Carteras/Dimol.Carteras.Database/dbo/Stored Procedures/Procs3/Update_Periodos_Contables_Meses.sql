

Create Procedure Update_Periodos_Contables_Meses(@pcm_codemp integer, @pcm_anio integer, @pcm_mes numeric (2), @pcm_inicio datetime, @pcm_fin datetime,
                                                                                         @pcm_habilitado char (1), @pcm_finalizado char (1)) as  
  UPDATE periodos_contables_meses  
     SET pcm_inicio = @pcm_inicio,   
         pcm_fin = @pcm_fin,   
         pcm_habilitado = @pcm_habilitado,   
         pcm_finalizado = @pcm_finalizado 
   WHERE ( periodos_contables_meses.pcm_codemp = @pcm_codemp ) AND  
         ( periodos_contables_meses.pcm_anio = @pcm_anio ) AND  
         ( periodos_contables_meses.pcm_mes = @pcm_mes )
