create Procedure [dbo].[_Comprobante_Cuadrar_Detalle](@codemp integer, @sucid integer, @tpcid integer, @numero numeric(15), @item integer, @tipo_cambio decimal(15,2),
												@precio decimal(15,2) , @cantidad int) as


declare @neto decimal(15,2), @total_desc decimal(15,2), @impuesto decimal(15,2), @total decimal(15,2), @total_saldo decimal(15,2), 
		@total_retenido decimal(15,2), @total_exento decimal(15,2), @redondear varchar(1) , @cantidad_rol int, @item_cursor int, @indice int = 1

-------------------Hago el Update en los totales de la cabecera---------------------------
		select  @neto = sum(dcc_neto)* @tipo_cambio, 
		@total_desc = isnull(sum(dcc_prereal * dcc_cantidad) - sum(dcc_precio * dcc_cantidad),0)* @tipo_cambio , 
		@impuesto=isnull(sum(dcc_impuesto),0)* @tipo_cambio, 
		@total=isnull(sum(dcc_total),0)  * @tipo_cambio,
		@total_saldo =isnull(sum(dcc_total),0) 
		from detalle_comprobantes
		where dcc_codemp= @codemp
		and dcc_sucid=@sucid
		and dcc_tpcid=@tpcid
		and dcc_numero=@numero

        --Select @neto, @total_desc, @impuesto, @total, @total_saldo 


		select @total_retenido = isnull(sum(dcc_impuesto),0)   * @tipo_cambio
		from detalle_comprobantes 
		where dcc_codemp=  @codemp
		and dcc_sucid=@sucid
		and dcc_tpcid=@tpcid
		and dcc_numero=@numero
		and dcc_retenido='S'
		
		--select @total_retenido

--------------Reviso si es un valor exento----------------------------
		select @total_exento = isnull(sum(dcc_exento)  ,0) * @tipo_cambio
		from detalle_comprobantes 
		where dcc_codemp=  @codemp
		and dcc_sucid=@sucid
		and dcc_tpcid=@tpcid
		and dcc_numero=@numero

		--select @total_exento
		
		select @redondear = EMC_VALTXT from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 46

        if @redondear = 'S' 
        begin
			set @neto = ROUND(@neto,0)
			set  @total_desc = round(@total_desc,0)
			set   @impuesto= round(@impuesto,0)
			set  @total = round(@total,0)
			set   @total_saldo= round(@total_saldo,0)
			set  @total_retenido = round(@total_retenido,0)
			set   @total_exento= round(@total_exento,0)
			
        end        
        
        set @total_saldo = @total
        
		UPDATE cabacera_comprobantes  
		SET cbc_neto = @neto,   
		cbc_impuestos = @impuesto,   
		cbc_retenido = @total_retenido,   
		cbc_descuentos = @total_desc,   
		cbc_final = @total,   
		cbc_saldo = @total_saldo,
		cbc_exento  = @total_exento
		WHERE ( cabacera_comprobantes.cbc_codemp = @codemp ) AND  
		( cabacera_comprobantes.cbc_sucid = @sucid ) AND  
		( cabacera_comprobantes.cbc_tpcid = @tpcid ) AND  
		( cabacera_comprobantes.cbc_numero = @numero )

-----------------Busco la Cantidad de rol que el item debera dividir----------------
		select @cantidad_rol =  count(dcr_codemp) 
		from detalle_comprobantes_rol
		where dcr_codemp =@codemp
		and dcr_sucid =@sucid
		and dcr_tpcid =@tpcid
		and dcr_numero =@numero
		and dcr_item = @item
		
		if @cantidad_rol > 0
		begin
			set @total = (@precio * @cantidad) / @cantidad_rol
			UPDATE detalle_comprobantes_rol  
			SET  dcr_monto = @total  
			WHERE ( detalle_comprobantes_rol.dcr_codemp = @codemp ) AND  
			( detalle_comprobantes_rol.dcr_sucid = @sucid ) AND  
			( detalle_comprobantes_rol.dcr_tpcid = @tpcid ) AND  
			( detalle_comprobantes_rol.dcr_numero = @numero ) AND  
			( detalle_comprobantes_rol.dcr_item = @item )
		
		end

-----------------Busco la Cantidad del ProvCli que el item debera dividir----------------
		select @cantidad_rol = count(dbp_codemp) from detalle_comprobantes_provcli
		where dbp_codemp = @codemp
		and dbp_sucid =@sucid
		and dbp_tpcid =@tpcid
		and dbp_numero =@numero
		and dbp_item = @item
		
		if @cantidad_rol >0
		begin
			set @total = (@precio * @cantidad) / @cantidad_rol
			UPDATE detalle_comprobantes_provcli  
			SET dbp_monto = @total  
			WHERE ( detalle_comprobantes_provcli.dbp_codemp = @codemp ) AND  
			( detalle_comprobantes_provcli.dbp_sucid = @sucid ) AND  
			( detalle_comprobantes_provcli.dbp_tpcid = @tpcid ) AND  
			( detalle_comprobantes_provcli.dbp_numero = @numero ) AND  
			( detalle_comprobantes_provcli.dbp_item = @item )
		end

		EXEC Insertar_Cabecera_Comprobantes_OP @codemp,@sucid,@tpcid,@numero;

---------------Cambio el item de los detalles-----------------
		set @indice = 1
		declare cur cursor for 
					select dcc_item from detalle_comprobantes 
					where dcc_codemp=@codemp
					and dcc_sucid=@sucid
					and dcc_tpcid=@tpcid
					and dcc_numero=@numero
		open cur
		  
		fetch next from cur into  @item_cursor
		while (@@FETCH_STATUS = 0)
		begin
			
			exec Update_Detalle_Comprobantes_Item @codemp, @sucid, @tpcid, @numero, @item_cursor, @indice
			set @indice = @indice + 1
			fetch next from cur into @item_cursor
		end
		close cur
		deallocate cur
