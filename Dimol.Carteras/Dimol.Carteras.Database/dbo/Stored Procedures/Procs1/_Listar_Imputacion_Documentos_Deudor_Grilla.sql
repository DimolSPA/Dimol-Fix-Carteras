CREATE PROCEDURE [dbo].[_Listar_Imputacion_Documentos_Deudor_Grilla]
(
--declare
@codemp int,
@pclid int,
@ctcid int,
@docs nvarchar(max),
@docsFinalizar nvarchar(max),
@montoHonorario decimal(15,2),
@montoInteres decimal(15,2),
@montoCapital decimal(15,2),
@montoGastoPre decimal(15,2),
@montoGastoJud decimal(15,2)
)
AS
BEGIN
SET NOCOUNT ON;
declare
@ccb_ccbid int,
@ccb_monto decimal(15,2),
@ccb_saldo decimal(15,2),
@CCB_INTERESES decimal(15,2),
@CCB_HONORARIOS decimal(15,2),
@CCB_GASTJUD decimal(15,2),
@CCB_GASTOTRO decimal(15,2),
@finalizar int=0,
@sqlstatement nvarchar(max),
@objcursor as cursor 

DECLARE @Imputado TABLE (Ccbid int, PagoCapital decimal(15,2) not null, PagoInteres decimal(15,2) not null, PagoHonorario decimal(15,2) not null, 
		DocCapital decimal(15,2) NOT NULL, DocInteres decimal(15,2) NOT NULL, DocHonorario decimal(15,2) NOT NULL, 
		CapitalDebitado decimal(15,2) not null, InteresDebitado decimal(15,2) not null, HonorarioDebitado decimal(15,2) not null, 
		RestoCapital decimal(15,2) not null, RestoInteres decimal(15,2) not null,RestoHonorario decimal(15,2) not null,
		PagoJud decimal(15,2) not null, DocJud decimal(15,2) NOT NULL, PagoJudDebitado decimal(15,2) not null, RestoPagoJud decimal(15,2) not null,
		PagoPre decimal(15,2) not null, DocPre decimal(15,2) NOT NULL, PagoPreDebitado decimal(15,2) not null, RestoPagoPre decimal(15,2) not null,
		IndicaImputado int);

set @sqlstatement = 'set @cursor =  cursor STATIC LOCAL READ_ONLY FORWARD_ONLY for 
	select  dum.CCB_CCBID, 
dum.ccb_monto, 
dum.ccb_saldo, 
dum.CCB_INTERESES, 
dum.CCB_HONORARIOS, 
dum.CCB_GASTJUD, dum.CCB_GASTOTRO, dum.Finalizar 
from
(select CCB_CCBID, ccb_monto, ccb_saldo, CCB_INTERESES, CCB_HONORARIOS, CCB_GASTJUD, CCB_GASTOTRO, 0 as Finalizar, CCB_FECVENC
			from
			CARTERA_CLIENTES_CPBT_DOC with(nolock)
			where CCB_CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
			and CCB_PCLID = ' + CONVERT(VARCHAR,@pclid) +'
			and CCB_CTCID = ' + CONVERT(VARCHAR,@ctcid) +'
			and CCB_CCBID in (' + CONVERT(NVARCHAR(max),@docs) + ') 
			and CCB_CCBID not in (' + CONVERT(NVARCHAR(max),@docsFinalizar) + ')
union 
select CCB_CCBID, ccb_monto, ccb_saldo, CCB_INTERESES, CCB_HONORARIOS, CCB_GASTJUD, CCB_GASTOTRO, 1 as Finalizar, CCB_FECVENC
			from
			CARTERA_CLIENTES_CPBT_DOC with(nolock)
			where CCB_CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
			and CCB_PCLID = ' + CONVERT(VARCHAR,@pclid) +'
			and CCB_CTCID = ' + CONVERT(VARCHAR,@ctcid) +'
			and CCB_CCBID in (' + CONVERT(NVARCHAR(max),@docsFinalizar) + '))dum
order by dum.CCB_FECVENC asc open @cursor' 

exec sp_executesql
	@sqlstatement
	,N'@cursor cursor output'
	,@objcursor output


