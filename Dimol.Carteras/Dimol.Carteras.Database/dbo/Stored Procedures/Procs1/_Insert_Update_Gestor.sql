CREATE PROCEDURE [dbo].[_Insert_Update_Gestor](
@ges_codemp integer, 
@ges_sucid integer, 
@ges_gesid integer,
@ges_nombre varchar (500),
@ges_telefono integer, 
@ges_email varchar (100), 
@ges_tipcart smallint, 
@ges_emplid integer,
@ges_remoto char(1),
@ges_estado char(1),
@ges_grupoid integer,
@ges_visita_terreno char(1),
@ges_telefono_terreno varchar(9),
@ges_imei varchar (100))
AS
BEGIN
	declare @existe int = 0,
	@gesid int = 0

		select @existe = count(ges_gesid)
		from gestor
		where ges_codemp  = @ges_codemp and 
				   ges_sucid = @ges_sucid and
				   ges_gesid = @ges_gesid

	if(@existe = 0 )
	begin  
		set @gesid = (select IsNull(Max(ges_gesid)+1, 1)
						from gestor
						where ges_codemp  = @ges_codemp and 
								   ges_sucid = @ges_sucid)
		INSERT INTO gestor  
			 ( ges_codemp,   
			   ges_sucid,   
			   ges_gesid,   
			   ges_nombre,   
			   ges_telefono,   
			   ges_email,   
			   ges_tipcart,   
			   ges_comki,   
			   ges_comhon,   
			   ges_emplid,
			   ges_remoto,
			   ges_estado,
			   ges_comjki,
			   ges_comjhon,
			   ges_visita_terreno,
			   ges_telefono_terreno,
			   ges_imei)  
		VALUES (@ges_codemp,   
			   @ges_sucid,   
			   @gesid,   
			   @ges_nombre,   
			   @ges_telefono,   
			   @ges_email,   
			   @ges_tipcart,   
			   0,   
			   0,   
			   @ges_emplid,
			   @ges_remoto,
			   @ges_estado,
			   0,
			   0,
			   @ges_visita_terreno,
			   @ges_telefono_terreno,
			   @ges_imei)


		exec dbo.Insertar_Grupo_Cobranza_Gestor @ges_codemp, @ges_sucid, @ges_grupoid, @gesid
	
	end
	else
	begin
		UPDATE gestor  
		 SET ges_nombre = @ges_nombre,   
			 ges_telefono = @ges_telefono,   
			 ges_email = @ges_email,   
			 ges_tipcart = @ges_tipcart,   
			 ges_emplid = @ges_emplid,
			 ges_remoto = @ges_remoto,
			 ges_estado = @ges_estado,
			 ges_visita_terreno = @ges_visita_terreno,
			 ges_telefono_terreno = @ges_telefono_terreno,
			 ges_imei= @ges_imei
	   WHERE ( gestor.ges_codemp = @ges_codemp ) AND  
			 ( gestor.ges_sucid = @ges_sucid ) AND  
			 ( gestor.ges_gesid = @ges_gesid )


		UPDATE	grupo_cobranza_gestor 
		SET gcg_grcid = @ges_grupoid
		WHERE  gcg_codemp=@ges_codemp
		AND gcg_sucid = @ges_sucid
		AND gcg_gesid = @ges_gesid

	end
END
