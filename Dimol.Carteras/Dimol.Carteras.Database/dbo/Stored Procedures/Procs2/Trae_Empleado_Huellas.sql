

Create Procedure Trae_Empleado_Huellas(@eph_codemp integer) as
  SELECT empleados_huellas.eph_emplid,   
         empleados_huellas.eph_ephid,   
         empleados_huellas.eph_huella  
    FROM empleados_huellas  
   WHERE empleados_huellas.eph_codemp = @eph_codemp
