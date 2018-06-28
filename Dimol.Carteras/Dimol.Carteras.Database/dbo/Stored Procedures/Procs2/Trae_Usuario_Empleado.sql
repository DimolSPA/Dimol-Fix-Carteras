CREATE Procedure [dbo].[Trae_Usuario_Empleado](
	@codemp integer,
	@usuario integer
) AS
SELECT
	empleados.epl_codemp,
	empleados.epl_emplid,
	empleados.epl_rut,
	empleados.epl_nombre,
	empleados.epl_apepat,
	empleados.epl_apemat,
	empleados.epl_eemid,
	empleados.epl_comid,
	empleados.epl_direccion,
	empleados.epl_telefono,
	empleados.epl_celular,
	empleados.epl_mail,
	--empleados.epl_fecing,
	FORMAT(empleados.epl_fecing,'yyy-MM-dd HH:mm:ss'),
	empleados.epl_fecfin,
	empleados.epl_sucid,
	estados_empleado.eem_accion
FROM
	empleados,   
	estados_empleado  
WHERE 
	( estados_empleado.eem_codemp = empleados.epl_codemp ) and  
	( estados_empleado.eem_eemid = empleados.epl_eemid ) and  
	(
		( empleados.epl_codemp = @codemp ) AND  
		( empleados.epl_usrid = @usuario )   
	)
