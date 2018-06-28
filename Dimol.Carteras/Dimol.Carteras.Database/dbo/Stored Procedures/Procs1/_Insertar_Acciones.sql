Create Procedure [dbo].[_Insertar_Acciones](@codemp integer, @nombre varchar (80), @agrupa smallint, @idid smallint) as
declare @accid int

set @accid = (select IsNull(Max(acc_accid)+1, 1) from acciones where acc_codemp = @codemp)

  INSERT INTO acciones  
         ( acc_codemp,   
           acc_accid,   
           acc_nombre,   
           acc_agrupa )  
  VALUES ( @codemp,   
           @accid,   
           @nombre,   
           @agrupa )
           
  INSERT INTO acciones_idiomas  
         ( aci_codemp,   
           aci_accid,   
           aci_idid,   
           aci_nombre )  
  VALUES ( @codemp,   
           @accid,   
           @idid,   
           @nombre )
