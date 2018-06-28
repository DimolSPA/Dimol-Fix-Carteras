

Create Procedure Update_Ciudad(@ciu_regid integer, @ciu_ciuid integer, @ciu_nombre varchar(200), @ciu_codarea smallint) as
  UPDATE ciudad  
     SET ciu_regid = @ciu_regid,   
         ciu_nombre = @ciu_nombre,   
         ciu_codarea = @ciu_codarea  
   WHERE ciudad.ciu_ciuid = @ciu_ciuid
