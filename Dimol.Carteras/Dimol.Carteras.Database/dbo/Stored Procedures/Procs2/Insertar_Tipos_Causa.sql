

Create Procedure Insertar_Tipos_Causa(@tca_codemp integer, @tca_tcaid integer, @tca_nombre varchar (500)) as  
  INSERT INTO tipos_causa  
         ( tca_codemp,   
           tca_tcaid,   
           tca_nombre )  
  VALUES ( @tca_codemp,   
           @tca_tcaid,   
           @tca_nombre )
