
CREATE PROCEDURE [dbo].[_Calculo_Remesa](
--declare
@idsConciliacion nvarchar(max) 
)
as
BEGIN
SET NOCOUNT ON;
declare
@id int,
@conciliacionId int,
@ccb_ccbid int,
@pclid int,
@ctcid int,
@numComp int,
@deudor varchar(300),
@tipo varchar(100),
@numeroDoc varchar(50),
@ccb_saldo decimal(15,2),
@CCB_INTERESES decimal(15,2),
@CCB_HONORARIOS decimal(15,2),
@RecuperadoGasto decimal(15,2),
@regionId int,
@codigoCarga int,
@diasVencido int,
@vquery nvarchar(max),
@sqlstatement nvarchar(max),
@objcursor as cursor 

DECLARE @Remesado TABLE (Id int not null, ConciliacionId int not null, Ccbid int not null, Pclid int not null, Ctcid int not null,
NumComprobante int, Deudor varchar(300),Tipo varchar(100), NumDocumento varchar(50),
Saldo Decimal(25,2) not null, Interes Decimal(15,2) not null, Honorario Decimal(15,2) not null, RecuperadoGasto Decimal(15,2) not null,
PorCapital int not null, PorInteres int not null, PorHonorario int not null,
GananciaCapital Decimal(15,2) not null, GananciaInteres Decimal(15,2) not null, GananciaHonorario Decimal(15,2),
TotalGanancia Decimal(15,2), TotalCliente Decimal(15,2), TotalAnticipo Decimal(15,2), DocumentoId int, AnticipoDebitado Decimal(15,2));
set @vquery = 'select cdi.CONCILIACION_DOCUMENTO_IMPUTADO_ID Id,
	cdi.CONCILIACION_ID ConciliacionId,
	cdi.CCBID,
	cmd.PCLID,
	cmd.CTCID,
	cmd.NUM_COMPROBANTE,
	d.CTC_NOMFANT  + ''-'' + reg.REG_NOMBRE Deudor,
	TDOC.TPC_NOMBRE TipoDocumento,
	cpbt.CCB_NUMERO,
	cdi.SALDO,
	cdi.INTERES,
	cdi.HONORARIO,
	reg.REG_REGID RegionId,
	cpbt.CCB_CODID CodigoCarga,
	datediff(day, cpbt.CCB_FECVENC, getdate()) diasVenc,
	cdi.gastopre + cdi.gastojud RecuperadoGasto
from 
CONCILIACION_DOCUMENTO_IMPUTADO cdi with(nolock)
join CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd with(nolock)
on cdi.CONCILIACION_ID = cmd.CONCILIACION_ID
join CARTOLA_MOVIMIENTOS ct with(nolock)
on cmd.MOVIMIENTO_ID = ct.MOVIMIENTO_ID
join PROVCLI cli with(nolock)
on cmd.PCLID = cli.PCL_PCLID
and ct.CODEMP = cli.PCL_CODEMP
join DEUDORES d with(nolock)
on cmd.CTCID = d.CTC_CTCID
and ct.CODEMP = d.CTC_CODEMP
join COMUNA com with(nolock)
on d.CTC_COMID = com.COM_COMID
join CIUDAD ciu with(nolock)
on com.COM_CIUID = ciu.CIU_CIUID 
join REGION reg with(nolock)
on ciu.CIU_REGID = reg.REG_REGID 
join CARTERA_CLIENTES_CPBT_DOC cpbt with(nolock)
on ct.CODEMP = cpbt.CCB_CODEMP
and cmd.PCLID = cpbt.CCB_PCLID
and cmd.CTCID = cpbt.CCB_CTCID
and cdi.CCBID = cpbt.CCB_CCBID
JOIN TIPOS_CPBTDOC TDOC with(nolock)
ON cpbt.CCB_CODEMP = TDOC.TPC_CODEMP
AND cpbt.CCB_TPCID = TDOC.TPC_TPCID
where cdi.CONCILIACION_ID in (' + CONVERT(NVARCHAR(max),@idsConciliacion) + ') order by cdi.CONCILIACION_ID '
--print (@vquery)
set @sqlstatement = 'set @cursor =  cursor forward_only static for ' + @vquery + 'open @cursor' 

exec sp_executesql
	@sqlstatement
	,N'@cursor cursor output'
	,@objcursor output


