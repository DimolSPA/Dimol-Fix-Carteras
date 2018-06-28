

Create Procedure Insertar_Deudores_Contactos_Mail(@dcm_codemp integer, @dcm_ctcid numeric (15), @dcm_ddcid smallint,
                                                                                     @dcm_mail varchar (80), @dcm_tipo char (1), @dcm_masivo char(1)) as 
  INSERT INTO deudores_contactos_mail  
         ( dcm_codemp,   
           dcm_ctcid,   
           dcm_ddcid,   
           dcm_mail,   
           dcm_tipo,
           dcm_masivo )  
  VALUES ( @dcm_codemp,   
           @dcm_ctcid,   
           @dcm_ddcid,   
           @dcm_mail,   
           @dcm_tipo,
           @dcm_masivo )
