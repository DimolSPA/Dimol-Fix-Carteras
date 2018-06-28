
-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Trae_Padre_Detalle_Comprobante] (@codemp int, @sucid int, @tpcid int, @numero varchar(30), @item int )
AS
BEGIN
	SET NOCOUNT ON;

select isnull(dcc_tpcidpad,0) tpcidpad, isnull(dcc_numeropad,0) numeropad, isnull(dcc_itempad,0) itempad from detalle_comprobantes
where dcc_codemp =  @codemp
and dcc_sucid =  @sucid
and dcc_tpcid =  @tpcid
and dcc_numero =  @numero
and dcc_item =  @item

END
