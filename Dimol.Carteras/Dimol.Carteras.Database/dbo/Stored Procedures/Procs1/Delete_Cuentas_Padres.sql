

Create Procedure Delete_Cuentas_Padres(@ctp_codemp integer, @ctp_ctpid integer) as 
  DELETE FROM cuentas_padres_idiomas
   WHERE ( cuentas_padres_idiomas.cpi_codemp = @ctp_codemp ) AND  
         ( cuentas_padres_idiomas.cpi_ctpid = @ctp_ctpid )  

  DELETE FROM cuentas_padres  
   WHERE ( cuentas_padres.ctp_codemp = @ctp_codemp ) AND  
         ( cuentas_padres.ctp_ctpid = @ctp_ctpid )
