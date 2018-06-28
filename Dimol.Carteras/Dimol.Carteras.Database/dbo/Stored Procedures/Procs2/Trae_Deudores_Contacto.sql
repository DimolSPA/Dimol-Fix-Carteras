

Create Procedure Trae_Deudores_Contacto(@ddc_codemp integer, @ddc_ctcid integer) as
  SELECT deudores_contactos.ddc_ddcid,   
         deudores_contactos.ddc_nombre  
    FROM deudores_contactos  
   WHERE ( deudores_contactos.ddc_codemp = @ddc_codemp ) AND  
         ( deudores_contactos.ddc_ctcid = @ddc_ctcid ) AND  
         ( deudores_contactos.ddc_estado = 'A' )   
ORDER BY deudores_contactos.ddc_nombre ASC
