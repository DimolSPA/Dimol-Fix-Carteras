-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Salida_Direcciones] 
(
	@codemp as integer,
	@pclid as int,
	@fecha_inicio as datetime,
	@fecha_termino as datetime
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
		select convert(varchar,d.CTC_NUMERO)+';'+
		rtrim(ltrim(ds.DIRECCION))+';'+ 
		ds.TIPO+';'+ 
		ISNULL( (select REPLACE(STR(CODIGO, 20), SPACE(1), '0') from SITREL_COMUNA where CODIGO = ds.comid), (SELECT TOP 1 REPLACE(STR(st.CODIGO, 20), SPACE(1), '0') comid 
		FROM SITREL_COMUNA ST, COMUNA C WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE) and st.CODEMP = ds.CODEMP and c.COM_COMID = ds.COMID))+';'+
		convert(varchar,ISNULL((select top 1 c.COM_CODPOST from COMUNA C where  c.COM_COMID = ds.comid) , 
		(SELECT TOP 1 C.COM_CODPOST comid FROM SITREL_COMUNA ST, COMUNA C WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE) and st.CODEMP = ds.CODEMP and st.CODIGO = ds.COMID)))
		from DEUDORES_DIRECCION_SITREL ds, DEUDORES d
		where d.ctc_codemp = @codemp
		and ds.CTCID = d.CTC_CTCID
		and ds.CODEMP = d.CTC_CODEMP
		and fecha > @fecha_inicio and fecha < @fecha_termino
		and ORIGEN = 'G'
		and (select COUNT(CCB_CCBID) from CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = @codemp and CCB_PCLID = @pclid and CCB_CTCID = d.CTC_CTCID) > 0

		
          


END
