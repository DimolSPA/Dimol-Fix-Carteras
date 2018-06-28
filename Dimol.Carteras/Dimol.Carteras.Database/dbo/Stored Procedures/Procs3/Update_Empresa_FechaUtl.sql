

Create Procedure Update_Empresa_FechaUtl(@emp_codemp integer) as
  UPDATE empresa  
     SET emp_fecutl = getdate()  
   WHERE empresa.emp_codemp = 1
