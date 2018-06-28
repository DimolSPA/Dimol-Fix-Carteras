CREATE PROC [dbo].[_Listar_Rol_Estados_Grilla_Count]      
      (          
       @codemp int,          
       @idid int,     
       @rolid int,     
       @where varchar(1000),          
       @sidx varchar(255),          
       @sord varchar(10),          
       @inicio int,          
       @limite int          
      )          
      
AS      
      
declare @query varchar(7000);    
DECLARE @TIPO_CLIENTE VARCHAR(1) = '' 

SELECT @TIPO_CLIENTE = P.PCL_TIPCLI FROM ROL R
INNER JOIN PROVCLI P
ON R.ROL_CODEMP = P.PCL_CODEMP
AND R.ROL_PCLID = P.PCL_PCLID
WHERE ROL_ROLID = @rolid       
            
set @query = '  select count(*) count from          
  (select *,ROW_NUMBER() OVER (ORDER BY Rolid desc) as row from              
  (   
   select * from (
	SELECT rol_estados.rle_rolid Rolid,   
	rol_estados.rle_estid IdEstado,   
	rol_estados.rle_esjid IdMateria,   
	rol_estados.rle_fecha Fecha,   
	materia_judicial_idiomas.mji_nombre Materia,  
	'''' Cuaderno,
	estados_cartera_idiomas.eci_nombre Estado,   
	rol_estados.rle_fecjud FechaJudicial,   
	rol_estados.rle_comentario Comentario, 
	usr_nombre Usuario,
	'''' Archivo
	FROM rol_estados,   
	materia_judicial_idiomas,   
	estados_cartera_idiomas, usuarios
	WHERE  rol_estados.rle_codemp = materia_judicial_idiomas.mji_codemp  and  
	rol_estados.rle_esjid = materia_judicial_idiomas.mji_esjid  and  
	rol_estados.rle_codemp = estados_cartera_idiomas.eci_codemp  and  
	rol_estados.rle_estid = estados_cartera_idiomas.eci_estid
	and rle_codemp = usr_codemp
	and rle_usrid = usr_usrid
	and rle_codemp= '  + CONVERT(VARCHAR,@codemp) +'
	and mji_idid= '  + CONVERT(VARCHAR,@idid) +'
	and eci_idid='  + CONVERT(VARCHAR,@idid) +'
	and rle_rolid= '  + CONVERT(VARCHAR,@rolid) 
	--union all
	--select 
	--r.rol_rolid Rolid,
	--0 IdEstado,
	--0 IdMateria,
	--r.rol_fecemi Fecha,
	--'''' Materia,
	--c.Desc_cuaderno Cuaderno,
	--h.ETAPA Estado,
	--h.FECHA_TRAMITE FechaJudicial,
	--h.TRAMITE + '' - ''+h.DESC_TRAMITE Comentario,
	--''PODER JUDICIAL'' USUARIO,
	--h.Ruta_Documento Archivo
	--from rol_poder_judicial rpj, poder_judicial_tribunal t, view_rol r, PODER_JUDICIAL_CUADERNO c,PODER_JUDICIAL_HISTORIAL h
	--where rpj.rpj_rolid = '  + CONVERT(VARCHAR,@rolid) +'
	--and rpj.rpj_tribunal = t.id_tribunal
	--and r.rol_codemp = rpj.rpj_codemp
	--and r.rol_rolid = rpj.rpj_rolid
	--and c.ID_CAUSA = rpj.rpj_ID_CAUSA
	--and h.ID_CAUSA = rpj.rpj_ID_CAUSA
	--and c.id_cuaderno = h.id_cuaderno' +
	
IF @TIPO_CLIENTE = 'P'
BEGIN
set @query = @query + 	' union all
	select  
	r.rol_rolid Rolid,
	0 IdEstado,
	0 IdMateria,
	r.rol_fecemi Fecha,
	'''' Materia,
	pjr.cuaderno collate SQL_Latin1_General_CP1_CI_AS Cuaderno,
	pjh.ETAPA collate SQL_Latin1_General_CP1_CI_AS Estado,
	pjh.FECHA_TRAMITE FechaJudicial,
	pjh.TRAMITE + '' - ''+pjh.DESC_TRAMITE collate SQL_Latin1_General_CP1_CI_AS Comentario,
	''PODER JUDICIAL'' USUARIO,
	pjh.Ruta_Documento Archivo
	from view_rol r with(nolock)
	inner join [10.0.1.11].[PoderJudicial].[dbo].[NUEVO_PJ_ROL] pjr with(nolock)
	on pjr.rit = r.rol_tipo_rol +''-''+r.rol_numero
	and pjr.[ID_TRIBUNAL] = 1329
	inner join [10.0.1.11].[PoderJudicial].[dbo].[NUEVO_PJ_HISTORIAL] pjh with(nolock)
	on pjr.[key] = pjh.[key]
	where rol_rolid = '  + CONVERT(VARCHAR,@rolid) +'

  ) as r
  where 1 = 1 ' 
END
ELSE
BEGIN
set @query = @query + 	' union all
	select 
	r.rol_rolid Rolid,
	0 IdEstado,
	0 IdMateria,
	r.rol_fecemi Fecha,
	'''' Materia,
	c.Desc_cuaderno collate SQL_Latin1_General_CP1_CI_AS Cuaderno,
	h.ETAPA collate SQL_Latin1_General_CP1_CI_AS Estado,
	h.FECHA_TRAMITE FechaJudicial,
	h.TRAMITE + '' - ''+h.DESC_TRAMITE collate SQL_Latin1_General_CP1_CI_AS Comentario,
	''PODER JUDICIAL'' USUARIO,
	h.Ruta_Documento Archivo
	from [10.0.1.11].[PoderJudicial].[dbo].rol_poder_judicial rpj  with(nolock), [10.0.1.11].[PoderJudicial].[dbo].poder_judicial_tribunal t  with(nolock), view_rol r, 
	[10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_CUADERNO c  with(nolock),[10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_HISTORIAL h  with(nolock)
	where rpj.rpj_rolid = '  + CONVERT(VARCHAR,@rolid) +'
	and rpj.rpj_tribunal = t.id_tribunal
	and r.rol_codemp = rpj.rpj_codemp
	and r.rol_rolid = rpj.rpj_rolid
	and c.ID_CAUSA = rpj.rpj_ID_CAUSA
	and h.ID_CAUSA = rpj.rpj_ID_CAUSA
	and c.id_cuaderno = h.id_cuaderno
  ) as r
  where 1 = 1 ' 

END

 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > 0'    
          
        
          
--select @query          
exec(@query) 