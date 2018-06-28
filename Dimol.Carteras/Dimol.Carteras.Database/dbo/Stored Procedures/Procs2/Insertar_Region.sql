

Create Procedure Insertar_Region(@reg_paiid integer, @reg_regid integer, @reg_nombre varchar(200), @reg_orden smallint) as
  INSERT INTO region  
         ( reg_paiid,   
           reg_regid,   
           reg_nombre,   
           reg_orden )  
  VALUES ( @reg_paiid,   
           @reg_regid,   
           @reg_nombre,   
           @reg_orden )
