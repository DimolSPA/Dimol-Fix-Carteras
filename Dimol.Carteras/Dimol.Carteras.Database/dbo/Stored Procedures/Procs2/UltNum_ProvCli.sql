

Create Procedure UltNum_ProvCli(@pcl_codemp integer) as
select IsNull(Max(pcl_pclid)+1, 1)  
from provcli
where pcl_codemp = @pcl_codemp
