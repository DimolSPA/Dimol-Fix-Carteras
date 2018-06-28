

Create Procedure UltNum_Cuentas_Padres(@ctp_codemp integer) as
  SELECT IsNull(Max(ctp_ctpid)+1, 1)
    FROM cuentas_padres  
   WHERE ( cuentas_padres.ctp_codemp = @ctp_codemp )
