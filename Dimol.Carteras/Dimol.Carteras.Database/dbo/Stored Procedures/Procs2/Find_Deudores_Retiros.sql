

Create Procedure Find_Deudores_Retiros(@ddr_codemp integer, @ddr_ctcid numeric(15), @ddr_ddrid integer) as
  SELECT count(deudores_retiros.ddr_codemp)
    FROM deudores_retiros  
   WHERE ( deudores_retiros.ddr_codemp = @ddr_codemp ) AND  
         ( deudores_retiros.ddr_ctcid = @ddr_ctcid ) AND  
         ( deudores_retiros.ddr_ddrid = @ddr_ddrid )
