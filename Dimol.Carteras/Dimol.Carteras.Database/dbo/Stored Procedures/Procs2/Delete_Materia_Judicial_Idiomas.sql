

Create Procedure Delete_Materia_Judicial_Idiomas(@mji_codemp integer, @mji_esjid integer, @mji_idid integer) as  
  DELETE FROM materia_judicial_idiomas  
   WHERE ( materia_judicial_idiomas.mji_codemp = @mji_codemp ) AND  
         ( materia_judicial_idiomas.mji_esjid = @mji_esjid ) AND  
         ( materia_judicial_idiomas.mji_idid = @mji_idid )
