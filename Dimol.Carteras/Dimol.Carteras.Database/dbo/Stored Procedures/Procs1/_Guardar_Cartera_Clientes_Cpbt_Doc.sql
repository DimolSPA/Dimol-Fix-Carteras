CREATE Procedure [dbo].[_Guardar_Cartera_Clientes_Cpbt_Doc](@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_ccbid integer,
                                                                                     @ccb_tpcid integer, @ccb_tipcart smallint, @ccb_numero varchar (30),
                                                                                     @ccb_fecdoc datetime, @ccb_fecvenc datetime, @ccb_estid smallint, @ccb_estcpbt char (1),
                                                                                     @ccb_codmon integer, @ccb_tipcambio decimal (10,2), @ccb_asignado decimal (15,2), @ccb_monto decimal (15,2),
                                                                                     @ccb_saldo decimal (15,2), @ccb_gastjud decimal (15,2), @ccb_gastotro decimal (15,2), 
                                                                                      @ccb_bcoid integer, @ccb_rutgir varchar (20),
                                                                                     @ccb_nomgir varchar (200), @ccb_mtcid integer, @ccb_comentario text, @ccb_retent char (1),
                                                                                     @ccb_codid integer, @ccb_numesp varchar (40), @ccb_numagrupa varchar (40),
                                                                                     @ccb_cctid integer, @ccb_sbcid integer, @ccb_docori char(1), @ccb_docant char(1), @terceroId int,
                                                                                     @CCB_IDCUENTA varchar (20),@CCB_DESCCUENTA varchar (200)) as 
                       
declare @existe int =0;
declare @cartera int =0;
declare @ccbid int = @ccb_ccbid;

set @existe = isnull((SELECT CCB_CCBID
  FROM CARTERA_CLIENTES_CPBT_DOC
  where CCB_CODEMP = @ccb_codemp
  and CCB_PCLID = @ccb_pclid
  and CCB_CTCID = @ccb_ctcid
  and CCB_numero= @ccb_numero) ,0)   

set @cartera = (select count(ctc_ctcid)
				from cartera_clientes
				where ctc_codemp = @ccb_codemp and
						   ctc_pclid = @ccb_pclid and
						   ctc_ctcid = @ccb_ctcid)

if @cartera = 0
	begin
		INSERT INTO cartera_clientes  
			 ( ctc_codemp,   
			   ctc_pclid,   
			   ctc_ctcid )  
		VALUES ( @ccb_codemp,   
			   @ccb_pclid,   
			   @ccb_ctcid )
		set @cartera = @@ROWCOUNT
	end

if @cartera >0
	begin        
    if @existe = 0 
		begin
      
		set @ccbid = (select IsNull(Max(ccb_ccbid)+1, 1)
			from cartera_clientes_cpbt_doc
			where ccb_codemp = @ccb_codemp and
						ccb_pclid = @ccb_pclid and
						ccb_ctcid = @ccb_ctcid)      
                       
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
					ccb_docant,
					ccb_estidj,
					terceroid,
					CCB_IDCUENTA,
					CCB_DESCCUENTA )  
			VALUES ( @ccb_codemp,   
					@ccb_pclid,   
					@ccb_ctcid,   
					@ccbid,   
					@ccb_tpcid,   
					@ccb_tipcart,   
					@ccb_numero,   
					getdate(),   
					@ccb_fecdoc,   
					@ccb_fecvenc,   
					getdate(),   
					null,   
					null,   
					null,   
					@ccb_estid,   
					@ccb_estcpbt,   
					@ccb_codmon,   
					@ccb_tipcambio,   
					@ccb_asignado,   
					@ccb_monto,   
					@ccb_saldo,   
					@ccb_gastjud,   
					@ccb_gastotro,   
					0,   
					0,   
					'S',   -- se crean los docs con calculos S por defecto
					@ccb_bcoid,   
					@ccb_rutgir,   
					@ccb_nomgir,   
					@ccb_mtcid,   
					@ccb_comentario,   
					@ccb_retent,   
					@ccb_codid,   
					@ccb_numesp,   
					@ccb_numagrupa,   
					0,   
					'S',   
					@ccb_cctid,   
					@ccb_sbcid,
					@ccb_docori,
					@ccb_docant,
					@ccb_estid,
					@terceroId,
					@CCB_IDCUENTA,
					@CCB_DESCCUENTA )
         
		 --Se calcula los honorarios
		exec dbo._Calculo_Honorarios_Individual @ccb_codemp, @ccb_pclid, @ccb_ctcid, @ccbid
		--Se calcula los intereses
		exec dbo._Calculo_Intereses_Individual @ccb_codemp, @ccb_pclid, @ccb_ctcid, @ccbid
		  
			select @ccbid ccbid
		

		end --end INSERT INTO cartera_clientes_cpbt_doc 
		else
		begin
		UPDATE cartera_clientes_cpbt_doc  
				SET ccb_tpcid = @ccb_tpcid,   
					ccb_tipcart = @ccb_tipcart,   
					ccb_numero = @ccb_numero,   
					ccb_fecdoc = @ccb_fecdoc,   
					ccb_fecvenc = @ccb_fecvenc,   
					ccb_codmon = @ccb_codmon,   
					ccb_tipcambio = @ccb_tipcambio,   
					ccb_asignado = @ccb_asignado,   
					ccb_monto = @ccb_monto,   
					ccb_saldo = @ccb_saldo,   
					ccb_gastjud = @ccb_gastjud,   
					ccb_gastotro = @ccb_gastotro,   
					ccb_bcoid = @ccb_bcoid,   
					ccb_rutgir = @ccb_rutgir,   
					ccb_nomgir = @ccb_nomgir,   
					ccb_mtcid = @ccb_mtcid,   
					ccb_comentario = @ccb_comentario,   
					ccb_codid = @ccb_codid,   
					ccb_numesp = @ccb_numesp,   
					ccb_numagrupa = @ccb_numagrupa,   
					ccb_cctid = @ccb_cctid,   
					ccb_sbcid = @ccb_sbcid,
					ccb_docori = @ccb_docori,
					ccb_docant = @ccb_docant,
					CCB_FECULTGEST = GETDATE(),
					terceroid = @terceroId,
					CCB_IDCUENTA = @CCB_IDCUENTA,
					CCB_DESCCUENTA  = @CCB_DESCCUENTA
			WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
					( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
					( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
					( cartera_clientes_cpbt_doc.ccb_ccbid = @existe )

         --Se calcula los honorarios
		exec dbo._Calculo_Honorarios_Individual @ccb_codemp, @ccb_pclid, @ccb_ctcid, @ccbid
		--Se calcula los intereses
		exec dbo._Calculo_Intereses_Individual @ccb_codemp, @ccb_pclid, @ccb_ctcid, @ccbid 

			select @existe ccbid

		end  --end UPDATE cartera_clientes_cpbt_doc      
	end --end if @cartera >0
 else 
 select -1 ccbid


