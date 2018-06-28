

CREATE procedure [dbo].[_Eliminar_Datos_Estampes](@codemp int, @pclid numeric(15), @ctcid numeric(15), @rolid int, @insid numeric(15), @item int, @tpcid int, @numero numeric(15)) as
	DELETE 
	FROM DEUDORES_ESTAMPES 
	where DDE_CODEMP = @codemp 
	AND DDE_PCLID = @pclid 
	AND DDE_CTCID = @ctcid 
	AND DDE_ROLID = @rolid 
	AND DDE_INSID = @insid 
	AND DDE_ITEM = @item 
	AND DDE_TPCID = @tpcid 
	AND DDE_NUMERO = @numero 

