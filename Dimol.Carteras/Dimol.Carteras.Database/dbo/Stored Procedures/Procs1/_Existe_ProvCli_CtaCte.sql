CREATE Procedure [dbo].[_Existe_ProvCli_CtaCte](@pct_pclid numeric (15)) as
select 1 
from provcli_ctacte
where pct_pclid = @pct_pclid
