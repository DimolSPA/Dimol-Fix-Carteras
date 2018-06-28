

/****** Object:  StoredProcedure [dbo].[_Insert_Log_Buscar_Facturas]    Script Date: 28-04-2017 13:30:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[_Insert_Log_Buscar_Facturas] (@rut varchar(20),
	 @cliente varchar (200),
	 @factura varchar (200),
	 @ruta varchar(200)) as

begin
insert into Log_Buscar_Facturas values(@rut,@cliente,@factura,getdate(),@ruta)
end
GO

