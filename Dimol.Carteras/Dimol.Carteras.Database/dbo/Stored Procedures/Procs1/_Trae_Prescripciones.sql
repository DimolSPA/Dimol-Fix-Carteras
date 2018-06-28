

CREATE Procedure [dbo].[_Trae_Prescripciones](@rol_codemp integer, @diasprescr integer) as 


Select Distinct View_Rol_Datos.pcl_rut as RutCli, 
				View_Rol_Datos.pcl_nomfant as NomCli,
				View_Rol_Datos.ctc_rut as RutDeu,
				View_Rol_Datos.ctc_nomfant as NomDeu,
				isnull(replace(rol_numero, char(9), ''), '') as Rol,
				trb_nombre as Tribunal,
				tci_nombre as TipCausa,
				isnull(rol_fecjud, '') as FecJud,
				mji_nombre as MatJud,
				View_Rol_Datos.eci_nombre as EstRol,
				replace(ccb_numero, char(9), '') as Numero,
				isnull(ccb_fecvenc, '') as FecVenc,
				DATEADD(yy, 1, ccb_fecvenc) as FecPresc,
				case mon_nombre
					when 'UF' then rdc_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 2), 0)
					when 'DOLAR' then rdc_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 3), 0)
					else rdc_saldo 
				end 
				as Demandado,
				case mon_nombre
					when 'UF' then ccb_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 2), 0)
					when 'DOLAR' then ccb_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 3), 0)
					else ccb_saldo 
				end 
				as Saldo,
				isnull(pcc_nombre, '') as CodCarg,
				sbc_nombre as Asegurado
				,isnull((select top 1 epl_nombre from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID in (select ejr_etjid from entejud_rol er where er.EJR_ROLID = View_Rol_Datos.rol_rolid)), 'SIN TRIBUNAL') as Abogado
					
into #Arbol			
from View_Rol_Datos 
 where rol_codemp = @rol_codemp and ccb_estcpbt = 'J'
 and DATEDIFF(dd, DATEADD(yy, 1, ccb_fecvenc), getdate()) between 0 and @diasprescr
 Union 
Select Distinct View_CpbtDoc_Judicial_noRol.pcl_rut as RutCli,
View_CpbtDoc_Judicial_noRol.pcl_nomfant as NomCli,
View_CpbtDoc_Judicial_noRol.ctc_rut as RutDeu,
View_CpbtDoc_Judicial_noRol.ctc_nomfant as NomDeu,			
isnull(replace(rol_numero, char(9), ''), '') as Rol,
trb_nombre as Tribunal,
tci_nombre as TipCausa,
isnull(convert(varchar,rol_fecjud, 103), '') as FecJud,
mji_nombre as MatJud,	
View_CpbtDoc_Judicial_noRol.eci_nombre as EstRol,
replace(ccb_numero, char(9), '') as Numero,
isnull(ccb_fecvenc, '') as FecVenc,
DATEADD(yy, 1, ccb_fecvenc) as FecPresc,
				case mon_nombre
					when 'UF' then rdc_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 2), 0)
					when 'DOLAR' then rdc_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 3), 0)
					else rdc_saldo 
				end 
				as Demandado,
				case mon_nombre
					when 'UF' then ccb_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 2), 0)
					when 'DOLAR' then ccb_saldo * isnull((select TOP 1 MNV_VALOR from dbo.MONEDAS_VALORES where mnv_fecha = convert(date,getdate()) and MNV_CODMON = 3), 0)
					else ccb_saldo 
				end 
				as Saldo,
isnull(pcc_nombre, '') as CodCarg,
sbc_nombre as Asegurado
,isnull((select top 1 epl_nombre from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID in (select ejr_etjid from entejud_rol er where er.EJR_ROLID = View_CpbtDoc_Judicial_noRol.rol_rolid)), 'SIN TRIBUNAL') as Abogado
from View_CpbtDoc_Judicial_noRol  
where rol_codemp = @rol_codemp and ccb_estcpbt = 'J'
and DATEDIFF(dd, DATEADD(yy, 1, ccb_fecvenc), getdate()) between 0 and @diasprescr 
   
 select *
 from #Arbol 
 order by 1,3 

 drop table #Arbol 
 
