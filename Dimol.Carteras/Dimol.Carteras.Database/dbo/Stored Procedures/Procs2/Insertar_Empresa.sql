

Create Procedure Insertar_Empresa(@emp_codemp integer, @emp_rut varchar(20), @emp_nombre varchar(400), @emp_rutrepleg varchar(20), @emp_replegal varchar(200)) as

  INSERT INTO empresa  
         ( emp_codemp,   
           emp_rut,   
           emp_nombre,   
           emp_rutrepleg,   
           emp_replegal,   
           emp_giro,   
           emp_logo )  
  VALUES ( @emp_codemp,   
           @emp_rut,   
           @emp_nombre,   
           @emp_rutrepleg,   
           @emp_replegal,   
           null,   
           null )
