﻿create Procedure Update_ProvCli_Logo(@pcl_codemp integer, @pcl_pclid integer, @pcl_logo image) as    UPDATE provcli         SET pcl_logo = @pcl_logo     WHERE ( provcli.pcl_codemp = @pcl_codemp ) AND             ( provcli.pcl_pclid = @pcl_pclid )                