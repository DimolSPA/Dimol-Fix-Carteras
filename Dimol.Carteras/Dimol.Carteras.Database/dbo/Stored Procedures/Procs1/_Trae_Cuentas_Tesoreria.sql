CREATE PROCEDURE _Trae_Cuentas_Tesoreria         
(            
  @codemp int
 )            
           
as              
BEGIN            
select CUENTA_ID, NUM_CUENTA 
from TESORERIA_CUENTAS_BANCARIAS 
where CODEMP = @codemp
and NUM_CUENTA != '1'
END
