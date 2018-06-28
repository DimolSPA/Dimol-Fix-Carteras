

Create Procedure Update_Estados_Empleados_Idiomas(@eei_codemp integer, @eei_eemid integer, @eei_idid integer, @eei_nombre varchar (60)) as  
  UPDATE estados_empleado_idiomas  
     SET eei_nombre = @eei_nombre  
   WHERE ( estados_empleado_idiomas.eei_codemp = @eei_codemp ) AND  
         ( estados_empleado_idiomas.eei_eemid = @eei_eemid ) AND  
         ( estados_empleado_idiomas.eei_idid = @eei_idid )
