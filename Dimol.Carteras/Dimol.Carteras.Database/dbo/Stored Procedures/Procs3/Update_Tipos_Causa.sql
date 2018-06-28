

Create Procedure Update_Tipos_Causa(@tca_codemp integer, @tca_tcaid integer, @tca_nombre varchar (500)) as  
  UPDATE tipos_causa  
     SET tca_codemp = @tca_codemp,   
         tca_tcaid = @tca_tcaid,   
         tca_nombre = @tca_nombre  
   WHERE ( tipos_causa.tca_codemp = @tca_codemp ) AND  
         ( tipos_causa.tca_tcaid = @tca_tcaid )
