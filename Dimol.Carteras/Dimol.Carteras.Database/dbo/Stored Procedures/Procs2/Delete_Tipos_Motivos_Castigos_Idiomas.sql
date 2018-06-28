

Create Procedure Delete_Tipos_Motivos_Castigos_Idiomas(@tmi_codemp integer, @tmi_tmcid integer, @tmi_idid integer) as
  DELETE FROM tipos_motivos_castigos_idiomas  
   WHERE ( tipos_motivos_castigos_idiomas.tmi_codemp = @tmi_codemp ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_tmcid = @tmi_tmcid ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_idid = @tmi_idid )
