

CREATE Procedure [dbo].[Insertar_Deudores_Telefonos](@ddt_codemp integer, @ddt_ctcid numeric (15), @ddt_numero numeric(12),
                                                                            @ddt_tipo char (1), @ddt_estado char (1)) as 
                                                                            
declare @existe int=0

set @existe = (select count(ddt_codemp)    from deudores_telefonos where  ddt_codemp = @ddt_codemp and ddt_ctcid=@ddt_ctcid and ddt_numero=@ddt_numero)  
                                                                  
if @existe = 0
begin                                                                        
 INSERT INTO deudores_telefonos  
         ( ddt_codemp,   
           ddt_ctcid,   
           ddt_numero,   
           ddt_tipo,   
           ddt_estado,
           ddt_prioridad )  
  VALUES ( @ddt_codemp,   
           @ddt_ctcid,   
           @ddt_numero,   
           @ddt_tipo,   
           @ddt_estado,
           4 )
end 