

Create Procedure Delete_Ciudad(@ciu_ciuid integer) as
  DELETE FROM ciudad  
   WHERE ciudad.ciu_ciuid = @ciu_ciuid
