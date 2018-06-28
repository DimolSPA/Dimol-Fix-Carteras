create Procedure [dbo].[ST_Guardar_Cartera_Clientes_original](@codemp integer, @pclid numeric (15), @ID_CARGA numeric (15), @CODIGO_CARGA integer,
                                                     @tipcart smallint,@ipred varchar (40),@ipmaquina varchar (40), @estcpbt char (1),
                                                     @cONTRATO integer, @SUCID integer, @USRID integer, @GESid integer) as 
                       
--parametros entrada
--declare @id_carga int = 19
--declare @codemp int = 1
--declare @pclid int = 522
--declare @CODIGO_CARGA int = 2
--declare @tipcart int = 1
--declare @ipred  varchar(30) = '10.0.1.141'
--declare @ipmaquina varchar(30)= '10.0.1.141'
--declare @usrid int  = 309
--declare @gesid int  = 22

declare @ctcid int=0
declare @ccbid int =0
--declare @sucid int = 1
--declare @estcpbt varchar(1) = 'V'
--declare @contrato int = 1


declare @rut varchar(9)
declare @dv varchar(1)
declare @tipo_persona varchar(20)
declare @NOMBRES varchar(50)
declare @APELLIDO_PATERNO varchar(50)
declare @APELLIDO_MATERNO varchar(50)
declare @NOMBRE varchar(80)
declare @RAZON_SOCIAL varchar(80)
declare @NOMBRE_FANTASIA varchar(80)
declare @SEXO varchar(9)
declare @SEGMENTO_DEUDOR varchar(20)
declare @CUENTA_CORRIENTE varchar(1)

declare @comid int
declare @direccion varchar(255)

declare @existe int = 0
declare @estid int
declare @accid int = 1 -- telefono

declare @codigo_producto varchar(20)
declare @numero_operacion varchar(24)
declare @numero_cuota varchar(3)
declare @fecha_vencimiento varchar(20)
declare @monto_detalle decimal(15,2)
declare @capital decimal(15,2)
declare @intereses decimal(15,2)
declare @gastos decimal(15,2)
declare @dias_mora int

declare @tipo varchar(20)

declare @NUMERO varchar(20)
declare @ANEXO varchar(10)
declare @CODIGO_AREA varchar(20)

declare @email varchar(255)

DECLARE @tpcid INT
declare @sucursal varchar(50)
declare @fecha datetime

--declare @codigo_empresa varchar(20)
--declare @fecha varchar(15)
--declare @codigo_accion varchar(20)
--declare @codigo_contacto varchar(20)
--declare @codigo_respuesta varchar(20)
declare @glosa varchar(255)
--declare @fecha_compromiso varchar(8)
--declare @monto_compromiso varchar(20)
--declare @monto_gestion varchar(20)
--declare @nombre_contacto varchar(255)
--declare @programacion varchar(15)
--declare @numero_telefono varchar(20)
--declare @campania varchar(20)

select top 1 @ID_CARGA = ID_CARGA from SITREL_CARGA where CODEMP = @codemp and ESTADO = 'PR' and PCLID = @pclid order by FECHA_CARGA desc

INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)

declare cur_deudor cursor for 
select  [RUT]
      ,[DIGITO_VERIFICADOR]
      ,[TIPO_PERSONA]
      ,[NOMBRES]
      ,[APELLIDO_PATERNO]
      ,[APELLIDO_MATERNO]
      ,[NOMBRE]
      ,[RAZON_SOCIAL]
      ,[NOMBRE_FANTASIA]
      ,[SEXO]
      ,[SEGMENTO_DEUDOR]
      ,[CUENTA_CORRIENTE] from sitrel_deudor s with (nolock) left join  deudores d with (nolock) on  s.rut = d.ctc_numero
where s.id_carga=@id_carga AND S.PCLID = @PCLID
and d.ctc_ctcid is null
open cur_deudor