fetch next from @objcursor into @ccb_ccbid, @ccb_monto, @ccb_saldo, @CCB_INTERESES, @CCB_HONORARIOS, @CCB_GASTJUD, @CCB_GASTOTRO, @finalizar
while (@@FETCH_STATUS = 0)
begin
	declare @balanceCapital decimal(15,2) = 0, @balanceHonorario decimal(15,2) = 0, @balanceInteres decimal(15,2) = 0,
	@capitalDebitado decimal(15,2), @capitalTotal decimal(15,2),
	@interesDebitado decimal(15,2), @interesTotal decimal(15,2),
	@honorarioDebitado decimal(15,2), @honorarioTotal decimal(15,2),
	@balanceGastPre decimal(15,2) = 0, @GastPreDebitado decimal(15,2), @GastPreTotal decimal(15,2),
	@balanceGastJud decimal(15,2) = 0, @GastJudDebitado decimal(15,2), @GastJudTotal decimal(15,2),
	@indicaImputado int = 0;
	--CAPITAL
	IF (@finalizar = 0)
	BEGIN
		if (@montoCapital >= @ccb_saldo)
		begin
			set @balanceCapital = @montoCapital
			if @ccb_saldo = 0
				begin set @capitalDebitado = @ccb_saldo end
			else
				begin set @capitalDebitado = @ccb_saldo -1 end
			set @capitalTotal = @ccb_saldo - @capitalDebitado
			if @ccb_saldo = 0
				begin set @montoCapital = @montoCapital - @ccb_saldo end
			else
				begin set @montoCapital = @montoCapital - @ccb_saldo + 1 end
		end
		else
		begin
			if (@montoCapital < @ccb_saldo)
			begin
				set @balanceCapital = @montoCapital
				set @capitalDebitado = @montoCapital
				set @capitalTotal = @ccb_saldo - @capitalDebitado
				set @montoCapital = 0
			end
		end
	END
	ELSE
	BEGIN
		if (@montoCapital >= @ccb_saldo)
		begin
			set @balanceCapital = @montoCapital
			set @capitalDebitado = @ccb_saldo
			set @capitalTotal = @ccb_saldo - @capitalDebitado
			set @montoCapital = @montoCapital - @ccb_saldo
		
		end
		else
		begin
			if (@montoCapital < @ccb_saldo)
			begin
				set @balanceCapital = @montoCapital
				set @capitalDebitado = @montoCapital
				set @capitalTotal = @ccb_saldo - @capitalDebitado
				set @montoCapital = 0
			end
		end
	END
	--INTERES
	if (@montoInteres >= @CCB_INTERESES)
	begin
		set @balanceInteres = @montoInteres
		set @interesDebitado = @CCB_INTERESES
		set @interesTotal = @CCB_INTERESES - @interesDebitado
		set @montoInteres = @montoInteres - @CCB_INTERESES
	end
	else
	begin
		if (@montoInteres < @CCB_INTERESES)
		begin
			set @balanceInteres = @montoInteres
			set @interesDebitado = @montoInteres
			set @interesTotal = @CCB_INTERESES - @interesDebitado
			set @montoInteres = 0
		end
	end
	--HONORARIO
	if (@montoHonorario >= @CCB_HONORARIOS)
	begin
		set @balanceHonorario = @montoHonorario
		set @honorarioDebitado = @CCB_HONORARIOS
		set @honorarioTotal = @CCB_HONORARIOS - @honorarioDebitado
		set @montoHonorario = @montoHonorario - @CCB_HONORARIOS
	end
	else
	begin
		if (@montoHonorario < @CCB_HONORARIOS)
		begin
			set @balanceHonorario = @montoHonorario
			set @honorarioDebitado = @montoHonorario
			set @honorarioTotal = @CCB_HONORARIOS - @honorarioDebitado
			set @montoHonorario = 0
		end
	end
	--GASTOS JUDICIALES
	if (@montoGastoJud >= @CCB_GASTJUD)
	begin
		set @balanceGastJud = @montoGastoJud
		set @GastJudDebitado = @CCB_GASTJUD
		set @GastJudTotal = @CCB_GASTJUD - @GastJudDebitado
		set @montoGastoJud = @montoGastoJud - @CCB_GASTJUD
	end
	else
	begin
		if (@montoGastoJud < @CCB_GASTJUD)
		begin
			set @balanceGastJud = @montoGastoJud
			set @GastJudDebitado = @montoGastoJud
			set @GastJudTotal = @CCB_GASTJUD - @GastJudDebitado
			set @montoGastoJud = 0
		end
	end
	--GASTO PREJUDICIALES
	if (@montoGastoPre >= @CCB_GASTOTRO)
	begin
		set @balanceGastPre = @montoGastoPre
		set @GastPreDebitado = @CCB_GASTOTRO
		set @GastPreTotal = @CCB_GASTOTRO - @GastPreDebitado
		set @montoGastoPre = @montoGastoPre - @CCB_GASTOTRO
	end
	else
	begin
		if (@montoGastoPre < @CCB_GASTOTRO)
		begin
			set @balanceGastPre = @montoGastoPre
			set @GastPreDebitado = @montoGastoPre
			set @GastPreTotal = @CCB_GASTOTRO - @GastPreDebitado
			set @montoGastoPre = 0
		end
	end
	--PRINT 'doc: ' + Cast(@ccb_ccbid AS VARCHAR) +
	--	' Saldo: ' + Cast(@ccb_saldo AS VARCHAR)
	if (@capitalDebitado > 0 or @interesDebitado > 0 or @honorarioDebitado > 0 or @GastPreDebitado > 0 or @GastJudDebitado > 0)
	begin
		set @indicaImputado = 1
	end
	INSERT INTO @Imputado (Ccbid,PagoCapital, PagoInteres, PagoHonorario,
						DocCapital, DocInteres, DocHonorario, 
						CapitalDebitado, InteresDebitado, HonorarioDebitado, 
						RestoCapital, RestoInteres, RestoHonorario,
						PagoJud, DocJud, PagoJudDebitado, RestoPagoJud,
						PagoPre, DocPre, PagoPreDebitado, RestoPagoPre,
						IndicaImputado)
    VALUES (@ccb_ccbid, @balanceCapital, @balanceInteres, @balanceHonorario,
			@ccb_saldo, @CCB_INTERESES, @CCB_HONORARIOS, 
			@capitalDebitado, @interesDebitado, @honorarioDebitado, 
			@capitalTotal, @interesTotal, @honorarioTotal,
			@balanceGastJud, @CCB_GASTJUD,  @GastJudDebitado, @GastJudTotal,
			@balanceGastPre, @CCB_GASTOTRO, @GastPreDebitado, @GastPreTotal,
			@indicaImputado);
