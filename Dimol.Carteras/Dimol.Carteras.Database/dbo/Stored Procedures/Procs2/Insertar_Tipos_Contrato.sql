

Create Procedure Insertar_Tipos_Contrato(@tic_codemp integer, @tic_ticid integer, @tic_nombre varchar(50), @tic_duracion integer, @tic_indefinido char(1)) as
   INSERT INTO tipos_contrato  
         ( tic_codemp,   
           tic_ticid,   
           tic_nombre,   
           tic_duracion,   
           tic_indefinido )  
  VALUES ( @tic_codemp,   
           @tic_ticid,   
           @tic_nombre,   
           @tic_duracion,   
           @tic_indefinido )
