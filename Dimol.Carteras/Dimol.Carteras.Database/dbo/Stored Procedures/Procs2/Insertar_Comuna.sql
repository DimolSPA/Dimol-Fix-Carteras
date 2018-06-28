

Create Procedure Insertar_Comuna(@com_ciuid integer, @com_comid integer, @com_nombre varchar(200), @com_codpost varchar(20)) as
  INSERT INTO comuna  
         ( com_ciuid,   
           com_comid,   
           com_nombre,   
           com_codpost )  
  VALUES ( @com_ciuid,   
           @com_comid,   
           @com_nombre,   
           @com_codpost )