fetch next from @objcursor into @ccb_ccbid, @ccb_monto, @ccb_saldo, @CCB_INTERESES, @CCB_HONORARIOS, @CCB_GASTJUD, @CCB_GASTOTRO, @finalizar
end

close @objcursor
deallocate @objcursor

SELECT 
	cpbt.ccb_pclid Pclid,
	cpbt.ccb_ctcid Ctcid,
	s.Ccbid,
	sbc_nombre Asegurado,
	cpbt.tci_nombre TipoDocumento, 
	RIGHT(cpbt.CCB_NUMERO, LEN(cpbt.CCB_NUMERO+'a') -PATINDEX('%[^0 ]%', cpbt.CCB_NUMERO + 'a' )) Numero,
	case cpbt.CCB_ESTCPBT
	when 'J' then 'JUDICIAL'
	when 'F' then 'FINALIZADO'
	when 'V' then 'VIGENTE'
	when 'X' then 'NULO'
    end  Estado,
	ccb_fecvenc FechaVencimiento,
    mon_nombre Moneda,
	ccb_monto Monto,
	s.RestoCapital Saldo,
	s.CapitalDebitado,
	s.RestoInteres Intereses,
	s.InteresDebitado,
	s.RestoHonorario Honorarios,
	s.HonorarioDebitado,
	s.RestoPagoJud GastoJudicial,
	s.PagoJudDebitado,
	s.RestoPagoPre GastoPrejudicial,
	s.PagoPreDebitado,
	s.RestoCapital + s.RestoHonorario + s.RestoInteres + s.RestoPagoJud + s.RestoPagoPre as TotalDeuda,
	s.IndicaImputado
FROM   @Imputado s
Join cartera_clientes_documentos_cpbt_doc cpbt
on s.Ccbid = cpbt.CCB_CCBID
where cpbt.CCB_CODEMP = @codemp
and cpbt.CCB_PCLID = @pclid
and cpbt.CCB_CTCID = @ctcid
order by cpbt.CCB_FECVENC asc;

END