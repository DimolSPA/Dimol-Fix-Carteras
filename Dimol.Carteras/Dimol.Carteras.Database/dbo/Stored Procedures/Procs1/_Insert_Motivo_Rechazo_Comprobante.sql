
CREATE PROCEDURE [dbo].[_Insert_Motivo_Rechazo_Comprobante](
@codemp int, 
@tipoComprobante int, 
@folio int, 
@pclid int, 
@motivo varchar(1000),
@user integer)
AS
BEGIN
	insert into CASTIGO_DEVOLUCION_RECHAZO(CODEMP,TIPOCOMPROBANTE, FOLIO, PCLID, MOTIVO, USRID_REGISTRO)
	values(@codemp, @tipoComprobante, @folio, @pclid, @motivo, @user)
END

select * from CASTIGO_DEVOLUCION_RECHAZO
