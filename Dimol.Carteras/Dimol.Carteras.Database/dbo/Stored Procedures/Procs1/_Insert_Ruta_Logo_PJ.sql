
	 
create procedure [dbo].[_Insert_Ruta_Logo_PJ] (@id int, @pclid int, @ruta varchar(500)) as
	
	if (@id = 0) 
		begin
			INSERT INTO USUARIOS_PJ_RUTAS (PCLID, RUTA)
			VALUES (@pclid, @ruta)
		end

	else
		begin
			update USUARIOS_PJ_RUTAS 
			set PCLID = @pclid,
			RUTA = @ruta 
			WHERE ID = @id
		end

