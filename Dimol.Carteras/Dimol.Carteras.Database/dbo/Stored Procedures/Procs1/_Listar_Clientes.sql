-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Clientes
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Clientes] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	Select distinct rol_pclid, pcl_nomfant from view_rol 
    where rol_codemp = @codemp
      and eci_idid = @idioma
      and tci_idid = @idioma
      and mji_idid = @idioma
      order by pcl_nomfant
  
END