fetch next from cur_deudor into @rut,@dv,@tipo_persona,@NOMBRES,@APELLIDO_PATERNO,@APELLIDO_MATERNO, @NOMBRE,@RAZON_SOCIAL,@NOMBRE_FANTASIA,@SEXO,@SEGMENTO_DEUDOR,@CUENTA_CORRIENTE
while (@@FETCH_STATUS = 0)
begin
	
	
	set @ctcid = (  SELECT IsNull(Max(ctc_ctcid)+1, 1)     FROM deudores   WHERE ( deudores.ctc_codemp = @codemp ))
	
	
	select top 1 @direccion = direccion, @comid = (SELECT TOP 1 C.COM_COMID comid FROM SITREL_COMUNA ST, COMUNA C 
													WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE)
													AND ST.CODIGO = convert(int,zona_geografica)
													AND ST.PCLID = @PCLID
													and st.CODEMP = @CODEMP)  from sitrel_direccion where id_carga=@id_carga and tipo_direccion = 'PARTICULAR'
		BEGIN TRY											
 INSERT INTO deudores  
         ( ctc_codemp,   
           ctc_ctcid,   
           ctc_rut,   
           ctc_numero,   
           ctc_digito,   
           ctc_nombre,   
           ctc_apepat,   
           ctc_apemat,   
           ctc_nomfant,   
           ctc_comid,   
           ctc_direccion,   
           ctc_partemp,   
           ctc_fecing,   
           ctc_socid, 
           ctc_estdir,
           ctc_quiebra,
           ctc_nacext )  
  VALUES ( @codemp,   
           @ctcid,   
           @RUT+@dv,   
           @RUT, 
           @DV,   
           @nombres,   
           @apellido_paterno,   
           @APELLIDO_MATERNO,   
           @nombre,   
           @comid,   
           @direccion,   
           'P',   
           getdate(),  
           null,
           1,
           'N',
           'N' )
           
           
end try
	begin catch
		INSERT INTO [LOG_ERROR]
           ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,convert(varchar(MAX),ERROR_MESSAGE())
           ,convert(varchar(MAX),ERROR_LINE())
           ,'INSERTA DEUDOR'
           ,@usrid)
	end catch
    fetch next from cur_deudor into @rut,@dv,@tipo_persona,@NOMBRES,@APELLIDO_PATERNO,@APELLIDO_MATERNO, @NOMBRE,@RAZON_SOCIAL,@NOMBRE_FANTASIA,@SEXO,@SEGMENTO_DEUDOR,@CUENTA_CORRIENTE

end
close cur_deudor
deallocate cur_deudor

--direccion
--declare cur_direccion cursor for 
--select distinct	s.rut,
--	s.direccion, 
--	(SELECT TOP 1 C.COM_COMID comid FROM SITREL_COMUNA ST, COMUNA C WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE) AND ST.CODIGO = convert(int,s.zona_geografica)	AND ST.PCLID = @PCLID	and st.CODEMP = @CODEMP)
--	, tipo_direccion 
--	from sitrel_direccion s left join deudores_direccion_sitrel d on d.ctcid = @ctcid and LTRIM(RTRIM(d.direccion)) = LTRIM(RTRIM(s.direccion ))
--	where s.id_carga = @id_carga 
--	--and s.rut = @rut
--	and d.ctcid is null
--open cur_direccion

--fetch next from cur_direccion into @rut,@direccion,@comid,@tipo
--while (@@FETCH_STATUS = 0)
--begin
	
--	set @ctcid = (  SELECT ctc_ctcid  FROM deudores   WHERE ctc_codemp = @codemp and ctc_numero = @rut )
	
--	if not exists (select 1 from DEUDORES_DIRECCION_SITREL where CTCID = @ctcid and LTRIM(RTRIM([DIRECCION])) = LTRIM(RTRIM(@DIRECCION)))
--	begin
--	begin try
--	INSERT INTO [DEUDORES_DIRECCION_SITREL]
--           ([CODEMP]
--           ,[CTCID]
--           ,[DIRECCION]
--           ,[COMID]
--           ,[TIPO]
--           ,[FECHA]
--           ,[ORIGEN]
--           ,[ENVIADO])
--     VALUES
--           (@CODEMP
--           ,@CTCID
--           ,@DIRECCION
--           ,@COMID
--           ,@TIPO
--           ,getdate()
--			,'C'
--			,'N')
--	end try
--	begin catch
--	INSERT INTO [LOG_ERROR]
--           ([FECHA]
--           ,[EXCEPTION_MESSAGE]
--           ,[STACKTRACE]
--           ,[PAGINA]
--           ,[USER_ID])
--     VALUES
--           (GETDATE()
--           ,convert(varchar(MAX),ERROR_MESSAGE())
--           ,convert(varchar(MAX),ERROR_LINE())
--           ,'INSERTA DIRECCION DEUDOR'
--           ,@usrid)
--	end catch
--	end

