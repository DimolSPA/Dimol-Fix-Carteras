-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
create PROCEDURE [dbo].[_Asociados] 
(
	@codemp int,
	@ctcid as integer
)

AS
BEGIN
	-- Declare the return variable here
create table #orgchart(
Id varchar(50),
Nombre varchar(1000),
Rut varchar(50),
Caratulado varchar(500),
Rol  varchar(50),
Tribunal varchar(800),
Monto varchar(30),
RepLegal varchar(1),
Padre varchar(50),
Tooltip varchar(500)
)

select  rol_pclid, pcl_nomfant, pcl_rut, rol_ctcid, ctc_nomfant, ctc_rut, rol_rolid, eci_nombre, tci_nombre, mji_nombre, rol_tipo_rol, rol_numero, trb_nombre, rol_total
into #data
from VIEW_ROL 
where rol_codemp = @codemp
and rol_ctcid = @ctcid 
order by rol_rolid 

insert into #orgchart 
select distinct  rol_ctcid Id, ctc_nomfant Nombre, dbo._formato_rut(ctc_rut) Rut, null Catarulado, null Rol, null Tribunal, null Monto, null RepLegal, null, null from #data

insert into #orgchart
select distinct rol_rolid, null, null, pcl_nomfant +' vs '+ ctc_nomfant, isnull(rol_tipo_rol,'C')+ '-' + rol_numero, trb_nombre, dbo._formato_numero(isnull(rol_total,0)), null, rol_ctcid, null  from #data

insert into #orgchart
select cast(substring(RLD_RUT,1,LEN(RLD_RUT)-1) as int), RLD_NOMBRE , dbo._formato_rut(RLD_RUT), null, null, null, null, RLD_REPLEG,RLD_ROLID, null
from ROL_DEMANDADOS
where RLD_CODEMP = @codemp
and RLD_ROLID in (select rol_rolid
from VIEW_ROL 
where rol_codemp = @codemp
and rol_ctcid = (select distinct rol_ctcid  from #data)  )

drop table #data

select * from #orgchart
drop table #orgchart
         
END
