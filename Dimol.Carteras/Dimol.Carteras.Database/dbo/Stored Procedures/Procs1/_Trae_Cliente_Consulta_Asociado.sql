create Procedure [dbo].[_Trae_Cliente_Consulta_Asociado]                
(                
  @codemp int  ,
  @usrid int  
  )                
               
as                  
                   
              
BEGIN                

SELECT top 1 pc.[PCC_PCLID_VER] as PCLID, 
	pc.PCC_ESTADOS_VER as ESTADOS,
	p.PCL_RUT as RUT,
	p.PCL_NOMFANT NOMBRE
  FROM [dbo].[PROVCLI_CONSULTA] pc, PROVCLI p
  Where PCC_CODEMP = @codemp
  and  PCC_USRID = @usrid
  and pc.PCC_CODEMP = p.PCL_CODEMP
  and pc.PCC_PCLID_VER = p.PCL_PCLID
      
END 

