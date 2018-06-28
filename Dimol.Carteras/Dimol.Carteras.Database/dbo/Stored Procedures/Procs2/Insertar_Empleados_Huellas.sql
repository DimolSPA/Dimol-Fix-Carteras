

Create Procedure Insertar_Empleados_Huellas(@eph_codemp integer, @eph_emplid integer, @eph_ephid integer, @eph_huella varchar(8000)) as


  DELETE FROM empleados_huellas  
   WHERE ( empleados_huellas.eph_codemp = @eph_codemp ) AND  
         ( empleados_huellas.eph_emplid = @eph_emplid and eph_ephid = @eph_ephid )   
           

  INSERT INTO empleados_huellas  
         ( eph_codemp,   
           eph_emplid,   
           eph_ephid,   
           eph_huella )  
  VALUES ( @eph_codemp,   
           @eph_emplid,   
           @eph_ephid,   
           @eph_huella )