--    fetch next from cur_direccion into @rut,@direccion,@comid,@tipo

--end
--close cur_direccion
--deallocate cur_direccion

--telefonos
declare cur_telefonos cursor for 
SELECT  s.rut,
	s.numero,
	s.tipo_telefono,
	s.anexo,
	s.codigo_area
	FROM sitrel_telefono s left join [DEUDORES_TELEFONOS_SITREL] d on d.ctcid = @ctcid and CONVERT(INT,d.numero) = CONVERT(INT,s.numero)
	where s.id_carga = @id_carga 
	--and s.rut = @rut
	and d.ctcid is null
open cur_telefonos

fetch next from cur_telefonos into @rut,@numero,@tipo,@anexo,@codigo_area
while (@@FETCH_STATUS = 0)
begin
		set @ctcid = (  SELECT ctc_ctcid  FROM deudores   WHERE ctc_codemp = @codemp and ctc_numero = @rut )
	if not exists (select 1 from [DEUDORES_TELEFONOS_SITREL] where CTCID = @ctcid and LTRIM(RTRIM([NUMERO])) = LTRIM(RTRIM(@NUMERO)))
	begin
	begin try
	INSERT INTO [DEUDORES_TELEFONOS_SITREL]
           ([CODEMP]
           ,[CTCID]
           ,[NUMERO]
           ,[TIPO]
           ,[ANEXO]
           ,[CODIGO_AREA]
           ,[FECHA]
           ,[ORIGEN]
           ,[ENVIADO])
     VALUES
           (@CODEMP
           ,@CTCID
           ,@NUMERO
           ,@TIPO
           ,@ANEXO
           ,@CODIGO_AREA
           ,getdate()
			,'C'
			,'N')	
	end try
	begin catch
		INSERT INTO [LOG_ERROR]
           ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,convert(varchar(MAX),ERROR_MESSAGE())
           ,convert(varchar(MAX),ERROR_LINE())
           ,'INSERTA TELEFONO DEUDOR'
           ,@usrid)
	end catch
	end

    fetch next from cur_telefonos into @rut,@numero,@tipo,@anexo,@codigo_area

end
close cur_telefonos
deallocate cur_telefonos

--email
declare cur_email cursor for 
select s.rut,s.email
	from sitrel_email s left join deudores_mail_sitrel d on d.ctcid = @ctcid and LTRIM(RTRIM(d.mail)) = LTRIM(RTRIM(s.email))
	 where s.id_carga = @id_carga 
	--and s.rut = @rut
	and d.ctcid is null
open cur_email

fetch next from cur_email into @rut,@email
while (@@FETCH_STATUS = 0)
begin	
	set @ctcid = (  SELECT ctc_ctcid  FROM deudores   WHERE ctc_codemp = @codemp and ctc_numero = @rut )
	if not exists (select 1 from [DEUDORES_MAIL_SITREL] where CTCID = @ctcid and LTRIM(RTRIM([MAIL])) = LTRIM(RTRIM(@eMAIL)))
	begin
	begin try
	INSERT INTO [DEUDORES_MAIL_SITREL]
           ([CODEMP]
           ,[CTCID]
           ,[MAIL]
           ,[FECHA]
           ,[ORIGEN]
           ,[ENVIADO])
     VALUES
           (@CODEMP
           ,@CTCID
           ,@eMAIL
           ,getdate()
			,'C'
			,'N')
			end try
			begin catch
			INSERT INTO [LOG_ERROR]
				   ([FECHA]
				   ,[EXCEPTION_MESSAGE]
				   ,[STACKTRACE]
				   ,[PAGINA]
				   ,[USER_ID])
			 VALUES
				   (GETDATE()
				   ,convert(varchar(MAX),ERROR_MESSAGE())
				   ,convert(varchar(MAX),ERROR_LINE())
				   ,'INSERTA EMAIL DEUDOR'
				   ,@usrid)
			end catch
	end
    fetch next from cur_email into @rut,@email

