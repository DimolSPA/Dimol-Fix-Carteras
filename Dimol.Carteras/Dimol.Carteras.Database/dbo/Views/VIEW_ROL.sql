


/*==============================================================
 View: VIEW_ROL                                               
==============================================================*/
CREATE VIEW [dbo].[VIEW_ROL]
AS
SELECT        dbo.rol.ROL_CODEMP, dbo.rol.ROL_ROLID, dbo.rol.ROL_PCLID, dbo.rol.ROL_CTCID, dbo.rol.ROL_NUMERO, dbo.rol.ROL_TRBID, dbo.rol.ROL_TCAID, dbo.rol.ROL_ESTID, dbo.rol.ROL_FECEMI, dbo.rol.ROL_FECDEM, 
                         dbo.rol.ROL_FECROL, dbo.rol.ROL_TOTAL, dbo.rol.ROL_COMENTARIO, dbo.rol.ROL_ESJID, dbo.rol.ROL_FECJUD, dbo.rol.ROL_FECULTGEST, dbo.tribunales.TRB_NOMBRE, dbo.estados_cartera_idiomas.ECI_IDID, 
                         dbo.estados_cartera_idiomas.ECI_NOMBRE, dbo.tipos_causa_idiomas.TCI_IDID, dbo.tipos_causa_idiomas.TCI_NOMBRE, dbo.materia_judicial_idiomas.MJI_IDID, dbo.materia_judicial_idiomas.MJI_NOMBRE, 
                         dbo.provcli.PCL_RUT, dbo.provcli.PCL_NOMFANT, dbo.deudores.CTC_RUT, dbo.deudores.CTC_NOMFANT, dbo.VIEW_DATOS_GEOGRAFICOS.pai_paiid, dbo.VIEW_DATOS_GEOGRAFICOS.pai_nombre, 
                         dbo.VIEW_DATOS_GEOGRAFICOS.pai_codtel, dbo.VIEW_DATOS_GEOGRAFICOS.reg_regid, dbo.VIEW_DATOS_GEOGRAFICOS.reg_nombre, dbo.VIEW_DATOS_GEOGRAFICOS.reg_orden, 
                         dbo.VIEW_DATOS_GEOGRAFICOS.ciu_ciuid, dbo.VIEW_DATOS_GEOGRAFICOS.ciu_nombre, dbo.VIEW_DATOS_GEOGRAFICOS.ciu_codarea, dbo.VIEW_DATOS_GEOGRAFICOS.com_comid, 
                         dbo.VIEW_DATOS_GEOGRAFICOS.com_nombre, dbo.VIEW_DATOS_GEOGRAFICOS.com_codpost, dbo.tribunales.TRB_DIRECCION, dbo.deudores.CTC_NUMERO, dbo.deudores.CTC_DIGITO, dbo.deudores.CTC_NOMBRE, 
                         dbo.deudores.CTC_APEPAT, dbo.deudores.CTC_APEMAT, dbo.deudores.CTC_DIRECCION, dbo.rol.ROL_BLOQUEO, dbo.rol.ROL_PREQUIEBRA, dbo.rol.ROL_TIPO_ROL, dbo.deudores.CTC_QUIEBRA
						 ,dbo.COMPETENCIA.ID_COMPETENCIA
FROM            
                         dbo.rol LEFT OUTER JOIN
                         dbo.materia_judicial_idiomas ON dbo.rol.ROL_CODEMP = dbo.materia_judicial_idiomas.MJI_CODEMP AND dbo.rol.ROL_ESJID = dbo.materia_judicial_idiomas.MJI_ESJID INNER JOIN
                         dbo.tribunales ON dbo.rol.ROL_CODEMP = dbo.tribunales.TRB_CODEMP AND dbo.rol.ROL_TRBID = dbo.tribunales.TRB_TRBID INNER JOIN
                         dbo.tipos_causa_idiomas ON dbo.rol.ROL_CODEMP = dbo.tipos_causa_idiomas.TCI_CODEMP AND dbo.rol.ROL_TCAID = dbo.tipos_causa_idiomas.TCI_TCAID INNER JOIN
                         dbo.estados_cartera_idiomas ON dbo.rol.ROL_CODEMP = dbo.estados_cartera_idiomas.ECI_CODEMP AND dbo.rol.ROL_ESTID = dbo.estados_cartera_idiomas.ECI_ESTID INNER JOIN
                         dbo.provcli ON dbo.rol.ROL_CODEMP = dbo.provcli.PCL_CODEMP AND dbo.rol.ROL_PCLID = dbo.provcli.PCL_PCLID INNER JOIN
                         dbo.deudores ON dbo.rol.ROL_CODEMP = dbo.deudores.CTC_CODEMP AND dbo.rol.ROL_CTCID = dbo.deudores.CTC_CTCID INNER JOIN
                         dbo.VIEW_DATOS_GEOGRAFICOS ON dbo.tribunales.TRB_COMID = dbo.VIEW_DATOS_GEOGRAFICOS.com_comid 
						 LEFT OUTER JOIN dbo.COMPETENCIA on dbo.COMPETENCIA.id_competencia = dbo.tribunales.id_competencia


