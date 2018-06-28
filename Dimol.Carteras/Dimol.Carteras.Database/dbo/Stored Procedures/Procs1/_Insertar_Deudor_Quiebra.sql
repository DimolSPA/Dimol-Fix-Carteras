CREATE PROCEDURE [dbo].[_Insertar_Deudor_Quiebra](
@codemp int,
@rut varchar(20),
@nombre varchar(600),
@rolNumero varchar(20),
@tribunalId int,
@tipoCausaId int,
@materiaId int,
@user int)
	
AS
BEGIN

declare
	@existDeudor int = 0
	
	-- INSERTAR DATOS EN VISITA TERRENO
	SET @existDeudor = (select count(RUT) from DEUDOR_QUIEBRA where RUT = @rut)
	if @existDeudor = 0
	begin
		INSERT INTO DEUDOR_QUIEBRA (CODEMP,RUT,NOMBRE,ROLNUMERO,TRIBUNALID,TIPOCAUSAID,MATERIAJODICIALID, USRID_REGISTRO)
		VALUES(@codemp,@rut,@nombre,@rolNumero,@tribunalId,@tipoCausaId,@materiaId,@user)
		select 2 as rut
	end
	else
	begin
		select 0 as rut
	end
		
	
END