end
close cur_email
deallocate cur_email

-- CUOTAS NUEVAS
declare cur_cuota cursor for 
select [RUT]
      ,[NUMERO_OPERACION]
      ,[PRODUCTO]
      ,[NUMERO_CUOTA]
      ,[FECHA_VENCIMIENTO]
      ,[MONTO_DETALLE]
      ,[CAPITAL]
      ,[INTERESES]
      ,[GASTOS]
      ,[DIAS_MORA] 
from SITREL_CUOTA sc left join CARTERA_CLIENTES_CPBT_DOC cc 
on cc.CCB_CODEMP = sc.CODEMP and cc.CCB_NUMERO = sc.NUMERO_OPERACION + '/'+ REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0') 
where sc.ID_CARGA = @id_carga
AND SC.PCLID = @PCLID
and cc.CCB_CCBID is null 
open cur_cuota

fetch next from cur_cuota into  @rut ,@numero_operacion,@codigo_producto,@numero_cuota,@fecha_vencimiento,@monto_detalle, @capital, @intereses, @gastos, @dias_mora 
while (@@FETCH_STATUS = 0)
begin

	set @ctcid = (  SELECT ctc_ctcid  FROM deudores   WHERE ctc_codemp = @codemp and ctc_numero = @rut )
	set @existe = 0
	select @existe = count(ctc_pclid) from cartera_clientes
	where ctc_codemp =@codemp
	and  ctc_pclid= @pclid
	and  ctc_ctcid= @ctcid 

if @existe <= 0 
begin
print(@existe)
  INSERT INTO cartera_clientes  
         ( ctc_codemp,   
           ctc_pclid,   
           ctc_ctcid )  
  VALUES ( @codemp,   
           @pclid,   
           @ctcid )
