

Create Procedure Delete_Empresa(@emp_codemp integer) as
DELETE FROM empresa  
   WHERE empresa.emp_codemp = @emp_codemp
