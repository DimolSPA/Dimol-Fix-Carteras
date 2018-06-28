CREATE PROCEDURE _Job_Cargar_Datos_Poder_Judicial_Monitoreo_AlertaRoles AS
BEGIN
INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza tabla con informaion de los roles actualizados y no actualizados', 'Comienzo', 'Job 5 AM',0 )

declare @Anio int
declare @minAnio int = 2013, @maxAnio int= (SELECT year( getdate() ));
declare curAnios cursor for 
WITH anios AS
(
   SELECT @minAnio AS ANIO
   UNION ALL
   SELECT t.ANIO + 1 FROM anios t
   WHERE t.ANIO < @maxAnio
) 

select * from anios
OPTION (MAXRECURSION 0)
open curAnios
fetch next from curAnios into @Anio
while (@@FETCH_STATUS = 0)
begin
	--print(@Anio)
	declare @IdTribunal int,@Tribunal varchar(400), @IdTribunalJudicial int, @TribunalJudicial varchar(400)
	declare curTribunales cursor for
	select tr.TRB_TRBID IdTribunal, tr.TRB_NOMBRE Tribunal,trj.ID_TRIBUNAL IdTribunalJudicial, trj.TRIBUNAL TribunalJudicial
	from TRIBUNALES tr with(nolock)
	join TRIBUNALES_A_PODER_JUDICIAL eq with(nolock)
	on tr.TRB_TRBID = eq.TRBID
	join [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_TRIBUNAL trj with(nolock)
	on eq.ID_TRIBUNAL = trj.ID_TRIBUNAL
	open curTribunales
	fetch next from curTribunales into @IdTribunal, @Tribunal, @IdTribunalJudicial, @TribunalJudicial
	while (@@FETCH_STATUS = 0)
	begin
		--print(@TribunalJudicial)
		declare @minRol int = 1, @maxRol int= (SELECT Max(NUMERO) 
												FROM [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_ROL PJR with(nolock)
												WHERE ANIO = @Anio 
												and TRIBUNAL = @IdTribunalJudicial)

		;WITH temp AS
		(
		   SELECT @minRol AS ID
		   UNION ALL
		   SELECT t.ID + 1 FROM temp t
		   WHERE t.ID < @maxRol
		) 
		INSERT INTO PODER_JUDICIAL_MONITOREO_ALERTAROLES(
					ROL,
					ANIO,
					TRIBUNALID,
					TRIBUNAL,
					ROLENCONTRADO,
					MINROL,
					MAXROL)
		SELECT t.ID RolBuscado, @Anio AnioBuscado, @IdTribunalJudicial TribunalId, @TribunalJudicial Tribunal, roles.ROL, @minRol MinRol, @maxRol MaxRol  from temp t
		left join (select distinct 
				  B.NUMERO 'ROL',
				  B.ANIO 'AÑO',
				  C.TRIBUNAL 'TRIBUNAL'
									   from [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_LITIGANTE as a with(nolock)
									   INNER JOIN [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_ROL AS B with(nolock)
									   ON A.ID_CAUSA=B.ID_CAUSA
									   INNER JOIN [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_TRIBUNAL AS C with(nolock)
									   ON B.TRIBUNAL=C.ID_TRIBUNAL
									   WHERE A.PARTICIPANTE IN ('DDOR.','DDO.')
									   and C.TRIBUNAL LIKE '%' + @TribunalJudicial +'%'
									   AND B.ANIO = @Anio) as roles
		on t.ID = roles.ROL
		OPTION (MAXRECURSION 0)
		fetch next from curTribunales into @IdTribunal, @Tribunal, @IdTribunalJudicial, @TribunalJudicial
	end
	close curTribunales
	deallocate curTribunales
 fetch next from curAnios into @Anio
end
close curAnios
deallocate curAnios

INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza tabla con informaion de los roles actualizados y no actualizados', 'Termino', 'Job 5 AM',0 )

END
