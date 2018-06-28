CREATE PROCEDURE [dbo].[_Listar_Panel_Demandas_Documentos_Grilla]
(
@codemp int,
@panelId int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = ''
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('
  
set @query = @query + 'select 
	PDD.PANEL_ID PANELID,
	CPBT.CCB_PCLID PCLID, 
	CPBT.CCB_CTCID CTCID, 
	CPBT.CCB_CCBID CCBID, 
	ccb_numero Numero,
  ccb_fecing FechaAsignacion,
  ccb_fecvenc FechaVencimiento,
  ccb_monto Monto, 
  ccb_saldo Saldo,
  case ccb_estcpbt
	when ''J'' then ''JUDICIAL''
	when ''F'' then ''FINALIZADO''
	when ''V'' then ''VIGENTE''
	when ''X'' then ''NULO''
  end Estado,
  (select sbc.SBC_NOMBRE from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado,
	(select TDOC.TPC_NOMBRE from TIPOS_CPBTDOC TDOC with (nolock) where TDOC.TPC_CODEMP = CPBT.CCB_CODEMP AND TDOC.TPC_TPCID = CPBT.CCB_TPCID) TipoDocumento
from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
ON PDD.CODEMP = CPBT.CCB_CODEMP
AND PDD.PCLID = cpbt.CCB_PCLID
and PDD.CTCID = cpbt.CCB_CTCID
and PDD.CCBID = cpbt.CCB_CCBID
where PANEL_ID = '+ convert(varchar,@panelId) +'
and CODEMP = '+ convert(varchar,@codemp) +''

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
