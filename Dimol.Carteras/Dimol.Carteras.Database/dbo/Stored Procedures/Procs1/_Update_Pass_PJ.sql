
 create procedure [dbo].[_Update_Pass_PJ](@user varchar(30), @pass varchar(100)) as
	 update USUARIOS_PJ 
	 set PASSWORD = @pass
	 where LOGIN = @user

