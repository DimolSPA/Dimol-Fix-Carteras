

Create Procedure Update_Usuarios_Estado(@usr_codemp integer, @usr_usrid integer, @usr_estado char(1)) as
IF @usr_estado = 'B'
	BEGIN
		  UPDATE usuarios  
			  SET 	usr_fecblock = getdate(),   
					usr_estado = @usr_estado  
			WHERE ( usuarios.usr_codemp = @usr_codemp ) AND  
					( usuarios.usr_usrid = @usr_usrid )   
	END
ELSE
		  UPDATE usuarios  
			  SET  usr_godlog = 0,   
					usr_badlog = 0,   
					usr_fecblock = null,   
					usr_estado = @usr_estado  
			WHERE ( usuarios.usr_codemp = @usr_codemp ) AND  
					( usuarios.usr_usrid = @usr_usrid )
