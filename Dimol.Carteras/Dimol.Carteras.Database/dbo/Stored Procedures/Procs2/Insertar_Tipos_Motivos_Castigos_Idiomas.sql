

Create Procedure Insertar_Tipos_Motivos_Castigos_Idiomas(@tmi_codemp integer, @tmi_tmcid integer, @tmi_idid integer, @tmi_nombre varchar(200)) as
    INSERT INTO tipos_motivos_castigos_idiomas  
         ( tmi_codemp,   
           tmi_tmcid,   
           tmi_idid,   
           tmi_nombre )  
  VALUES ( @tmi_codemp,   
           @tmi_tmcid,   
           @tmi_idid,   
           @tmi_nombre )
