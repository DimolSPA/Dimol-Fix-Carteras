

Create Procedure Find_Deudores_Contactos_TipNom(@ddc_codemp integer, @ddc_ctcid numeric(15), @ddc_ticid integer, @ddc_nombre varchar(200)) as
  SELECT deudores_contactos.ddc_ddcid  
    FROM deudores_contactos  
   WHERE ( deudores_contactos.ddc_codemp = @ddc_codemp ) AND  
         ( deudores_contactos.ddc_ctcid = @ddc_ctcid ) AND  
         ( deudores_contactos.ddc_ticid = @ddc_ticid ) AND  
         ( deudores_contactos.ddc_nombre = @ddc_nombre )
