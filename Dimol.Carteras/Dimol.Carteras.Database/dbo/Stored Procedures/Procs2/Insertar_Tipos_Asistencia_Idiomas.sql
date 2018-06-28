

Create Procedure Insertar_Tipos_Asistencia_Idiomas(@tai_codemp integer, @tai_tipoid integer, @tai_idid integer,  @tai_nombre varchar(50)) as
  INSERT INTO tipos_asistencia_idiomas  
         ( tai_codemp,   
           tai_tipoid,   
           tai_idid,   
           tai_nombre )  
  VALUES ( @tai_codemp,   
           @tai_tipoid,   
           @tai_idid,   
           @tai_nombre )
