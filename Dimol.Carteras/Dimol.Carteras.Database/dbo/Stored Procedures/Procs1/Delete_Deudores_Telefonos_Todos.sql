

Create Procedure Delete_Deudores_Telefonos_Todos(@ddt_codemp integer, @ddt_ctcid numeric (15), @ddt_numero integer) as  
  DELETE FROM deudores_telefonos  
   WHERE ( deudores_telefonos.ddt_codemp = @ddt_codemp ) AND  
         ( deudores_telefonos.ddt_ctcid = @ddt_ctcid ) AND  
         ( deudores_telefonos.ddt_numero = @ddt_numero )   

  DELETE FROM deudores_contactos_telefonos  
   WHERE ( deudores_contactos_telefonos.dct_codemp = @ddt_codemp ) AND  
         ( deudores_contactos_telefonos.dct_ctcid = @ddt_ctcid ) AND  
         ( deudores_contactos_telefonos.dct_numero = @ddt_numero )
