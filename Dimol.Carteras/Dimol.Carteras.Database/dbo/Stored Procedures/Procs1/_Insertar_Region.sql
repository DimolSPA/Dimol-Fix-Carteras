Create Procedure [dbo].[_Insertar_Region](
		@reg_paiid integer, 
   	    @reg_nombre varchar(200), 
		@reg_orden smallint) as  
  
declare @id int                      
set @id = (select IsNull(Max(reg_regid )+1, 1) from region )                  
  
  INSERT INTO region    
         ( reg_paiid,     
           reg_regid,     
           reg_nombre,     
           reg_orden )    
  VALUES ( @reg_paiid,     
           @id,     
           @reg_nombre,     
           @reg_orden )
