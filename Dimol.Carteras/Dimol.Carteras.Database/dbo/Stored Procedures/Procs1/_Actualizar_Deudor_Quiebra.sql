CREATE PROCEDURE [dbo].[_Actualizar_Deudor_Quiebra](
@codemp int,
@rut varchar(20),
@tipoCausaId int,
@materiaId int)
	
AS
BEGIN
	declare
	@existDeudor int = 0
	
	SET @existDeudor = (select count(RUT) from DEUDOR_QUIEBRA where RUT = @rut)
	if @existDeudor > 0
	begin
		UPDATE DEUDOR_QUIEBRA 
		SET TIPOCAUSAID = @tipoCausaId
		where RUT = @rut and TIPOCAUSAID IS NULL

		UPDATE DEUDOR_QUIEBRA 
		SET MATERIAJODICIALID = @materiaId
		where RUT = @rut and MATERIAJODICIALID IS NULL
		select 2 rut
	end	
	else
	begin
	 select 1 rut
	end
	
END
