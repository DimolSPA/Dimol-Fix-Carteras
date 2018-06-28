

Create Procedure Update_Despachos_Documentos(@dcd_codemp integer, @dcd_sucid integer, @dcd_dpcid numeric (15),
																@dcd_tpcid integer, @dcd_numero numeric (15), @dcd_edpid integer, 
																@dcd_fecdesp datetime, @dcd_fecent datetime, @dcd_recibido varchar (200)) as
  UPDATE despachos_documentos  
     SET dcd_codemp = @dcd_codemp,   
         dcd_sucid = @dcd_sucid,   
         dcd_dpcid = @dcd_dpcid,   
         dcd_tpcid = @dcd_tpcid,   
         dcd_numero = @dcd_numero,   
         dcd_edpid = @dcd_edpid,   
         dcd_fecdesp = @dcd_fecdesp,   
         dcd_fecent = @dcd_fecent,   
         dcd_recibido = @dcd_recibido 
   WHERE ( despachos_documentos.dcd_codemp = @dcd_codemp ) AND  
         ( despachos_documentos.dcd_sucid = @dcd_sucid ) AND  
         ( despachos_documentos.dcd_dpcid = @dcd_dpcid ) AND  
         ( despachos_documentos.dcd_tpcid = @dcd_tpcid ) AND  
         ( despachos_documentos.dcd_numero = @dcd_numero )
