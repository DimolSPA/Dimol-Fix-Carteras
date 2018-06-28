-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
Create PROCEDURE [dbo].[_Listar_Tipo_Contacto]
(
@codemp int,
@idioma integer
)
AS
BEGIN
	SET NOCOUNT ON;
	
	select tci_ticid Id, 
	tci_nombre Descripcion
	from tipos_contacto_idiomas 
	where tci_codemp =@codemp 
	and tci_idid=@idioma
	order by tci_nombre

	

END
