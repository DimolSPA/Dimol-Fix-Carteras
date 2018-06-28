-- =============================================
-- Author:		FM
-- Create date: 09-10-2014
-- Description:	Proceso aceptar comprobantes
-- =============================================
CREATE PROCEDURE [dbo].[_Aceptar_Comprobante_bajar_CARTERA] (@codemp int , @codsuc int, @idioma int, @tipo_documento int, @numero int, @crea_aplicacion varchar(1), @numapl int, @usuario int, @ip_red varchar(20), @ip_pc varchar(20))
AS
BEGIN
	SET NOCOUNT ON;

declare @pclid int 
declare @ctcid int
declare @ccbid int
declare @saldo decimal(15,2)
declare @neto  decimal(15,2)
declare @item int 
declare @indice int =0
declare @estId int 
declare @rolid int =0
declare @fecha_rol datetime
declare @matjud int
declare @rol_anterior int = 0
declare @anio int = Year(getdate())
declare @mes int = Month(getdate())
declare @fecha datetime = getdate()
    --Try
declare cursor_comprobante cursor for 
SELECT detalle_comprobantes.dcc_pclid,   
detalle_comprobantes.dcc_ctcid,   
detalle_comprobantes.dcc_ccbid,   
detalle_comprobantes.dcc_saldo, 
dcc_item, 
dcc_neto
FROM detalle_comprobantes
WHERE  detalle_comprobantes.dcc_codemp = @codemp
and detalle_comprobantes.dcc_sucid = @codsuc
and detalle_comprobantes.dcc_tpcid = @tipo_documento
and detalle_comprobantes.dcc_numero = @numero


If @crea_aplicacion = 'S' 
begin
	--------------Creo la aplicacion--------------

	exec Insertar_Aplicaciones @codemp,@codsuc,@anio,@mes, @numapl, 5, -1, @usuario
	--select 'Insertar_Aplicaciones', @codemp, @codsuc, Year( getdate()), month( getdate()), @numapl, 5, -1, @usuario

	if @@ROWCOUNT =0 and @@ERROR <> 0 begin
		return
	end

	set @crea_aplicacion = 'N'
End 

OPEN cursor_comprobante  
FETCH NEXT FROM cursor_comprobante INTO @pclid,@ctcid,@ccbid, @saldo, @item, @neto 
WHILE @@FETCH_STATUS = 0  
BEGIN  

--select @pclid,@ctcid,@ccbid, @saldo, @item, @neto 

set @indice = @indice + 1

-------------Inserto el Detalle de la Aplicacion-------------------
exec Insertar_Aplicaciones_Items @codemp,@codsuc,@anio,@numapl,@indice,null,null,null,null,@tipo_documento,@numero,null,null,@pclid,@ctcid,@ccbid,@saldo,0,0,0,0,null,null
--select 'Insertar_Aplicaciones_Items', @codemp,@codsuc,year(getdate()),@numapl,@indice,null,null,null,null,@tipo_documento,@numero,null,null,@pclid,@ctcid,@ccbid,@saldo,0,0,0,0,null,null

if @@ROWCOUNT =0 and @@ERROR <> 0 begin
	return
end

---------------Actualizo el detalle---------------------
exec Update_Detalle_Comprobantes_Saldos @codemp,@codsuc,@tipo_documento, @numero,@item,0
--select 'Update_Detalle_Comprobantes_Saldos', @codemp,@codsuc,@tipo_documento, @numero,@item,0
if @@ROWCOUNT =0 and @@ERROR <> 0 begin
	return
end

----------------Reviso el tipo de comprobante------------
select @estId = splitdata from dbo.fnSplitString((SELECT    
         empresa_configuracion.emc_valtxt  
    FROM empresa_configuracion  
   WHERE ( empresa_configuracion.emc_codemp = 1 ) AND  
         ( empresa_configuracion.emc_emcid = 74 )),',')
