

Create Procedure Update_Empresa_Menu(@emp_codemp integer, @emp_menu varchar(1000)) as
  UPDATE empresa  
     SET emp_menu = @emp_menu
   WHERE empresa.emp_codemp = @emp_codemp
