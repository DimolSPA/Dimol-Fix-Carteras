-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Rol_Comprobante] (@codemp int, @numero varchar(30), @tipo varchar(1), @tribunal int)
AS
BEGIN
	SET NOCOUNT ON;
	select rol_pclid Pclid,
		rol_ctcid Ctcid,
		rol_rolid Rolid,
		pcl_rut + ' - ' + pcl_nomfant NombreCliente,
		PCS_PCSID IdSucursal,
		PCS_NOMBRE Sucursal,
		ctc_rut RutDeudor,
		ctc_rut + ' - ' + ctc_nomfant NombreDeudor,
		rol_numero NumeroRol,
		(SELECT STUFF((
						select distinct ', ' + SBC_NOMBRE 
						from ROL_DOCUMENTOS rd 
						inner join CARTERA_CLIENTES_CPBT_DOC cc
						on cc.CCB_CODEMP = rd.RDC_CODEMP
						and cc.CCB_CTCID = rd.RDC_CTCID
						and cc.CCB_CCBID = rd.RDC_CCBID
						inner join SUBCARTERAS sc
						on sc.SBC_CODEMP = cc.CCB_CODEMP
						and sc.SBC_SBCID = cc.CCB_SBCID
						where rd.RDC_CODEMP = rol_codemp
						and rd.RDC_ROLID = rol_rolid
						FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) Asegurados

from VIEW_ROL join PROVCLI_SUCURSAL
on rol_codemp = pcs_codemp and rol_pclid = PCS_PCLID
where rol_codemp = @codemp
and rol_numero = @numero
and isnull(rol_tipo_rol,'C') = @tipo
and rol_trbid = @tribunal
END

