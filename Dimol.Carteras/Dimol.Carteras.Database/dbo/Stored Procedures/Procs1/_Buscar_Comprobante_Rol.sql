-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Comprobante_Rol] (@codemp int, @sucid int, @tpcid int,  @numero int)
AS
BEGIN
	SET NOCOUNT ON;
			
		select distinct top 1 ROL_ROLID,
				ROL_NUMERO,
				ROL_TRBID,
				isnull(ROL_TIPO_ROL,'C')  ROL_TIPO_ROL,
				TRB_NOMBRE, 
				ROL_CTCID 
		from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp =ROL_CODEMP and DCR_ROLID =ROL_ROLID
		inner join TRIBUNALES on DCR_CODEMP = TRB_CODEMP and ROL_TRBID = TRB_TRBID
		where dcr_codemp =@codemp
		and dcr_sucid =@sucid
		and dcr_tpcid =@tpcid
		and dcr_numero =@numero
END
