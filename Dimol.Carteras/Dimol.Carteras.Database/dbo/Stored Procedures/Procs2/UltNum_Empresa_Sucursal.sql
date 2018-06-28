

Create Procedure UltNum_Empresa_Sucursal(@esu_codemp integer) as
  SELECT IsNull(Max(empresa_sucursal.esu_sucid  )+1, 1)  
    FROM empresa_sucursal
   WHERE empresa_sucursal.esu_codemp = @esu_codemp
