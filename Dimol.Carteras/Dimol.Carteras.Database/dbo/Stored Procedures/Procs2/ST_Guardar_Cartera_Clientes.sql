CREATE Procedure [dbo].[ST_Guardar_Cartera_Clientes](@codemp integer, @pclid numeric (15), @ID_CARGA numeric (15), @CODIGO_CARGA integer,
                                                     @tipcart smallint,@ipred varchar (40),@ipmaquina varchar (40), @estcpbt char (1),
                                                     @cONTRATO integer, @SUCID integer, @USRID integer, @GESid integer) as 
                       
--parametros entrada
--declare @id_carga int = 240
--declare @codemp int = 1
--declare @sucid int = 1
--declare @pclid int = 522
--declare @CODIGO_CARGA int = 2
--declare @tipcart int = 1
--declare @ipred  varchar(30) = '10.0.1.141'
--declare @ipmaquina varchar(30)= '10.0.1.141'
--declare @usrid int  = 309
--declare @gesid int  = 22
--declare @estcpbt char(1)  = 'V'
--declare @cONTRATO integer = 1

declare @ctcid int=0
declare @ccbid int =0

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
declare @glosa varchar(255)

select top 1 @ID_CARGA = ID_CARGA from SITREL_CARGA  with (nolock) where CODEMP = @codemp and ESTADO = 'PR' and PCLID = @pclid order by FECHA_CARGA desc

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

-- DEUDORES    
set @ctcid = (  SELECT IsNull(Max(ctc_ctcid), 1)     FROM deudores  with (nolock)   WHERE ( deudores.ctc_codemp = @codemp ))

begin try

	INSERT INTO deudores  	       
	select @codemp
	, @ctcid + ROW_NUMBER()  OVER(ORDER BY [RUT] DESC) AS Row
	,[RUT] + [DIGITO_VERIFICADOR]
	,[RUT]
	,[DIGITO_VERIFICADOR]
	,[NOMBRES]
	,[APELLIDO_PATERNO]
	,[APELLIDO_MATERNO]
	,[NOMBRE]
	,isnull((select top 1 (SELECT TOP 1 C.COM_COMID comid FROM SITREL_COMUNA ST  with (nolock) , COMUNA C   with (nolock) 
											WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE)
											AND ST.CODIGO = convert(int,zona_geografica)
											AND ST.PCLID = @PCLID
											and st.CODEMP = @CODEMP)  from sitrel_direccion sd where sd.id_carga=@id_carga 
											and sd.tipo_direccion = 'PARTICULAR' and sd.RUT = s.rut),112)
	,isnull((select top 1 direccion from sitrel_direccion sd with (nolock) where sd.id_carga=@id_carga and sd.tipo_direccion = 'PARTICULAR' and sd.RUT = s.RUT),'SIN DIRECCION')
	,'P'       
	,getdate()
	,null
	,1
	,'N'
	,'N'
	from sitrel_deudor s with (nolock)-- left join  deudores d with (nolock) on  s.rut = d.ctc_numero and s.CODEMP = @codemp
	where s.id_carga=@id_carga AND S.PCLID = @PCLID
	--and d.ctc_ctcid is null
	--AND D.CTC_CODEMP = @codemp
	and RUT not in (select CTC_NUMERO from DEUDORES)

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

INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: DEUDORES NUEVOS TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)

--DIRECCION
begin try
	INSERT INTO [DEUDORES_DIRECCION_SITREL]
	select distinct	@codemp,
	(select ctc_ctcid from DEUDORES with (nolock) where CTC_CODEMP = @codemp and CTC_NUMERO = s.rut),
	LTRIM(RTRIM(s.direccion )), 
	isnull((SELECT TOP 1 C.COM_COMID comid FROM SITREL_COMUNA ST with (nolock), COMUNA C with (nolock) WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE) AND ST.CODIGO = convert(int,s.zona_geografica)	AND ST.PCLID = @PCLID	and st.CODEMP = @CODEMP),112)
	, tipo_direccion ,
	getdate(),
	'C',
	'N'
	from sitrel_direccion s with (nolock) left join deudores_direccion_sitrel d with (nolock) on LTRIM(RTRIM(d.direccion)) = LTRIM(RTRIM(s.direccion )) 
	AND D.CTCID = (select ctc_ctcid from DEUDORES with (nolock)where CTC_CODEMP = @codemp and CTC_NUMERO = s.rut)
	where s.id_carga = @id_carga 
	AND D.CODEMP = @codemp
	AND D.CTCID IS NULL
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
	,'INSERTA DIRECCION DEUDOR'
	,@usrid)
