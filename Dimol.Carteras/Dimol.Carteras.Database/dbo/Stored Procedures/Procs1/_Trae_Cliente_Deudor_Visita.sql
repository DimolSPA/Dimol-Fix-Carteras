CREATE PROCEDURE _Trae_Cliente_Deudor_Visita
(@codemp int, 
@solicitudId int) 
AS 
BEGIN
	select cc.CTC_PCLID PCLID, cc.CTC_CTCID CTCID
	from VISITA_TERRENO_SOLICITUD vs
	join CARTERA_CLIENTES  cc
	on vs.CODEMP = cc.CTC_CODEMP
	and vs.CTCID = cc.CTC_CTCID
	where vs.SOLICITUD_ID = @solicitudId
END

