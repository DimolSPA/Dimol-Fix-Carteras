-- =============================================      
-- Author:  Pablo Leyton      
-- Create date: 10-06-2014      
-- Description: Procedimiento Inserta Tipo imagenes documentos jQgrid      
-- =============================================      

CREATE Procedure [dbo].[_Insertar_Tipos_Imagenes_Documentos]  
   (  
   @codemp integer,   
   @nombre varchar (80),  
   @idid smallint)     
as    
declare @id int    
    
set @id = (select IsNull(Max(TPC_TPCID )+1, 1) from tipos_imagenes_cpbtdoc 
				where TPC_CODEMP = @codemp)    
    
  INSERT INTO tipos_imagenes_cpbtdoc      
         (  
			TPC_CODEMP,  
			TPC_TPCID,   
			TPC_NOMBRE )      

  VALUES ( @codemp,       
           @id,       
           @nombre)    
          
   
   INSERT INTO tipos_imagenes_cpbtdoc_idiomas
   (
	   TPI_CODEMP ,
	   TPI_TPCID ,
	   TPI_NOMBRE ,
	   TPI_IDID 
   )
   VALUES
		(@codemp,       
		@id,       
		@nombre,
		@idid)
