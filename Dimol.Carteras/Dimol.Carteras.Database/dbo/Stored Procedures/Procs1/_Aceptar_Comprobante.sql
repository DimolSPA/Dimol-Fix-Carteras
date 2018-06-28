-- =============================================
-- Author:		FM
-- Create date: 09-10-2014
-- Description:	Proceso aceptar comprobantes
-- =============================================
CREATE PROCEDURE [dbo].[_Aceptar_Comprobante] (@codemp int , @codsuc int, @idioma int, @tipo_documento int, @numero int, @fecha datetime, @fecha_contable datetime, @usuario int, @ip_red varchar(20), @ip_pc varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	
declare @saldo decimal(15,2)
declare @fecha_trans datetime
declare @result int = 0

set @fecha_trans = GETDATE()

---------------Busco el saldo---------------------

set @saldo = (select cbc_saldo  from cabacera_comprobantes
where cbc_codemp = @codemp
and cbc_sucid = @codsuc
and cbc_tpcid = @tipo_documento
and cbc_numero = @numero)

--------------------------Hago el Insert en la cabecera estado-------------------------------------
declare @estado_cpbt varchar(1) = 'A'

exec @result = Insertar_Cabecera_Comprobantes_Estados @codemp, @codsuc, @tipo_documento, @numero, @estado_cpbt, @fecha_trans, @usuario, @ip_pc,@ip_red,''
print(@result)

exec @result = Update_Cabecera_Comprobantes_Estado @codemp, @codsuc, @tipo_documento, @numero, @estado_cpbt, @saldo
print(@result)

exec @result = Insertar_Cabecera_Comprobantes_OP @codemp, @codsuc, @tipo_documento, @numero
print(@result)

END
