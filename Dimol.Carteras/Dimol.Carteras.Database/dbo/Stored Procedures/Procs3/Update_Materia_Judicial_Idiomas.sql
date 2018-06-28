

Create Procedure Update_Materia_Judicial_Idiomas(@mji_codemp integer, @mji_esjid integer, @mji_idid integer, @mji_nombre varchar (150)) as  
  UPDATE materia_judicial_idiomas  
     SET mji_codemp = @mji_codemp,   
         mji_esjid = @mji_esjid,   
         mji_idid = @mji_idid,   
         mji_nombre = @mji_nombre  
   WHERE ( materia_judicial_idiomas.mji_codemp = @mji_codemp ) AND  
         ( materia_judicial_idiomas.mji_esjid = @mji_esjid ) AND  
         ( materia_judicial_idiomas.mji_idid = @mji_idid )
