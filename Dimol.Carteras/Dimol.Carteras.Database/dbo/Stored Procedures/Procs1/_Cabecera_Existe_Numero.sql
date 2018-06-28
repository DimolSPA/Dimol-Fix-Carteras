CREATE Procedure [dbo].[_Cabecera_Existe_Numero](@codemp int, @codsuc int, @tpcid int, @pclid int, @cpbt varchar(20), @numero int) as 

if @cpbt is null or @cpbt = '' 
begin
Select  cbc_numero Numero
FROM cabacera_comprobantes
WHERE  cbc_codemp =  @codemp
and cbc_sucid =  @codsuc
and cbc_tpcid =  @tpcid
and cbc_pclid = @pclid
and cbc_numprovcli = @numero
ORDER BY cbc_numero DESC

end
else
Select  cbc_numero Numero
FROM cabacera_comprobantes
WHERE  cbc_codemp =  @codemp
and cbc_sucid =  @codsuc
and cbc_tpcid =  @tpcid
and cbc_pclid = @pclid
and cbc_numprovcli = @cpbt
ORDER BY cbc_numero DESC
