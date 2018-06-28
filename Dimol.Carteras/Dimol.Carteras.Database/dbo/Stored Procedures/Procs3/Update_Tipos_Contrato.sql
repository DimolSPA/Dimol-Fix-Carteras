

Create Procedure Update_Tipos_Contrato(@tic_codemp integer, @tic_ticid integer, @tic_nombre varchar (50), 
                                                                    @tic_duracion integer, @tic_indefinido char (1)) as  
  UPDATE tipos_contrato  
     SET tic_nombre = @tic_nombre,   
         tic_duracion = @tic_duracion,   
         tic_indefinido = @tic_indefinido  
   WHERE ( tipos_contrato.tic_codemp = @tic_codemp ) AND  
         ( tipos_contrato.tic_ticid = @tic_ticid )
