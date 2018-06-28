CREATE Procedure [dbo].[_Insertar_Rol]
(
	@rol_codemp integer,
	@rol_pclid numeric (15),
	@rol_ctcid integer,
	@rol_trbid integer,
	@rol_tcaid integer,
	@rol_estid smallint,
	@rol_fecdem datetime = null,
	@rol_fecrol datetime = null,
	@rol_comentario text = null,
	@rol_esjid integer,
	@rol_fecjud datetime = null,
	@rol_tipo_rol varchar(1),
	@rol_numero varchar(200),
	@rle_usrid integer,
	@rle_ipred varchar (30),
	@rle_ipmaquina varchar (30),
	@rle_comentario text
) as  
	declare @rol_rolid int

	set @rol_rolid = (select IsNull(Max(rol_rolid)+1, 1) from rol)
		
  INSERT INTO rol  
         ( rol_codemp,   
           rol_rolid,   
           rol_pclid,   
           rol_ctcid,   
           rol_numero,   
           rol_trbid,   
           rol_tcaid,   
           rol_estid,   
           rol_fecemi,   
           rol_fecdem,   
           rol_fecrol,   
           rol_total,   
           rol_comentario,   
           rol_esjid,   
           rol_fecjud,   
           rol_fecultgest,
           rol_bloqueo,
           rol_prequiebra,
           rol_tipo_rol )  
  VALUES ( @rol_codemp,   
           @rol_rolid,   
           @rol_pclid,   
           @rol_ctcid,   
           @rol_numero,   
           @rol_trbid,   
           @rol_tcaid,   
           @rol_estid,   
           getdate(),   
           @rol_fecdem,   
           @rol_fecrol,   
           0,   
           @rol_comentario,   
           @rol_esjid,   
           GETDATE(),   
           getdate(),
           'N',
           'N',
           @rol_tipo_rol )


INSERT INTO rol_estados  
         ( rle_codemp,   
           rle_rolid,   
           rle_estid,   
           rle_esjid,   
           rle_fecha,   
           rle_usrid,   
           rle_ipred,   
           rle_ipmaquina,   
           rle_comentario,   
           rle_fecjud )  
  VALUES ( @rol_codemp,   
           @rol_rolid,   
           @rol_estid,   
           @rol_esjid,   
           getdate(),   
           @rle_usrid,   
           @rle_ipred,   
           @rle_ipmaquina,   
           @rle_comentario,   
            getdate())

--
declare @rolid int = isnull((Select ROLID from ROL_ACTUALIZA_PODERJUDICIAL where ROLID = @rol_rolid and CODEMP = @rol_codemp), 0)

if @rolid  > 0
begin 
	update ROL_ACTUALIZA_PODERJUDICIAL
	set [FLAG_PODERJUDICIAL] = 'S'
	where ROLID = @rolid
	
	insert into ROL_ACTUALIZA_PODERJUDICIAL_HISTORIAL ([CODEMP]
      ,[ROLID]
      ,[FLAG_PODERJUDICIAL]
      ,[USERID]
      ,[FEC_REGISTRO])
      values (@rol_codemp,   
           @rol_rolid,
           'S', @rle_usrid, getdate() )
end
else
	begin
	insert into ROL_ACTUALIZA_PODERJUDICIAL ([CODEMP]
		  ,[ROLID]
		  ,[FLAG_PODERJUDICIAL])
		  values (@rol_codemp,   
			   @rol_rolid,
			   'S')

	insert into ROL_ACTUALIZA_PODERJUDICIAL_HISTORIAL ([CODEMP]
      ,[ROLID]
      ,[FLAG_PODERJUDICIAL]
      ,[USERID]
      ,[FEC_REGISTRO])
      values (@rol_codemp,   
           @rol_rolid,
           'S', @rle_usrid, getdate() )
end


--

select @rol_rolid as rolid