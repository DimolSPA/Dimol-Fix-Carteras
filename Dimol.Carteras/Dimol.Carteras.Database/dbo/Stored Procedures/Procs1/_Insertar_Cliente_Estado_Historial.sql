CREATE Procedure [dbo].[_Insertar_Cliente_Estado_Historial](@pclid int, @estid int, @accion int, @usrid int) 
as
begin
  INSERT INTO CLIENTES_ESTADOS_HISTORIAL  
         ( PCLID,   
           ESTID,   
           ACCION,
		   USRID_REGISTRO )  
  VALUES ( @pclid,   
           @estid,   
           @accion,
		   @usrid)
end   
