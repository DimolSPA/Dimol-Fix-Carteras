

Create Procedure UltNum_Servicios(@sve_codemp integer) as
  SELECT IsNull(Max(sve_sveid)+1, 1)
    FROM servicios  
   WHERE ( servicios.sve_codemp = @sve_codemp )
