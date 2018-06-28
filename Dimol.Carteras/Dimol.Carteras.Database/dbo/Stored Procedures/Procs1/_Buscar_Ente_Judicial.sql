-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Buscar_Ente_Judicial] (@codemp int,  @rut varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	Select entes_judicial.etj_etjid
            FROM entes_judicial,   
              empleados,   
            estados_empleado
            WHERE  empleados.epl_codemp = entes_judicial.etj_codemp  and  
             empleados.epl_emplid = entes_judicial.etj_emplid  and  
             estados_empleado.eem_codemp = empleados.epl_codemp  and  
             estados_empleado.eem_eemid = empleados.epl_eemid  and  
             entes_judicial.etj_codemp =  @codemp
             and empleados.epl_rut = @rut

            UNION
            Select entes_judicial.etj_etjid
            FROM entes_judicial,   
            provcli
            WHERE  provcli.pcl_codemp = entes_judicial.etj_codemp  and  
             provcli.pcl_pclid = entes_judicial.etj_pclid  and  
             entes_judicial.etj_codemp = @codemp
                and  provcli.pcl_rut = @rut
            and provcli.pcl_estado = 'V'    
	
	
END
