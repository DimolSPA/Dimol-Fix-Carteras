create Procedure [dbo].[_Insertar_Cartera_Categoria](@codemp integer, 
													@ctcid numeric (15),  
													@categoria varchar(2),
                                                    @usrid integer) as 
                                                    
                                                    
declare @ddcid smallint
declare @existe int = 0

select @existe = count([CCC_CODEMP]) from [CARTERA_CLIENTES_CATEGORIA] where [CCC_CODEMP] = @codemp and [CCC_CTCID] = @ctcid
if @existe = 0     
begin
INSERT INTO [CARTERA_CLIENTES_CATEGORIA]
           ([CCC_CODEMP]
           ,[CCC_CTCID]
           ,[CCC_CATEGORIA]
           ,[CCC_FECHA]
           ,[CCC_USRID])
     VALUES
           (@codemp
           ,@ctcid
           ,@categoria
           ,GETDATE()
           ,@usrid)
           
end
else
begin

UPDATE [CARTERA_CLIENTES_CATEGORIA]
   SET [CCC_CATEGORIA] = @categoria
      ,[CCC_FECHA] = GETDATE()
      ,[CCC_USRID] = @usrid
 WHERE [CCC_CODEMP] = @codemp
  and [CCC_CTCID] = @ctcid


end