fetch next from @objcursor into @id, @conciliacionId, @ccb_ccbid, @pclid, @ctcid, @numComp,@deudor, @tipo, @numeroDoc, @ccb_saldo, @CCB_INTERESES, @CCB_HONORARIOS, @regionId, @codigoCarga, @diasVencido, @RecuperadoGasto
while (@@FETCH_STATUS = 0)
begin
	declare @getGananciaCapital int = 0, @getGananciaInteres int = 0, @getGananciahonorario int = 0,
	@capitalTotal decimal(15,2), @interesTotal decimal(15,2), @honorarioTotal decimal(15,2),  @facturarTotal decimal(15,2),
	@facturarCliente decimal(15,2)= 0, @headerDeudor varchar(1000), @documentoAnticipo decimal(15,2)=0, @documentoAnticipoId int= null,
	@anticipoDebitado decimal(15,2),@saldoAnticipo decimal(15,2), @montoFacturaAnticipo decimal(15,2);

	select top 1 @documentoAnticipoId = crd.DOCUMENTO_ID, @documentoAnticipo = crd.MONTO_FACTURAR
	from CAJA_RECEPCION_DOCUMENTOS crd
	join CAJA_CRITERIO_FACTURACION ccf
	on crd.CODEMP = ccf.CODEMP
	and crd.CRITERIO_ID = ccf.CRITERIO_ID
	and crd.NUM_FACT is not null
	and crd.CTCID = @ctcid

	if @pclid = 90
	SET @headerDeudor = @deudor + ' Anticipo: ' + replace(convert(varchar, cast(@documentoAnticipo as money), 1), '.00', '')
	else
	SET @headerDeudor = @deudor
	----CAPITAL
	EXEC @getGananciaCapital = dbo._Get_Ganancia_Capital @pclid, @diasVencido, @regionId, @codigoCarga
	----INTERES
	EXEC @getGananciaInteres = dbo._Get_Ganancia_Interes @pclid, @diasVencido, @regionId, @codigoCarga
	----HONORARIO
	EXEC @getGananciahonorario = dbo._Get_Ganancia_Honorario @pclid, @diasVencido, @regionId, @codigoCarga

	set @capitalTotal = isnull(round(CAST((@ccb_saldo * isnull(@getGananciaCapital, 0)) / 100 As Decimal(10,2)),0), 0)  
	set @interesTotal = isnull(round(CAST((@CCB_INTERESES * isnull(@getGananciaInteres, 0)) / 100 As Decimal(10,2)),0), 0)
	set @honorarioTotal = isnull(round(CAST((@CCB_HONORARIOS  * isnull(@getGananciahonorario,0)) / 100 As Decimal(10,2)),0),0)
	set @facturarTotal = @capitalTotal + @interesTotal + @honorarioTotal
	set @facturarCliente = @capitalTotal
	if (@getGananciaInteres != 100)
	begin
		set @facturarCliente = @facturarCliente + @interesTotal
	end
	if (@getGananciahonorario != 100)
	begin
		set @facturarCliente = @facturarCliente + @honorarioTotal
	end
	--ANTICIPO
	if (@facturarCliente >= @documentoAnticipo)
	begin
		set @anticipoDebitado = @documentoAnticipo
		set @saldoAnticipo = @documentoAnticipo - @anticipoDebitado
		set @montoFacturaAnticipo = @facturarCliente - @documentoAnticipo
	end
	else
	begin
		if (@facturarCliente < @documentoAnticipo)
		begin
			set @anticipoDebitado = @facturarCliente
			set @saldoAnticipo = @documentoAnticipo - @anticipoDebitado
			set @montoFacturaAnticipo = 0
		end
	end
	insert into @Remesado(Id,ConciliacionId,Ccbid, Pclid,Ctcid,NumComprobante, Deudor,Tipo, NumDocumento,
						Saldo, Interes, Honorario,RecuperadoGasto, PorCapital, PorInteres, PorHonorario,
						GananciaCapital, GananciaInteres, GananciaHonorario, TotalGanancia, TotalCliente, TotalAnticipo,
						DocumentoId, AnticipoDebitado)
	values(@id, @conciliacionId, @ccb_ccbid, @pclid, @ctcid,@numComp, @headerDeudor,@tipo, @numeroDoc,
			@ccb_saldo, @CCB_INTERESES, @CCB_HONORARIOS, @RecuperadoGasto, @getGananciaCapital,
			@getGananciaInteres,@getGananciahonorario,@capitalTotal,@interesTotal,@honorarioTotal,@facturarTotal, @facturarCliente,
			@documentoAnticipo, @documentoAnticipoId, @anticipoDebitado)

fetch next from @objcursor into @id, @conciliacionId, @ccb_ccbid, @pclid, @ctcid, @numComp,@deudor, @tipo, @numeroDoc, @ccb_saldo, @CCB_INTERESES, @CCB_HONORARIOS, @regionId, @codigoCarga, @diasVencido, @RecuperadoGasto
end

close @objcursor
deallocate @objcursor
select 
	Id ImputacionId,
	ConciliacionId,
	Ccbid, Pclid,Ctcid,NumComprobante, Deudor,Tipo, NumDocumento,
	Saldo Capital, Interes, Honorario, PorCapital, PorInteres, PorHonorario,
	GananciaCapital, GananciaInteres, GananciaHonorario, TotalGanancia,TotalCliente,
	TotalAnticipo, ISNULL(DocumentoId, 0) DocumentoId, AnticipoDebitado, RecuperadoGasto,
	Saldo + Interes + Honorario + RecuperadoGasto TotalRecuperado
FROM   @Remesado r
order by Deudor asc

END
