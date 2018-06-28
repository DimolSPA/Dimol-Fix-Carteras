

Create Procedure Insertar_Ciudad(@ciu_regid integer, @ciu_ciuid integer, @ciu_nombre varchar(200), @ciu_codarea smallint) as
  INSERT INTO ciudad  
         ( ciu_regid,   
           ciu_ciuid,   
           ciu_nombre,   
           ciu_codarea )  
  VALUES ( @ciu_regid,   
           @ciu_ciuid,   
           @ciu_nombre,   
           @ciu_codarea )
