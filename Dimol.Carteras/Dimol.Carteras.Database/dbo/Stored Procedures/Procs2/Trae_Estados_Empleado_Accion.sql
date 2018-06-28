

Create Procedure Trae_Estados_Empleado_Accion(@eem_codemp integer, @eem_eemid integer) as
  SELECT estados_empleado.eem_accion  
    FROM estados_empleado  
   WHERE ( estados_empleado.eem_codemp = @eem_codemp ) AND  
         ( estados_empleado.eem_eemid = @eem_eemid )
