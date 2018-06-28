CREATE PROCEDURE [dbo].[_Exist_Conciliacion_NumComprobante](
@numComprobante int
)
AS
BEGIN
	
	--Verificar si el numero de comprobante existe registrado en el sistema
	declare @existComprobante int = 0;
	set @existComprobante= (select count(NUM_COMPROBANTE) 
							from CONCILIACION_MOVIMIENTOS_DOCUMENTOS 
							where NUM_COMPROBANTE =@numComprobante)

	select @existComprobante countComprobante
	
END
