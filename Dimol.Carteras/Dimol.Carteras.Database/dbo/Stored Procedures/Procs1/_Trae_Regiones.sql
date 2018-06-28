CREATE Procedure [dbo].[_Trae_Regiones]   
	@idPais  int =null     
          
as              
               
          
BEGIN        
     
  select REG_REGID ,REG_NOMBRE from region
  where @idPais is null or REG_PAIID=@idPais
  order by REG_NOMBRE  
END
