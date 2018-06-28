CREATE Procedure [dbo].[_Update_Rol](@rol_codemp integer, @rol_rolid integer, @rol_pclid numeric (15), @rol_ctcid integer, @rol_numero varchar (20),
									@rol_trbid integer, @rol_tcaid integer, @rol_fecdem datetime,
									@rol_fecrol datetime,  @rol_comentario text, @rol_bloqueo char(1), @rol_prequiebra char(1), @rol_tipo_rol char(1)) as  
  UPDATE rol  
     SET   rol_pclid = @rol_pclid,   
         rol_ctcid = @rol_ctcid,   
         rol_numero = @rol_numero,   
         rol_trbid = @rol_trbid,   
         rol_tcaid = @rol_tcaid,   
         rol_fecdem = @rol_fecdem,   
         rol_fecrol = @rol_fecrol,   
         rol_comentario = @rol_comentario,
         rol_bloqueo = @rol_bloqueo,
         rol_prequiebra = @rol_prequiebra,
         ROL_TIPO_ROL = @rol_tipo_rol
   WHERE ( rol.rol_codemp = @rol_codemp ) AND  
         ( rol.rol_rolid = @rol_rolid )
