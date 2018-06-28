

Create Procedure Update_Empresa_Logo(@emp_codemp integer, @emp_logo image) as
  UPDATE empresa  
     SET emp_logo = @emp_logo
   WHERE empresa.emp_codemp = @emp_codemp
