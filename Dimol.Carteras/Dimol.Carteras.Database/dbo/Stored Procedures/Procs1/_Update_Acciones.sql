CREATE Procedure [dbo].[_Update_Acciones](@codemp integer, @accid integer, @nombre varchar (80), @agrupa smallint, @idid smallint) as
  UPDATE acciones  
     SET acc_nombre = @nombre,   
         acc_agrupa = @agrupa  
   WHERE ( acciones.acc_codemp = @codemp ) AND  
         ( acciones.acc_accid = @accid )
         
  UPDATE acciones_idiomas  
     SET aci_nombre = @nombre  
   WHERE ( acciones_idiomas.aci_codemp = @codemp ) AND  
         ( acciones_idiomas.aci_accid = @accid ) AND  
         ( acciones_idiomas.aci_idid = @idid )
