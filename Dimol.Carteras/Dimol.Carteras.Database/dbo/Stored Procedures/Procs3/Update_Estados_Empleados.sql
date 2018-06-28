

Create Procedure Update_Estados_Empleados(@eem_codemp integer, @eem_eemid integer, @eem_nombre varchar (40), @eem_accion char (1)) as  
  UPDATE estados_empleado  
     SET eem_nombre = @eem_nombre,   
         eem_accion = @eem_accion  
   WHERE ( estados_empleado.eem_codemp = @eem_codemp ) AND  
         ( estados_empleado.eem_eemid = @eem_eemid )
