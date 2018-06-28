-- =============================================
-- Author:		FM
-- Create date: 09-10-2014
-- Description:	Proceso aceptar comprobantes
-- =============================================
CREATE PROCEDURE [dbo].[_Aceptar_Comprobante_bajar_cpbt] (@codemp int , @codsuc int, @idioma int, @tipo_documento int, @numero int, @crea_aplicacion varchar(1), @numapl int, @usuario int, @ip_red varchar(20), @ip_pc varchar(20))
AS
BEGIN
	SET NOCOUNT ON;


declare @item int = 1

declare @pclid int 
declare @ctcid int
declare @ccbid int
declare @saldo decimal(15,2)


declare @indice int =0
declare @estId int =0
declare @rolid int =0
declare @fecha_rol datetime
declare @matjud int
declare @rol_anterior int = 0


declare @saldo_original decimal(15,2)
declare @tippad int
declare @numpad int
declare @SalCap decimal(15,2)
declare @SalGJu decimal(15,2) = 0
declare @SalGPr decimal(15,2)= 0
declare @SalInt decimal(15,2)= 0
declare @SalHon decimal(15,2)= 0
declare @gesid int = 0
declare @vendid  int =0
declare @estCpbt varchar(2)
declare @anio int = Year(getdate())
declare @mes int = Month(getdate())
declare @fecha datetime = getdate()


select @saldo = cbc_saldo, @pclid =   cbc_pclid, @saldo_original = cbc_final/cbc_tipcambio  
from cabacera_comprobantes 
where cbc_codemp= @codemp and cbc_sucid=@codsuc
and cbc_tpcid=@tipo_documento and cbc_numero=@numero

select @saldo , @pclid , @saldo_original
begin try

SELECT top 1 @tippad = dcc_tpcidpad,   
@numpad = dcc_numeropad
FROM detalle_comprobantes
WHERE  detalle_comprobantes.dcc_codemp = @codemp
and detalle_comprobantes.dcc_sucid = @codsuc
and detalle_comprobantes.dcc_tpcid = @tipo_documento
and detalle_comprobantes.dcc_numero = @numero

end try
begin catch
	RAISERROR( 'This message will show up right away...',0,1) WITH NOWAIT
end catch

     

            If @crea_aplicacion = 'S' begin

                select @numapl = IsNull(Max(apl_numapl)+1, 1)
				from aplicaciones
				where apl_codemp = @codemp and
						   apl_anio = Year( getdate())
				--select @numapl

                -------------Creo la aplicacion------------
                exec Insertar_Aplicaciones @codemp,@codsuc,@anio, @mes,@numapl,3,-1,@usuario
				--select 'Insertar_Aplicaciones', @codemp,@codsuc, year(getdate()), month(getdate()),@numapl,3,-1,@usuario
				 if @@ROWCOUNT =0 and @@ERROR <> 0 begin
				 return
				end

                ------------------Grabo el saldo del comprobante---------------------
                select @SalCap = cbc_saldo 
                from cabacera_comprobantes 
                where cbc_codemp=@codemp 
                and cbc_sucid=@codsuc
                and cbc_tpcid=@tippad 
                and cbc_numero=@numpad


                set @SalCap = @SalCap - @saldo_original

                If @SalCap = 0 begin
                    set @estCpbt = 'F'
                end Else
                    set @estCpbt = 'B'
                

                -------------Busco el Vendedor--------------@gesid = vdc_vdeid
                --select @gesid = vde_vdeid from vendedores 
                --where vde_codemp = @codemp
                --and vde_sucid =@codsuc
                --and vde_pclid =@pclid


                --If ds.Tables.Count > 0 begin
                --    If ds.Tables(0).Rows.Count > 0 Then
                --        gesid = ds.Tables(0).Rows(0)(0)
                --    Else
                --        @gesid = 0
                --    End If
                --    end
                --Else
                --    @gesid = 0
                


                -----------Busco el Vendedor--------------
                select @vendid = vdc_vdeid from vendedores_cartera 
                where vdc_codemp = @codemp
                and vdc_sucid =@codsuc
                and vdc_pclid =@pclid

                --If ds.Tables.Count > 0 Then
                --    If ds.Tables(0).Rows.Count > 0 Then
                --        vendid = ds.Tables(0).Rows(0)(0)
                --    Else
                --        vendid = 0
                --    End If
                --Else
                --    vendid = 0
                --End If


                -----------------------Hago el Update de los saldos--------------------------------
                exec Update_Cabecera_Comprobantes_Estado @codemp,@codsuc,@tippad,@estCpbt,@SalCap
                --select 'Update_Cabecera_Comprobantes_Estado', @codemp,@codsuc,@tippad,@estCpbt,@SalCap
                --spA3.AgregarParametro("cbc_codemp", codemp)
                --spA3.AgregarParametro("cbc_sucid", codsuc)
                --spA3.AgregarParametro("cbc_tpcid", tipPad)
                --spA3.AgregarParametro("cbc_numero", numpad)
                --spA3.AgregarParametro("cbt_estado", estCpbt)
                --spA3.AgregarParametro("cbc_saldo", SalCap)

                --err = spA3.EjecutarProcedimiento(conn, mytrans)

				if @@ROWCOUNT =0 and @@ERROR <> 0 begin
				 return
				end

                exec Insertar_Cabecera_Comprobantes_OP @codemp, @codsuc,@tipo_documento,@numero
				--select 'Insertar_Cabecera_Comprobantes_OP', @codemp, @codsuc,@tipo_documento,@numero
				if @@ROWCOUNT =0 and @@ERROR <> 0 begin
				 return
				end
				
                exec Update_Cabecera_Comprobantes_Estados @codemp,@codsuc,@tipo_documento,@numero,@estCpbt,@fecha,@usuario,@ip_red,@ip_pc,''
				--select 'Update_Cabecera_Comprobantes_Estados', @codemp,@codsuc,@tipo_documento,@numero,@estCpbt,getdate(),@usuario,@ip_red,@ip_pc,''
				if @@ROWCOUNT =0 and @@ERROR <> 0 begin
				 return
				end
                --item = item + 1

          


                    -------------Inserto el Detalle de la Aplicacion-------------------
                    exec Insertar_Aplicaciones_Items @codemp,@codsuc,@numapl,@anio,@item,null,null,null,null,@tipo_documento,@numero,@tippad,@numpad, null,null,null, @saldo_original,0,0,0,0,null,@vendid
                    --select 'Insertar_Aplicaciones_Items', @codemp,@codsuc,@numapl,year(getdate()),@item,null,null,null,null,@tipo_documento,@numero,@tippad,@numpad, null,null,null, @saldo_original,0,0,0,0,null,@vendid


            End
END
