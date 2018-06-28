

Create procedure UltNum_Bodegas_Sector_Cubiculo(@bsc_codemp integer, @bsc_bodid integer, @bsc_bdsid integer) as
select IsNull(Max(bsc_bscid)+1, 1)
from bodegas_sector_cubiculo
where bsc_codemp = @bsc_codemp and
           bsc_bodid = @bsc_bodid and
           bsc_bdsid = @bsc_bdsid
