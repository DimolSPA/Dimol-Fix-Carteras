

Create Procedure Find_Tipos_Contacto(@tic_codemp integer, @tic_ticid integer) as
select count(tic_ticid)
from tipos_contacto
where tic_codemp = @tic_codemp and
           tic_ticid = @tic_ticid