end catch

INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: DIRECCION DEUDORES TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)


--telefono
begin try
	INSERT INTO [DEUDORES_TELEFONOS_SITREL]
	SELECT  @codemp,
	(select ctc_ctcid from DEUDORES with (nolock) where CTC_CODEMP = @codemp and CTC_NUMERO = s.rut),
	s.numero,
	s.tipo_telefono,
	s.anexo,
	s.codigo_area,
	getdate(),
	'C',
	'N'
	FROM sitrel_telefono s  with (nolock) left join [DEUDORES_TELEFONOS_SITREL] d with (nolock) on CONVERT(INT,d.numero) = CONVERT(INT,s.numero) 
	AND D.CTCID = (select ctc_ctcid from DEUDORES with (nolock) where CTC_CODEMP = @codemp and CTC_NUMERO = s.rut)
	where s.id_carga = @id_carga 
	and s.PCLID = @pclid 
	and d.CTCID is null   
	AND D.CODEMP = @codemp
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


INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: TELEFONO DEUDORES TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)
	
	----email
begin try	
	INSERT INTO [DEUDORES_MAIL_SITREL]
	select @CODEMP,
	(select ctc_ctcid from DEUDORES with (nolock) where CTC_CODEMP = @codemp and CTC_NUMERO = s.rut),
	s.email,
	getdate()
	,'C'
	,'N'
	from sitrel_email s  with (nolock) left join deudores_mail_sitrel d with (nolock) on LTRIM(RTRIM(d.mail)) = LTRIM(RTRIM(s.email)) 
	AND D.CTCID = (select ctc_ctcid from DEUDORES with (nolock) where CTC_CODEMP = @codemp and CTC_NUMERO = s.rut)
	where s.id_carga = @id_carga 
	and s.PCLID = @pclid
	and d.CTCID is null

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
	
INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: EMAIL DEUDORES TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)

---- CUOTAS NUEVAS

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
from SITREL_CUOTA sc with (nolock) left join CARTERA_CLIENTES_CPBT_DOC cc with (nolock)
on cc.CCB_CODEMP = sc.CODEMP and cc.CCB_NUMERO = sc.NUMERO_OPERACION + '/'+ REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0') and cc.CCB_PCLID = sc.PCLID 
where sc.ID_CARGA = @id_carga
AND SC.PCLID = @PCLID
and cc.CCB_CCBID is null
open cur_cuota

fetch next from cur_cuota into  @rut ,@numero_operacion,@codigo_producto,@numero_cuota,@fecha_vencimiento,@monto_detalle, @capital, @intereses, @gastos, @dias_mora 
while (@@FETCH_STATUS = 0)
begin

	set @ctcid = (  SELECT ctc_ctcid  FROM deudores with (nolock)  WHERE ctc_codemp = @codemp and ctc_numero = @rut )
	set @existe = 0
	select @existe = count(ctc_pclid) from cartera_clientes with (nolock)
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

		set @ccbid = (select IsNull(Max(ccb_ccbid)+1, 1) from cartera_clientes_cpbt_doc with (nolock) where ccb_codemp = @codemp and ccb_pclid = @pclid and ccb_ctcid = @ctcid)      
	    select @tpcid=TPC_TPCID from TIPOS_CPBTDOC with (nolock) where TPC_CODIGO = @codigo_producto 
	    select @glosa = GLOSA, @sucursal = NOMBRE_SUCURSAL from SITREL_OPERACION with (nolock) where ID_CARGA = @id_carga and RUT = @rut and NUMERO_OPERACION = @numero_operacion    
	    set @fecha = CONVERT(date,@fecha_vencimiento,120)
	    
	    if @fecha > GETDATE()
	    begin
			set @estid = (SELECT [EMC_VALNUM]  FROM [EMPRESA_CONFIGURACION] with (nolock) where EMC_EMCID = 100 and EMC_CODEMP = @codemp)
	    end
	    else
	    begin
			set @estid = (SELECT [EMC_VALNUM]  FROM [EMPRESA_CONFIGURACION] with (nolock) where EMC_EMCID = 99 and EMC_CODEMP = @codemp)
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

INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: CUOTA NUEVA TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)

-- CUOTAS ACTUALIZAR VIGENTES

UPDATE
    cc
