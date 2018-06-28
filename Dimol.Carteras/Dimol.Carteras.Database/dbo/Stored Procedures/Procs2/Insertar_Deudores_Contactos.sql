

Create Procedure Insertar_Deudores_Contactos(@ddc_codemp integer, @ddc_ctcid numeric (15), @ddc_ddcid smallint, @ddc_ticid integer,
                                                                            @ddc_nombre varchar (250), @ddc_comid integer, @ddc_direccion varchar (800)) as 
  INSERT INTO deudores_contactos  
         ( ddc_codemp,   
           ddc_ctcid,   
           ddc_ddcid,   
           ddc_ticid,   
           ddc_nombre,   
           ddc_comid,   
           ddc_direccion,   
           ddc_fecing,
           ddc_estdir,
           ddc_estado   )  
  VALUES ( @ddc_codemp,   
           @ddc_ctcid,   
           @ddc_ddcid,   
           @ddc_ticid,   
           @ddc_nombre,   
           @ddc_comid,   
           @ddc_direccion,   
           getdate(),
           1,
           'A' )
