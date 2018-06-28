CREATE Procedure [dbo].[_Existe_EnteJudicial](@etj_pclid numeric (15)) as
select 1 
from ENTES_JUDICIAL
where etj_pclid = @etj_pclid
