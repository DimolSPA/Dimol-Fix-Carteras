
CREATE PROCEDURE [dbo].[_Calculo_Honorarios_Contratos_Vigentes] AS
INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza los honorarios en Contratos Vigentes', 'Comienzo', 'Job 2 AM',0 )

DECLARE
@CCB_CODEMP INT,
@CCB_PCLID INT,
@CCB_CTCID INT,
@CCB_CCBID INT,
@CCB_TPCID INT,
@CCB_TIPCART INT,
@CCB_CODMON INT,
@CCB_FECCALCINT DATETIME, 
@CCB_FECVENC DATETIME, 
@CCB_FECING DATETIME,
@CCB_FECDOC DATETIME,
@CCB_ASIGNADO DECIMAL(15,4), 
@CCB_CALCHON VARCHAR(1),
@CCB_CCTID INT,
@CCB_ESTCPBT VARCHAR(1),

---Variables
@diasVenc INT,
@diasAsign INT,
@redondear INT,
@valorMoneda DECIMAL(15,2),
@ctc_cctidCount INT,
@regId INT,
@valCamb DECIMAL(15,4) = 0,
@valMonedaCodMon DECIMAL(15,2),
@hono DECIMAL(15,4) = 0,
@codmon INT = dbo._Trae_Valor_ValNum(1, 20), -- es 2,
@dCalc INT = dbo._Trae_Valor_ValNum(1, 21),
@IsCheque VARCHAR(1),
@abonosHonorarios DECIMAL(15,2),
---clausulas
@pIntRangoClausula decimal(15,4),
@rol_documentos INT,
@rol_documentosEstado VARCHAR(1),
@clc_clccid int,
@clc_tiprango int,
@clc_valor decimal(15,2),
@clc_rango VARCHAR(1)


DECLARE curMain CURSOR FOR 

SELECT CCB_CODEMP, CCB_PCLID, CCB_CTCID, CCB_CCBID, CCB_TPCID, CCB_TIPCART, CCB_CODMON, CCB_FECCALCINT, CCB_FECVENC, CCB_FECING, 
		CCB_ASIGNADO, CCB_CALCHON,CCB_CCTID, CCB_ESTCPBT,
		datediff(day, CARTERA_CLIENTES_CPBT_DOC.CCB_FECVENC, getdate()) diasVenc,
		datediff(day, CARTERA_CLIENTES_CPBT_DOC.CCB_FECING, CARTERA_CLIENTES_CPBT_DOC.CCB_FECVENC) diasAsign,
		--decimales de la moneda
		(Select mon_decimales 
			From monedas 
			Where mon_codemp = CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP
			and mon_codmon = CARTERA_CLIENTES_CPBT_DOC.CCB_CODMON )redondear,
		--Contrato clientes vigentes
		(Select  ccli.ctc_cctid
			From  contratos_clientes ccli
			JOIN contratos_cartera cca
			On ccli.ctc_codemp = cca.cct_codemp 
			and ccli.ctc_cctid = cca.cct_cctid
			Where cca.cct_codemp = CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP  
			and cca.cct_tipo = CARTERA_CLIENTES_CPBT_DOC.CCB_TIPCART
			and ccli.ctc_cctid = CARTERA_CLIENTES_CPBT_DOC.CCB_CCTID
			and ccli.ctc_pclid = CARTERA_CLIENTES_CPBT_DOC.CCB_PCLID 
			and (ccli.ctc_indefinido = 'S' OR
			   (ccli.ctc_indefinido = 'N' AND
			  ccli.ctc_fecfin >= getdate())) ) ctc_cctidCount,
		(Select ciudad.ciu_regid       
			From  ciudad 
			JOIN comuna 
			ON ciudad.ciu_ciuid = comuna.com_ciuid 
			JOIN deudores 
			ON comuna.com_comid = deudores.ctc_comid
			Where deudores.ctc_codemp = CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP
			and deudores.ctc_ctcid = CARTERA_CLIENTES_CPBT_DOC.CCB_CTCID)regId,
		CASE
			WHEN EXISTS (SELECT 1
							WHERE CARTERA_CLIENTES_CPBT_DOC.CCB_TPCID IN (9, 39, 44)) THEN 'S'
			ELSE 'N'
		END AS IsCheque
FROM CARTERA_CLIENTES_CPBT_DOC   WITH (nolock)    
WHERE CCB_ESTCPBT IN ('V','J')
--and  CCB_CALCHON = 'S' -- calcula honorarios a todos los documentos
and CCB_TIPCART != 1
ORDER BY CCB_FECDOC ASC

