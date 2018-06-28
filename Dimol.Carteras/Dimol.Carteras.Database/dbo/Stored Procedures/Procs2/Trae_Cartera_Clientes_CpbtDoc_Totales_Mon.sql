

CREATE Procedure [dbo].[Trae_Cartera_Clientes_CpbtDoc_Totales_Mon](@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_estcpbt char(1), @idi_idid integer) as  

if @ccb_pclid  = 424 
begin
declare @prevNumero as varchar(20) =''


declare @montoTotal as decimal(15,2)  = 0

IF OBJECT_ID('tempdb..#Totales2','u') IS NOT NULL
  DROP TABLE #Totales2

IF OBJECT_ID('tempdb..#TotMon2','u') IS NOT NULL 
  DROP TABLE #TotMon2

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
         

  SELECT sum(ccb_asignado * mnv_valor) as TotReal,
                sum(ccb_monto * mnv_valor) as TotMonto,
                sum(ccb_saldo * mnv_valor) as TotSaldo,
                sum(ccb_gastjud * mnv_valor) as TotGjud,
                sum(ccb_gastotro * mnv_valor) as TotGPre,
                sum(ccb_intereses * mnv_valor) as TotInte,
                sum(ccb_honorarios * mnv_valor) as TotHono,
                sum((ccb_saldo + ccb_gastjud +   ccb_gastotro +  ccb_intereses + ccb_honorarios) * mnv_valor) as Total,
                sum(ccb_compromiso * mnv_valor) as TotComp
                into #TotMon2 
    FROM cartera_clientes_documentos_cpbt_doc, monedas_valores  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1 ) and
         ccb_codemp = mnv_codemp and
         ccb_codmon = mnv_codmon and
		 mnv_fecha = CONVERT (date, getdate(), 112)

update #Totales2
set #Totales2.TotReal = #Totales2.TotReal + #TotMon2.TotReal,
    #Totales2.TotMonto = @montoTotal,
	#Totales2.TotSaldo = #Totales2.TotSaldo + #TotMon2.TotSaldo,
	#Totales2.TotGjud = #Totales2.TotGjud + #TotMon2.TotGjud,
	#Totales2.TotGPre = #Totales2.TotGPre + #TotMon2.TotGPre,
	#Totales2.TotInte = #Totales2.TotInte + #TotMon2.TotInte,
	#Totales2.TotHono = #Totales2.TotHono + #TotMon2.TotHono,
	#Totales2.Total = #Totales2.Total + #TotMon2.Total,
	#Totales2.TotComp = #Totales2.TotComp + #TotMon2.TotComp
from #Totales2, #TotMon2
where #TotMon2.TotReal is not null


select * from #Totales2
end
else
begin
IF OBJECT_ID('tempdb..#Totales','u') IS NOT NULL
  DROP TABLE #Totales

IF OBJECT_ID('tempdb..#TotMon','u') IS NOT NULL 
  DROP TABLE #TotMon

SELECT sum(ccb_asignado) as TotReal,
                sum(ccb_monto) as TotMonto,
                sum(ccb_saldo) as TotSaldo,
                sum(ccb_gastjud) as TotGjud,
                sum(ccb_gastotro) as TotGPre,
                sum(ccb_intereses) as TotInte,
                sum(ccb_honorarios) as TotHono,
                sum(ccb_saldo + ccb_gastjud +   ccb_gastotro +  ccb_intereses + ccb_honorarios) as Total,
                sum(ccb_compromiso) as TotComp
                into #Totales
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1 ) and
         ccb_codmon in (select emc_valnum from empresa_configuracion where emc_codemp =  @ccb_codemp and emc_emcid = 18)


update #Totales set TotReal = 0.0000
where TotReal is null

update #Totales set TotMonto = 0.0000
where TotMonto is null

update #Totales set TotSaldo = 0.0000
where TotSaldo is null


update #Totales set TotGjud = 0.0000
where TotGjud is null

update #Totales set TotGPre = 0.0000
where TotGPre is null

update #Totales set TotInte = 0.0000
where TotInte is null

update #Totales set TotHono = 0.0000
where TotHono is null

update #Totales set Total = 0.0000
where Total is null

update #Totales set TotComp = 0.0000
where TotComp is null
         


  SELECT sum(ccb_asignado * mnv_valor) as TotReal,
                sum(ccb_monto * mnv_valor) as TotMonto,
                sum(ccb_saldo * mnv_valor) as TotSaldo,
                sum(ccb_gastjud * mnv_valor) as TotGjud,
                sum(ccb_gastotro * mnv_valor) as TotGPre,
                sum(ccb_intereses * mnv_valor) as TotInte,
                sum(ccb_honorarios * mnv_valor) as TotHono,
                sum((ccb_saldo + ccb_gastjud +   ccb_gastotro +  ccb_intereses + ccb_honorarios) * mnv_valor) as Total,
                sum(ccb_compromiso * mnv_valor) as TotComp
                into #TotMon 
    FROM cartera_clientes_documentos_cpbt_doc, monedas_valores  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and tci_idid =  @idi_idid and eci_idid  = @idi_idid and mci_idid =  @idi_idid  and eci_estid > 1 ) and
         ccb_codemp = mnv_codemp and
         ccb_codmon = mnv_codmon and
         datepart(year, mnv_fecha) = datepart(year, getdate()) and
		 datepart(month, mnv_fecha) = datepart(month, getdate()) and
         datepart(day, mnv_fecha) = datepart(day, getdate()) 


update #Totales
set #Totales.TotReal = #Totales.TotReal + #TotMon.TotReal,
    #Totales.TotMonto = #Totales.TotMonto + #TotMon.TotMonto,
	#Totales.TotSaldo = #Totales.TotSaldo + #TotMon.TotSaldo,
	#Totales.TotGjud = #Totales.TotGjud + #TotMon.TotGjud,
	#Totales.TotGPre = #Totales.TotGPre + #TotMon.TotGPre,
	#Totales.TotInte = #Totales.TotInte + #TotMon.TotInte,
	#Totales.TotHono = #Totales.TotHono + #TotMon.TotHono,
	#Totales.Total = #Totales.Total + #TotMon.Total,
	#Totales.TotComp = #Totales.TotComp + #TotMon.TotComp
from #Totales, #TotMon
where #TotMon.TotReal is not null


select * from #Totales
end