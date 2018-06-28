

Create Procedure Update_Deudores_DatosBasicos(@ctc_codemp integer, @ctc_ctcid numeric (15),  @ctc_nomfant varchar (600), @ctc_comid integer, @ctc_direccion varchar (800),  @ctc_estdir smallint) as  
  UPDATE deudores  
     SET   ctc_nomfant = @ctc_nomfant,   
         ctc_comid = @ctc_comid,   
         ctc_direccion = @ctc_direccion,   
         ctc_estdir = @ctc_estdir   
   WHERE ( deudores.ctc_codemp = @ctc_codemp ) AND  
         ( deudores.ctc_ctcid = @ctc_ctcid )