end		

		set @ccbid = (select IsNull(Max(ccb_ccbid)+1, 1) from cartera_clientes_cpbt_doc where ccb_codemp = @codemp and ccb_pclid = @pclid and ccb_ctcid = @ctcid)      
	    select @tpcid=TPC_TPCID from TIPOS_CPBTDOC where TPC_CODIGO = @codigo_producto 
	    select @glosa = GLOSA, @sucursal = NOMBRE_SUCURSAL from SITREL_OPERACION where ID_CARGA = @id_carga and RUT = @rut and NUMERO_OPERACION = @numero_operacion    
	    set @fecha = CONVERT(date,@fecha_vencimiento,120)
	    
	    if @fecha > GETDATE()
	    begin
			set @estid = (SELECT [EMC_VALNUM]  FROM [EMPRESA_CONFIGURACION] where EMC_EMCID = 100 and EMC_CODEMP = @codemp)
	    end
	    else
	    begin
			set @estid = (SELECT [EMC_VALNUM]  FROM [EMPRESA_CONFIGURACION] where EMC_EMCID = 99 and EMC_CODEMP = @codemp)
	    end
	    
	    
	    
	    
	    BEGIN TRY              
	  INSERT INTO cartera_clientes_cpbt_doc  
			 ( ccb_codemp,   
			   ccb_pclid,   
			   ccb_ctcid,   
			   ccb_ccbid,   
			   ccb_tpcid,   
			   ccb_tipcart,   
			   ccb_numero,   
			   ccb_fecing,   
			   ccb_fecdoc,   
			   ccb_fecvenc,   
			   ccb_fecultgest,   
			   ccb_fecplazo,   
			   ccb_feccalcint,   
			   ccb_feccast,   
			   ccb_estid,   
			   ccb_estcpbt,   
			   ccb_codmon,   
			   ccb_tipcambio,   
			   ccb_asignado,   
			   ccb_monto,   
			   ccb_saldo,   
			   ccb_gastjud,   
			   ccb_gastotro,   
			   ccb_intereses,   
			   ccb_honorarios,   
			   ccb_calchon,   
			   ccb_bcoid,   
			   ccb_rutgir,   
			   ccb_nomgir,   
			   ccb_mtcid,   
			   ccb_comentario,   
			   ccb_retent,   
			   ccb_codid,   
			   ccb_numesp,   
			   ccb_numagrupa,   
			   ccb_carta,   
			   ccb_cobrable,   
			   ccb_cctid,   
			   ccb_sbcid,
			   ccb_docori,
			   ccb_docant )  
	  VALUES ( @codemp,   
			   @pclid,   
			   @ctcid,   
			   @ccbid,   
			   @tpcid,   
			   @tipcart,   
			   @numero_operacion+'/'+REPLACE(STR(@numero_cuota, 3), SPACE(1), '0'),   
			   getdate(),   
			   CONVERT(DATE,@fecha_vencimiento,120),   
			   CONVERT(DATE,@fecha_vencimiento,120),   
			   getdate(),   
			   null,   
			   null,   
			   null,   
			   @estid,   
			   @estcpbt,   
			   1,--@codmon,   
			   1,--@tipcambio,   
			   @monto_detalle,   
			   @capital,   
			   @capital,   
			   0,--@gastjud,   
			   @gastos,   
			   @intereses,   
			   0,--@honorarios,   
			   'N',   
			   NULL,--@ccb_bcoid,   
			   null,--@ccb_rutgir,   
			   null,--@ccb_nomgir,   
			   '26',--@ccb_mtcid,   
			   @glosa,   
			   NULL,--@ccb_retent,   
			   @CODIGO_CARGA,   
			   @sucursal,   
			   null,--@ccb_numagrupa,   
			   0,   
			   'S',   
			   @contrato,   
			   null,--@ccb_sbcid,
			   'N',
			   'S' )
			   
			  -- INSERT INTO [CARTERA_CLIENTES_ESTADOS_HISTORIAL]
     --      ([CEH_CODEMP]
     --      ,[CEH_PCLID]
     --      ,[CEH_CTCID]
     --      ,[CEH_CCBID]
     --      ,[CEH_FECHA]
     --      ,[CEH_ESTID]
     --      ,[CEH_SUCID]
     --      ,[CEH_GESID]
     --      ,[CEH_IPRED]
     --      ,[CEH_IPMAQUINA]
     --      ,[CEH_COMENTARIO]
     --      ,[CEH_MONTO]
     --      ,[CEH_SALDO]
     --      ,[CEH_USRID])
     --VALUES
     --      (@CODEMP
     --      ,@PCLID
     --      ,@CTCID
     --      ,@CCBID
     --      ,getdate()
     --      ,@ESTID
     --      ,@SUCID
     --      ,@GESID
     --      ,@IPRED
     --      ,@IPMAQUINA
     --      ,@glosa
     --      ,@monto_detalle
     --      ,@capital
     --      ,@USRID)		
		
		END TRY
		begin catch
			INSERT INTO [LOG_ERROR]
           ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,convert(varchar(MAX),ERROR_MESSAGE())
           ,convert(varchar(MAX),ERROR_LINE())
           ,'INSERTA DOCUMENTO DEUDOR'
           ,@usrid)
			end catch
			
    fetch next from cur_cuota into @rut ,@numero_operacion,@codigo_producto,@numero_cuota,@fecha_vencimiento,@monto_detalle, @capital, @intereses, @gastos, @dias_mora 

end
close cur_cuota
deallocate cur_cuota

-- CUOTAS ACTUALIZAR
declare cur_cuota cursor for 
select [RUT]
      ,[NUMERO_OPERACION]
      ,[PRODUCTO]
      ,[NUMERO_CUOTA]
      ,[FECHA_VENCIMIENTO]
      ,[MONTO_DETALLE]
      ,[CAPITAL]
      ,[INTERESES]
      ,[GASTOS]
      ,[DIAS_MORA] , CC.CCB_CCBID, cc.CCB_ESTID
from SITREL_CUOTA sc left join CARTERA_CLIENTES_CPBT_DOC cc 
on cc.CCB_CODEMP = sc.CODEMP and cc.CCB_NUMERO = sc.NUMERO_OPERACION + '/'+REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0') 
where sc.ID_CARGA = @id_carga
AND SC.PCLID = @PCLID
and cc.CCB_CCBID is NOT null 
open cur_cuota

