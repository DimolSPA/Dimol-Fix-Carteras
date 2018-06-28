CREATE Procedure [dbo].[_Insertar_Perfiles_Estados_Historial](@perfil int, @estid int, @accion int, @usrid int) 
as
begin
  INSERT INTO PERFILES_ESTADOS_HISTORIAL  
         ( PERFIL_ID,   
           ESTID,   
           ACCION,
		   USRID_REGISTRO )  
  VALUES ( @perfil,   
           @estid,   
           @accion,
		   @usrid)
end
