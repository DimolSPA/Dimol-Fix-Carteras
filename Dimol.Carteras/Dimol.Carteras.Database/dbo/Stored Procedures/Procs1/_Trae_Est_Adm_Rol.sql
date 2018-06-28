CREATE Procedure [dbo].[_Trae_Est_Adm_Rol]      
(                        
  @rolid int             
  )      
     
as        
         
    
BEGIN    

SELECT isnull(ESTADO_ADM,'')  ESTADO_ADM
		FROM [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] PJ 
		INNER JOIN [10.0.1.11].[PoderJudicial].[dbo].ROL_PODER_JUDICIAL RPJ
		ON PJ.ID_CAUSA = RPJ.RPJ_ID_CAUSA
		WHERE RPJ.RPJ_ROLID = @rolid

END
