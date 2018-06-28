

Create Procedure Trae_Historial_Gestiones_Ultima(@cea_codemp integer, @cea_pclid integer, @cea_ctcid integer, @aci_idid integer) as
  SELECT distinct acciones_idiomas.aci_nombre,   
         cartera_clientes_estados_acciones.cea_fecha,   
         substring(cea_comentario, 1, 1000) as comentario,   
         usuarios.usr_nombre,   
         tipos_contacto.tic_nombre,   
         deudores_contactos.ddc_nombre,
         aci_accid as accion,
         0 as estado,
         0 as agrupa,  
         '' as utiliza,
         tic_ticid,
         cea_ddcid as contacto,
         cea_pclid     as provcli,
         cea_ctcid as ctcid
         into #Gestion
    FROM {oj cartera_clientes_estados_acciones LEFT OUTER JOIN deudores_contactos ON cartera_clientes_estados_acciones.cea_codemp = deudores_contactos.ddc_codemp AND cartera_clientes_estados_acciones.cea_ctcid = deudores_contactos.ddc_ctcid AND cartera_clientes_estados_acciones.cea_ddcid = deudores_contactos.ddc_ddcid LEFT OUTER JOIN tipos_contacto ON deudores_contactos.ddc_codemp = tipos_contacto.tic_codemp AND deudores_contactos.ddc_ticid = tipos_contacto.tic_ticid},   
         acciones_idiomas,   
         usuarios  
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = acciones_idiomas.aci_codemp ) and  
         ( cartera_clientes_estados_acciones.cea_accid = acciones_idiomas.aci_accid ) and  
         ( usuarios.usr_codemp = cartera_clientes_estados_acciones.cea_codemp ) and  
         ( usuarios.usr_usrid = cartera_clientes_estados_acciones.cea_usrid ) and  
         ( ( cartera_clientes_estados_acciones.cea_codemp = @cea_codemp ) AND  
         ( cartera_clientes_estados_acciones.cea_pclid = @cea_pclid ) AND  
         ( cartera_clientes_estados_acciones.cea_ctcid = @cea_ctcid )   AND
         ( acciones_idiomas.aci_idid = @aci_idid )   
         )    
Union
  SELECT DISTINCT estados_cartera_idiomas.eci_nombre,   
         cartera_clientes_estados_historial.ceh_fecha,   
         substring(ceh_comentario, 1, 1000) as comentario,   
         usuarios.usr_nombre ,
         '',
         '',
         0 as accion,
         eci_estid as estado,
         ect_agrupa as agrupa,
         ect_utiliza as utiliza,
         0,
         0 as contacto,
         ceh_pclid     as provcli,
         ceh_ctcid as ctcid
    FROM cartera_clientes_estados_historial,   
         estados_cartera_idiomas,   
         usuarios,
         estados_cartera  
   WHERE ( cartera_clientes_estados_historial.ceh_codemp = estados_cartera_idiomas.eci_codemp ) and  
         ( cartera_clientes_estados_historial.ceh_estid = estados_cartera_idiomas.eci_estid ) and  
         ( usuarios.usr_codemp = cartera_clientes_estados_historial.ceh_codemp ) and  
         ( usuarios.usr_usrid = cartera_clientes_estados_historial.ceh_usrid ) and  
         ( ( cartera_clientes_estados_historial.ceh_codemp = @cea_codemp) AND  
         ( estados_cartera.ect_codemp = eci_codemp ) AND  
         ( estados_cartera.ect_estid = eci_estid )  AND
         ( cartera_clientes_estados_historial.ceh_pclid = @cea_pclid ) AND  
         ( cartera_clientes_estados_historial.ceh_ctcid = @cea_ctcid)  AND
         ( estados_cartera_idiomas.eci_idid = @aci_idid  )     
         )  

order by cea_fecha desc


select max(cea_fecha) as Fecha
       into #UltGest
       from #Gestion


select TOP 1 Fecha, aci_nombre, comentario
        from #Gestion a, #UltGest b
        where a.cea_fecha = Fecha
