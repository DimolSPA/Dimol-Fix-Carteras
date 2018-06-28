Create Procedure Update_Deudores_Quiebra(@ctc_codemp integer, @ctc_ctcid integer, @ctc_quiebra char(1)) as
  UPDATE deudores  
    SET ctc_quiebra = @ctc_quiebra
   WHERE ( deudores.ctc_codemp = @ctc_codemp ) AND  
         ( deudores.ctc_ctcid = @ctc_ctcid )   