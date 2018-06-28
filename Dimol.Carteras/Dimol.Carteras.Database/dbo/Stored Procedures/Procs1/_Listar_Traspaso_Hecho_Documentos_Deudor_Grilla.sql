﻿CREATE PROCEDURE _Listar_Traspaso_Hecho_Documentos_Deudor_Grilla
(
@codemp int,
@ctcid int,
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
  
set @query = @query + 'SELECT 
  cpbt.ccb_pclid Pclid,
	cpbt.ccb_ctcid Ctcid,
	cpbt.ccb_ccbid ccbid,
	p.PCL_NOMFANT Cliente,
	d.CTC_RUT RutDeudor,
	d.CTC_NOMFANT Deudor,
	tci.tci_nombre TipoDocumento,
	cpbt.ccb_numero Numero,
	cpbt.ccb_fecing FechaAsignacion,
	cpbt.ccb_fecvenc FechaVencimiento,
	cpbt.ccb_monto Monto, 
	cpbt.ccb_saldo Saldo,
  case cpbt.ccb_estcpbt
	when ''J'' then ''JUDICIAL''
	when ''F'' then ''FINALIZADO''
	when ''V'' then ''VIGENTE''
	when ''X'' then ''NULO''
  end Estado,
  cpbt.ccb_estcpbt EstadoCpbt,
  CONVERT(char(10),  h.CEH_FECHA, 105) Fecha
from 
CARTERA_CLIENTES_ESTADOS_HISTORIAL h WITH (NOLOCK)
join CARTERA_CLIENTES_CPBT_DOC cpbt WITH (NOLOCK)
on h.CEH_CODEMP = cpbt.CCB_CODEMP
and h.CEH_PCLID = cpbt. CCB_PCLID
and h.CEH_CTCID = cpbt.CCB_CTCID
and h.CEH_CCBID = cpbt.CCB_CCBID
and h.CEH_ESTID = cpbt.CCB_ESTIDJ
join PROVCLI p WITH (NOLOCK)
on cpbt.CCB_CODEMP = p.PCL_CODEMP
and cpbt.CCB_PCLID =  p.PCL_PCLID
join DEUDORES d WITH (NOLOCK)
on cpbt.CCB_CODEMP = d.CTC_CODEMP
and cpbt.CCB_CTCID = d.CTC_CTCID
join TIPOS_CPBTDOC_IDIOMAS tci WITH (NOLOCK)
on cpbt.CCB_CODEMP = tci.TCI_CODEMP
and cpbt.CCB_TPCID = tci.TCI_TPCID
where h.CEH_CODEMP = '+ convert(char,@codemp) +
' and h.CEH_ESTID = (select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = '+ convert(char,@codemp) +' and EMC_EMCID = 68) '+
' and h.CEH_CTCID = '+ convert(char,@ctcid) +''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END