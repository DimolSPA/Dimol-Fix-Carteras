

Create Procedure Find_Rol(@rol_codemp integer, @rol_rolid integer) as
select count(rol_rolid)
      from rol
where rol_codemp = @rol_codemp and
           rol_rolid = @rol_rolid
