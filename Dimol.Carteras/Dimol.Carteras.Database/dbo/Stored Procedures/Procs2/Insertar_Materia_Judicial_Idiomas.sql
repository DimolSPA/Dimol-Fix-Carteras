

Create Procedure Insertar_Materia_Judicial_Idiomas(@mji_codemp integer, @mji_esjid integer, @mji_idid integer, @mji_nombre varchar (150)) as  
  INSERT INTO materia_judicial_idiomas  
         ( mji_codemp,   
           mji_esjid,   
           mji_idid,   
           mji_nombre )  
  VALUES ( @mji_codemp,   
           @mji_esjid,   
           @mji_idid,   
           @mji_nombre )
