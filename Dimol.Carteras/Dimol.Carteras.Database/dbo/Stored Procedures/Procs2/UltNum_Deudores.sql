

Create Procedure UltNum_Deudores(@ctc_codemp integer) as
  SELECT IsNull(Max(ctc_ctcid)+1, 1) 
    FROM deudores  
   WHERE ( deudores.ctc_codemp = @ctc_codemp )
