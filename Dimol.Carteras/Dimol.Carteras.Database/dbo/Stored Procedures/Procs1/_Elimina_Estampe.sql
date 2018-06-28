create procedure [dbo].[_Elimina_Estampe] (@codemp int, @ddeid int, @pclid int, @ctcid int, @rolid int) as
	delete from DEUDORES_ESTAMPES 
	where DDE_CODEMP = @codemp 
	and DDE_DDEID = @ddeid
	and DDE_PCLID = @pclid 
	and DDE_CTCID = @ctcid 
	and DDE_ROLID = @rolid
