

CREATE Procedure [dbo].[Update_Deudores_Telefonos_Prioridad](@ddt_codemp integer, @ddt_ctcid numeric(15), @ddt_numero bigint, @ddt_estado char(1), @ddt_prioridad smallint) as
  UPDATE deudores_telefonos  
     SET ddt_estado = @ddt_estado,   
         ddt_prioridad = @ddt_prioridad  
   WHERE ( deudores_telefonos.ddt_codemp = @ddt_codemp ) AND  
         ( deudores_telefonos.ddt_ctcid = @ddt_ctcid ) AND  
         ( deudores_telefonos.ddt_numero = @ddt_numero )   


  UPDATE deudores_contactos_telefonos  
     SET dct_estado = @ddt_estado,   
         dct_prioridad = @ddt_prioridad  
   WHERE ( deudores_contactos_telefonos.dct_codemp = @ddt_codemp ) AND  
         ( deudores_contactos_telefonos.dct_ctcid =@ddt_ctcid  ) AND  
         ( deudores_contactos_telefonos.dct_numero = @ddt_numero )
