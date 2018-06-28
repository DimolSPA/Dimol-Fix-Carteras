

Create Procedure Ultnum_Estados_Empleado(@eem_codemp integer) as
  SELECT IsNull(Max(estados_empleado.eem_eemid)+1, 1)
    FROM estados_empleado  
   WHERE ( estados_empleado.eem_codemp = @eem_codemp )
