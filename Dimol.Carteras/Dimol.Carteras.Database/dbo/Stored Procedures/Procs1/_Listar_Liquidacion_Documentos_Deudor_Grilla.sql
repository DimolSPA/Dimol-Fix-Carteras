CREATE PROCEDURE [dbo].[_Listar_Liquidacion_Documentos_Deudor_Grilla]
(
@codemp int,
@pclid int,
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
  ccb_pclid Pclid,
  ccb_ctcid Ctcid,
  ccb_ccbid Ccbid,
  sbc_nombre Asegurado,
  tci_nombre TipoDocumento,
  RIGHT(CCB_NUMERO, LEN(CCB_NUMERO+''a'') -PATINDEX(''%[^0 ]%'', CCB_NUMERO + ''a'' )) Numero,
  case CPBT.CCB_ESTCPBT
	when ''J'' then ''JUDICIAL''
	when ''F'' then ''FINALIZADO''
	when ''V'' then ''VIGENTE''
	when ''X'' then ''NULO''
    end  Estado,
  ccb_fecvenc FechaVencimiento,
   mon_nombre Moneda,
  ccb_monto Monto, 
  ccb_saldo Saldo,
  ccb_intereses Intereses,             
  ccb_honorarios Honorarios,
  ccb_gastjud GastoJudicial, 
  ccb_gastotro GastoPrejudicial,
  ccb_saldo +   ccb_gastjud +  ccb_gastotro + ccb_intereses + ccb_honorarios as TotalDeuda
FROM cartera_clientes_documentos_cpbt_doc cpbt
where CPBT.CCB_CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
 AND CPBT.CCB_PCLID = ' + CONVERT(VARCHAR,@pclid) +'
 AND CPBT.CCB_CTCID = ' + CONVERT(VARCHAR,@ctcid) +'
 AND CPBT.CCB_ESTCPBT in (''V'',''J'')'


set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
