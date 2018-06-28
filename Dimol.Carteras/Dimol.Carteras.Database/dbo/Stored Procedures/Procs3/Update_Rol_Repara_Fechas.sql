

Create procedure Update_Rol_Repara_Fechas as 

SELECT rol_estados.rle_rolid rolid,   
         max(rle_fecjud) as fecha
         into #EstRol  
    FROM rol_estados  
    where rle_estid not in(27, 1, 229)
GROUP BY rol_estados.rle_rolid   


select rolid, rle_esjid, rle_estid, rle_fecjud
into #EstJud
from #EstRol, rol_estados
where rle_rolid = rolid and
      rle_fecjud = fecha


update rol 
set rol_esjid = rle_esjid,
       rol_estid = rle_estid,
       rol_fecjud = rle_fecjud
from rol, #EstJud
where rol_rolid = rolid and
      rol_fecjud <> rle_fecjud
