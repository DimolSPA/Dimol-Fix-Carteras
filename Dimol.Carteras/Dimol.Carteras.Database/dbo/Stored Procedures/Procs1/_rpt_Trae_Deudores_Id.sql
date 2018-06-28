
CREATE Procedure [dbo].[_rpt_Trae_Deudores_Id](@codemp integer, @ctcid integer) as
select CTC_RUT, CTC_NOMBRE 
from deudores 
WHERE CTC_CODEMP = @codemp AND CTC_CTCID = @ctcid 
