

Create Procedure Trae_Usuarios_Asignar(@usr_codemp integer) as
SELECT empleados.epl_usrid  
into #Usuarios
FROM empleados  
WHERE empleados.epl_codemp = @usr_codemp  and epl_usrid is not null

insert into  #Usuarios
SELECT provcli.pcl_usrid  
FROM provcli  
WHERE provcli.pcl_codemp = @usr_codemp    and pcl_usrid is not null

SELECT usuarios.usr_usrid,   
         usuarios.usr_nombre  
    FROM usuarios  
   WHERE usuarios.usr_codemp = @usr_codemp   AND  
         usuarios.usr_usrid not in (  SELECT epl_usrid  
                                          FROM #Usuarios where epl_usrid is not null  ) AND
          usuarios.usr_estado = 'H'   
Order by usr_nombre
