create Procedure [dbo].[_Insertar_Deudores_Contactos_Telefonos](@dct_codemp integer, @dct_ctcid numeric (15), @dct_ddcid smallint,
                                                                                              @dct_numero numeric(12), @dct_tipo char (1), @dct_estado char (1)) as 

declare @existe int = 0

select @existe = count(dct_ddcid)
from deudores_contactos_telefonos
where dct_codemp = @dct_codemp and
           dct_ctcid = @dct_ctcid and
           dct_ddcid = @dct_ddcid and
           dct_numero = @dct_numero
if(@existe = 0 )
begin  
  INSERT INTO deudores_contactos_telefonos  
         ( dct_codemp,   
           dct_ctcid,   
           dct_ddcid,   
           dct_numero,   
           dct_tipo,   
           dct_estado,
           dct_prioridad )  
  VALUES ( @dct_codemp,   
           @dct_ctcid,   
           @dct_ddcid,   
           @dct_numero,   
           @dct_tipo,   
           @dct_estado,
           4 )
end
else
begin
  UPDATE deudores_contactos_telefonos  
     SET dct_tipo = @dct_tipo,   
         dct_estado = @dct_estado  
   WHERE ( deudores_contactos_telefonos.dct_codemp = @dct_codemp ) AND  
         ( deudores_contactos_telefonos.dct_ctcid = @dct_ctcid ) AND  
         ( deudores_contactos_telefonos.dct_ddcid = @dct_ddcid ) AND  
         ( deudores_contactos_telefonos.dct_numero = @dct_numero )
end

