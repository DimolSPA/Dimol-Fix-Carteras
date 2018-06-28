
CREATE procedure [dbo].[_LogOff_Usr](@ip varchar(50)) as  
  update usuarios_pj   
  set ip = null   
  where ip = @ip  