

Create Procedure Delete_Materia_Judicial(@esj_codemp integer, @esj_esjid integer) as  

  DELETE FROM materia_judicial_idiomas  
   WHERE ( materia_judicial_idiomas.mji_codemp = @esj_codemp ) AND  
         ( materia_judicial_idiomas.mji_esjid = @esj_esjid )   

  DELETE FROM materia_judicial  
   WHERE ( materia_judicial.esj_codemp = @esj_codemp ) AND  
         ( materia_judicial.esj_esjid = @esj_esjid )
