

Create Procedure Update_Materia_Estados(@mej_codemp integer, @mej_esjid integer, @mej_estid smallint) as  
  UPDATE materia_estados  
     SET mej_codemp = @mej_codemp,   
         mej_esjid = @mej_esjid,   
         mej_estid = @mej_estid  
   WHERE ( materia_estados.mej_codemp = @mej_codemp ) AND  
         ( materia_estados.mej_esjid = @mej_esjid ) AND  
         ( materia_estados.mej_estid = @mej_estid )
