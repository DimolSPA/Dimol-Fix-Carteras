CREATE Procedure [dbo].[_rpt_Trae_Historial_Gestiones](@codemp integer, @pclid integer, @ctcid integer) as


--DECLARE @FECHA DATETIME
--		SET @FECHA = ISNULL((SELECT TOP 1 CEA_FECHA FROM CARTERA_CLIENTES_ESTADOS_ACCIONES WHERE CEA_COMENTARIO LIKE 
--'%realizadas las gestiones prejudiciales indicadas precedentemente, el crédito no ha podido ser recuperado” y/o “que agotadas prudencialmente los medios de cobro, el crédito no ha podido ser recuperado%'
--		AND CEA_PCLID = @pclid
--		AND CEA_CODEMP = @codemp
--		AND CEA_CTCID = @ctcid), NULL)

  SELECT distinct
         cartera_clientes_estados_acciones.cea_fecha Fecha,   
         substring(cea_comentario, 1, 4000) as Comentario,   
         usuarios.usr_nombre  Usuario,
         ai.ACI_NOMBRE Tipo
        
    FROM {oj cartera_clientes_estados_acciones LEFT OUTER JOIN deudores_contactos ON cartera_clientes_estados_acciones.cea_codemp = deudores_contactos.ddc_codemp AND cartera_clientes_estados_acciones.cea_ctcid = deudores_contactos.ddc_ctcid AND cartera_clientes_estados_acciones.cea_ddcid = deudores_contactos.ddc_ddcid LEFT OUTER JOIN tipos_contacto ON deudores_contactos.ddc_codemp = tipos_contacto.tic_codemp AND deudores_contactos.ddc_ticid = tipos_contacto.tic_ticid},    
         usuarios,
		  acciones_idiomas ai 
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = ai.aci_codemp ) and  
         ( cartera_clientes_estados_acciones.cea_accid = ai.aci_accid ) and  
		 ( usuarios.usr_codemp = cartera_clientes_estados_acciones.cea_codemp ) and  
         ( usuarios.usr_usrid = cartera_clientes_estados_acciones.cea_usrid ) and  
         ( ( cartera_clientes_estados_acciones.cea_codemp = @codemp ) AND  
         ( cartera_clientes_estados_acciones.cea_pclid = @pclid ) AND  
         ( cartera_clientes_estados_acciones.cea_ctcid = @ctcid )   and datalength(RTRIM(LTRIM(convert(varchar(max),cea_comentario))))>0  )
         --and cartera_clientes_estados_acciones.cea_fecha < '2017-01-04'  --para cortar historial
         --AND CEA_FECHA <= @FECHA
Union
  SELECT DISTINCT   
         cartera_clientes_estados_historial.ceh_fecha,   
         substring(ceh_comentario, 1, 1000) as comentario,   
         usuarios.usr_nombre, 
		 eci.ECI_NOMBRE  
    FROM cartera_clientes_estados_historial,   
         usuarios, 
		 estados_cartera_idiomas eci
   WHERE ( cartera_clientes_estados_historial.ceh_codemp = eci.eci_codemp ) and  
         ( cartera_clientes_estados_historial.ceh_estid = eci.eci_estid ) and 
		 ( usuarios.usr_codemp = cartera_clientes_estados_historial.ceh_codemp ) and  
         ( usuarios.usr_usrid = cartera_clientes_estados_historial.ceh_usrid ) and  
         ( ( cartera_clientes_estados_historial.ceh_codemp = @codemp ) AND  
         ( cartera_clientes_estados_historial.ceh_pclid = @pclid ) AND  
         ( cartera_clientes_estados_historial.ceh_ctcid = @ctcid  and datalength(RTRIM(LTRIM(convert(varchar(max),ceh_comentario))))>0 )     
         )  --and cartera_clientes_estados_historial.ceh_fecha < '2017-01-04'  --para cortar historial
         --AND CEh_FECHA <= @FECHA
order by cea_fecha desc
