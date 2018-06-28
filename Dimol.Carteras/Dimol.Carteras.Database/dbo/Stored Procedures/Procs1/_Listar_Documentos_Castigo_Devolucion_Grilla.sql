
CREATE PROCEDURE [dbo].[_Listar_Documentos_Castigo_Devolucion_Grilla]
(
@codemp int,
@pclid int,
@ctcid int,
@estcpbt varchar(1),
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
  
set @query = @query + 'select CCB_PCLID Pclid,
CCB_CTCID Ctcid,
REPLACE(d.CTC_NOMFANT, ''"'', '''') Deudor,
CCB_CCBID Ccbid, 
t.TPC_NOMBRE Tipo, 
m.MON_NOMBRE Moneda ,
CCB_NUMERO Numero, 
ccb_fecing FechaAsignacion,
CCB_MONTO Monto, 
CCB_SALDO Saldo, 
CCB_ASIGNADO Asignado, 
e.ECT_NOMBRE UltimoEstado, 
case CCB_ESTCPBT
when ''J'' then ''JUDICIAL''
when ''F'' then ''FINALIZADO''
when ''V'' then ''VIGENTE''
when ''X'' then ''NULO''
end Estado,
CCB_FECVENC FechaVencimiento,
UPPER(CCB_ESTCPBT) EstadoCpbt,
(select REPLACE(sbc.SBC_NOMBRE, ''"'', '''') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CCB_CODEMP and sbc.SBC_SBCID = CCB_SBCID) Asegurado,
r.ROL_NUMERO RolNumero, rd.RDC_ROLID RolId
from CARTERA_CLIENTES_CPBT_DOC with (nolock) 
join TIPOS_CPBTDOC t with (nolock)
on CCB_CODEMP = t.TPC_CODEMP
and CCB_TPCID = t.TPC_TPCID
join ESTADOS_CARTERA e with (nolock)
on CCB_CODEMP = e.ECT_CODEMP
and CCB_ESTID = e.ECT_ESTID
join MONEDAS m with (nolock)
on CCB_CODEMP = m.MON_CODEMP
and CCB_CODMON = m.MON_CODMON
join DEUDORES d with (nolock)
on CCB_CTCID = d.CTC_CTCID
left join ROL_DOCUMENTOS rd with (nolock)
on CCB_CODEMP = rd.RDC_CODEMP
and CCB_PCLID = rd.RDC_PCLID
and CCB_CTCID = rd.RDC_CTCID
and CCB_CCBID = rd.RDC_CCBID
left join rol r with(nolock)
on rd.RDC_CODEMP = r.ROL_CODEMP
and rd.RDC_ROLID = r.ROL_ROLID
where CCB_CODEMP = '+ convert(char,@codemp) +
' and CCB_PCLID = '+ convert(char,@pclid) +
' and CCB_CTCID = '+ convert(char,@ctcid) +''
if @estcpbt = 'X'--Ambos
begin
set @query = @query + ' AND CCB_estcpbt IN (''V'',''J'') '
end
else
begin
set @query = @query + ' and CCB_estcpbt = '''+ @estcpbt +''''
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
