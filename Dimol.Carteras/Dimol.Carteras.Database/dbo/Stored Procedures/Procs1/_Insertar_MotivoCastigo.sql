CREATE Procedure [dbo].[_Insertar_MotivoCastigo]  
   (  
			   @codemp integer,   
			   @nombre varchar (80),
			   @idid smallint     
   )  
as    

declare @idMotivo int    
    
set @idMotivo = (select IsNull(Max(TMC_TMCID)+1, 1) from tipos_motivos_castigos 
					where TMC_CODEMP = @codemp)    
    
  INSERT INTO tipos_motivos_castigos      
         (  
			TMC_CODEMP,  
			TMC_TMCID,   
			TMC_NOMBRE )      
  
  VALUES ( @codemp,       
           @idMotivo,       
           @nombre  )    
              
              
   
   INSERT INTO  tipos_motivos_castigos_idiomas
			(TMI_CODEMP ,
			TMI_TMCID,
			TMI_IDID ,
			TMI_NOMBRE)       
   VALUES ( @codemp,       
           @idMotivo,  
           @idid,     
           @nombre  )
