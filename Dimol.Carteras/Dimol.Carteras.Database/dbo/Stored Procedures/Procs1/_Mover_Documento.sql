CREATE Procedure [dbo].[_Mover_Documento](@codemp integer,@pclid integer, @ctcid integer, @ccbid integer, @comentario varchar(800), @estCpbt varchar(2), @estid integer) as

select getdate()