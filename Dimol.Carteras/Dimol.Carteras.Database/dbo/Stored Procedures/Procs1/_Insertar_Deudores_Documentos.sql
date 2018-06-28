CREATE Procedure [dbo].[_Insertar_Deudores_Documentos](@codemp integer, @ctcid integer, @tddid integer, @nombre varchar(800), @pclid varchar(20)) as

declare @dcdid int =(  SELECT IsNull(Max(dcd_dcdid)+1, 1)
    FROM deudores_documentos  
   WHERE ( deudores_documentos.dcd_codemp = @codemp ) AND  
         ( deudores_documentos.dcd_ctcid = @ctcid ))
         
  INSERT INTO deudores_documentos  
         ( dcd_codemp,   
           dcd_ctcid,   
           dcd_dcdid,   
           dcd_tddid,   
           dcd_nombre,   
           dcd_pclid )  
  VALUES ( @codemp,   
           @ctcid,   
           @dcdid,   
           @tddid,   
           @nombre,   
           @pclid )
           
           select @dcdid dcdid
