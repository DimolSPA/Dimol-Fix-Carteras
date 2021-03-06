﻿

Create Procedure Trae_Reporte_Historial_Gestiones(@cea_codemp integer, @cea_pclid integer, @cea_ctcid integer, @aci_idid integer) as
  SELECT distinct acciones_idiomas.aci_nombre,   
         convert(datetime, Convert(varchar(10), cea_fecha,112)) as cea_fecha, 
         substring(cea_comentario, 1, 8000) as comentario,   
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
         cea_ctcid as ctcid,
         pcl_rut,
         pcl_nomfant,
         ctc_numero,
         ctc_digito,
         ctc_nomfant  
    FROM {oj cartera_clientes_estados_acciones LEFT OUTER JOIN deudores_contactos ON cartera_clientes_estados_acciones.cea_codemp = deudores_contactos.ddc_codemp AND cartera_clientes_estados_acciones.cea_ctcid = deudores_contactos.ddc_ctcid AND cartera_clientes_estados_acciones.cea_ddcid = deudores_contactos.ddc_ddcid LEFT OUTER JOIN tipos_contacto ON deudores_contactos.ddc_codemp = tipos_contacto.tic_codemp AND deudores_contactos.ddc_ticid = tipos_contacto.tic_ticid},   
         acciones_idiomas,   
         usuarios,
         provcli,
         deudores   
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = acciones_idiomas.aci_codemp ) and  
         ( cartera_clientes_estados_acciones.cea_accid = acciones_idiomas.aci_accid ) and  
         ( usuarios.usr_codemp = cartera_clientes_estados_acciones.cea_codemp ) and  
         ( usuarios.usr_usrid = cartera_clientes_estados_acciones.cea_usrid ) and  
         ( cea_codemp = pcl_codemp ) and  
         ( cea_pclid = pcl_pclid ) and  
         ( cea_codemp = ctc_codemp ) and  
         ( cea_ctcid = ctc_ctcid ) and  
         ( ( cartera_clientes_estados_acciones.cea_codemp = @cea_codemp ) AND  
         ( cartera_clientes_estados_acciones.cea_pclid = @cea_pclid ) AND  
         ( cartera_clientes_estados_acciones.cea_ctcid = @cea_ctcid )   AND
         ( acciones_idiomas.aci_idid = @aci_idid )   
         )    

Union

  SELECT DISTINCT estados_cartera_idiomas.eci_nombre,   
         convert(datetime, Convert(varchar(10), ceh_fecha,112)) as ceh_fecha, 
        substring(ceh_comentario, 1, 8000) as comentario,   
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
         ceh_ctcid as ctcid,
         pcl_rut,
         pcl_nomfant,
         ctc_numero,
         ctc_digito,
         ctc_nomfant  
    FROM cartera_clientes_estados_historial,   
         estados_cartera_idiomas,   
         usuarios,
         estados_cartera,
         provcli,
         deudores     
   WHERE ( cartera_clientes_estados_historial.ceh_codemp = estados_cartera_idiomas.eci_codemp ) and  
         ( cartera_clientes_estados_historial.ceh_estid = estados_cartera_idiomas.eci_estid ) and  
         ( usuarios.usr_codemp = cartera_clientes_estados_historial.ceh_codemp ) and  
         ( usuarios.usr_usrid = cartera_clientes_estados_historial.ceh_usrid ) and  
         ( ceh_codemp = pcl_codemp ) and  
         ( ceh_pclid = pcl_pclid ) and  
         ( ceh_codemp = ctc_codemp ) and  
         ( ceh_ctcid = ctc_ctcid ) and  
         ( ( cartera_clientes_estados_historial.ceh_codemp = @cea_codemp ) AND  
         ( estados_cartera.ect_codemp = eci_codemp ) AND  
         ( estados_cartera.ect_estid = eci_estid )  AND
         ( cartera_clientes_estados_historial.ceh_pclid = @cea_pclid ) AND  
         ( cartera_clientes_estados_historial.ceh_ctcid = @cea_ctcid )  AND
         ( estados_cartera_idiomas.eci_idid = @aci_idid  )     
         )
