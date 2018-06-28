CREATE PROCEDURE [dbo].[_Rebajar_Conciliacion_Documento_Cartera]
(
--declare
@codemp int,
@pclid int,
@ctcid int,
@ccbid int,
@montoHonorario decimal(15,2),
@montoInteres decimal(15,2),
@montoCapital decimal(15,2),
@montoGastoPre decimal(15,2),
@montoGastoJud decimal(15,2),
@userId int)
as
begin
	
	if (@montoHonorario > 0)
	begin
		update CARTERA_CLIENTES_CPBT_DOC
		set ccb_honorarios = ccb_honorarios- @montoHonorario
		where ccb_codemp = @codemp 
		and ccb_pclid = @pclid 
		and ccb_ctcid = @ctcid
		and ccb_ccbid = @ccbid
	end
	if (@montoInteres > 0)
	begin
		update CARTERA_CLIENTES_CPBT_DOC
		set ccb_intereses = ccb_intereses - @montoInteres
		where ccb_codemp = @codemp 
		and ccb_pclid = @pclid 
		and ccb_ctcid = @ctcid
		and ccb_ccbid = @ccbid
	end
	if (@montoCapital > 0)
	begin
		update CARTERA_CLIENTES_CPBT_DOC
		set ccb_saldo = ccb_saldo - @montoCapital
		where ccb_codemp = @codemp 
		and ccb_pclid = @pclid 
		and ccb_ctcid = @ctcid
		and ccb_ccbid = @ccbid
	end
	if (@montoGastoPre > 0)
	begin
		update CARTERA_CLIENTES_CPBT_DOC
		set ccb_gastotro = ccb_gastotro - @montoGastoPre
		where ccb_codemp = @codemp 
		and ccb_pclid = @pclid 
		and ccb_ctcid = @ctcid
		and ccb_ccbid = @ccbid
	end
	if (@montoGastoJud > 0)
	begin
		update CARTERA_CLIENTES_CPBT_DOC
		set ccb_gastjud = ccb_gastjud - @montoGastoJud
		where ccb_codemp = @codemp 
		and ccb_pclid = @pclid 
		and ccb_ctcid = @ctcid
		and ccb_ccbid = @ccbid
	end
end
