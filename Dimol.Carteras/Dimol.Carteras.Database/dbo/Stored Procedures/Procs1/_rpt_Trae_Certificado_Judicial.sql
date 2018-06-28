CREATE Procedure [dbo].[_rpt_Trae_Certificado_Judicial](@codemp integer, @rolid integer) as

SELECT rol_rolid,
		isnull(rol_tipo_rol,'C') tipo_rol,
		rol_numero,
		trb_nombre,
		epl_rut,   
        epl_nombre,   
        epl_apepat,   
        epl_apemat
    FROM view_rol,  
         entejud_rol,   
         entes_judicial,   
         empleados 
   WHERE ( etj_codemp = ejr_codemp ) and  
         ( etj_etjid = ejr_etjid ) and  
         ( rol_codemp = ejr_codemp ) and  
         ( rol_rolid = ejr_rolid ) and  
         ( epl_codemp = etj_codemp ) and  
         ( epl_emplid = etj_emplid ) and  
         ( ( rol_codemp = @codemp ) AND  
         ( rol_rolid = @rolid ) AND  
         ( etj_abogado = 'S' ) )
