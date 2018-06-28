

Create Procedure Insertar_Materia_Estados(@mej_codemp integer, @mej_esjid integer, @mej_estid smallint) as  
  INSERT INTO materia_estados  
         ( mej_codemp,   
           mej_esjid,   
           mej_estid )  
  VALUES ( @mej_codemp,   
           @mej_esjid,   
           @mej_estid )
