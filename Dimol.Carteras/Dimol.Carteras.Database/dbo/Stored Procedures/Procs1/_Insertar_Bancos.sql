Create Procedure [dbo].[_Insertar_Bancos](
		@bco_codemp integer, 
		@bco_rut varchar (20), 
		@bco_nombre varchar (200), 
		@bco_protesto text) 
as   

declare @id int                              
set @id = (select IsNull(Max(bco_bcoid )+1, 1) from bancos    
   where bco_codemp = @bco_codemp)                          

  INSERT INTO bancos    
         ( bco_codemp,     
           bco_bcoid,     
           bco_rut,     
           bco_nombre,  
           bco_protesto )    
  VALUES ( @bco_codemp,     
           @id,     
           @bco_rut,     
           @bco_nombre,  
           @bco_protesto )
