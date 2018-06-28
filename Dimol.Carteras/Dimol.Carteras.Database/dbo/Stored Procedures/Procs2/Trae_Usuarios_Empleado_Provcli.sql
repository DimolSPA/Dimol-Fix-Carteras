﻿

Create Procedure Trae_Usuarios_Empleado_Provcli(@usuario varchar(20)) as
  SELECT usuarios.usr_codemp,   
         usuarios.usr_usrid,   
         usuarios.usr_nombre,   
         usuarios.usr_login,   
         usuarios.usr_password,   
         usuarios.usr_fecing,   
         usuarios.usr_godlog,   
         usuarios.usr_badlog,   
         usuarios.usr_fecultlog,   
         usuarios.usr_fecblock,   
         usuarios.usr_mail,   
         usuarios.usr_tipquest,   
         usuarios.usr_answer,   
         usuarios.usr_sucid,   
         usuarios.usr_prfid,   
         usuarios.usr_permisos,   
         usuarios.usr_estado,   
         empleados.epl_emplid,   
         empleados.epl_rut,   
         empleados.epl_nombre,   
         empleados.epl_apepat,   
         empleados.epl_apemat,   
         provcli.pcl_pclid,   
         provcli.pcl_rut,   
         provcli.pcl_nombre,   
         provcli.pcl_apepat,   
         provcli.pcl_apemat,   
         provcli.pcl_nomfant,   
         usuarios_sucursal.uss_sucid,
         usr_campass,
         usr_feccambio 
    FROM {oj usuarios LEFT OUTER JOIN empleados ON usuarios.usr_codemp = empleados.epl_codemp AND usuarios.usr_usrid = empleados.epl_usrid LEFT OUTER JOIN provcli ON usuarios.usr_codemp = provcli.pcl_codemp AND usuarios.usr_usrid = provcli.pcl_usrid LEFT OUTER JOIN usuarios_sucursal ON usuarios.usr_codemp = usuarios_sucursal.uss_codemp AND usuarios.usr_usrid = usuarios_sucursal.uss_usrid}  
   WHERE usuarios.usr_login = @usuario
