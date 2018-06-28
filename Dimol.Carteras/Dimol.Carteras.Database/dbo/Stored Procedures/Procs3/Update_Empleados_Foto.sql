

Create Procedure Update_Empleados_Foto(@epl_codemp integer, @epl_emplid integer, @epl_foto image) as
  UPDATE empleados  
     SET epl_foto = @epl_foto
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_emplid = @epl_emplid )
