-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Salida_Gestiones] 
(
	@codemp as integer,
	@pclid as integer,
	@fecha_inicio as datetime,
	@fecha_termino as datetime
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
	select DISTINCT convert(varchar,d.ctc_numero)+';'+
	'DIRECTO'+';'+
	T.TPC_CODIGO+';'+
	(select top 1 NUMERO_OPERACION from SITREL_OPERACION where CODEMP = c.CCB_CODEMP and PCLID = CCB_PCLID and ccb_numero like '%'+NUMERO_OPERACION+ '%' order by ID_CARGA desc)+';'+
	M.MON_NOMBRE+';'+
	CS.[CODIGO_EMPRESA]+';'+
	convert(varchar(25), CS.FECHA, 112) + ' '+  replace(convert(varchar(25), CS.FECHA, 108),':','') +';'+
	CS.[CODIGO_ACCION]+';'+
	CS.[CODIGO_CONTACTO]+';'+
	CS.[CODIGO_RESPUESTA]+';'+
	replace(CS.[GLOSA_GESTION],';','|') +';'+
	isnull(CS.[FECHA_COMPROMISO],'')+';'+
	CASE WHEN CS.[MONTO_COMPROMISO] = 0 THEN 
	case when CS.[FECHA_COMPROMISO] != '' then   replace(convert(varchar(25),CS.[MONTO_GESTION]),'.','')  else '' end
	else replace(convert(varchar(25),CS.[MONTO_COMPROMISO]),'.','') END +';'+
	CASE WHEN CS.[MONTO_GESTION] = 0 THEN ''  else replace(convert(varchar(25),CS.[MONTO_GESTION]),'.','') END +';'+
	isnull(CS.[NOMBRE_CONTACTO],'')+';'+
	CS.[PROGRAMACION_LLAMADA]+';'+
	isnull(replace(replace(replace(replace(replace(CS.[TELEFONO_CONTACTO],CHAR(9),''),'-','0'),' ','0'),'(','0'),')','0'),'')+';'+
	(select top 1 CAMPANIA from SITREL_OPERACION where CODEMP = c.CCB_CODEMP and PCLID = CCB_PCLID and ccb_numero like '%'+NUMERO_OPERACION+ '%' order by ID_CARGA desc)
	from CARTERA_CLIENTES_ESTADOS_SITREL cs  with (nolock), DEUDORES d  with (nolock), CARTERA_CLIENTES_CPBT_DOC C   with (nolock), MONEDAS M  with (nolock), TIPOS_CPBTDOC T  with (nolock)--, SITREL_OPERACION O  with (nolock)
	where c.CCB_CODEMP = @codemp
	and c.CCB_PCLID = @pclid
	and fecha > @fecha_inicio and fecha < @fecha_termino
	and cs.codemp = d.CTC_CODEMP
	and cs.ctcid = d.CTC_CTCID
	and c.CCB_CODEMP = d.CTC_CODEMP
	and c.CCB_CTCID = d.CTC_CTCID
	AND M.MON_CODEMP = CS.CODEMP
	AND M.MON_CODMON = C.CCB_CODMON
	AND T.TPC_CODEMP = C.CCB_CODEMP
	AND T.TPC_TPCID = C.CCB_TPCID
	--AND C.CCB_NUMERO LIKE '%'+O.NUMERO_OPERACION+ '%'
	--AND C.CCB_CODEMP = O.CODEMP
	--AND C.CCB_PCLID = O.PCLID        


END
