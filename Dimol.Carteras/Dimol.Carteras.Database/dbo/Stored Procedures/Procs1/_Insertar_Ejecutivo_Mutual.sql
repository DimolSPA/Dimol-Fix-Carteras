CREATE procedure [dbo].[_Insertar_Ejecutivo_Mutual] (@cliente int, @ejecutivo varchar(200), @email varchar(200), @oficina varchar(200), @idejecutivo int) as 
	
	if @idejecutivo = 0
	begin
		insert into EJECUTIVO_MUTUAL (NOMBRE, EMAIL, OFICINA)
		values (@ejecutivo, @email, @oficina)

		insert into PROVCLI_EJECUTIVO (PCLID, ID_EJECUTIVO) values (@cliente, SCOPE_IDENTITY())
	end
	else
	begin
		update EJECUTIVO_MUTUAL 
		set EMAIL = @email, OFICINA = @oficina
		where ID_EJECUTIVO = @idejecutivo
	end
