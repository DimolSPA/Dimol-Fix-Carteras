-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
Create PROCEDURE [dbo].[_Listar_Estados_Cartera_Reversa_Traspaso_Judicial]
(
@codemp int,
@idioma integer
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT ECT_ESTID Id,  [ECT_NOMBRE] Nombre
  FROM [ESTADOS_CARTERA], [ESTADOS_CARTERA_IDIOMAS]
  where ECT_CODEMP =@codemp
  and ECT_PREJUD in ('P','A')
  and ECT_CODEMP = ECI_CODEMP
  and ECT_ESTID = ECI_ESTID
  and ECI_IDID = @idioma
  order by ECT_NOMBRE asc
  
END
