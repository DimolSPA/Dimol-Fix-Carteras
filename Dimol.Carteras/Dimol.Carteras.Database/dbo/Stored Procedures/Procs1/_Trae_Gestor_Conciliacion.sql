CREATE PROCEDURE [dbo].[_Trae_Gestor_Conciliacion](
@codemp int,
@pclid int,
@ctcid int
)
AS
BEGIN

select top 1 g.GES_GESID, g.GES_NOMBRE  from GESTOR_CARTERA gc
join GESTOR g
on gc.GSC_GESID = g.GES_GESID
where GSC_CODEMP= @codemp
and GSC_PCLID = @pclid 
and GSC_CTCID = @ctcid

END
