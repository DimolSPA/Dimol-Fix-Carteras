

Create Procedure Update_Empresa_CodBarr(@emp_codemp integer, @emp_codbarr image) as
  UPDATE empresa  
     SET emp_codbarr = @emp_codbarr
   WHERE empresa.emp_codemp = @emp_codemp
