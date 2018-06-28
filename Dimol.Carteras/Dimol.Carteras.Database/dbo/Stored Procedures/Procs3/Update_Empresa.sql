

Create Procedure Update_Empresa(@emp_codemp integer, @emp_rut varchar (20), @emp_nombre varchar (200), @emp_rutrepleg varchar (20), 
                                                          @emp_replegal varchar (150), @emp_giro varchar (1000)) as
  UPDATE empresa  
     SET emp_rut = @emp_rut,   
         emp_nombre = @emp_nombre,   
         emp_rutrepleg = @emp_rutrepleg,   
         emp_replegal = @emp_replegal,   
         emp_giro = @emp_giro  
   WHERE empresa.emp_codemp = @emp_codemp
