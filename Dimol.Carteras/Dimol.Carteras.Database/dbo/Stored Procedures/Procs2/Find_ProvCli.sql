

Create Procedure Find_ProvCli(@pcl_codemp integer, @pcl_pclid integer) as
select count(pcl_pclid)
from provcli
where pcl_codemp = @pcl_codemp and
           pcl_pclid = @pcl_pclid
