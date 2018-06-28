CREATE PROCEDURE _Trae_Cliente_Deudor
(@codemp int, 
@ctcid int) 
AS 
BEGIN
SELECT cc.CTC_PCLID PCLID, cc.CTC_CTCID CTCID
from CARTERA_CLIENTES  cc
where cc.CTC_CODEMP = @codemp 
and cc.CTC_CTCID = @ctcid
END
