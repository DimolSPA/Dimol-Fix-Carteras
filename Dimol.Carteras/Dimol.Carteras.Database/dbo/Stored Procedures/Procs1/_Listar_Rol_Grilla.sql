-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rol_Grilla]
(
@codemp int,
@ctcid int,
@idioma int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query +'SELECT r.rol_rolid Rolid,  
						r.pcl_nomfant Cliente,  
						r.ctc_nomfant Deudor, 
						r.rol_bloqueo Bloqueo, 
						r.rol_numero NumeroRol,  
						r.eci_nombre Estado, 
						r.trb_nombre Tribunal, 
						r.mji_nombre Materia, 
						r.tci_nombre Causa,
						(SELECT isnull(ESTADO_ADM,'''') FROM [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] PJ  with (nolock) 
						INNER JOIN [10.0.1.11].[PoderJudicial].[dbo].ROL_PODER_JUDICIAL RPJ  with (nolock) 
						ON PJ.ID_CAUSA = RPJ.RPJ_ID_CAUSA
						WHERE rpj.rpj_rolid = r.rol_rolid) EstAdm, 
						(	select TOP 1 convert(varchar,DDE_FECJUD,105) 
							from DEUDORES_ESTAMPES de  with (nolock) 
							LEFT JOIN ACCION_JUDICIAL aj  with (nolock) 
							ON aj.CODEMP = de.DDE_CODEMP
							AND aj.INSID = de.DDE_INSID
							WHERE de.DDE_CODEMP = r.rol_codemp 
							and de.DDE_PCLID = r.rol_pclid 
							and de.DDE_CTCID = r.rol_ctcid 
							and de.DDE_ROLID = r.rol_rolid
							ORDER BY DDE_FECJUD DESC) FechaAccion, 
						 (	 select TOP 1 CASE aj.TIPO 
											when 1 then ''<span style=color:green;font-weight:bold>'' + convert(varchar,INS_NOMBRE) + ''</span>''
											when 2 then ''<span style=color:red;font-weight:bold>'' + convert(varchar,INS_NOMBRE) + ''</span>''
											end as INS_NOMBRE
							 from DEUDORES_ESTAMPES de with (nolock) 
							 INNER JOIN INSUMOS  with (nolock) 
							 ON INS_CODEMP = DDE_CODEMP 
							 AND INS_INSID = DDE_INSID 
							 LEFT JOIN ACCION_JUDICIAL aj 
							 ON aj.CODEMP = de.DDE_CODEMP
							 AND aj.INSID = de.DDE_INSID
							 WHERE de.DDE_CODEMP = r.rol_codemp 
							 and de.DDE_PCLID = r.rol_pclid 
							 and de.DDE_CTCID = r.rol_ctcid 
							 and de.DDE_ROLID = r.rol_rolid
							 ORDER BY DDE_FECJUD DESC)	AccionJudicial 	
				FROM view_rol r with (nolock) 
				WHERE  r.rol_codemp =' + CONVERT(VARCHAR,@codemp) +'
						and r.rol_ctcid = ' + CONVERT(VARCHAR,@ctcid ) +'
						and r.eci_idid = ' + CONVERT(VARCHAR,@idioma) +'
						and r.mji_idid = ' + CONVERT(VARCHAR,@idioma) +'
						and tci_idid =' + CONVERT(VARCHAR,@idioma)

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
