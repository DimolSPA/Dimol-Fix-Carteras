

Create Procedure Find_Estados_Empleado(@eem_codemp integer, @eem_eemid integer) as
  SELECT count(estados_empleado.eem_eemid)  
    FROM estados_empleado  
   WHERE ( estados_empleado.eem_codemp = @eem_codemp ) AND  
         ( estados_empleado.eem_eemid = @eem_eemid )
