

CREATE Procedure [dbo].[Update_Deudores_Telefonos](@ddt_codemp integer, @ddt_ctcid numeric (15), @ddt_numero bigint,
                                                                             @ddt_tipo char (1), @ddt_estado char (1)) as  
  UPDATE deudores_telefonos  
     SET ddt_tipo = @ddt_tipo,   
         ddt_estado = @ddt_estado  
   WHERE ( deudores_telefonos.ddt_codemp = @ddt_codemp ) AND  
         ( deudores_telefonos.ddt_ctcid = @ddt_ctcid ) AND  
         ( deudores_telefonos.ddt_numero = @ddt_numero )
