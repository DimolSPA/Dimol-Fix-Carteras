

Create Procedure Insertar_Despachos_Documentos(@dcd_codemp integer, @dcd_sucid integer, @dcd_dpcid numeric (15),
																@dcd_tpcid integer, @dcd_numero numeric (15), @dcd_edpid integer, 
																@dcd_fecdesp datetime, @dcd_fecent datetime, @dcd_recibido varchar (200)) as
  INSERT INTO despachos_documentos  
         ( dcd_codemp,   
           dcd_sucid,   
           dcd_dpcid,   
           dcd_tpcid,   
           dcd_numero,   
           dcd_edpid,   
           dcd_fecdesp,   
           dcd_fecent,   
           dcd_recibido )  
  VALUES ( @dcd_codemp,   
           @dcd_sucid,   
           @dcd_dpcid,   
           @dcd_tpcid,   
           @dcd_numero,   
           @dcd_edpid,   
           @dcd_fecdesp,   
           @dcd_fecent,   
           @dcd_recibido )
