CREATE procedure [dbo].[Calculo_Interes_Reajuste_Gasto_Mutual_Ley] as

insert into LOG_ERROR values (GETDATE(), 'Calculo Interes, Reajuste y gasto Mutual Ley', 'Comienzo', 'Job 2 AM',0 )

declare @fecha datetime = getdate(),
@CODEMP int = 1, 
@PCLID int, 
@CTCID int,
@CCBID int, 
@TPCID int, 
@TIPCART int, 
@CODMON int,
@FECCALCINT datetime, 
@FECVENC datetime, 
@FECING datetime,
@FECdoc datetime,
@ASIGNADO decimal(15,2), 
@CALCHON varchar(1),
@CCTID int,
@ESTCPBT varchar(1),
@SALDO decimal(15,2)

declare 
@interes int,  
@reajuste int =0 ,   
@gasto decimal(15,4) ,
@uf_dia decimal(15,4),
@total_uf decimal(15,4)
                

-----------Valores Basicos-------------------------
declare cur cursor for 
select  CCB_PCLID, CCB_CTCID,CCB_CCBID,  ccb_fecdoc, CCB_SALDO
from CARTERA_CLIENTES_CPBT_DOC   with (nolock)    
where CCB_ESTCPBT in ('V')
and CCB_CODEMP = @CODEMP   
and ccb_pclid = 559-- and ccb_numero = '17139481' 
order by CCB_FECDOC asc
open cur

fetch next from cur into  @PCLID, @CTCID, @CCBID,@FECdoc, @SALDO 
while (@@FETCH_STATUS = 0)
begin
		--print(@SALDO)
		set @gasto = 0
		select  @interes = round(isnull(dbo._trae_interes_mutual_ley (@FECdoc),0) *@saldo / 100,0)
		select  @reajuste = round(isnull(dbo._trae_reajuste_mutual_ley (@FECdoc),0) *(@saldo) /100,0)
		select @uf_dia = mnv_valor from MONEDAS_VALORES where MNV_CODMON = 2 and MNV_FECHA = CAST(convert(varchar(10),GETDATE(),112) as DATE)
		--print(@SALDO + @interes)
		set @total_uf = (@SALDO + @interes + @reajuste) / @uf_dia
		 --print(@uf_dia)
		 --print(@total_uf)
		 
	if DATEADD(month,4, @FECdoc) <= getdate()
	begin
		if  @total_uf <= 10 
		begin
			set @gasto = .09 * @total_uf 
			--print ('10:'+convert(varchar,@gasto) )
		end
		if  @total_uf > 10  and @total_uf <= 50 
		begin
			set @gasto = .09 * 10 +  (@total_uf - 10) * .06
			--print ('<50:'+convert(varchar,@gasto)) 
		end
		if  @total_uf > 50 
		begin
			set @gasto = .09 * 10 +  40 * .06 + (@total_uf - 50) * .03
			--print ('>50:'+convert(varchar,@gasto) )
		end
		set @gasto = CEILING(@gasto * @uf_dia )
		--print(@gasto)
	end 
	else
	begin 
		set @gasto = 0
	end
	
	update CARTERA_CLIENTES_CPBT_DOC set
	ccb_intereses = @interes,
	ccb_honorarios = @reajuste,
	CCB_GASTOTRO = @gasto,
	CCB_FECCALCINT = GETDATE()
	where ccb_codemp = @codemp
	and ccb_pclid = @pclid
	and ccb_ctcid = @ctcid
	and ccb_ccbid = @ccbid
		
    fetch next from cur into  @PCLID, @CTCID, @CCBID,@FECdoc, @SALDO 
end
close cur
deallocate cur

insert into LOG_ERROR values (GETDATE(), 'Calculo Interes, Reajuste y gasto Mutual Ley', 'Termino', 'Job 2 AM',0 )

