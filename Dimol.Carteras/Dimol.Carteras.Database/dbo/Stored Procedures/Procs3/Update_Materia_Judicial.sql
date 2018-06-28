

Create Procedure Update_Materia_Judicial(@esj_codemp integer, @esj_esjid integer, @esj_nombre varchar (120), @esj_orden smallint) as  
  UPDATE materia_judicial  
     SET esj_codemp = @esj_codemp,   
         esj_esjid = @esj_esjid,   
         esj_nombre = @esj_nombre,   
         esj_orden = @esj_orden  
   WHERE ( materia_judicial.esj_codemp = @esj_codemp ) AND  
         ( materia_judicial.esj_esjid = @esj_esjid )
