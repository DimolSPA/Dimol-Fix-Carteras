CREATE Procedure [dbo].[_Insertar_Rol_Estados](@codemp integer, @rolid integer, @estid smallint, @esjid integer, 
												@usrid integer, @ipred varchar (30), @ipmaquina varchar (30), @comentario text, @fecjud datetime,
												@codsuc integer, @gesid integer) as  

declare 
@pclid integer, 
@ctcid integer, 
@ccbid integer, 
@monto decimal,
@saldo decimal,
@prejud varchar (1),
@date datetime = getdate()


--  set nocount on;
    declare @trancount int;
    set @trancount = @@trancount;
    begin try
        if @trancount = 0
            begin transaction
        else
            save transaction _Insertar_Rol_Estados;

         INSERT INTO rol_estados  
         ( rle_codemp,   
           rle_rolid,   
           rle_estid,   
           rle_esjid,   
           rle_fecha,   
           rle_usrid,   
           rle_ipred,   
           rle_ipmaquina,   
           rle_comentario,   
           rle_fecjud )  
  VALUES ( @codemp,   
           @rolid,   
           @estid,   
           @esjid,   
           @date,   
           @usrid,   
           @ipred,   
           @ipmaquina,   
           @comentario,   
           dateadd(MILLISECOND, cast(FORMAT(GETDATE(),'fff') as int),
           dateadd(SECOND, cast(FORMAT(GETDATE(),'ss') as int),
		   dateadd(minute, cast(FORMAT(GETDATE(),'mm') as int),
		   dateadd(hour,cast(FORMAT(GETDATE(),'HH') as int), @fecjud)))))  

  SELECT max(rle_fecjud) as fecjud
     into #Fecha
    FROM rol_estados  
    where rle_codemp = @codemp and rle_rolid = @rolid


select rol_estados.*
into #RolEst
from rol_estados, #Fecha
where rle_codemp = @codemp and
      rle_rolid = @rolid and
      rle_fecjud =  fecjud 



  UPDATE rol  
     SET rol_estid = rle_estid,   
         rol_esjid = rle_esjid,   
         rol_fecjud = rle_fecjud,   
         rol_fecultgest = @date  
from rol, #RolEst
   WHERE  rol.rol_codemp = @codemp  AND  
          rol.rol_rolid =  @rolid  and
          rol_codemp = rle_codemp and
          rol_rolid = rle_rolid




declare cur cursor for 
SELECT rol_documentos.rdc_pclid,   
rol_documentos.rdc_ctcid,   
rol_documentos.rdc_ccbid,   
cartera_clientes_cpbt_doc.ccb_monto,   
cartera_clientes_cpbt_doc.ccb_saldo
FROM rol_documentos,   
cartera_clientes_cpbt_doc
WHERE  cartera_clientes_cpbt_doc.ccb_codemp = rol_documentos.rdc_codemp  and  
cartera_clientes_cpbt_doc.ccb_pclid = rol_documentos.rdc_pclid  and  
cartera_clientes_cpbt_doc.ccb_ctcid = rol_documentos.rdc_ctcid  and  
cartera_clientes_cpbt_doc.ccb_ccbid = rol_documentos.rdc_ccbid  and  
rol_documentos.rdc_codemp =  @codemp
and rol_documentos.rdc_rolid =  @rolid
and cartera_clientes_cpbt_doc.ccb_estcpbt = 'J' 
open cur

fetch next from cur into  @pclid, @ctcid , @ccbid , @monto , @saldo
while (@@FETCH_STATUS = 0)
begin

		--select  @pclid, @ctcid , @ccbid , @monto , @saldo
		-----------------Grabo el Historial-----------------------------
		Exec Insertar_Cartera_Clientes_Estados_Historial_Especial @codemp,@pclid,@ctcid,@ccbid,@date,@estid,@codsuc,null,@ipred,@ipmaquina,@comentario,@monto,@saldo,@usrid

		-----------------Cambio el Estado--------------------------

        ----------Busco que el estado actual no sea un estado misto, sino lo es se cambia el estado---

		Select @prejud = estados_cartera.ect_prejud
		FROM cartera_clientes_cpbt_doc, estados_cartera
		WHERE  estados_cartera.ect_codemp = cartera_clientes_cpbt_doc.ccb_codemp  and  
		estados_cartera.ect_estid = cartera_clientes_cpbt_doc.ccb_estid  and  
		cartera_clientes_cpbt_doc.ccb_codemp = @codemp
		and cartera_clientes_cpbt_doc.ccb_pclid =  @pclid
		and cartera_clientes_cpbt_doc.ccb_ctcid =  @ctcid
		and cartera_clientes_cpbt_doc.ccb_ccbid =  @ccbid
		--select @prejud
		
		--if @prejud != 'A'
		begin
			exec _Update_Cartera_Clientes_Cpbt_Doc_Estados @codemp,@pclid,@ctcid,@ccbid,@estid,'J'
			
		end
		
    fetch next from cur into @pclid, @ctcid , @ccbid , @monto , @saldo
end
close cur
deallocate cur


