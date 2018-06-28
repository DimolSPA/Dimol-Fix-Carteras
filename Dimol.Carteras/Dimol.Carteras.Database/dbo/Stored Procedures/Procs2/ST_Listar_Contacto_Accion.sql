-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Listar_Contacto_Accion] 
(
	@codemp as integer,
	@pclid as integer,
	@accion as varchar(20)
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	  SELECT distinct c.[CODIGO]
      ,c.[NOMBRE]
  FROM [SITREL_CONTACTO] c, [SITREL_RELACION] rl
  where c.CODEMP = @codemp
  and c.PCLID=@pclid
  and c.CODEMP = rl.CODEMP
  and c.PCLID = rl.PCLID
  and c.CODIGO = rl.CONTACTO
  and rl.ACCION = @accion  
  AND C.ESTADO = 'V'  

END