SET
	ccb_asignado = monto_detalle,
	ccb_monto= capital,
	ccb_saldo = capital,
	ccb_gastotro = GASTOS, 
	ccb_intereses = INTERESES,
	ccb_fecultgest = GETDATE(),
	ccb_comentario = (select GLOSA from SITREL_OPERACION where ID_CARGA = sc.id_carga and sc.RUT = rut and NUMERO_OPERACION = sc.numero_operacion),
	ccb_estcpbt = @estcpbt,
	ccb_codid = @CODIGO_CARGA,
	ccb_fecvenc = CONVERT(DATE,convert(varchar,fecha_vencimiento),120)
FROM
    CARTERA_CLIENTES_CPBT_DOC AS cc with (nolock)
    INNER JOIN SITREL_CUOTA AS sc with (nolock)
    ON cc.CCB_CODEMP = sc.CODEMP and cc.CCB_NUMERO = sc.NUMERO_OPERACION + '/'+REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0')
WHERE
    sc.ID_CARGA = @id_carga
	AND SC.PCLID = @PCLID
	and cc.CCB_CCBID is NOT null 
	AND CC.CCB_ESTCPBT = 'V'

INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: ACTUALIZAR CUOTA VIGENTE TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)
	       
-- CUOTAS ACTUALIZAR FINALIZADAS    
UPDATE
    cc
SET
	ccb_asignado = monto_detalle,
	ccb_monto= capital,
	ccb_saldo = capital,
	ccb_gastotro = GASTOS, 
	ccb_intereses = INTERESES,
	ccb_fecultgest = GETDATE(),
	ccb_comentario = (select GLOSA from SITREL_OPERACION with (nolock) where ID_CARGA = sc.id_carga and sc.RUT = rut and NUMERO_OPERACION = sc.numero_operacion),
	ccb_estcpbt = @estcpbt,
	CCB_ESTID = 1,
	ccb_codid = @CODIGO_CARGA,
	ccb_fecvenc = CONVERT(DATE,convert(varchar,fecha_vencimiento),120)
FROM
    CARTERA_CLIENTES_CPBT_DOC AS cc with (nolock)
    INNER JOIN SITREL_CUOTA AS sc with (nolock)
        ON cc.CCB_CODEMP = sc.CODEMP 
        and cc.CCB_NUMERO = sc.NUMERO_OPERACION + '/'+REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0')
        and cc.CCB_PCLID = sc.PCLID
WHERE
    sc.ID_CARGA = @id_carga
	AND SC.PCLID = @PCLID
	and cc.CCB_CCBID is NOT null 
	AND CC.CCB_ESTCPBT = 'F'
	
INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: ACTUALIZAR CUOTA FINALIZADA TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)


-- CUOTAS REBAJAR
select CC.CCB_CTCID, CC.CCB_CCBID, cc.CCB_NUMERO	
from CARTERA_CLIENTES_CPBT_DOC cc   with (nolock)
WHERE cc.CCB_NUMERO NOT IN 
(SELECT sc.NUMERO_OPERACION + '/'+ REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0') FROM SITREL_CUOTA SC with (nolock) WHERE sc.ID_CARGA = @id_carga
AND SC.PCLID = @PCLID
AND SC.CODEMP =@CODEMP
) 
AND CC.CCB_PCLID = @PCLID
AND CC.CCB_CODEMP = @CODEMP 
AND cc.CCB_ESTCPBT = 'V'

UPDATE
    CARTERA_CLIENTES_CPBT_DOC
SET
	ccb_saldo = 0,
	ccb_gastotro = 0, 
	ccb_intereses = 0,
	ccb_fecultgest = GETDATE(),
	ccb_comentario = 'REBAJADO POR CARGA',
	ccb_estcpbt = 'F',
	CCB_ESTID = 50,
	ccb_codid = @CODIGO_CARGA
WHERE CCB_NUMERO NOT IN (SELECT sc.NUMERO_OPERACION + '/'+ REPLACE(STR(convert(varchar,sc.NUMERO_CUOTA), 3), SPACE(1), '0') 
								FROM SITREL_CUOTA SC with (nolock)
								WHERE sc.ID_CARGA = @id_carga
								AND SC.PCLID = @PCLID
								AND SC.CODEMP =@CODEMP) 
	AND CCB_PCLID = @PCLID
	AND CCB_CODEMP = @CODEMP 
	AND CCB_ESTCPBT = 'V'
	
INSERT INTO [LOG_ERROR]
 ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,'INICIO CARGA ITAU: REBAJAR CUOTA TERMINADO '+ CONVERT(VARCHAR,@ID_CARGA)
           ,''
           ,'CARGA ITAU'
	       ,@usrid)


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