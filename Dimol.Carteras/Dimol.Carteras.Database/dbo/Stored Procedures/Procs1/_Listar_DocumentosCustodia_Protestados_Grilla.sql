CREATE PROCEDURE _Listar_DocumentosCustodia_Protestados_Grilla
(
@codemp int,
@numCuenta varchar(60),
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select 
	dc.CUSTODIA_ID CustodiaId,
	dc.FEC_DOC FecDoc,
	cli.PCL_RUT RutCliente,
	cli.PCL_NOMFANT Cliente,
	d.CTC_RUT RutDeudor,
	d.CTC_NOMFANT Deudor,
	dc.MONTO Monto,
	ges.GES_NOMBRE Gestor,
	dc.RECIBE GiradoA,
	ban.BCO_NOMBRE TipoBanco,
	dc.num_documento NumDocumento,
	tipoEstado.DESCRIPCION Estado,
	dc.FEC_PRORROGA FecProrroga,
	dc.PCLID Pclid,
	dc.CTCID Ctcid,
	dc.GESTORID GestorId
from DOCUMENTOS_CUSTODIA dc
join TESORERIA_TIPO_ESTADO_BANCO est
on dc.TIPO_ESTADO_BANCO_ID = est.TIPO_ESTADO_BANCO_ID
join PROVCLI cli
on dc.PCLID = cli.PCL_PCLID
and dc.CODEMP = cli.PCL_CODEMP
join DEUDORES d
on dc.CTCID = d.CTC_CTCID
and dc.CODEMP = d.CTC_CODEMP
join GESTOR ges
on dc.CODEMP = ges.GES_CODEMP
and dc.GESTORID = ges.GES_GESID
join BANCOS ban
on dc.CODEMP = ban.BCO_CODEMP
and dc.BANCO_ID = ban.BCO_BCOID
join TESORERIA_TIPO_ESTADO_BANCO tipoEstado
on dc.TIPO_ESTADO_BANCO_ID = tipoEstado.TIPO_ESTADO_BANCO_ID
where dc.CODEMP = ' + CONVERT(VARCHAR,@codemp) + '
and dc.NUM_CUENTA = ''' + CONVERT(VARCHAR,@numCuenta) + '''
and dc.TIPO_ESTADO_BANCO_ID = 3'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END