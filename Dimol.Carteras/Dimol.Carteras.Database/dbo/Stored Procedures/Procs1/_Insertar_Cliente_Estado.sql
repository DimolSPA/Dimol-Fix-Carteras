CREATE Procedure [dbo].[_Insertar_Cliente_Estado](@codemp int,@pclid int, @estid int) 
as
begin
  INSERT INTO CLIENTES_ESTADOS 
         ( CODEMP,   
           PCLID,   
           ESTID)  
  VALUES ( @codemp,   
           @pclid,   
           @estid)
end 
