

create procedure [dbo].[_Insert_User_PJ] (@iduser int, @nombre varchar(200), @username varchar(30), @pass varchar(100), @activa int, @pclid int, @adm varchar(1)) as

	if (@iduser = 0) 
		begin
			insert into USUARIOS_PJ (NOMBRE, LOGIN, PASSWORD, FECING, FECULTLOG, ACTIVA, ENUSO, PCLID, ADM)
			VALUES (@nombre, @username, @pass, getdate(), getdate(), @activa, 0, @pclid, @adm)
		end

	else
		begin
			update USUARIOS_PJ 
			set NOMBRE = @nombre, 
			LOGIN = @username, 
			PASSWORD = @pass, 
			PCLID = @pclid, 
			ADM = @adm, 
			ACTIVA = @activa 
			WHERE USRID = @iduser
		end



