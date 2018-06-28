

Create Procedure Insertar_ProvCli_Sucursal_Contacto(@psc_codemp integer, @psc_pclid numeric (15), @psc_pcsid integer, @psc_pscid integer,
                                                                                        @psc_ticid integer, @psc_nombre varchar (250), @psc_telefono varchar (100), @psc_anexo varchar (10),
                                                                                        @psc_fax varchar (100), @psc_celular varchar (100), @psc_mail varchar (120)) as
  INSERT INTO provcli_sucursal_contacto  
         ( psc_codemp,   
           psc_pclid,   
           psc_pcsid,   
           psc_pscid,   
           psc_ticid,   
           psc_nombre,   
           psc_telefono,   
           psc_anexo,   
           psc_fax,   
           psc_celular,   
           psc_mail )  
  VALUES ( @psc_codemp,   
           @psc_pclid,   
           @psc_pcsid,   
           @psc_pscid,   
           @psc_ticid,   
           @psc_nombre,   
           @psc_telefono,   
           @psc_anexo,   
           @psc_fax,   
           @psc_celular,   
           @psc_mail )
