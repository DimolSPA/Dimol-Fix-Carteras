-- =============================================            
-- Author:  Pablo Leyton            
-- Create date: 07-08-2014            
-- Description: Procedimiento Inserta Gestores jQgrid            
-- =============================================            
      
CREATE Procedure [dbo].[_Insertar_Gestor](  
 @ges_codemp integer,   
 @ges_sucid integer,   
 @ges_nombre varchar (500),    
 @ges_telefono integer,   
 @ges_email varchar (100),   
 @ges_tipcart smallint,   
 @ges_comki decimal (8,4),    
 @ges_comhon decimal (8,4),   
 @ges_emplid integer,   
 @ges_remoto char(1),    
 @ges_comjki decimal(8,4),   
 @ges_comjhon decimal(8,4),
 @ges_grupoid int)   
 as    
   
 declare @id int          
          
 set @id = (select IsNull(Max(GES_GESID )+1, 1) from gestor       
     where GES_CODEMP = @ges_codemp  
     and GES_SUCID=@ges_sucid)     
       
  INSERT INTO gestor      
         ( ges_codemp,       
           ges_sucid,       
           ges_gesid,       
           ges_nombre,       
           ges_telefono,       
           ges_email,       
           ges_tipcart,       
           ges_comki,       
           ges_comhon,       
           ges_emplid,    
           ges_remoto,    
           ges_estado,    
           ges_comjki,    
           ges_comjhon  )      
  VALUES ( @ges_codemp,       
           @ges_sucid,       
           @id,       
           @ges_nombre,       
           @ges_telefono,       
           @ges_email,       
           @ges_tipcart,       
           @ges_comki,       
           @ges_comhon,       
           @ges_emplid,    
           @ges_remoto,    
           'A',    
           @ges_comjki,    
           @ges_comjhon  ) 
           
           
   INSERT INTO grupo_cobranza_gestor
   (GCG_CODEMP,GCG_SUCID,GCG_GRCID,GCG_GESID)
   VALUES 
   (@ges_codemp,@ges_sucid,@ges_grupoid,@id)
