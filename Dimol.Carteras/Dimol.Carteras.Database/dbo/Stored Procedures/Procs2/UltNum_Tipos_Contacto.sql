

Create Procedure UltNum_Tipos_Contacto(@tic_codemp integer) as
select IsNull(Max(tic_ticid)+1, 1) 
from tipos_contacto
where tic_codemp = @tic_codemp
