

Create Procedure Delete_Materia_Estados(@mej_codemp integer, @mej_esjid integer, @mej_estid smallint) as  
  DELETE FROM materia_estados  
   WHERE ( materia_estados.mej_codemp = @mej_codemp ) AND  
         ( materia_estados.mej_esjid = @mej_esjid ) AND  
         ( materia_estados.mej_estid = @mej_estid )
