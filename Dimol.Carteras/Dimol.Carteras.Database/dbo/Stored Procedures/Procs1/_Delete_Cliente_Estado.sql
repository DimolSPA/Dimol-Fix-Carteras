CREATE PROCEDURE _Delete_Cliente_Estado(@codemp int, @pclid int, @estid int) 
AS
BEGIN
  DELETE FROM CLIENTES_ESTADOS  
   WHERE CODEMP = @codemp
   AND PCLID = @pclid
   AND ESTID = @estid
END		                
