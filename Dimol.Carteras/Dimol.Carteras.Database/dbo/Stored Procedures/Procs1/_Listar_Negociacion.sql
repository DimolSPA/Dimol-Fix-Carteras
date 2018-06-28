-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
create PROCEDURE [dbo].[_Listar_Negociacion] 
(
	@codemp as integer,
	@ctcid as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	 SELECT neg_anio,
		neg_negid
		--, convert(varchar, neg_anio) + ',' + convert(varchar, neg_negid) as Nego
		FROM negociacion
		WHERE  neg_codemp =  @codemp
		and neg_ctcid =   @ctcid
		and neg_estado in ( 'A', 'F' ) 
		ORDER BY neg_negid  
END