fetch next from cur_cuota into  @rut ,@numero_operacion,@codigo_producto,@numero_cuota,@fecha_vencimiento,@monto_detalle, @capital, @intereses, @gastos, @dias_mora, @CCBID, @estid 
while (@@FETCH_STATUS = 0)
begin

	set @ctcid = (  SELECT ctc_ctcid  FROM deudores   WHERE ctc_codemp = @codemp and ctc_numero = @rut )

		--set @ccbid = (select IsNull(Max(ccb_ccbid)+1, 1) from cartera_clientes_cpbt_doc where ccb_codemp = @codemp and ccb_pclid = @pclid and ccb_ctcid = @ctcid)      
	    --select @tpcid=TPC_TPCID from TIPOS_CPBTDOC where TPC_CODIGO = @codigo_producto 
	    select @glosa = GLOSA, @sucursal = NOMBRE_SUCURSAL from SITREL_OPERACION where ID_CARGA = @id_carga and RUT = @rut and NUMERO_OPERACION = @numero_operacion    
	    set @fecha = CONVERT(date,@fecha_vencimiento,120)
	    
	    if @estid = 50 --previamente pagado en cliente
	    begin
			set @estid = isnull((select top 1 CEH_ESTID from CARTERA_CLIENTES_ESTADOS_HISTORIAL where CEH_CODEMP =@codemp and CEH_PCLID =	@pclid	and CEH_CTCID= @ctcid and CEH_CCBID=	@ccbid order by CEH_FECHA desc),229)
	    end
	    
	    BEGIN TRY              
	  UPDATE cartera_clientes_cpbt_doc SET
	   ccb_asignado = @monto_detalle,
	   ccb_monto= @capital,
	   ccb_saldo = @capital,
	   ccb_gastotro = @GASTOS, 
	   ccb_intereses = @INTERESES,
	   ccb_fecultgest = GETDATE(),
	   ccb_comentario = @GLOSA,
	   ccb_estcpbt = @estcpbt,
	   CCB_ESTID = @estid,
	   ccb_codid = @CODIGO_CARGA,
	   ccb_fecvenc = CONVERT(DATE,@fecha_vencimiento,120)  
	   WHERE ccb_codemp = @CODEMP
	   AND ccb_pclid =@PCLID  
	   AND ccb_ctcid = @CTCID
	   AND ccb_ccbid = @CCBID
	 
  --INSERT INTO [CARTERA_CLIENTES_ESTADOS_HISTORIAL]
  --         ([CEH_CODEMP]
  --         ,[CEH_PCLID]
  --         ,[CEH_CTCID]
  --         ,[CEH_CCBID]
  --         ,CEH_FECHA
  --         ,[CEH_ESTID]
  --         ,[CEH_SUCID]
  --         ,[CEH_GESID]
  --         ,[CEH_IPRED]
  --         ,[CEH_IPMAQUINA]
  --         ,[CEH_COMENTARIO]
  --         ,[CEH_MONTO]
  --         ,[CEH_SALDO]
  --         ,[CEH_USRID])
  --   VALUES
  --         (@CODEMP
  --         ,@PCLID
  --         ,@CTCID
  --         ,@CCBID
  --         ,GETDATE()
  --         ,@ESTID
  --         ,@SUCID
  --         ,@GESID
  --         ,@IPRED
  --         ,@IPMAQUINA
  --         ,@glosa
  --         ,@monto_detalle
  --         ,@capital
  --         ,@USRID)	
           
     --      INSERT INTO [LOG_ERROR]
     --      ([FECHA]
     --      ,[EXCEPTION_MESSAGE]
     --      ,[STACKTRACE]
     --      ,[PAGINA]
     --      ,[USER_ID])
     --VALUES
     --      (GETDATE()
     --      ,'ACTUALIZANDO: ' + @numero_operacion+'/'+REPLACE(STR(@numero_cuota, 3), SPACE(1), '0')
     --      ,''
     --      ,'ACTUALIZA DOCUMENTO DEUDOR'
     --      ,@usrid)	
		
		END TRY
		begin catch
			INSERT INTO [LOG_ERROR]
           ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,convert(varchar(MAX),ERROR_MESSAGE())
           ,convert(varchar(MAX),ERROR_LINE())
           ,'REBAJA DOCUMENTO DEUDOR'
           ,@usrid)
			end catch
			
    fetch next from cur_cuota into @rut ,@numero_operacion,@codigo_producto,@numero_cuota,@fecha_vencimiento,@monto_detalle, @capital, @intereses, @gastos, @dias_mora, @CCBID , @estid

