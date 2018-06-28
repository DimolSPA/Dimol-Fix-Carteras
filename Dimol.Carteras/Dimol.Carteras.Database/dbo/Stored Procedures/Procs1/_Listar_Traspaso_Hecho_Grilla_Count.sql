-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Traspaso_Hecho_Grilla_Count]
(
@codemp int,
@fecha_desde varchar(20) ,
@fecha_hasta varchar(20) ,
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
declare @desde datetime, @hasta datetime

set @desde = CONVERT(datetime, @fecha_desde, 105)
set @hasta = CONVERT(datetime, @fecha_hasta, 105)

set @query = 'select count (Ctcid) as count from
  (select *,ROW_NUMBER() OVER (ORDER BY Ctcid asc) as row from    
  ('

set @query = @query + 'select distinct ccb_pclid Pclid,
ccb_ctcid Ctcid,
p.PCL_NOMFANT Cliente, 
d.CTC_RUT RutDeudor, 
d.CTC_NOMFANT Deudor, 
CONVERT (char(10),CEH_FECHA,105) Fecha 
from CARTERA_CLIENTES_CPBT_DOC with (nolock), PROVCLI p with (nolock), DEUDORES  d  with (nolock), CARTERA_CLIENTES_ESTADOS_HISTORIAL h with (nolock)
where CEH_CODEMP = '+ convert(char,@codemp) +
' and CEH_ESTID = (select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = '+ convert(char,@codemp) +' and EMC_EMCID = 68) '+
' and CEH_FECHA > '''+cONVERT (char(10),@desde,112) + ''''+
' and CEH_FECHA < '''+cONVERT (char(10),DATEADD(day,1, @hasta),112) + ''''+
' and CCB_CODEMP = p.PCL_CODEMP
and CCB_PCLID = p.PCL_PCLID
and CCB_CODEMP = d.CTC_CODEMP
and CCB_CTCID = d.CTC_CTCID
and CCB_PCLID = h.CEH_PCLID
and CCB_CTCID = h.CEH_CTCID
and CCB_ESTID = h.CEH_ESTID
and CCB_CODEMP = h.CEH_CODEMP'

--'SELECT cbc_tpcid,   
--cbc_numero,   
--tci_nombre,   
--cbc_numprovcli,   
--pcl_nomfant,   
--cbc_feccpbt
--FROM view_cabecera_comprobantes
--WHERE  cbc_codemp = '+ convert(char,@codemp) +
--' and cbc_sucid =  '+ convert(char,@codsuc) +
--' and cbc_tpcid = (select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = '+ convert(char,@codemp) +' and EMC_EMCID = 67)
--and cbt_estado <> ''X'' 
--and idi_idid =  '+ convert(char,@idioma) +
--' and cbc_feccpbt >= '''+cONVERT (char(10),@desde,112) + ''''+
--' and cbc_feccpbt < '''+cONVERT (char(10),@hasta,112) + ''''

set @query = @query +') as tabla  ) as t
  where  row > 0' 

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
