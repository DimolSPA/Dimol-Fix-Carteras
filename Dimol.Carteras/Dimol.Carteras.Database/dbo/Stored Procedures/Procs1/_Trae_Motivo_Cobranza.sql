CREATE Procedure [dbo].[_Trae_Motivo_Cobranza](@codemp integer, @mtcid varchar(3)) as 
declare @Query varchar(7000)
set @Query='select mtc_mtcid, mtc_nombre 
			from motivo_cobranza 
			where mtc_codemp = ' + CONVERT(VARCHAR,@codemp)
			
if  @mtcid <> ''
begin
	set @Query= @Query + ' and mtc_mtcid = ' + @mtcid + ' order by mtc_nombre '
end

 exec(@query)
