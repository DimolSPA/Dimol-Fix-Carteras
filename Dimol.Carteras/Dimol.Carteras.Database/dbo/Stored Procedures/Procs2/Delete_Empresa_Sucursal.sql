

Create Procedure Delete_Empresa_Sucursal(@esu_codemp integer, @esu_sucid integer) as 
DELETE FROM empresa_sucursal  
   WHERE ( empresa_sucursal.esu_codemp = @esu_codemp ) AND  
         ( empresa_sucursal.esu_sucid = @esu_sucid )
