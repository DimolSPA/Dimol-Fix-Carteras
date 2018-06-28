CREATE PROCEDURE [dbo].[_Listar_PanelAprobar_CastigoDevolucion_Documentos_Grilla]
(
@codemp int,
@tipoComprobante int,
@folio int,
@pclid int,
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
  
set @query = @query + 'select 
  d.CTC_RUT RutDeudor,
  d.CTC_NOMFANT Deudor,
  t.TPC_NOMBRE Tipo,
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
CCB_ESTCPBT EstadoCpbt,
(select sbc.SBC_NOMBRE from SUBCARTERAS  sbc with (nolock) where sbc.SBC_CODEMP = cpbt.CCB_CODEMP and sbc.SBC_SBCID = cpbt.CCB_SBCID) Asegurado,
cpbt.CCB_PCLID pclid, cpbt.CCB_CTCID ctcid, cpbt.CCB_CCBID ccbid,
r.ROL_NUMERO RolNumero, rd.RDC_ROLID RolId
from 
DETALLE_COMPROBANTES ddc with(nolock)
join CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
on ddc.DCC_CODEMP = cpbt.CCB_CODEMP
and ddc.DCC_PCLID = cpbt.CCB_PCLID
and ddc.DCC_CTCID = cpbt.CCB_CTCID
and ddc.DCC_CCBID = cpbt.CCB_CCBID
join TIPOS_CPBTDOC t with (nolock)
on cpbt.CCB_CODEMP = t.TPC_CODEMP
and cpbt.CCB_TPCID = t.TPC_TPCID
join ESTADOS_CARTERA e with (nolock)
on cpbt.CCB_CODEMP = e.ECT_CODEMP 
and cpbt.CCB_ESTID = e.ECT_ESTID
JOIN DEUDORES  d  with (nolock)
ON cpbt.CCB_CTCID = d.CTC_CTCID
AND cpbt.CCB_CODEMP = d.CTC_CODEMP
left join ROL_DOCUMENTOS rd with (nolock)
on cpbt.CCB_CODEMP = rd.RDC_CODEMP
and cpbt.CCB_PCLID = rd.RDC_PCLID
and cpbt.CCB_CTCID = rd.RDC_CTCID
and cpbt.CCB_CCBID = rd.RDC_CCBID
left join rol r with(nolock)
on rd.RDC_CODEMP = r.ROL_CODEMP
and rd.RDC_ROLID = r.ROL_ROLID
where DCC_CODEMP = '+ CONVERT(VARCHAR,@codemp) + ' 
and ddc.DCC_TPCID = '+ CONVERT(VARCHAR,@tipoComprobante) + '
and ddc.DCC_NUMERO = '+ CONVERT(VARCHAR,@folio) + '
and ddc.DCC_PCLID = '+ CONVERT(VARCHAR,@pclid) + ''
set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
