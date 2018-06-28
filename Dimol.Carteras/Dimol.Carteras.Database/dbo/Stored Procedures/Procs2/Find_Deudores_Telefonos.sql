

Create Procedure Find_Deudores_Telefonos(@ddt_codemp integer, @ddt_ctcid integer, @ddt_numero numeric(12)) as
  SELECT count(ddt_numero)
    FROM deudores_telefonos  
   WHERE ( deudores_telefonos.ddt_codemp = @ddt_codemp ) AND  
         ( deudores_telefonos.ddt_ctcid = @ddt_ctcid ) AND  
         ( deudores_telefonos.ddt_numero = @ddt_numero )
