CREATE Procedure [dbo].[_Bloquear_Rol](
		@codemp int,
		@rolid integer, 
		@bloqueo varchar(1), 
		@usrid  int) as  
		
begin

declare @demonio varchar(1) 
if @bloqueo = 'S'
begin
	set @demonio = 'N'
end 
else
begin
	set @demonio = 'S'
end
			
           exec dbo._Insertar_Rol_Actualiza_PoderJudicial @codemp , @rolid, @demonio
           
           exec dbo._Insertar_Rol_Actualiza_PoderJudicial_Historial @codemp , @rolid, @demonio,@usrid 
           
           update ROL set ROL_BLOQUEO = @bloqueo
           where ROL_CODEMP = @codemp
           and ROL_ROLID = @rolid
           
           
end
