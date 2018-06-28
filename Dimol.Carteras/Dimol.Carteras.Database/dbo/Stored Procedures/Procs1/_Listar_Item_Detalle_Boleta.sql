
-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Item_Detalle_Boleta] (@texto varchar(200),@codemp int , @tipprod int,@provcli int, @gastjud varchar(2))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'
declare @codigo varchar(20) = '%' +@texto + '%'
declare @query varchar(2000)

set @query = 'select ins_codigo + '' - '' + INS_NOMBRE, ins_insid
				from insumos
				where ins_codemp = ' + CONVERT(VARCHAR,@codemp) +'
				and ins_abreviado is not null 
				and ins_estado =''U''
				and (ins_codigo like ''%' + @texto +'%'' or ins_nombre like ''%' + @texto +'%'')'
If @tipprod = 3 
begin
	set @query = @query + ' and ins_tipo in (1,2,3) '
end
Else
begin
	set @query = @query + ' and ins_tipo= ' + CONVERT(VARCHAR,@tipprod) 
End 

If @gastjud = 'J'
begin
	set @query = @query + ' and ins_gastjud=''S'''
End

If @provcli > 0
begin
	set @query = @query + ' and ins_insid in (select mip_insid from maestra_insumos_provcli '
	set @query = @query + ' where mip_codemp =  ' + CONVERT(VARCHAR,@codemp) 
	set @query = @query + ' and mip_pclid =  ' + CONVERT(VARCHAR,@provcli) +')'
end 
If @tipprod >= 2 
begin
	set @query = @query + ' union '
	set @query = @query + ' Select ins_codigo + '' - '' + INS_NOMBRE, ins_insid   from insumos '
	set @query = @query + ' where ins_codemp = ' + CONVERT(VARCHAR,@codemp)
	set @query = @query + ' and ins_abreviado is not null '
	set @query = @query + ' and ins_estado =''U'''
	set @query = @query + ' and ins_tipo in (2) and (ins_codigo like ''%' + @texto +'%'' or ins_nombre like ''%' + @texto +'%'')'
End


exec  (@query)
end
