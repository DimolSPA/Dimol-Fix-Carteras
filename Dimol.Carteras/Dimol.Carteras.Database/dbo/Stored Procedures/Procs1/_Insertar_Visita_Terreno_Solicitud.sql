CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Solicitud](
@codemp int,
@ctcid int,
@direccion varchar(50),
@idRegion int,
@idCiudad int,
@idComuna int,
@comuna varchar(50),
@userId int)

AS
	declare
	@deuda decimal(15,2) = 0,
	@solicitudId int = 0,
	@estado int = 1
	-- Se verifica si no existe la dirección de visita ya en solicitud

	set @solicitudId = isnull((SELECT SOLICITUD_ID 
								FROM VISITA_TERRENO_SOLICITUD
								WHERE CODEMP = @codemp
								AND CTCID = @ctcid
								AND DIRECCION = @direccion
								AND IDCOMUNA = @idComuna),0)
	if (@solicitudId = 0)
	BEGIN
	
		SET @solicitudId = (SELECT IsNull(Max(SOLICITUD_ID)+1, 1)
								FROM VISITA_TERRENO_SOLICITUD)
		SET @deuda = (SELECT SUM(CCB_SALDO)
						FROM CARTERA_CLIENTES_CPBT_DOC
						WHERE CCB_ESTCPBT IN ('V','J')
						AND CCB_CTCID =@ctcid)
		if @deuda is null
		begin
		 select -2 solicitud
		end
		else
		begin
			INSERT INTO VISITA_TERRENO_SOLICITUD (CODEMP
			, SOLICITUD_ID
			, CTCID
			, DIRECCION
			, IDREGION
			, IDCIUDAD
			, IDCOMUNA
			, COMUNA
			, DEUDA
			, ID_ESTATUS
			, USRID_CREACION)
			VALUES (@codemp, @solicitudId, @ctcid, @direccion, @idRegion, @idCiudad, @idComuna, @comuna, round(@deuda, 0), @estado, @userId)
					
			select @solicitudId solicitud
		end
		

	END
	ELSE
	select -3 solicitud


