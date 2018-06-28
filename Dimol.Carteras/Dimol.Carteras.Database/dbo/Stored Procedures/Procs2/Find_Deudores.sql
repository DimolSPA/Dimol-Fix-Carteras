

Create Procedure Find_Deudores(@ctc_codemp integer, @ctc_ctcid integer) as
  SELECT count(deudores.ctc_ctcid  )
    FROM deudores  
   WHERE ( deudores.ctc_codemp = @ctc_codemp ) AND  
         ( deudores.ctc_ctcid = @ctc_ctcid )
