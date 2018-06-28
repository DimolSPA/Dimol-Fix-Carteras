CREATE Procedure [dbo].[_Existe_ProvCli](@pcl_rut varchar(20)) as
select pcl_pclid 
from provcli
where pcl_rut = @pcl_rut
