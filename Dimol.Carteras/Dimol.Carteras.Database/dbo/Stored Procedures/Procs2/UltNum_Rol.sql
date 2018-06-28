

Create Procedure UltNum_Rol(@rol_codemp integer) as
select IsNull(Max(rol_rolid)+1, 1)
      from rol
where rol_codemp = @rol_codemp
