

Create Procedure Update_Tipos_Motivos_Castigos_Idiomas(@tmi_codemp integer, @tmi_tmcid integer, @tmi_idid integer, @tmi_nombre varchar(200)) as
   UPDATE tipos_motivos_castigos_idiomas  
     SET tmi_nombre = @tmi_nombre  
   WHERE ( tipos_motivos_castigos_idiomas.tmi_codemp = @tmi_codemp ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_tmcid = @tmi_tmcid ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_idid = @tmi_idid )
