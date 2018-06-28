
CREATE procedure [dbo].[_Trae_Ruta_Estampes](@codemp int, @pclid numeric(15), @ctcid numeric(15), @rolid int, @insid numeric(15), @item int, @tpcid int, @numero numeric(15)) as
	select CAST(DDE_PCLID AS VARCHAR) + '\' + CAST(DDE_CTCID AS VARCHAR) + '\' + CAST(DDE_ROLID AS VARCHAR) + '\' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT AS Estampe 
	FROM DEUDORES_ESTAMPES 
	where DDE_CODEMP = @codemp 
	AND DDE_PCLID = @pclid 
	AND DDE_CTCID = @ctcid 
	AND DDE_ROLID = @rolid 
	AND DDE_INSID = @insid 
	AND DDE_ITEM = @item 
	AND DDE_TPCID = @tpcid 
	AND DDE_NUMERO = @numero 
