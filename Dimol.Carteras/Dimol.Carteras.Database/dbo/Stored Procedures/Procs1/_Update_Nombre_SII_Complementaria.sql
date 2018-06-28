CREATE Procedure [dbo].[_Update_Nombre_SII_Complementaria](@ctcid integer, @nombre varchar(200)) as  
 
  UPDATE AA_CASTIGO_MASIVO_MUTUAL_COMPLEMENTARIA2  
     SET nombre_sii = @nombre
   WHERE CTCID = @ctcid
