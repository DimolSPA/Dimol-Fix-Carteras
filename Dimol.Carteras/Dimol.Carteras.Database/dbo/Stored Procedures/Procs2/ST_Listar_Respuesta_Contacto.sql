-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Listar_Respuesta_Contacto] 
(
	@codemp as integer,
	@pclid as integer,
	@accion as varchar(20),
	@contacto as  varchar(20)
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
	SELECT distinct CONVERT(varchar,e.ECT_ESTID) + '|'+ r.[CODIGO] [CODIGO]
      ,r.[NOMBRE]
  FROM [SITREL_RESPUESTA] r, [SITREL_RELACION] rl, ESTADOS_CARTERA e
  where r.CODEMP = @codemp
  and r.PCLID=@pclid
  and r.CODEMP = rl.CODEMP
  and r.PCLID = rl.PCLID
  and r.CODIGO = rl.RESPUESTA
  and rl.ACCION = @accion
  and rl.CONTACTO = @contacto
  and e.ECT_CODEMP = r.CODEMP
  and e.ECT_NOMBRE = r.NOMBRE
  AND R.ESTADO = 'V'

          


END
