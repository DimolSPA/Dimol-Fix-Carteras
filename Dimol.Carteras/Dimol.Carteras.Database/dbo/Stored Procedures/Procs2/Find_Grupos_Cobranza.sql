

Create Procedure Find_Grupos_Cobranza(@grc_codemp integer, @grc_sucid integer, @grc_grcid integer) as
select count(grc_grcid)
from grupos_cobranza
where grc_codemp = @grc_codemp and
           grc_sucid = @grc_sucid and
            grc_grcid = @grc_grcid