OPEN curMain
FETCH NEXT FROM curMain INTO  @CCB_CODEMP, @CCB_PCLID, @CCB_CTCID, @CCB_CCBID,@CCB_TPCID, @CCB_TIPCART, @CCB_CODMON, @CCB_FECCALCINT, @CCB_FECVENC,
@CCB_FECING, @CCB_ASIGNADO, @CCB_CALCHON, @CCB_CCTID, @CCB_ESTCPBT, @diasVenc, @diasAsign, @redondear, @ctc_cctidCount, @regId, @IsCheque

WHILE (@@FETCH_STATUS = 0)
	BEGIN ---Inicio WHILE curMain
	---logica de negocio
	
	----Buscar el Tipo de Cambio
		-- @codmon  -- es 2
		SET @valorMoneda = dbo._Trae_Valor_Moneda(@CCB_CODEMP, @codmon) -- 26401.49

	--Si existen Contrato Vigentes
		if @ctc_cctidCount > 0
		SET @valCamb = 0
		SET @hono = 0
		--Revisar si ha realizado algun abono de Honorarios
		SET @abonosHonorarios = 0
		SET @abonosHonorarios = isnull(dbo._Trae_Honorarios_Aplicaciones(@CCB_CODEMP, @CCB_CTCID, @CCB_PCLID,@CCB_CCBID), 0)			

			
		---Si el documento es Judicial se calcula el 10% sobre el monto asignado menos el abono
		if (@CCB_ESTCPBT = 'J') or (@IsCheque = 'S')
		begin 
			----------Revisar si esta en al gun ROL-----------
			/*PRINT 'Judicial @IsCheque: ' 
									+ Cast(@IsCheque AS VARCHAR) +
					' Judicial @CCB_ESTCPBT: ' 
									+ Cast(@CCB_ESTCPBT AS VARCHAR)*/
			SET @rol_documentos = 0
			Select  @rol_documentos = count(rol_documentos.rdc_codemp)
			from rol_documentos
			where   rol_documentos.rdc_codemp= @CCB_CODEMP
			and rol_documentos.rdc_pclid = @CCB_PCLID
			and rol_documentos.rdc_ctcid = @CCB_CTCID
			and rol_documentos.rdc_ccbid = @CCB_CCBID	

			if @rol_documentos > 0
			begin
				SET @rol_documentosEstado = 'J'
			end
			else
			begin
				SET @rol_documentosEstado = 'P'
			end
			
			--En contratos vigentes se busca las  clausulas los rangos
			SET @pIntRangoClausula = 0
			-- Buscar si las clausulas tienen rangos
			select @clc_rango= clausulas_contcart.CLC_RANGO,
					@clc_clccid = clausulas_contcart.CLC_CLCID, 
					@clc_tiprango = clausulas_contcart.CLC_TIPRANGO, 
					@clc_valor = clausulas_contcart.CLC_VALOR
			from contratos_cartera cca
			JOIN contratos_clientes ccli
			on cca.cct_codemp = ccli.ctc_codemp 
			and cca.cct_cctid = ccli.ctc_cctid 
			JOIN contratos_cartera_clausulas
			on cca.cct_codemp = contratos_cartera_clausulas.ccl_codemp 
			and cca.cct_cctid = contratos_cartera_clausulas.ccl_cctid 
			JOIN clausulas_contcart 
			on contratos_cartera_clausulas.ccl_codemp = clausulas_contcart.clc_codemp 
			and contratos_cartera_clausulas.ccl_clcid = clausulas_contcart.clc_clcid
			where cca.cct_codemp = @CCB_CODEMP --1
			and cca.cct_cctid = @CCB_CCTID --1
			and ccli.CTC_PCLID = @CCB_PCLID --559
			and (ccli.ctc_indefinido = 'S' OR
				(ccli.ctc_indefinido = 'N' AND
				ccli.ctc_fecfin >= getdate()))
			and clausulas_contcart.clc_tipo = 1
			and clausulas_contcart.clc_prejud IN ('A', @rol_documentosEstado)

			IF @clc_rango = 'N'
			BEGIN
				SET @pIntRangoClausula = @clc_valor/100
			END
			IF @clc_rango = 'S'
			BEGIN
				EXEC @pIntRangoClausula = dbo.Get_Valor_Rango @CCB_CODEMP,@clc_clccid, @clc_tiprango,@diasVenc,@diasAsign,@regId,@CCB_ASIGNADO,@CCB_FECVENC
				SET @pIntRangoClausula = @pIntRangoClausula / 100 
			END

			IF 	@pIntRangoClausula != 0.10
			BEGIN 
				SET @pIntRangoClausula = 0.10
			END	
				
			SET @hono = @CCB_ASIGNADO * @pIntRangoClausula
			SET @hono = @hono - @abonosHonorarios
			SET @hono = ROUND(@hono, @redondear)
						
	        -----------------Grabar el Honorario-------------
			/*PRINT 'Judicial @hono: ' 
									+ Cast(@hono AS VARCHAR)*/
			--Si da honorarios negativo, se debe reflejar en la tabla
			if @hono < 0
			begin 
				SET @hono = 0
			end
			exec dbo.Update_Cartera_Clientes_Cpbt_Doc_Hon @CCB_CODEMP,@CCB_PCLID, @CCB_CTCID, @CCB_CCBID, @hono
			
		end
		else
		---Si el documento es vigente se calcula con el 3-6-9 menos el abono
		begin
			if (@CCB_ESTCPBT = 'V')	and (@IsCheque != 'S')
			BEGIN 
				/*PRINT 'Vigente @IsCheque: ' 
									+ Cast(@IsCheque AS VARCHAR) +
					' Vigente @CCB_ESTCPBT: ' 
									+ Cast(@CCB_ESTCPBT AS VARCHAR)*/
				-- Se compara el codmon que se está iterando con al empresa_configuracion.emc_valnum obtenido y que es 2
				IF @CCB_CODMON = @codmon 
				BEGIN
				  ----------------------Realizar el Tipo de Cambio a la Moneda----------------
					SET @valCamb = @CCB_ASIGNADO
				END
				ELSE
				BEGIN 
					------------Buscar el Valor en la moneda que correponde y se hace la division
					--26401.49 para codMon 2  650.98 para codmon 3 para codmon 1 es 1
					SET @valMonedaCodMon = dbo._Trae_Valor_Moneda(@CCB_CODEMP,@CCB_CODMON)
						if @valMonedaCodMon is null
						begin
							SET @valMonedaCodMon = 1
						end
				
					SET @valCamb = (@CCB_ASIGNADO * @valMonedaCodMon)/ @valorMoneda	
				END
						
				If @diasVenc > @dCalc
				begin 
					-- Si es menor o igual a 10, se aplica 9
					if  @valCamb <= 10 
					begin
						set @hono = .09 * @valCamb   
					end
					-- si es mayor que 10 y menor o igual a 50, se aplica 6, 9
					if  @valCamb > 10  and @valCamb <= 50 
					begin
						set @hono = .09 * 10 +  (@valCamb - 10) * .06
					end
					--Si es mayor que 50, se aplica 3, 6, 9
					if  @valCamb > 50 
					begin
						set @hono = .09 * 10 +  40 * .06 + (@valCamb - 50) * .03
					end
					
					SET @hono = (@hono * @valorMoneda) / @valMonedaCodMon
					SET @hono = @hono - @abonosHonorarios
					SET @hono = ROUND(@hono, @redondear)

					--Si da honorarios negativo, se debe reflejar en la tabla
					if @hono < 0
					begin 
						SET @hono = 0
					end
				
				               
					-----------------Grabar el Honorario-------------
					/*PRINT 'Vigente @hono: ' 
										+ Cast(@hono AS VARCHAR)*/
					exec dbo.Update_Cartera_Clientes_Cpbt_Doc_Hon @CCB_CODEMP,@CCB_PCLID, @CCB_CTCID, @CCB_CCBID, @hono
			
				end
			END
		end
		

	FETCH NEXT FROM curMain INTO  @CCB_CODEMP, @CCB_PCLID, @CCB_CTCID, @CCB_CCBID,@CCB_TPCID, @CCB_TIPCART, @CCB_CODMON, @CCB_FECCALCINT, @CCB_FECVENC,
	@CCB_FECING, @CCB_ASIGNADO, @CCB_CALCHON, @CCB_CCTID, @CCB_ESTCPBT, @diasVenc, @diasAsign, @redondear, @ctc_cctidCount, @regId, @IsCheque
	END ---Fin WHILE curMain
-- Close the cursor
CLOSE curMain
--Deallocate the cursor to free up any memory or open result sets. 
DEALLOCATE  curMain

INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza los Honorarios en Contratos Vigentes', 'Termino', 'Job 2 AM',0 )
