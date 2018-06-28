create procedure [dbo].[_Listar_Cuentas_Provcli_Banco] (@codemp int, @pclid int, @tipo int) as 
 select CUENTA
 from PROVCLI_BANCO
 WHERE CODEMP = @codemp and
 PCLID = @pclid and
 IDBANCO = @tipo
