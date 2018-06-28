

CREATE Procedure [dbo].[Trae_Configuracion_Sistema](@cfid integer) as  
SELECT configuracion_sistema.cfs_valnum,   
         configuracion_sistema.cfs_valtxt  
    FROM configuracion_sistema with (nolock)  
   WHERE configuracion_sistema.cfs_cfsid = @cfid
