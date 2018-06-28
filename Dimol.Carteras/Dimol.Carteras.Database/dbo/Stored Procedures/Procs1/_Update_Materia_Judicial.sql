Create Procedure [dbo].[_Update_Materia_Judicial](
@esj_codemp integer, 
@esj_esjid integer, 
@esj_nombre varchar (120), 
@esj_orden smallint,
@idid integer) 

as    
  UPDATE materia_judicial    
     SET esj_codemp = @esj_codemp,     
         esj_esjid = @esj_esjid,     
         esj_nombre = @esj_nombre,     
         esj_orden = @esj_orden    
   WHERE ( materia_judicial.esj_codemp = @esj_codemp )
    AND  ( materia_judicial.esj_esjid = @esj_esjid ) 
    
    
     UPDATE materia_judicial_idiomas    
     SET mji_codemp = @esj_codemp,     
         mji_esjid = @esj_esjid,     
         mji_idid = @idid,     
         mji_nombre = @esj_nombre    
   WHERE ( materia_judicial_idiomas.mji_codemp = @esj_codemp ) AND    
         ( materia_judicial_idiomas.mji_esjid = @esj_esjid ) AND    
         ( materia_judicial_idiomas.mji_idid = @idid )
