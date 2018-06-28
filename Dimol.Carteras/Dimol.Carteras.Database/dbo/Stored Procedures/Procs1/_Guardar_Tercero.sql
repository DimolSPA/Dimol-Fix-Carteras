CREATE PROCEDURE [dbo].[_Guardar_Tercero] (@codemp int, @rutTercero varchar(20), @nombreTercero varchar(400))
AS
BEGIN
	SET NOCOUNT ON;
	declare @existeTercero int = 0, @idTercero int = 0;
	set @existeTercero = (select count(TERCEROID) from CARTERA_CLIENTES_CPBT_DOC_TERCEROS (nolock)
						where CODEMP = @codemp and RUT = @rutTercero)
	-- SI NO EXISTE SE CREA
	if @existeTercero = 0
	begin
		SET @idTercero =(SELECT IsNull(Max(TERCEROID)+1, 1)
						FROM CARTERA_CLIENTES_CPBT_DOC_TERCEROS)
		INSERT INTO CARTERA_CLIENTES_CPBT_DOC_TERCEROS
		(CODEMP, TERCEROID, RUT, NOMBRE)
		VALUES(@codemp,@idTercero,@rutTercero,@nombreTercero)
		
		Select @idTercero as terceroid
	end
	else
	begin
		select TERCEROID as terceroid from CARTERA_CLIENTES_CPBT_DOC_TERCEROS (nolock)
		where  CODEMP = @codemp and RUT = @rutTercero
	end
	
END
