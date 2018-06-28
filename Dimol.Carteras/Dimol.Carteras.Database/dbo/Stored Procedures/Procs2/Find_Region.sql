

Create Procedure Find_Region(@reg_regid integer) as
  SELECT Count(region.reg_regid  )
    FROM region  
   WHERE region.reg_regid = @reg_regid
