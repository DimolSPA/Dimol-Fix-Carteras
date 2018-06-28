

Create Procedure Update_Empleados(@epl_codemp integer, @epl_emplid integer, @epl_rut varchar (20), @epl_nombre varchar (100),
                                                              @epl_apepat varchar (60), @epl_apemat varchar (60), @epl_eemid integer, @epl_comid integer,
                                                              @epl_direccion varchar (200), @epl_telefono varchar (80), @epl_celular varchar (80), @epl_mail varchar (160),
                                                              @epl_fecfin datetime,  @epl_sucid integer, @epl_usrid integer,
                                                              @epl_digito numeric (10)) as  
  UPDATE empleados  
     SET epl_rut = @epl_rut,   
         epl_nombre = @epl_nombre,   
         epl_apepat = @epl_apepat,   
         epl_apemat = @epl_apemat,   
         epl_eemid = @epl_eemid,   
         epl_comid = @epl_comid,   
         epl_direccion = @epl_direccion,   
         epl_telefono = @epl_telefono,   
         epl_celular = @epl_celular,   
         epl_mail = @epl_mail,   
         epl_fecfin = @epl_fecfin,   
         epl_sucid = @epl_sucid,   
         epl_usrid = @epl_usrid,   
         epl_digito = @epl_digito  
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_emplid = @epl_emplid )
