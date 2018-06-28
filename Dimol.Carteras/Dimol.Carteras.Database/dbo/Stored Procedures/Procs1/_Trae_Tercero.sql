CREATE PROCEDURE [dbo].[_Trae_Tercero] (@codemp int, @rutTercero varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	declare @existeTercero int = 0, @idTercero int = 0;
	set @existeTercero = (select count(TERCEROID) from CARTERA_CLIENTES_CPBT_DOC_TERCEROS (nolock)
						where CODEMP = @codemp and RUT = @rutTercero)
	-- SI NO EXISTE SE CREA
	if @existeTercero = 0
	begin
				
		Select 0 as terceroid
	end
	else
	begin
		select TERCEROID as terceroid from CARTERA_CLIENTES_CPBT_DOC_TERCEROS (nolock)
		where  CODEMP = @codemp and RUT = @rutTercero
	end
	
END
