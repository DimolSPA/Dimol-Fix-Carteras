-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Documentos_Reversa_Grilla]
(
@codemp int,
@pclid int,
@ctcid int,
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
CCB_CCBID Ccbid, 
t.TPC_NOMBRE Tipo, 
CCB_NUMERO Numero, 
CCB_MONTO Monto, 
CCB_SALDO Saldo, 
e.ECT_NOMBRE UltimoEstado, 
case CCB_ESTCPBT
when ''J'' then ''JUDICIAL''
when ''F'' then ''FINALIZADO''
when ''V'' then ''VIGENTE''
when ''X'' then ''NULO''
end Estado,
CCB_FECVENC FechaVencimiento
from CARTERA_CLIENTES_CPBT_DOC , TIPOS_CPBTDOC t, ESTADOS_CARTERA e
where CCB_CODEMP = '+ convert(char,@codemp) +
' and CCB_PCLID = '+ convert(char,@pclid) +
' and CCB_CTCID = '+ convert(char,@ctcid) +
' and t.TPC_CODEMP = CCB_CODEMP
and t.TPC_TPCID = CCB_TPCID
and e.ECT_CODEMP = CCB_CODEMP
and e.ECT_ESTID = CCB_ESTID '

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
