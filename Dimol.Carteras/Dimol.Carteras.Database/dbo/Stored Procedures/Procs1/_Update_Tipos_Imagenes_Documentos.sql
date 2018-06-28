CREATE Procedure [dbo].[_Update_Tipos_Imagenes_Documentos]  
   (  
   @codemp integer,   
   @id integer,   
   @nombre varchar (80),  
   @idid smallint)   
 as    
  UPDATE tipos_imagenes_cpbtdoc      
     SET TPC_NOMBRE = @nombre      
   WHERE ( TPC_CODEMP = @codemp )   
  AND ( TPC_TPCID = @id )    
    
 UPDATE tipos_imagenes_cpbtdoc_idiomas      
     SET TPI_NOMBRE = @nombre      
   WHERE ( TPI_CODEMP = @codemp )   
  AND ( TPI_TPCID = @id )    
  AND ( TPI_IDID = @idid )