lbexit:
        if @trancount = 0
            commit;
    end try
    begin catch
        declare @error int, @message varchar(4000), @xstate int;
        select @error = ERROR_NUMBER(), @message = ERROR_MESSAGE(), @xstate = XACT_STATE();
        if @xstate = -1
            rollback;
        if @xstate = 1 and @trancount = 0
            rollback
        if @xstate = 1 and @trancount > 0
            rollback transaction usp_my_procedure_name;

        raiserror ('_Insertar_Rol_Estados: %d: %s', 16, 1, @error, @message) ;
    end catch

--begin try

--set transaction isolation level serializable
--begin transaction

--  INSERT INTO rol_estados  
--         ( rle_codemp,   
--           rle_rolid,   
--           rle_estid,   
--           rle_esjid,   
--           rle_fecha,   
--           rle_usrid,   
--           rle_ipred,   
--           rle_ipmaquina,   
--           rle_comentario,   
--           rle_fecjud )  
--  VALUES ( @codemp,   
--           @rolid,   
--           @estid,   
--           @esjid,   
--           @date,   
--           @usrid,   
--           @ipred,   
--           @ipmaquina,   
--           @comentario,   
--           @fecjud)  


--  SELECT max(rle_fecjud) as fecjud
--     into #Fecha
--    FROM rol_estados  
--    where rle_codemp = @codemp and rle_rolid = @rolid


--select rol_estados.*
--into #RolEst
--from rol_estados, #Fecha
--where rle_codemp = @codemp and
--      rle_rolid = @rolid and
--      rle_fecjud =  fecjud 



--  UPDATE rol  
--     SET rol_estid = rle_estid,   
--         rol_esjid = rle_esjid,   
--         rol_fecjud = rle_fecjud,   
--         rol_fecultgest = @date  
--from rol, #RolEst
--   WHERE  rol.rol_codemp = @codemp  AND  
--          rol.rol_rolid =  @rolid  and
--          rol_codemp = rle_codemp and
--          rol_rolid = rle_rolid




--declare cur cursor for 
--SELECT rol_documentos.rdc_pclid,   
--rol_documentos.rdc_ctcid,   
--rol_documentos.rdc_ccbid,   
--cartera_clientes_cpbt_doc.ccb_monto,   
--cartera_clientes_cpbt_doc.ccb_saldo
--FROM rol_documentos,   
--cartera_clientes_cpbt_doc
--WHERE  cartera_clientes_cpbt_doc.ccb_codemp = rol_documentos.rdc_codemp  and  
--cartera_clientes_cpbt_doc.ccb_pclid = rol_documentos.rdc_pclid  and  
--cartera_clientes_cpbt_doc.ccb_ctcid = rol_documentos.rdc_ctcid  and  
--cartera_clientes_cpbt_doc.ccb_ccbid = rol_documentos.rdc_ccbid  and  
--rol_documentos.rdc_codemp =  @codemp
--and rol_documentos.rdc_rolid =  @rolid
--and cartera_clientes_cpbt_doc.ccb_estcpbt = 'J' 
--open cur

--fetch next from cur into  @pclid, @ctcid , @ccbid , @monto , @saldo
--while (@@FETCH_STATUS = 0)
--begin

--		--select  @pclid, @ctcid , @ccbid , @monto , @saldo
--		-----------------Grabo el Historial-----------------------------
--		Exec Insertar_Cartera_Clientes_Estados_Historial_Especial @codemp,@pclid,@ctcid,@ccbid,@date,@estid,@codsuc,null,@ipred,@ipmaquina,@comentario,@monto,@saldo,@usrid

--		-----------------Cambio el Estado--------------------------

--        ----------Busco que el estado actual no sea un estado misto, sino lo es se cambia el estado---

--		Select @prejud = estados_cartera.ect_prejud
--		FROM cartera_clientes_cpbt_doc, estados_cartera
--		WHERE  estados_cartera.ect_codemp = cartera_clientes_cpbt_doc.ccb_codemp  and  
--		estados_cartera.ect_estid = cartera_clientes_cpbt_doc.ccb_estid  and  
--		cartera_clientes_cpbt_doc.ccb_codemp = @codemp
--		and cartera_clientes_cpbt_doc.ccb_pclid =  @pclid
--		and cartera_clientes_cpbt_doc.ccb_ctcid =  @ctcid
--		and cartera_clientes_cpbt_doc.ccb_ccbid =  @ccbid
--		--select @prejud
		
--		if @prejud != 'A'
--		begin
--			exec Update_Cartera_Clientes_Cpbt_Doc_Estados @codemp,@pclid,@ctcid,@ccbid,@estid,'J'
			
--		end
		
--    fetch next from cur into @pclid, @ctcid , @ccbid , @monto , @saldo
--end
--close cur
--deallocate cur

--commit

--end try
--begin catch
--INSERT INTO [LOG_ERROR]
--           ([FECHA]
--           ,[EXCEPTION_MESSAGE]
--           ,[STACKTRACE]
--           ,[PAGINA]
--           ,[USER_ID])
--     VALUES
--           (GETDATE()
--           ,'ERROR INSERTAR ROL'
--           ,'FECHA:' + CONVERT(varchar, @fecjud)
--           ,'_Insertar_Rol_Estados'
--           ,0)



--end catch