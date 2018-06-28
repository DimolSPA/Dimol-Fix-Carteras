

Create Procedure UltNum_Deudores_Documentos(@dcd_codemp integer, @dcd_ctcid integer) as
  SELECT IsNull(Max(dcd_dcdid)+1, 1)
    FROM deudores_documentos  
   WHERE ( deudores_documentos.dcd_codemp = @dcd_codemp ) AND  
         ( deudores_documentos.dcd_ctcid = @dcd_ctcid )
