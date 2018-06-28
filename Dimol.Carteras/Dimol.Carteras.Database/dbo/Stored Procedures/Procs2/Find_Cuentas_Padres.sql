

Create Procedure Find_Cuentas_Padres(@ctp_codemp integer, @ctp_ctpid integer) as
  SELECT count(cuentas_padres.ctp_ctpid)  
    FROM cuentas_padres  
   WHERE ( cuentas_padres.ctp_codemp = @ctp_codemp ) AND  
         ( cuentas_padres.ctp_ctpid = @ctp_ctpid )
