CREATE Procedure [dbo].[_Delete_Cartera_Demanda_Pendiente](@codemp integer, @pclid numeric (15), @ctcid numeric (15), @ccbid integer) as 
                       
delete from [CARTERA_CLIENTES_DEMANDA_PENDIENTE]
where [CDP_CODEMP]=@codemp
and [CDP_PCLID]=@pclid
and [CDP_CTCID]=@ctcid
and [CDP_CCBID]=@ccbid
