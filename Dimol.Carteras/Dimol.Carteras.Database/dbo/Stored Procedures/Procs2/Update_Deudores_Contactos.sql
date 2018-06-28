

Create Procedure Update_Deudores_Contactos(@ddc_codemp integer, @ddc_ctcid numeric (15), @ddc_ddcid smallint, @ddc_ticid integer,
                                                                             @ddc_nombre varchar (250), @ddc_comid integer, @ddc_direccion varchar (800), @ddc_estdir integer, @ddc_estado char(1)) as  
  UPDATE deudores_contactos  
     SET ddc_ticid = @ddc_ticid,   
         ddc_nombre = @ddc_nombre,   
         ddc_comid = @ddc_comid,   
         ddc_direccion = @ddc_direccion,
         ddc_estdir = @ddc_estdir,
         ddc_estado = @ddc_estado  
   WHERE ( deudores_contactos.ddc_codemp = @ddc_codemp ) AND  
         ( deudores_contactos.ddc_ctcid = @ddc_ctcid ) AND  
         ( deudores_contactos.ddc_ddcid = @ddc_ddcid )
