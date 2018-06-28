

CREATE Procedure [dbo].[Trae_Cartera_Clientes_CpbtDoc_Totales](@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_estcpbt char(1), @idi_idid integer) as
if @ccb_pclid = 424
begin

 declare @prevNumero as varchar(20) =''

declare @montoTotal as decimal(15,2)  = 0

IF OBJECT_ID('tempdb..#Totales3','u') IS NOT NULL
  DROP TABLE #Totales3

DECLARE cursor_creditos CURSOR FOR
SELECT distinct SUBSTRING ( ccb_numero ,1 , 5 ) as ccb_numero 
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE  ccb_codemp = @ccb_codemp
	AND  Ccb_pclid = @ccb_pclid  
	AND  (ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1  )
	order by ccb_numero;

OPEN cursor_creditos;
FETCH NEXT FROM cursor_creditos into @prevNumero;

WHILE @@FETCH_STATUS = 0
BEGIN
	set @montoTotal = @montoTotal +(select top 1 ccb_monto from cartera_clientes_documentos_cpbt_doc 
	where  ccb_codemp = @ccb_codemp
	AND  Ccb_pclid = @ccb_pclid  
	AND  (ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1  )
	and ccb_numero like '%' + @prevNumero + '%'
	order by ccb_numero desc);
   FETCH NEXT FROM cursor_creditos into @prevNumero;
END
CLOSE cursor_creditos;
DEALLOCATE cursor_creditos;

SELECT sum(ccb_asignado) as TotReal,
                @montoTotal as TotMonto,
                sum(ccb_saldo) as TotSaldo,
                sum(ccb_gastjud) as TotGjud,
                sum(ccb_gastotro) as TotGPre,
                sum(ccb_intereses) as TotInte,
                sum(ccb_honorarios) as TotHono,
                sum(ccb_saldo + ccb_gastjud +   ccb_gastotro +  ccb_intereses + ccb_honorarios) as Total,
                sum(ccb_compromiso) as TotComp
                into #Totales3
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1 ) and
         ccb_codmon in (select emc_valnum from empresa_configuracion where emc_codemp =  @ccb_codemp and emc_emcid = 18)

update #Totales3 set TotReal = 0.0000
where TotReal is null

update #Totales3 set TotMonto = 0.0000
where TotMonto is null

update #Totales3 set TotSaldo = 0.0000
where TotSaldo is null


update #Totales3 set TotGjud = 0.0000
where TotGjud is null

update #Totales3 set TotGPre = 0.0000
where TotGPre is null

update #Totales3 set TotInte = 0.0000
where TotInte is null

update #Totales3 set TotHono = 0.0000
where TotHono is null

update #Totales3 set Total = 0.0000
where Total is null

update #Totales3 set TotComp = 0.0000
where TotComp is null

select * from #Totales3

end
else
begin
SELECT sum(ccb_asignado) as TotReal,
                sum(ccb_monto) as TotMonto,
                sum(ccb_saldo) as TotSaldo,
                sum(ccb_gastjud) as TotGjud,
                sum(ccb_gastotro) as TotGPre,
                sum(ccb_intereses) as TotInte,
                sum(ccb_honorarios) as TotHono,
                sum(ccb_saldo + ccb_gastjud +   ccb_gastotro +  ccb_intereses + ccb_honorarios) as Total,
                sum(ccb_compromiso) as TotComp
                into #Totales2
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1 ) and
         ccb_codmon in (select emc_valnum from empresa_configuracion where emc_codemp =  @ccb_codemp and emc_emcid = 18)

update #Totales2 set TotReal = 0.0000
where TotReal is null

update #Totales2 set TotMonto = 0.0000
where TotMonto is null

update #Totales2 set TotSaldo = 0.0000
where TotSaldo is null


update #Totales2 set TotGjud = 0.0000
where TotGjud is null

update #Totales2 set TotGPre = 0.0000
where TotGPre is null

update #Totales2 set TotInte = 0.0000
where TotInte is null

update #Totales2 set TotHono = 0.0000
where TotHono is null

update #Totales2 set Total = 0.0000
where Total is null

update #Totales2 set TotComp = 0.0000
where TotComp is null

select * from #Totales2

end