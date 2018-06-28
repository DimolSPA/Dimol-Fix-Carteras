CREATE PROCEDURE [dbo].[_Calculo_Interes_Contratos_Vigentes] AS
INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza los Intereses en Contratos Vigentes', 'Comienzo', 'Job 2 AM',0 )

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
@regId INT,
@ctc_cctidCount INT = 0,
@tipcart INT,
@rolcount INT,
@porcentajeInt DECIMAL(15,5),
@asignado DECIMAL(15,4),
@pInt DECIMAL(15,4),
@dCalc INT = dbo._Trae_Valor_ValNum(1, 21),
--Clausulas
@clc_clcid INT,
@clc_rango VARCHAR(1),
@clc_tiprango INT,
@clc_valor DECIMAL(15,4),
@pIntRangoClausula DECIMAL(15,4),
--Calculos
@int DECIMAL(15,4) = 0,
@interesesAplicaciones DECIMAL(15,4),
@abonoInteres DECIMAL,
@clc_clccid int

DECLARE curMain CURSOR FOR 

SELECT CCB_CODEMP, CCB_PCLID, CCB_CTCID, CCB_CCBID, CCB_TPCID, CCB_TIPCART, CCB_CODMON, CCB_FECCALCINT, CCB_FECVENC, CCB_FECING, 
		CCB_ASIGNADO, CCB_CALCHON,CCB_CCTID, CCB_ESTCPBT,
		datediff(day, CARTERA_CLIENTES_CPBT_DOC.CCB_FECVENC, getdate()) diasVenc,
		datediff(day, CARTERA_CLIENTES_CPBT_DOC.CCB_FECING, CARTERA_CLIENTES_CPBT_DOC.CCB_FECVENC) diasAsign,
		(Select mon_decimales 
			From monedas 
			Where mon_codemp = CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP
			and mon_codmon = CARTERA_CLIENTES_CPBT_DOC.CCB_CODMON )redondear,
		(Select ciudad.ciu_regid       
			From  ciudad 
			JOIN comuna 
			ON ciudad.ciu_ciuid = comuna.com_ciuid 
			JOIN deudores 
			ON comuna.com_comid = deudores.ctc_comid
			Where deudores.ctc_codemp = CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP
			and deudores.ctc_ctcid = CARTERA_CLIENTES_CPBT_DOC.CCB_CTCID)regId,
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
		'3' AS tipcart,
		--Porcentaje de Interes
		(Select mon_porcint / 100
			From monedas
			Where mon_codemp = CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP
			and mon_codmon = CARTERA_CLIENTES_CPBT_DOC.CCB_CODMON)porcentajeInt
	
			
FROM CARTERA_CLIENTES_CPBT_DOC   WITH (nolock)    
WHERE CCB_ESTCPBT IN ('V','J')  and ccb_tipcart != 1 and CCB_PCLID not in (559,603, 605)

-- y exista Contrato Vigente
and EXISTS (Select  ccli.*
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
			  ccli.ctc_fecfin >= getdate())) )    
ORDER BY CCB_FECDOC ASC
OPEN curMain
FETCH NEXT FROM curMain INTO  @CCB_CODEMP, @CCB_PCLID, @CCB_CTCID, @CCB_CCBID,@CCB_TPCID, @CCB_TIPCART, @CCB_CODMON, @CCB_FECCALCINT, @CCB_FECVENC,
@CCB_FECING, @CCB_ASIGNADO, @CCB_CALCHON, @CCB_CCTID, @CCB_ESTCPBT, @diasVenc, @diasAsign, @redondear, @regId, @ctc_cctidCount,@tipcart,@porcentajeInt
WHILE (@@FETCH_STATUS = 0)
	BEGIN ---Inicio WHILE curMain

	--Logica de Negocio
	--Si existen Contrato Vigentes
		if @ctc_cctidCount > 0
		begin
			If @diasVenc > @dCalc
			BEGIN 
				SET @pInt = @porcentajeInt
				SET @pIntRangoClausula = 0
				--- if @tipcart > 1 el tipo de Cartera es Dura u otra
				-- Si existen Contrato Vigentes se Busca las Clausulas
			
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
				and clausulas_contcart.clc_tipo = 9
				and clausulas_contcart.clc_codmon = @CCB_CODMON --1

				IF (@clc_rango = 'N')
						BEGIN
							SET @pIntRangoClausula = @clc_valor/100
						END
				IF (@clc_rango = 'S')
						BEGIN
							EXEC @pIntRangoClausula = dbo.Get_Valor_Rango @CCB_CODEMP,@clc_clccid, @clc_tiprango,@diasVenc,@diasAsign,@regId,@CCB_ASIGNADO,@CCB_FECVENC
							SET @pIntRangoClausula = @pIntRangoClausula / 100 
						END
		

				--Si @pIntRangoClausula es mayor diferente de cero, es porque se encontro valor rango
				-- Se le asigna a la variable @pInt
				if @pIntRangoClausula <> 0 
				begin 
					SET @pInt = @pIntRangoClausula
				end
			

				-- Se procede a realizar calculos
				--Revisar si tiene congelado los intereses
				--if @CCB_FECCALCINT is null OR (Cast(@CCB_FECCALCINT AS DATE) < Cast(getdate() AS DATE))
				--	begin 
							 
				SET @int = (((@pInt / 30) * @diasVenc) * @CCB_ASIGNADO)
				--	end

				--Revisar si ha realizado algun abono de intereses
				SET @interesesAplicaciones = 0
				SET @interesesAplicaciones = isnull(dbo._Trae_Interes_Aplicaciones(@CCB_CODEMP, @CCB_CTCID, @CCB_PCLID,@CCB_CCBID), 0)			

				SET @int = @int - @interesesAplicaciones
				
				--Si da interes negativo, se debe reflejar en la tabla
				if @int < 0
				begin 
					SET @int = 0
				end

				--Se aplica redondear
				SET @int = ROUND(@int,@redondear)

				---Si la fecha de calculo de interes es nula o menor o igual a la actual, se Actualiza los Intereses
				--if @CCB_FECCALCINT is null OR (Cast(@CCB_FECCALCINT AS DATE) <= Cast(getdate() AS DATE))
				--begin
					--Actualizar los Intereses
				exec dbo.Update_Cartera_Clientes_Cpbt_Doc_Int @CCB_CODEMP,@CCB_PCLID, @CCB_CTCID, @CCB_CCBID, @int
				--end
			END
			ELSE
			BEGIN
				SET @int = 0
				exec dbo.Update_Cartera_Clientes_Cpbt_Doc_Int @CCB_CODEMP,@CCB_PCLID, @CCB_CTCID, @CCB_CCBID, @int
			END
						
		end
		
		--PRINT 'Found porcentaje de interes: ' 
		--							+ Cast(@int AS VARCHAR)

	

	FETCH NEXT FROM curMain INTO  @CCB_CODEMP, @CCB_PCLID, @CCB_CTCID, @CCB_CCBID,@CCB_TPCID, @CCB_TIPCART, @CCB_CODMON, @CCB_FECCALCINT, @CCB_FECVENC,
	@CCB_FECING, @CCB_ASIGNADO, @CCB_CALCHON, @CCB_CCTID, @CCB_ESTCPBT,@diasVenc,@diasAsign,@redondear,@regId, @ctc_cctidCount,@tipcart,@porcentajeInt
	END ---Fin WHILE curMain
-- Close the cursor
CLOSE curMain
--Deallocate the cursor to free up any memory or open result sets. 
DEALLOCATE  curMain

INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza los Intereses en Contratos Vigentes', 'Termino', 'Job 2 AM',0 )

