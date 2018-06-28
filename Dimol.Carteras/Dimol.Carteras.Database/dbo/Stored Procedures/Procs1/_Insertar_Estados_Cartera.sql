-- =============================================        
-- Author:  Pablo Leyton        
-- Create date: 18-07-2014        
-- Description: Procedimiento Inserta Tipo imagenes documentos jQgrid        
-- =============================================        
  
CREATE Procedure [dbo].[_Insertar_Estados_Cartera](
@ect_codemp integer, 
@ect_nombre varchar (300), 
@ect_agrupa smallint, 
@ect_utiliza char (1), 
@ect_prejud char (1), 
@ect_solfecha char (1), 
@ect_genret char (1), 
@ect_compromiso char(1),
@eci_idid int) 
as        
      
declare @id int      
      
set @id = (select IsNull(Max(ECT_ESTID )+1, 1) from estados_cartera   
    where ECT_CODEMP = @ect_codemp)      
      
   INSERT INTO estados_cartera    
         ( ect_codemp,     
           ect_estid,     
           ect_nombre,     
           ect_agrupa,     
           ect_utiliza,     
           ect_prejud,     
           ect_solfecha,     
           ect_genret,  
           ect_compromiso,
           ECT_HABILITADO )    
  VALUES ( @ect_codemp,     
           @id,     
           @ect_nombre,     
           @ect_agrupa,     
           @ect_utiliza,     
           @ect_prejud,     
           @ect_solfecha,     
           @ect_genret,  
           @ect_compromiso,
           'S' )    
            
   INSERT INTO estados_cartera_idiomas    
         ( eci_codemp,     
           eci_estid,     
           eci_idid,     
           eci_nombre )    
  VALUES ( @ect_codemp,     
           @id,     
           @eci_idid,     
           @ect_nombre )
