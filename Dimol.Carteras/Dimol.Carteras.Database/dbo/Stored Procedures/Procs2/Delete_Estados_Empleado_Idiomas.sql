

Create Procedure Delete_Estados_Empleado_Idiomas(@eei_codemp integer, @eei_eemid integer, @eei_idid integer) as  
  DELETE FROM estados_empleado_idiomas  
   WHERE ( estados_empleado_idiomas.eei_codemp = @eei_codemp ) AND  
         ( estados_empleado_idiomas.eei_eemid = @eei_eemid ) AND  
         ( estados_empleado_idiomas.eei_idid = @eei_idid )
