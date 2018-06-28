CREATE PROCEDURE [dbo].[Trae_Reporte_Informe_Judicial_Quiebras] (@rol_codemp integer, @rol_pclid integer, @eci_idid integer)
AS
  SELECT DISTINCT
    view_rol_datos.rol_pclid,
    view_rol_datos.pcl_rut,
    view_rol_datos.pcl_nomfant,
    view_rol_datos.ctc_rut,
    view_rol_datos.ctc_nomfant,
    view_rol_datos.trb_nombre,
    view_rol_datos.tci_nombre,
    view_rol_datos.rol_rolid,
    view_rol_datos.rol_numero,
    view_rol_datos.rol_fecrol,
    SUBSTRING(rle_comentario, 1, 1000) AS rle_comentario,
    view_datos_geograficos.pai_nombre,
    view_datos_geograficos.reg_nombre,
    view_datos_geograficos.ciu_nombre,
    view_datos_geograficos.com_nombre,
    view_datos_geograficos.com_codpost,
    view_rol_datos.trb_direccion,
    materia_judicial_idiomas.mji_nombre,
    estados_cartera_idiomas.eci_nombre, --despues comentar
	estados_cartera.ect_nombre,
    view_rol_datos.tipdoc,
    view_rol_datos.ccb_numero,
    view_rol_datos.ccb_fecdoc,
    view_rol_datos.ccb_fecvenc,
    view_rol_datos.ccb_asignado,
    view_rol_datos.ccb_monto,
    view_rol_datos.ccb_saldo,
    view_rol_datos.ccb_gastjud,
    view_rol_datos.ccb_gastotro,
    view_rol_datos.ccb_intereses,
    view_rol_datos.ccb_honorarios,
    view_rol_datos.bco_nombre,
    view_rol_datos.ccb_numesp,
    view_rol_datos.ccb_numagrupa,
    view_rol_datos.sbc_rut,
    view_rol_datos.sbc_nombre,
    view_rol_datos.mon_nombre,
    view_rol_datos.pcc_nombre,
    view_rol_datos.mci_nombre,
    view_rol_datos.rol_fecjud,
    rol_estados.rle_fecha,
    ctc_numero,
    ctc_digito,
    ccb_codmon	
  FROM view_rol_datos,
       rol_estados,
       materia_judicial_idiomas,
       estados_cartera_idiomas,
       tribunales,
       view_datos_geograficos,
       materia_estados,
	   estados_cartera
  WHERE (view_rol_datos.rol_codemp = rol_estados.rle_codemp)
  AND (view_rol_datos.rol_rolid = rol_estados.rle_rolid)
  AND (view_rol_datos.rol_fecjud = rol_estados.rle_fecjud)
  AND (view_rol_datos.rol_esjid = rol_estados.rle_esjid)
  AND (view_rol_datos.rol_estid = rol_estados.rle_estid)
  AND (view_rol_datos.rol_esjid = mej_esjid)
  AND (view_rol_datos.rol_estid = mej_estid)
  AND (rol_estados.rle_codemp = materia_judicial_idiomas.mji_codemp)
  AND (rol_estados.rle_esjid = materia_judicial_idiomas.mji_esjid)
  AND (rol_estados.rle_codemp = estados_cartera_idiomas.eci_codemp)
  AND (rol_estados.rle_estid = estados_cartera_idiomas.eci_estid)
  AND (estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp)
  AND (estados_cartera_idiomas.eci_estid= estados_cartera.ect_estid)
  AND (view_rol_datos.rol_codemp = tribunales.trb_codemp)
  AND (view_rol_datos.rol_trbid = tribunales.trb_trbid)
  AND (tribunales.trb_comid = view_datos_geograficos.com_comid)
  AND (view_rol_datos.eci_idid = estados_cartera_idiomas.eci_idid)
  AND (view_rol_datos.eci_idid = materia_judicial_idiomas.mji_idid)
  AND ((view_rol_datos.rol_codemp = @rol_codemp)
  AND (view_rol_datos.rol_pclid = @rol_pclid)
  AND (view_rol_datos.ccb_estcpbt = 'J'
  AND rol_prequiebra = 'N'
  AND rol_esjid IN (SELECT
    emc_valnum
  FROM empresa_configuracion
  WHERE emc_codemp = @rol_codemp
  AND emc_emcid = 128)
  )
  AND (view_rol_datos.eci_idid = @eci_idid))
  UNION
  SELECT DISTINCT
    view_rol_datos.rol_pclid,
    view_rol_datos.pcl_rut,
    view_rol_datos.pcl_nomfant,
    view_rol_datos.ctc_rut,
    view_rol_datos.ctc_nomfant,
    view_rol_datos.trb_nombre,
    view_rol_datos.tci_nombre,
    view_rol_datos.rol_rolid,
    view_rol_datos.rol_numero,
    view_rol_datos.rol_fecrol,
    SUBSTRING(rle_comentario, 1, 1000) AS rle_comentario,
    view_datos_geograficos.pai_nombre,
    view_datos_geograficos.reg_nombre,
    view_datos_geograficos.ciu_nombre,
    view_datos_geograficos.com_nombre,
    view_datos_geograficos.com_codpost,
    view_rol_datos.trb_direccion,
    materia_judicial_idiomas.mji_nombre,
    estados_cartera_idiomas.eci_nombre, --despues comentar
	estados_cartera.ect_nombre,
    view_rol_datos.tipdoc,
    view_rol_datos.ccb_numero,
    view_rol_datos.ccb_fecdoc,
    view_rol_datos.ccb_fecvenc,
    view_rol_datos.ccb_asignado,
    view_rol_datos.ccb_monto,
    view_rol_datos.ccb_saldo,
    view_rol_datos.ccb_gastjud,
    view_rol_datos.ccb_gastotro,
    view_rol_datos.ccb_intereses,
    view_rol_datos.ccb_honorarios,
    view_rol_datos.bco_nombre,
    view_rol_datos.ccb_numesp,
    view_rol_datos.ccb_numagrupa,
    view_rol_datos.sbc_rut,
    view_rol_datos.sbc_nombre,
    view_rol_datos.mon_nombre,
    view_rol_datos.pcc_nombre,
    view_rol_datos.mci_nombre,
    view_rol_datos.rol_fecjud,
    rol_estados.rle_fecha,
    ctc_numero,
    ctc_digito,
    ccb_codmon
  FROM view_rol_datos,
       rol_estados,
       materia_judicial_idiomas,
       estados_cartera_idiomas,
       tribunales,
       view_datos_geograficos,
       materia_estados,
	   estados_cartera
  WHERE (view_rol_datos.rol_codemp = rol_estados.rle_codemp)
  AND (view_rol_datos.rol_rolid = rol_estados.rle_rolid)
  AND (view_rol_datos.rol_fecjud = rol_estados.rle_fecjud)
  AND (view_rol_datos.rol_esjid = rol_estados.rle_esjid)
  AND (view_rol_datos.rol_estid = rol_estados.rle_estid)
  AND (view_rol_datos.rol_esjid = mej_esjid)
  AND (view_rol_datos.rol_estid = mej_estid)
  AND (rol_estados.rle_codemp = materia_judicial_idiomas.mji_codemp)
  AND (rol_estados.rle_esjid = materia_judicial_idiomas.mji_esjid)
  AND (rol_estados.rle_codemp = estados_cartera_idiomas.eci_codemp)
  AND (rol_estados.rle_estid = estados_cartera_idiomas.eci_estid)
  AND (estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp)
  AND (estados_cartera_idiomas.eci_estid= estados_cartera.ect_estid)
  AND (view_rol_datos.rol_codemp = tribunales.trb_codemp)
  AND (view_rol_datos.rol_trbid = tribunales.trb_trbid)
  AND (tribunales.trb_comid = view_datos_geograficos.com_comid)
  AND (view_rol_datos.eci_idid = estados_cartera_idiomas.eci_idid)
  AND (view_rol_datos.eci_idid = materia_judicial_idiomas.mji_idid)
  AND ((view_rol_datos.rol_codemp = @rol_codemp)
  AND (view_rol_datos.rol_pclid = @rol_pclid)
  AND (view_rol_datos.ccb_estcpbt = 'J'
  AND rol_prequiebra = 'S'
  AND rol_esjid NOT IN (SELECT
    emc_valnum
  FROM empresa_configuracion
  WHERE emc_codemp = @rol_codemp
  AND emc_emcid = 128)
  )
  AND (view_rol_datos.eci_idid = @eci_idid))
  ORDER BY ctc_numero, ccb_fecvenc
