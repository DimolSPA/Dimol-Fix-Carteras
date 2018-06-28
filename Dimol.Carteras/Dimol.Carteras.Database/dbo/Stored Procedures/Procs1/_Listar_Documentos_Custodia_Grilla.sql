CREATE PROCEDURE _Listar_Documentos_Custodia_Grilla
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
	p.PCL_RUT RutCliente,
	p.PCL_NOMFANT Cliente,
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
	dc.GESTORID GestorId,
	dc.BANCO_ID BancoId,
	dc.TIPO_ESTADO_BANCO_ID EstadoId
	 
from DOCUMENTOS_CUSTODIA dc
join PROVCLI p
on dc.CODEMP = p.PCL_CODEMP
and dc.PCLID = p.PCL_PCLID
join DEUDORES d
on dc.CODEMP = d.CTC_CODEMP
and dc.CTCID = d.CTC_CTCID
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
and dc.TIPO_ESTADO_BANCO_ID = 2'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END