end
close cur_cuota
deallocate cur_cuota

-- CUOTAS REBAJAR
declare cur_cuota cursor for 
select CC.CCB_CTCID, CC.CCB_CCBID, cc.CCB_NUMERO	
from CARTERA_CLIENTES_CPBT_DOC cc  
WHERE cc.CCB_NUMERO NOT IN 
(SELECT sc.NUMERO_OPERACION + '/'+ REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0') FROM SITREL_CUOTA SC WHERE sc.ID_CARGA = @id_carga
AND SC.PCLID = @PCLID
AND SC.CODEMP =@CODEMP
) 
AND CC.CCB_PCLID = @PCLID
AND CC.CCB_CODEMP = @CODEMP 
AND cc.CCB_ESTCPBT = 'V'
open cur_cuota

fetch next from cur_cuota into  @CTCID , @CCBID , @numero_operacion
while (@@FETCH_STATUS = 0)
begin

	    BEGIN TRY              
	  UPDATE cartera_clientes_cpbt_doc SET
	   ccb_saldo = 0,
	   ccb_gastotro = 0, 
	   ccb_intereses = 0,
	   ccb_fecultgest = GETDATE(),
	   ccb_comentario = 'REBAJADO POR CARGA',
	   ccb_estcpbt = 'F',
	   CCB_ESTID = 50,
	   ccb_codid = @CODIGO_CARGA
	   WHERE ccb_codemp = @CODEMP
	   AND ccb_pclid =@PCLID  
	   AND ccb_ctcid = @CTCID
	   AND ccb_ccbid = @CCBID
	 
  --INSERT INTO [CARTERA_CLIENTES_ESTADOS_HISTORIAL]
  --         ([CEH_CODEMP]
  --         ,[CEH_PCLID]
  --         ,[CEH_CTCID]
  --         ,[CEH_CCBID]
  --         ,[CEH_FECHA]
  --         ,[CEH_ESTID]
  --         ,[CEH_SUCID]
  --         ,[CEH_GESID]
  --         ,[CEH_IPRED]
  --         ,[CEH_IPMAQUINA]
  --         ,[CEH_COMENTARIO]
  --         ,[CEH_MONTO]
  --         ,[CEH_SALDO]
  --         ,[CEH_USRID])
  --   VALUES
  --         (@CODEMP
  --         ,@PCLID
  --         ,@CTCID
  --         ,@CCBID
  --         ,GETDATE()
  --         ,@ESTID
  --         ,@SUCID
  --         ,@GESID
  --         ,@IPRED
  --         ,@IPMAQUINA
  --         ,@glosa
  --         ,@monto_detalle
  --         ,@capital
  --         ,@USRID)		
  
     --      INSERT INTO [LOG_ERROR]
     --      ([FECHA]
     --      ,[EXCEPTION_MESSAGE]
     --      ,[STACKTRACE]
     --      ,[PAGINA]
     --      ,[USER_ID])
     --VALUES
     --      (GETDATE()
     --      ,'REBAJANDO: ' + @numero_operacion
     --      ,''
     --      ,'ACTUALIZA DOCUMENTO DEUDOR'
     --      ,@usrid)
		
		END TRY
		begin catch
			INSERT INTO [LOG_ERROR]
           ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,convert(varchar(MAX),ERROR_MESSAGE())
           ,convert(varchar(MAX),ERROR_LINE())
           ,'ACTUALIZA DOCUMENTO DEUDOR'
           ,@usrid)
			end catch
			
    fetch next from cur_cuota into @CTCID , @CCBID , @numero_operacion

end
close cur_cuota
deallocate cur_cuota


update SITREL_CARGA set
ESTADO = 'OK'
 where CODEMP = @codemp and ID_CARGA = @ID_CARGA  and PCLID = @pclid


INSERT INTO [LOG_ERROR] ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'FIN CARGA ITAU: '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
           ,@usrid)