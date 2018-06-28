CREATE FUNCTION _Trae_DiasSemana 
(
   @startDate DATETIME,
   @endDate DATETIME
)
RETURNS INT
AS
BEGIN
 
	Return datediff(dd, @startDate, @endDate) - (datediff(wk, @startDate, @endDate) * 2) -
		   case when datepart(dw, @startDate) = 1 then 1 else 0 end +
		  case when datepart(dw, @endDate) = 1 then 1 else 0 end
END