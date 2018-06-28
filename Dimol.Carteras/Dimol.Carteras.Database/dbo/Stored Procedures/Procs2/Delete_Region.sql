

Create Procedure Delete_Region(@reg_regid integer) as
    DELETE FROM region  
   WHERE region.reg_regid = @reg_regid
