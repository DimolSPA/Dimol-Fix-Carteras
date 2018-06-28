-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar documentos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_CpbtDoc_Clientes_Grilla]
(
@codemp int,
@pclid integer, 
@ctcid integer, 
@estcpbt char(1),
@idid int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);

if @sidx = 'FechaVencimiento' and  @pclid = 424
begin
	set @sidx = 'NumeroCpbt'
end

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

if @pclid = 424
begin
set @query = @query +'SELECT ccb_pclid pclid,
ccb_ctcid ctcid,
pcl_rut RutCliente ,              
pcl_nombre NombreCliente,              
tci_nombre TipoCpbtNombre,              
ccb_ccbid ccbid,              
ccb_numero NumeroCpbt,              
ccb_fecing FechaIngreso,              
ccb_fecdoc FechaDocumento,              
ccb_fecvenc FechaVencimiento,              
ccb_fecultgest FechaUltimaGestion,              
ccb_fecplazo FechaPlazo,              
ccb_feccalcint FechaCalculoInteres,              
ccb_feccast FechaCastigo,              
eci_nombre EstadoCartera,
ect_nombre EstadoJudicial,          
ccb_estcpbt EstadoCpbt,              
ccb_codmon CodigoMoneda,              
ccb_tipcambio TipoCambio,              
ccb_asignado MontoAsignado,              
ccb_monto Monto,              
ccb_saldo Saldo,              
ccb_gastjud GastoJudicial,              
ccb_gastotro GastoOtros,              
ccb_intereses Intereses,             
ccb_honorarios Honorarios,              
ccb_calchon CalculoHonorarios,              
bco_nombre NombreBanco,              
ccb_rutgir RutGirador,              
ccb_nomgir NombreGirador,              
ccb_comentario Comentario,              
ccb_retent retent,              
ccb_numesp NumeroEspecial,              
ccb_numagrupa NumeroAgrupa,              
ccb_carta Carta,              
ccb_cobrable Cobrable,              
ccb_cctid cctid,              
sbc_rut SubcarteraRut,              
sbc_nombre SubcarteraNombre,              
ccb_docori DocumentoOrigen,              
ccb_docant docant,              
ccb_tipcart TipoCartera ,           
datediff(day, ccb_fecvenc, getdate()) as DiasVencido,           
mon_nombre Moneda,            
ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotalDeuda,
ccb_compromiso Compromiso,
pcc_nombre  CodigoCargaNombre, 
(SELECT count([CDP_CODEMP])
  FROM [CARTERA_CLIENTES_DEMANDA_PENDIENTE]
  where [CDP_CODEMP] = ccb_codemp
  and [CDP_PCLID] = ccb_pclid
  and [CDP_CTCID]= ccb_ctcid
  and [CDP_CCBID] =ccb_ccbid) DemandaPendiente   
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ') 
AND             ( ccb_pclid = '+ CONVERT(VARCHAR,@pclid)  +' ) 
AND             ( ccb_ctcid = '+ CONVERT(VARCHAR,@ctcid)  + ' 
and ccb_estcpbt = '''+ @estcpbt +'''
and tci_idid = '+ CONVERT(VARCHAR,@idid)+ '  
and eci_idid  = '+ CONVERT(VARCHAR,@idid)+ '  
and mci_idid =  '+ CONVERT(VARCHAR,@idid)+ '  
and eci_estid > 1 )'
--Order by ccb_numero asc
end 
else if @pclid = 22
begin
set @query = @query +'SELECT  ccb_pclid pclid,
ccb_ctcid ctcid,
pcl_rut RutCliente ,              
pcl_nombre NombreCliente,              
tci_nombre TipoCpbtNombre,              
ccb_ccbid ccbid,              
ccb_numero NumeroCpbt,              
ccb_fecing FechaIngreso,              
ccb_fecdoc FechaDocumento,              
ccb_fecvenc FechaVencimiento,              
ccb_fecultgest FechaUltimaGestion,              
ccb_fecplazo FechaPlazo,              
ccb_feccalcint FechaCalculoInteres,              
ccb_feccast FechaCastigo,              
eci_nombre EstadoCartera,
ect_nombre EstadoJudicial,              
ccb_estcpbt EstadoCpbt,              
ccb_codmon CodigoMoneda,              
ccb_tipcambio TipoCambio,              
ccb_asignado MontoAsignado,              
ccb_monto Monto,              
ccb_saldo Saldo,              
ccb_gastjud GastoJudicial,              
ccb_gastotro GastoOtros,              
ccb_intereses Intereses,             
ccb_honorarios Honorarios,              
ccb_calchon CalculoHonorarios,              
bco_nombre NombreBanco,              
ccb_rutgir RutGirador,              
ccb_nomgir NombreGirador,              
ccb_comentario Comentario,              
ccb_retent retent,              
ccb_numesp NumeroEspecial,              
ccb_numagrupa NumeroAgrupa,              
ccb_carta Carta,              
ccb_cobrable Cobrable,              
ccb_cctid cctid,              
sbc_rut SubcarteraRut,              
sbc_nombre SubcarteraNombre,              
ccb_docori DocumentoOrigen,              
ccb_docant docant,              
ccb_tipcart TipoCartera ,           
datediff(day, ccb_fecvenc, getdate()) as DiasVencido,           
mon_nombre Moneda,            
ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotalDeuda,
ccb_compromiso Compromiso,
pcc_nombre  CodigoCargaNombre, 
(SELECT count([CDP_CODEMP])
  FROM [CARTERA_CLIENTES_DEMANDA_PENDIENTE]
  where [CDP_CODEMP] = ccb_codemp
  and [CDP_PCLID] = ccb_pclid
  and [CDP_CTCID]= ccb_ctcid
  and [CDP_CCBID] =ccb_ccbid) DemandaPendiente       
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ') 
AND             ( ccb_pclid = '+ CONVERT(VARCHAR,@pclid)  +' ) 
AND             ( ccb_ctcid = '+ CONVERT(VARCHAR,@ctcid)  + ' 
and ccb_estcpbt = '''+ @estcpbt +'''
and tci_idid = '+ CONVERT(VARCHAR,@idid)+ '  
and eci_idid  = '+ CONVERT(VARCHAR,@idid)+ '  
and mci_idid =  '+ CONVERT(VARCHAR,@idid)+ '  
and eci_estid > 1 )'  
--Order by datediff(day, ccb_fecvenc, getdate()) desc 
end
else
begin
set @query = @query +'SELECT  ccb_pclid pclid,
ccb_ctcid ctcid,
pcl_rut RutCliente ,              
pcl_nombre NombreCliente,              
tci_nombre TipoCpbtNombre,              
ccb_ccbid ccbid,              
RIGHT(CCB_NUMERO, LEN(CCB_NUMERO+''a'') -PATINDEX(''%[^0 ]%'', CCB_NUMERO + ''a'' )) NumeroCpbt,              
ccb_fecing FechaIngreso,              
ccb_fecdoc FechaDocumento,              
ccb_fecvenc FechaVencimiento,              
ccb_fecultgest FechaUltimaGestion,              
ccb_fecplazo FechaPlazo,              
ccb_feccalcint FechaCalculoInteres,              
ccb_feccast FechaCastigo,              
eci_nombre EstadoCartera,
ect_nombre EstadoJudicial,              
ccb_estcpbt EstadoCpbt,              
ccb_codmon CodigoMoneda,              
ccb_tipcambio TipoCambio,              
ccb_asignado MontoAsignado,              
ccb_monto Monto,              
ccb_saldo Saldo,              
ccb_gastjud GastoJudicial,              
ccb_gastotro GastoOtros,              
ccb_intereses Intereses,             
ccb_honorarios Honorarios,              
ccb_calchon CalculoHonorarios,              
bco_nombre NombreBanco,              
ccb_rutgir RutGirador,              
ccb_nomgir NombreGirador,              
ccb_comentario Comentario,              
ccb_retent retent,              
ccb_numesp NumeroEspecial,              
ccb_numagrupa NumeroAgrupa,              
ccb_carta Carta,              
ccb_cobrable Cobrable,              
ccb_cctid cctid,              
sbc_rut SubcarteraRut,              
sbc_nombre SubcarteraNombre,              
ccb_docori DocumentoOrigen,              
ccb_docant docant,              
ccb_tipcart TipoCartera ,           
datediff(day, ccb_fecvenc, getdate()) as DiasVencido,           
mon_nombre Moneda,            
ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotalDeuda,
ccb_compromiso Compromiso,
pcc_nombre  CodigoCargaNombre, 
(SELECT count([CDP_CODEMP])
  FROM [CARTERA_CLIENTES_DEMANDA_PENDIENTE]
  where [CDP_CODEMP] = ccb_codemp
  and [CDP_PCLID] = ccb_pclid
  and [CDP_CTCID]= ccb_ctcid
  and [CDP_CCBID] =ccb_ccbid) DemandaPendiente          
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ') 
AND             ( ccb_pclid = '+ CONVERT(VARCHAR,@pclid)  +' ) 
AND             ( ccb_ctcid = '+ CONVERT(VARCHAR,@ctcid)  + ' 
and ccb_estcpbt = '''+ @estcpbt +'''
and tci_idid = '+ CONVERT(VARCHAR,@idid)+ '  
and eci_idid  = '+ CONVERT(VARCHAR,@idid)+ '  
and mci_idid =  '+ CONVERT(VARCHAR,@idid)+ '  
and eci_estid > 1 )'
--  Order by ccb_fecvenc asc 
end


  
set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
