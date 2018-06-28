CREATE VIEW [dbo].[PREGESTIONES]
AS
SELECT distinct PCL_RUT 'RUT CLIENTE', 
		PCL_NOMFANT 'NOMBRE CLIENTE',
		CTC_RUT 'RUT DEUDOR',
		CTC_NOMFANT 'NOMBRE DEUDOR',
         cartera_clientes_estados_acciones.cea_fecha FECHA, 
		ai.aci_nombre ESTADO,     
         u.usr_nombre 'GESTOR',   
         substring(cea_comentario, 1, 1000) as COMENTARIO 
         
		 
    FROM {oj cartera_clientes_estados_acciones 
		LEFT OUTER JOIN deudores_contactos ON cartera_clientes_estados_acciones.cea_codemp = deudores_contactos.ddc_codemp AND cartera_clientes_estados_acciones.cea_ctcid = deudores_contactos.ddc_ctcid AND cartera_clientes_estados_acciones.cea_ddcid = deudores_contactos.ddc_ddcid LEFT OUTER JOIN tipos_contacto ON deudores_contactos.ddc_codemp = tipos_contacto.tic_codemp AND deudores_contactos.ddc_ticid = tipos_contacto.tic_ticid} ,   
         acciones_idiomas ai,   
         usuarios u,
         provcli,
         deudores   
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = ai.aci_codemp ) and  
         ( cartera_clientes_estados_acciones.cea_accid = ai.aci_accid ) and  
         ( u.usr_codemp = cartera_clientes_estados_acciones.cea_codemp ) and  
         ( u.usr_usrid = cartera_clientes_estados_acciones.cea_usrid ) and  
         ( cea_codemp = pcl_codemp ) and  cea_accid != 11 AND
         ( cea_pclid = pcl_pclid ) and  
         ( cea_codemp = ctc_codemp ) and  
         ( cea_ctcid = ctc_ctcid ) and  
		( CEA_FECHA >= '01-01-2000'  and 
		   CEA_FECHA < '01-01-2019'  ) and
         ( ( cea_codemp = 1) AND  
         ( cea_pclid = 318 ) AND  
         ( ai.aci_idid = 1) AND 
		 U.USR_NOMBRE<> 'PEDRO SALAS' AND
		 U.USR_NOMBRE<> 'MIGUEL HERRERA' AND
		 U.USR_NOMBRE<> 'FELIPE MUÑOZ' AND
		CEA_CTCID IN 
			(SELECT CCB_CTCID 
			FROM CARTERA_CLIENTES_CPBT_DOC 
			WHERE cartera_clientes_estados_acciones.CEA_CODEMP = CCB_CODEMP AND 
			cartera_clientes_estados_acciones.CEA_CTCID = CCB_CTCID AND 
			cartera_clientes_estados_acciones.CEA_PCLID = CCB_PCLID AND
			CCB_ESTCPBT in ('V','F'))  
         )

	Union
  SELECT DISTINCT PCL_RUT 'RUT CLIENTE', 
		PCL_NOMFANT 'NOMBRE CLIENTE',
		CTC_RUT 'RUT DEUDOR',
		CTC_NOMFANT 'NOMBRE DEUDOR',
         cceh.ceh_fecha FECHA,   
		eci.eci_nombre ESTADO,   
         u.usr_nombre 'GESTOR', 
         substring(ceh_comentario, 1, 1000) as COMENTARIO  
        
		 
    FROM cartera_clientes_estados_historial cceh,   
         estados_cartera_idiomas eci,   
         usuarios u,
         estados_cartera ec,
         provcli,
         deudores     
   WHERE ( cceh.ceh_codemp = eci.eci_codemp ) and  
         ( cceh.ceh_estid = eci.eci_estid ) and  
         ( u.usr_codemp = cceh.ceh_codemp ) and  
         ( u.usr_usrid = cceh.ceh_usrid ) and  
         ( ceh_codemp = pcl_codemp ) and  
         ( ceh_pclid = pcl_pclid ) and  
         ( ceh_codemp = ctc_codemp ) and  
         ( ceh_ctcid = ctc_ctcid ) and  
		( CEH_FECHA >= '01-01-2000'  and 
		   CEH_FECHA < '01-01-2019'  ) and
         ( ( cceh.ceh_codemp = 1 ) AND  
         ( ec.ect_codemp = eci_codemp ) AND  
         ( ec.ect_estid = eci_estid )  AND
         ( cceh.ceh_pclid = 318 ) AND  
         ( eci.eci_idid =  1 ) AND 
		  (U.USR_NOMBRE<> 'PEDRO SALAS') AND
		 (U.USR_NOMBRE<> 'MIGUEL HERRERA') AND
		( U.USR_NOMBRE<> 'FELIPE MUÑOZ') AND
			CEH_CTCID IN 
			(SELECT CCB_CTCID 
			FROM CARTERA_CLIENTES_CPBT_DOC 
			WHERE cceh.CEH_CODEMP = CCB_CODEMP AND 
			cceh.CEH_CTCID = CCB_CTCID AND 
			cceh.CEH_PCLID = CCB_PCLID AND
			CCB_ESTCPBT  in ('V','F'))      
         )
