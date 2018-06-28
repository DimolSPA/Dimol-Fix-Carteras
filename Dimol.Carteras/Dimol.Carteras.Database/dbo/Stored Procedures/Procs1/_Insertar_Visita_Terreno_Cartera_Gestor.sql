CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Cartera_Gestor](
@carteraId varchar(100),
@gestorId int,
@carteraNombre varchar(100),
@carteraDescripcion varchar(100),
@userId int)

AS
BEGIN
declare @countCartera int = 0
	set @countCartera = (select count(gesid) from VISITA_TERRENO_CARTERA_GESTOR
						where GESID = @gestorId)
	if @countCartera = 0
	begin
		INSERT INTO [dbo].[VISITA_TERRENO_CARTERA_GESTOR]
           ([CARTERA_ID]
           ,[GESID]
           ,[CARTERA_NOMBRE]
           ,[DESCRIPCION]
           ,[USRID_REGISTRO])
		VALUES
           (@carteraId
           ,@gestorId
           ,@carteraNombre
           ,@carteraDescripcion
           ,@userId)
		select 1 cartera
	end
	else
	begin
	 select -2 cartera
	end
END
