

Create Procedure Update_Region(@reg_paiid integer, @reg_regid integer, @reg_nombre varchar(150), @reg_orden smallint) as
  UPDATE region  
     SET reg_paiid = reg_paiid,   
         reg_nombre = @reg_nombre,   
         reg_orden = reg_orden  
   WHERE region.reg_regid = @reg_regid
