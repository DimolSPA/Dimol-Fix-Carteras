CREATE Procedure [dbo].[_Insertar_materia_judicial]    
   (    
      @codemp integer,     
      @nombre varchar (80),  
      @orden  integer,
      @idid smallint       
   )    
as      
  
declare @Id int      
      
set @Id = (select IsNull(Max(ESJ_ESJID)+1, 1) from materia_judicial   
     where ESJ_CODEMP = @codemp)      
      
  INSERT INTO materia_judicial        
	(ESJ_CODEMP,ESJ_ESJID,ESJ_NOMBRE,ESJ_ORDEN )        
  VALUES 
	(@codemp,@Id,@nombre,@orden  )      
                
                
     
   INSERT INTO  materia_judicial_idiomas  
   (MJI_CODEMP ,MJI_ESJID,MJI_IDID,MJI_NOMBRE )
   VALUES 
   ( @codemp, @Id , @idid, @nombre  )
