-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Trae_Ultimo_Saldo_Reversa_Traspaso]
(
@codemp int,
@pclid int ,
@ctcid int,
@ccbid int
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT top 1 CEH_SALDO saldo
	FROM CARTERA_CLIENTES_ESTADOS_HISTORIAL 
	where CEH_CODEMP = @codemp
	and CEH_PCLID = @pclid
	and CEH_CTCID= @ctcid 
	and CEH_CCBID = @ccbid
	and CEH_SALDO > 0
	order by CEH_FECHA desc

END
