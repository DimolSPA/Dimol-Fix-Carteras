

Create Procedure Find_Ciudad(@ciu_ciuid integer) aS
  SELECT count(ciudad.ciu_ciuid)  
    FROM ciudad  
   WHERE ciudad.ciu_ciuid = @ciu_ciuid
