create Procedure [dbo].[_Update_Nombre_SII](@ctcid integer, @nombre varchar(200)) as  
 
  UPDATE [AA_CASTIGO_MASIVO_MUTUAL_LEY]  
     SET nombre_sii = @nombre
   WHERE [CTC_CTCID] = @ctcid
