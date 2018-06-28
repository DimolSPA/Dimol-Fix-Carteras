CREATE PROCEDURE [dbo].[_Listar_PanelAprobar_CastigoDevolucion_Grilla]
(
@codemp int,
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
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select distinct 
	cbc.CBC_PCLID pclid, 
	cbc.CBC_TPCID tpcid,  
	tpc.TPC_NOMBRE TipoComprobante, 
	cbc.CBC_NUMERO Folio,
	pv.PCL_NOMFANT Cliente, 
	cbc.CBC_FECEMI FecEmision,
	cbc.CBT_ESTADO CbtEstado,
	case cbc.CBT_ESTADO
when ''E'' then ''EMITIDO''
when ''A'' then ''APROBADO''
when ''F'' then ''FINALIZADO''
when ''X'' then ''RECHAZADO''
else ''NULO''
end Estado, cbc.CBC_SALDO Saldo, cbc.CBC_NETO Neto
from CABACERA_COMPROBANTES cbc
join tipos_cpbtdoc tpc
on cbc.CBC_CODEMP = tpc.TPC_CODEMP
and cbc.CBC_TPCID = tpc.TPC_TPCID
join PROVCLI pv
on cbc.CBC_CODEMP = pv.PCL_CODEMP
and cbc.CBC_PCLID = pv.PCL_PCLID
join DETALLE_COMPROBANTES dcc
on cbc.CBC_CODEMP = dcc.DCC_CODEMP
and cbc.CBC_TPCID = dcc.DCC_TPCID
and cbc.CBC_PCLID = dcc.DCC_PCLID
and cbc.CBC_NUMERO = dcc.DCC_NUMERO
join CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
on dcc.DCC_CODEMP = cpbt.CCB_CODEMP
and dcc.DCC_PCLID = cpbt.CCB_PCLID
and dcc.DCC_CTCID = cpbt.CCB_CTCID
and dcc.DCC_CCBID = cpbt.CCB_CCBID
where cbc.CBC_CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
and cbc.CBT_ESTADO = ''E''
and tpc.TPC_CLBID in (4,5,6)
and tpc.TPC_TALONARIO = ''S''
and cpbt.CCB_ESTCPBT != ''F'''
set @query = @query + ') as tabla  ) as t'
  --where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
