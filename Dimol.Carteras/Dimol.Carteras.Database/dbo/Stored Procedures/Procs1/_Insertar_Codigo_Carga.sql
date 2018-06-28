CREATE Procedure [dbo].[_Insertar_Codigo_Carga](
					@pcc_codemp integer, 
					@pcc_pclid numeric (15), 
					@pcc_codigo varchar (20), 
					@pcc_nombre varchar (120)) 
as   

declare @id_codigo int

set @id_codigo = (select IsNull(Max(PCC_CODID)+1, 1) from provcli_codigo_carga 
					where PCC_CODEMP = @pcc_codemp
					and PCC_PCLID=@pcc_pclid)    

  INSERT INTO provcli_codigo_carga    
         ( pcc_codemp,     
           pcc_pclid,     
           pcc_codid,     
           pcc_codigo,     
           pcc_nombre )    
  VALUES ( @pcc_codemp,     
           @pcc_pclid,     
           @id_codigo,     
           @pcc_codigo,     
           @pcc_nombre )
