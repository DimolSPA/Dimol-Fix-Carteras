﻿CREATE Procedure [dbo].[_Trae_Tipos_Cpbt_Perfil_Tipo_Cast_Dev](@clb_codemp integer, @clb_tipcpbtdoc char(1), @pfc_prfid integer, @tci_idid integer, @clb_cartcli char(1)) as
  SELECT tipos_cpbtdoc.tpc_tpcid,   
         tipos_cpbtdoc_idiomas.tci_nombre  
    FROM clasificacion_cpbtdoc,   
         tipos_cpbtdoc_idiomas,   
         tipos_cpbtdoc  
   WHERE ( tipos_cpbtdoc.tpc_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( tipos_cpbtdoc.tpc_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid ) and  
         ( tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp ) and  
         ( tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid ) and  
         ( ( clasificacion_cpbtdoc.clb_codemp = @clb_codemp ) AND  
         ( clasificacion_cpbtdoc.clb_tipcpbtdoc = @clb_tipcpbtdoc ) AND  
         ( clasificacion_cpbtdoc.clb_cartcli = @clb_cartcli ) AND  
         ( tipos_cpbtdoc_idiomas.tci_tpcid in (  SELECT perfiles_comprobantes.pfc_tpcid  
                                                   FROM perfiles_comprobantes  
                                                  WHERE ( perfiles_comprobantes.pfc_codemp = @clb_codemp ) 
                                                  AND  
                                                        ( perfiles_comprobantes.pfc_prfid = @pfc_prfid )   
                                                         )) 
                                                         AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid )   
         )   and TPC_TPCID in (31,32,33,34)
ORDER BY tipos_cpbtdoc_idiomas.tci_nombre ASC
