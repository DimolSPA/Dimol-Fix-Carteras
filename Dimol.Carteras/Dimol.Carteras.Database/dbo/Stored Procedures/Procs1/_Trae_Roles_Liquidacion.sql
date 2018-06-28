CREATE PROCEDURE [dbo].[_Trae_Roles_Liquidacion]
(@codemp int,
@rolId int)
AS
BEGIN
SELECT rolnumero Rol, SBCID, sum(MontoAsignado)Cuantia
  FROM
	(select   
		(select ROL_NUMERO from rol where ROL_CODEMP = rd.RDC_CODEMP and ROL_ROLID = rd.RDC_ROLID) rolnumero,
		sc.SBC_SBCID SBCID,
		cc.CCB_ASIGNADO MontoAsignado
		from ROL_DOCUMENTOS rd
		inner join CARTERA_CLIENTES_CPBT_DOC cc
		on rd.RDC_CODEMP = cc.CCB_CODEMP
		and rd.RDC_CTCID = cc.CCB_CTCID
		and rd.RDC_CCBID = cc.CCB_CCBID
		and rd.RDC_PCLID = cc.CCB_PCLID
		join DEUDORES d
		on cc.CCB_CODEMP = d.CTC_CODEMP
		and cc.CCB_CTCID = d.CTC_CTCID
		left join SUBCARTERAS sc
		on cc.CCB_CODEMP = sc.SBC_CODEMP
		and cc.CCB_sbcid = sc.SBC_SBCID
		where rd.RDC_CODEMP = @codemp
		and rd.RDC_ROLID = @rolId) Liquidacion
group by rolnumero, SBCID 
END
