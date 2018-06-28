

Create Procedure Update_ProvCli_Sucursal_Contacto(@psc_codemp integer, @psc_pclid numeric (15), @psc_pcsid integer, @psc_pscid integer,
                                                                                       @psc_ticid integer, @psc_nombre varchar (250), @psc_telefono varchar (100), @psc_anexo varchar (10),
                                                                                       @psc_fax varchar (100), @psc_celular varchar (100), @psc_mail varchar (120)) as
  UPDATE provcli_sucursal_contacto  
     SET psc_ticid = @psc_ticid,   
         psc_nombre = @psc_nombre,   
         psc_telefono = @psc_telefono,   
         psc_anexo = @psc_anexo,   
         psc_fax = @psc_fax,   
         psc_celular = @psc_celular,   
         psc_mail = @psc_mail  
   WHERE ( provcli_sucursal_contacto.psc_codemp = @psc_codemp ) AND  
         ( provcli_sucursal_contacto.psc_pclid = @psc_pclid ) AND  
         ( provcli_sucursal_contacto.psc_pcsid = @psc_pcsid ) AND  
         ( provcli_sucursal_contacto.psc_pscid = @psc_pscid )
