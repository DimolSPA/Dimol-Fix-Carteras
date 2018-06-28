

Create Procedure Trae_Empresa_Logo(@codemp integer) as
select emp_logo
From empresa
where emp_codemp = @codemp
