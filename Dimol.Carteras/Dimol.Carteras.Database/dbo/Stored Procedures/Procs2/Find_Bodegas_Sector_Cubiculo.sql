

Create procedure Find_Bodegas_Sector_Cubiculo(@bsc_codemp integer, @bsc_bodid integer, @bsc_bdsid integer, @bsc_bscid varchar(20)) as
select count(bsc_bscid)
from bodegas_sector_cubiculo
where bsc_codemp = @bsc_codemp and
           bsc_bodid = @bsc_bodid and
           bsc_bdsid = @bsc_bdsid and
           bsc_bscid = @bsc_bscid
