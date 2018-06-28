CREATE PROCEDURE [dbo].[_Tesoreria_Finalizar_Documento]
(
@codemp int,
@pclid int,
@ctcid int,
@ccbid int,
@estcpbt varchar(1),
@nuevo_estcpbt varchar(1),
@comentario varchar(100)
)
AS
BEGIN
if @estcpbt !=  'F' 
begin
	UPDATE cartera_clientes_cpbt_doc
		 Set	    	ccb_estcpbt = @nuevo_estcpbt,
						ccb_comentario = @comentario,
						ccb_saldo = 0
		   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @codemp ) AND  
				 ( cartera_clientes_cpbt_doc.ccb_pclid = @pclid ) AND  
				 ( cartera_clientes_cpbt_doc.ccb_ctcid = @ctcid ) AND  
				 ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccbid )
	
end
else select 1
END
