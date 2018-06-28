

CREATE Procedure [dbo].[_Trae_Arbol_Judicial](@rol_codemp integer) as 


Select Distinct View_Rol_Datos.pcl_rut as RutCli, 
				View_Rol_Datos.pcl_nomfant as NomCli,
				View_Rol_Datos.ctc_rut as RutDeu,
				View_Rol_Datos.ctc_nomfant as NomDeu,
				View_Rol_Datos.sbc_nombre as Asegurado,
				View_Rol_Datos.tci_nombre as TipCausa,				
				replace(ccb_numero, char(9), '') as Numero,
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
				mji_nombre as MatJud,
				View_Rol_Datos.eci_nombre as EstRol,
				isnull(replace(rol_numero, char(9), ''), '') as Rol,
				isnull(rol_fecjud, '') as FecJud,
				isnull(DATEDIFF(DD, rol_fecjud, getdate()), 0) as Dias,
				trb_nombre as Tribunal,
				isnull(pcc_nombre, '') as CodCarg,
				isnull(ccb_fecing, '') as FecAsig,
				mon_nombre as Moneda, 
				--dbo._Arbol_Judicial_Valida(View_Rol_Datos.pcl_nomfant, rol_numero, mji_nombre, View_Rol_Datos.eci_nombre, pcc_nombre, isnull((select top 1 abogado from Tribunal_Abogado where tribunal = trb_nombre), 'SIN TRIBUNAL')) as Valido, 
				--isnull((select top 1 abogado from Tribunal_Abogado where tribunal = trb_nombre), 'SIN TRIBUNAL') as Abogado, 
				case View_Rol_Datos.pcl_nomfant 
					when 'COOPEUCH LTDA' then 'COOPEUCH' 
					ELSE 
						case isnull((select top 1 CCC_CATEGORIA from CARTERA_CLIENTES_CATEGORIA where CCC_CTCID = rol_ctcid), 'S') 
							when 'S' then 'SIN CATEGORIA' 
							when 'M' then 'MALO' 
							when 'R' then 'REGULAR' 
							when 'B' then 'BUENO' 
						end	
					END AS Categoria, 
					isnull((select count(*) from ROL_ESTADOS where rle_rolid = View_Rol_Datos.rol_rolid and RLE_FECJUD > dateadd(mm, -1, GETDATE()) and RLE_FECJUD <= GETDATE()), 0) as Cuenta 
					--,(select max(ejr_etjid) from entejud_rol er where er.EJR_ROLID = View_Rol_Datos.rol_rolid) as etjid
					,isnull((select top 1 epl_emplid from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID in (select ejr_etjid from entejud_rol er where er.EJR_ROLID = View_Rol_Datos.rol_rolid)), 0) as CodAbogado
					,isnull((select top 1 epl_nombre from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID in (select ejr_etjid from entejud_rol er where er.EJR_ROLID = View_Rol_Datos.rol_rolid)), 'SIN TRIBUNAL') as Abogado
					, isnull((select deu.CTC_QUIEBRA from deudores deu where deu.CTC_CTCID = View_Rol_Datos.rol_ctcid and deu.CTC_CODEMP = View_Rol_Datos.rol_codemp), 'N') as ctc_quiebra 
					, isnull(rol_prequiebra, 'R') rol_prequiebra 
into #Arbol			
from View_Rol_Datos 
 where rol_codemp = @rol_codemp and ccb_estcpbt = 'J'
 Union 
Select Distinct View_CpbtDoc_Judicial_noRol.pcl_rut as RutCli,
View_CpbtDoc_Judicial_noRol.pcl_nomfant as NomCli,
View_CpbtDoc_Judicial_noRol.ctc_rut as RutDeu,
View_CpbtDoc_Judicial_noRol.ctc_nomfant as NomDeu,
View_CpbtDoc_Judicial_noRol.sbc_nombre as Asegurado,
View_CpbtDoc_Judicial_noRol.tci_nombre as TipCausa,
replace(ccb_numero, char(9), '') as Numero,
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
mji_nombre as MatJud,	
View_CpbtDoc_Judicial_noRol.eci_nombre as EstRol,			
isnull(replace(rol_numero, char(9), ''), '') as Rol,
isnull(convert(varchar,rol_fecjud, 103), '') as FecJud,
isnull(DATEDIFF(DD, rol_fecjud, getdate()), 0) as Dias,
trb_nombre as Tribunal,
isnull(pcc_nombre, '') as CodCarg,
isnull(ccb_fecing, '') as FecAsig,
mon_nombre as Moneda, 
case View_CpbtDoc_Judicial_noRol.pcl_nomfant 
					when 'COOPEUCH LTDA' then 'COOPEUCH' 
					ELSE 
						case isnull((select top 1 CCC_CATEGORIA from CARTERA_CLIENTES_CATEGORIA where CCC_CTCID = rol_ctcid), 'S') 
							when 'S' then 'SIN CATEGORIA' 
							when 'M' then 'MALO' 
							when 'R' then 'REGULAR' 
							when 'B' then 'BUENO' 
						end	
					END AS Categoria, 
					isnull((select count(*) from ROL_ESTADOS where rle_rolid = View_CpbtDoc_Judicial_noRol.rol_rolid and RLE_FECJUD > dateadd(mm, -1, GETDATE()) and RLE_FECJUD <= GETDATE()), 0) as Cuenta 
					--,(select max(ejr_etjid) from entejud_rol er where er.EJR_ROLID = View_CpbtDoc_Judicial_noRol.rol_rolid) as etjid
					,isnull((select top 1 epl_emplid from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID in (select ejr_etjid from entejud_rol er where er.EJR_ROLID = View_CpbtDoc_Judicial_noRol.rol_rolid)), 0) as CodAbogado
					,isnull((select top 1 epl_nombre from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID in (select ejr_etjid from entejud_rol er where er.EJR_ROLID = View_CpbtDoc_Judicial_noRol.rol_rolid)), 'SIN TRIBUNAL') as Abogado
					, isnull((select deu.CTC_QUIEBRA from deudores deu where deu.CTC_CTCID = View_CpbtDoc_Judicial_noRol.rol_ctcid and deu.CTC_CODEMP = View_CpbtDoc_Judicial_noRol.rol_codemp), 'N') as ctc_quiebra
					, isnull(rol_prequiebra, 'R') rol_prequiebra 
from View_CpbtDoc_Judicial_noRol  
where rol_codemp = @rol_codemp and ccb_estcpbt = 'J'
 
-- select *, 
-- isnull((select epl_nombre from empleados emp, entes_judicial ej where emp.epl_emplid = ej.ETJ_EMPLID and ej.ETJ_ABOGADO_ENCARGADO = 'S' and ej.ETJ_ETJID = etjid), 'SIN TRIBUNAL') as Abogado
-- INTO #Arbol2 
-- from #Arbol 

-- drop table #Arbol
 
 select *, 
 dbo._Arbol_Judicial_Valida(NomCli, Rol, MatJud, EstRol, CodCarg, Abogado, ctc_quiebra, rol_prequiebra) as Valido 
 into #Arbol2
 from #Arbol

 drop table #Arbol 

 select *,  
 case dbo._Arbol_Judicial_Logica(Valido, NomCli, FecAsig, Cuenta, Categoria) 
 when 'S' then 'SIN MOVIMIENTO' 
 when 'C' then 'CON MOVIMIENTO' 
 else '' 
 end as Logica
 into #Arbol3
 from #Arbol2

 drop table #Arbol2 

 select * from #Arbol3 order by 1,3 

 drop table #Arbol3
 
