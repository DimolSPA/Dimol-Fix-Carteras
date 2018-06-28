

Create Procedure Find_Deudores_Contactos_Telefonos(@dct_codemp integer, @dct_ctcid integer, @dct_ddcid integer, @dct_numero numeric(12)) as
select count(dct_numero)
from deudores_contactos_telefonos
where dct_codemp = @dct_codemp and
           dct_ctcid = @dct_ctcid and
           dct_ddcid = @dct_ddcid and
           dct_numero = @dct_numero
