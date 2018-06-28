

Create Procedure Delete_Deudores_Documentos(@dcd_codemp integer, @dcd_ctcid integer, @dcd_dcdid integer) as 
  DELETE FROM deudores_documentos  
   WHERE ( deudores_documentos.dcd_codemp = @dcd_codemp ) AND  
         ( deudores_documentos.dcd_ctcid = @dcd_ctcid) AND  
         ( deudores_documentos.dcd_dcdid = @dcd_dcdid )