where id = (select id from dbo.fnSplitString((SELECT    
         empresa_configuracion.emc_valtxt  
    FROM empresa_configuracion  
   WHERE ( empresa_configuracion.emc_codemp = 1 ) AND  
         ( empresa_configuracion.emc_emcid = 73 )),',')
         where splitdata = @tipo_documento)
         
--select @estId

---------------------Actualizo el detalle de la cartera------------------------
exec Update_Cartera_Clientes_Cpbt_Doc_Canc @codemp,@pclid,@ctcid,@ccbid,0,0,0,0,0,@estId,'F'
--select 'Update_Cartera_Clientes_Cpbt_Doc_Canc', @codemp,@pclid,@ctcid,@ccbid,0,0,0,0,0,@estId,'F'
if @@ROWCOUNT =0 and @@ERROR <> 0 begin
	return
end

----------------Actualizo la ultima gestion--------------------
exec Update_Cartera_Clientes_Cpbt_Doc_Estados @codemp,@pclid,@ctcid,@ccbid,@estId,'F'
--select 'Update_Cartera_Clientes_Cpbt_Doc_Estados', @codemp,@pclid,@ctcid,@ccbid,@estId,'F'
if @@ROWCOUNT =0 and @@ERROR <> 0 begin
	return
	--select 0
end


-----------------Inserto el estado--------------------------
--exec Insertar_Cartera_Clientes_Estados_Historial_Especial 
  INSERT INTO cartera_clientes_estados_historial  
         ( ceh_codemp,ceh_pclid,ceh_ctcid,ceh_ccbid,ceh_fecha,ceh_estid,ceh_sucid,ceh_gesid,ceh_ipred,ceh_ipmaquina,ceh_comentario,ceh_monto, ceh_saldo,ceh_usrid )  
  VALUES ( @codemp,@pclid,@ctcid,@ccbid,@fecha,@estid,@codsuc,null,@ip_red,@ip_pc,'SE FINALIZA DEUDA',@neto,@saldo,@usuario )
--select 'Insertar_Cartera_Clientes_Estados_Historial_Especial', @codemp,@pclid,@ctcid,@ccbid,getdate(),@estid,@codsuc,null,@ip_red,@ip_pc,'SE FINALIZA DEUDA',@neto,@saldo,@usuario
if @@ROWCOUNT =0 and @@ERROR <> 0 begin
	return
end

--fecHor = DateAdd(DateInterval.Second, seg, fecha)

--fecha = fecHor


--------------Reviso si esta el documento en algun ROL----------------------
select  @rolid=rdc_rolid from rol_documentos where rdc_codemp= @codemp
and rdc_pclid = @pclid
and rdc_ctcid = @ctcid
and rdc_ccbid = @ccbid

--select @rolid =  10

If @rolid > 0 begin

select @fecha_rol =  max(rle_fecha) from rol_estados where rle_codemp= @codemp and rle_rolid = @rolid

select @matjud = rle_esjid from rol_estados where rle_codemp= @codemp
and rle_rolid = @rolid
and rle_fecha >=@fecha_rol
and rle_fecha <= DATEADD(second,1,@fecha_rol)

select @matjud
    If @rol_anterior <> @rolid begin
        set @rol_anterior = @rolid

        exec Insertar_Rol_Estados @codemp,@rolid,@estId,@matjud,@usuario,@ip_red,@ip_pc,'FINALIZADO',@fecha
        --select 'Insertar_Rol_Estados', @codemp,@rolid,@estId,@matjud,@usuario,@ip_red,@ip_pc,'FINALIZADO',getdate()

        if @@ROWCOUNT =0 and @@ERROR <> 0 begin
			return
		end 

    End 


End 

       FETCH NEXT FROM cursor_comprobante INTO @pclid,@ctcid,@ccbid, @saldo, @item, @neto 
END   

CLOSE cursor_comprobante  
DEALLOCATE cursor_comprobante

END
