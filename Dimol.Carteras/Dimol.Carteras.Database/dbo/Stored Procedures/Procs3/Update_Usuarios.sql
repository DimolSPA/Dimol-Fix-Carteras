

Create Procedure Update_Usuarios(@usr_codemp integer, @usr_usrid integer, @usr_nombre varchar (200), @usr_login varchar (15),
                                                          @usr_password varchar (25), @usr_mail varchar (80), @usr_tipquest smallint,
                                                          @usr_answer varchar (200), @usr_sucid integer, @usr_prfid integer, @usr_permisos smallint, @usr_campass char(1), @usr_feccambio datetime ) as  

	  UPDATE usuarios  
		  SET usr_nombre = @usr_nombre,   
				usr_login = @usr_login,   
				usr_password = @usr_password,   
				usr_mail = @usr_mail,   
				usr_tipquest = @usr_tipquest,   
				usr_answer = @usr_answer,   
				usr_sucid = @usr_sucid,   
				usr_prfid = @usr_prfid,   
				usr_permisos = @usr_permisos,
                usr_campass = @usr_campass,
                usr_feccambio = @usr_feccambio 
		WHERE ( usuarios.usr_codemp = @usr_codemp ) AND  
				( usuarios.usr_usrid = @usr_usrid )
