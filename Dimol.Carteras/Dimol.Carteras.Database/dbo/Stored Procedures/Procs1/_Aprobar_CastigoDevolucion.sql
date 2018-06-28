CREATE PROCEDURE [dbo].[_Aprobar_CastigoDevolucion]
(
@codemp int,
@pclid int,
@ctcid int,
@ccbid int,
@monto decimal(15,2),
@estcpbt varchar(1),
@nuevo_estcpbt varchar(1),
@nuevoEstado int,
@comentario varchar(100),
@user int,
@ip_red  varchar(30),
@ip_maquina varchar(30)
)
AS
BEGIN
if @estcpbt !=  'F' 
begin
	UPDATE cartera_clientes_cpbt_doc
		 Set	    	ccb_estcpbt = @nuevo_estcpbt,
						ccb_estid = @nuevoEstado,
						ccb_comentario = @comentario,
						ccb_saldo = 0
		   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @codemp ) AND  
				 ( cartera_clientes_cpbt_doc.ccb_pclid = @pclid ) AND  
				 ( cartera_clientes_cpbt_doc.ccb_ctcid = @ctcid ) AND  
				 ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccbid )

	INSERT INTO [dbo].[CARTERA_CLIENTES_ESTADOS_HISTORIAL]
           ([CEH_CODEMP],[CEH_PCLID],[CEH_CTCID] ,[CEH_CCBID],[CEH_FECHA],[CEH_ESTID],[CEH_SUCID],[CEH_GESID],[CEH_IPRED],[CEH_IPMAQUINA],[CEH_COMENTARIO] ,[CEH_MONTO],[CEH_SALDO] ,[CEH_USRID])
		VALUES
           (@codemp  
           ,@pclid 
           ,@ctcid  
           ,@ccbid
           ,getdate()
           ,@nuevoEstado
           ,1
           ,null
           ,@ip_red
           ,@ip_maquina
           ,@comentario
           ,@monto
           ,0
           ,@user)
end
else select 1
END
