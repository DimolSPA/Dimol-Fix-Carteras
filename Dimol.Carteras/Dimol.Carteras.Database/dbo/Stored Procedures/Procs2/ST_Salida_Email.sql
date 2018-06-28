-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Salida_Email] 
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
		ltrim(rtrim(dm.MAIL ))
		from DEUDORES_MAIL_SITREL dm, DEUDORES d
		where d.ctc_codemp = @codemp
		and dm.ORIGEN = 'G'
		and fecha > @fecha_inicio and fecha < @fecha_termino
		and dm.CTCID = d.CTC_CTCID
		and dm.CODEMP = d.CTC_CODEMP
		and (select COUNT(CCB_CCBID) from CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = @codemp and CCB_PCLID = @pclid and CCB_CTCID = d.CTC_CTCID) > 0
		        


END
