create procedure [dbo].[_Update_Historial_Bajas_Cpbt_Doc] (@pclid int, @ctcid int, @ccbid int, @historial text) as

update BAJAS_CPBT_DOC
set COMENTARIO = @historial
where PCLID = @pclid and CTCID = @ctcid and CCBID = @ccbid
