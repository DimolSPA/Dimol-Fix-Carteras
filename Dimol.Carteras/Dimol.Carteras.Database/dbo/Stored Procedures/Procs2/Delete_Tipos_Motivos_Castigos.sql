

Create Procedure Delete_Tipos_Motivos_Castigos(@tmi_codemp integer, @tmi_tmcid integer) as
  DELETE FROM tipos_motivos_castigos_idiomas  
   WHERE ( tipos_motivos_castigos_idiomas.tmi_codemp = @tmi_codemp ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_tmcid = @tmi_tmcid ) 


  DELETE FROM tipos_motivos_castigos  
   WHERE ( tipos_motivos_castigos.tmc_codemp = @tmi_codemp ) AND  
         ( tipos_motivos_castigos.tmc_tmcid = @tmi_tmcid )
