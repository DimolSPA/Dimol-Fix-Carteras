-- =============================================
-- Author:		FM
-- Create date: 09-10-2014
-- Description:	Proceso aceptar comprobantes
-- =============================================
CREATE PROCEDURE [dbo].[_Facturar_Comprobante] (@codemp int , @codsuc int, @tipo_documento int, @numero int, @usuario int, @ip_red varchar(20), @ip_pc varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	
declare @saldo decimal(15,2)

DECLARE @estado_cpbt varchar(1) = 'F'

declare @fecha_trans datetime


set @fecha_trans = GETDATE()

---------------Busco el saldo---------------------

set @saldo = (select cbc_saldo  from cabacera_comprobantes
where cbc_codemp = @codemp
and cbc_sucid = @codsuc
and cbc_tpcid = @tipo_documento
and cbc_numero = @numero)

--------------------------Hago el Insert en la cabecera estado-------------------------------------
exec Insertar_Cabecera_Comprobantes_Estados @codemp, @codsuc, @tipo_documento, @numero, @estado_cpbt, @fecha_trans, @usuario, @ip_pc,@ip_red,''
--select 'Insertar_Cabecera_Comprobantes_Estados ',@codemp, @codsuc, @tipo_documento, @numero, @estado_cpbt, getdate(), @usuario, @ip_pc,@ip_red,''

exec Update_Cabecera_Comprobantes_Estado @codemp, @codsuc, @tipo_documento, @numero, @estado_cpbt, @saldo
--select 'Update_Cabecera_Comprobantes_Estado',@codemp, @codsuc, @tipo_documento, @numero, @estado_cpbt, @saldo
           
exec Insertar_Cabecera_Comprobantes_OP @codemp, @codsuc, @tipo_documento, @numero
--select 'Insertar_Cabecera_Comprobantes_OP',@codemp, @codsuc, @tipo_documento, @numero


END
