CREATE Procedure [dbo].[Update_Revision_CasosRemear] as  
  
UPDATE aplicaciones_items         
SET api_remesa = 'S'      
FROM aplicaciones_items,              aplicaciones       
WHERE ( aplicaciones.apl_codemp = aplicaciones_items.api_codemp ) 
and             ( aplicaciones.apl_sucid = aplicaciones_items.api_sucid ) 
and             ( aplicaciones.apl_anio = aplicaciones_items.api_anio ) 
and             ( aplicaciones.apl_numapl = aplicaciones_items.api_numapl ) 
and             ( ( convert(varchar, api_aniodoc) + '_' + convert(varchar, api_numdoc) + '_' + convert(varchar, api_capital) + '_' + convert(varchar, api_interes) + '_' + convert(varchar, api_honorario) + '_' + convert(varchar, api_gastpre) + '_' + convert(varchar, api_gastjud) 
in (SELECT convert(varchar, api_aniodoc) + '_' + convert(varchar, api_numdoc) + '_' + convert(varchar, api_capital) + '_' + convert(varchar, api_interes) + '_' + convert(varchar, api_honorario) + '_' + convert(varchar, api_gastpre) + '_' + convert(varchar, api_gastjud) 
FROM aplicaciones_items with (nolock), aplicaciones  with (nolock)
WHERE ( aplicaciones.apl_codemp = aplicaciones_items.api_codemp ) 
and ( aplicaciones.apl_sucid = aplicaciones_items.api_sucid ) 
and ( aplicaciones.apl_anio = aplicaciones_items.api_anio ) 
and ( aplicaciones.apl_numapl = aplicaciones_items.api_numapl ) 
and ( ( aplicaciones_items.api_codemp = 1 ) AND ( aplicaciones.apl_accion = 1 ) ) ) ) 
AND             ( aplicaciones.apl_accion = -1 ) 
AND             ( aplicaciones_items.api_remesa = 'N' )   
           )            
UPDATE aplicaciones_items         
SET api_remesa = 'S'     
FROM aplicaciones,              aplicaciones_items       
WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) 
and             ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) 
and             ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) 
and             ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) 
and             ( ( aplicaciones.apl_accion = 1 ) AND             ( aplicaciones_items.api_remesa = 'N' )              )    