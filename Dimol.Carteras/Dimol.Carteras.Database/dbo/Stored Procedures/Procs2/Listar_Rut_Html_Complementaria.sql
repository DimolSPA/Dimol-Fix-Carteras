CREATE PROCEDURE [dbo].[Listar_Rut_Html_Complementaria]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--20171221
	
	declare @indice int

  SELECT @indice= IsNull(Max(cbc_numero)+1, 1) +4000
    FROM cabacera_comprobantes  
   WHERE ( cabacera_comprobantes.cbc_codemp = 1 ) AND  
         ( cabacera_comprobantes.cbc_sucid = 1 ) AND  
         ( cabacera_comprobantes.cbc_tpcid = 31 ) 	
         
         
     select distinct CTC_NUMERO rut,
     CTC_DIGITO dv,
     CTC_NOMFANT nombre,
     CTC_CTCID ctcid,
     NULL  adherente
     INTO #temp
     from CARTERA_CLIENTES_CPBT_DOC cc
     inner join DEUDORES d
     on d.CTC_CODEMP = cc.CCB_CODEMP
     and d.CTC_CTCID = cc.CCB_CTCID
     where CTC_RUT in 
     ('615130001','700120007','700147002','780287004','818117000','915020003','700031004','709082000','781270008','811482005','815986008','830152008',
'86356400K','878517008','968343904','995200007','197058234','719476007','76075773K','767309902','779337006','796373709','799347903','846946004','877877000',
'882019004','896964003','966786809','967544507','995113104','99586830K','531258509','700009009','70006400K','700100006','761108093','761155733','77815160K',
'786023300','797305707','81554000K','821901006','824779007','86242400K','929750004','706029001','761105566','761175122','767439601','772252005','773893705',
'780169702','786023408','787159907','789086206','802079001','827894001','965623205','967728101','995899302','154121080','650260546','70248300K','743891007',
'745252001','761026658','761360760','781694304','797452807','815052005','921770006','965150102','966859806','967743003','967881007','968632604','968735705',
'66049639','71494748','81085536','81637792','108543078','115149490','121886693','137207141','139186400','141280767','159257940','520014055','520048677',
'53308138K','591064401','617020009','690302004','700023001','700034003','700738000','703601006','706051007','710578001','760603252','760822914','760975931',
'760996920','761005502','761013181','761013769','761037978','761038338','761040898','761084291','761114735','761120441','761121472','761212664','761246372',
'761264656','76134689K','761376535','761382926','761435418','761477382','761527339','761589474','761611585','761673157','761691031','761719769','761758357',
'761769855','761781944','761791699','761808125','761818937','761819321','761835823','761850725','762031590','762082446','762234823','76226854K','762344556',
'762378345','762448122','762449196','762567180','762583933','762587556','762649705','762654423','762709864','76272412K','762766396','762768151','762805979',
'762883503','762958384','762988763','762997843','763042871','763165922','763242919','763347281','763364615','763481867','763558789','763647064','763676978',
'763780139','763849465','763892921','76392016K','764064968','764118294','764152964','764318196','764339525','764362764','764376137','764597133','764665309',
'764797205','764852109','764862805','764919602','765356806','765614600','765857007','766348203','767705301','76794120K','767998902','768490309','769550003',
'770597900','773952205','774105506','774465200','774731903','77690600K','777453203','777457403','780734000','781749109','786139503','789448604','789632308',
'789777004','799145103','814506002','818096003','843886000','875808001','917620008','965051600','965454500','965477101','965906304','966415304','967094307',
'967278300','967849200','968238302','968524801','968545000','968585304','968844504','969505509','969705508','969796503','995258404','99564360K','995773902') --CCB_PCLID = 579  and CCB_CODID = 3
     

--ALTER TABLE #temp DROP COLUMN html

SELECT  t.* ,(select top 1 html from [SII].[dbo].[RUT_CAPTCHA] c where t.rut = c.rut)  html, (ROW_NUMBER() OVER (ORDER BY t.[rut])) + @indice numero 
FROM #temp t 
order by CONVERT(int, RUT)
drop table #temp
	
	
	--20170706
	
--	declare @indice int

--  SELECT @indice= IsNull(Max(cbc_numero)+1, 1) +4000
--    FROM cabacera_comprobantes  
--   WHERE ( cabacera_comprobantes.cbc_codemp = 1 ) AND  
--         ( cabacera_comprobantes.cbc_sucid = 1 ) AND  
--         ( cabacera_comprobantes.cbc_tpcid = 31 ) 	
         
         
--     select distinct CTC_NUMERO rut,
--     CTC_DIGITO dv,
--     CTC_NOMFANT nombre,
--     CTC_CTCID ctcid,
--     (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID = cc.CCB_SBCID)  adherente
--     INTO #temp
--     from CARTERA_CLIENTES_CPBT_DOC cc
--     inner join DEUDORES d
--     on d.CTC_CODEMP = cc.CCB_CODEMP
--     and d.CTC_CTCID = cc.CCB_CTCID
--     where  CCB_PCLID = 579  and CCB_CODID = 3
     

----ALTER TABLE #temp DROP COLUMN html

--SELECT  t.* ,(select top 1 html from [SII].[dbo].[RUT_CAPTCHA] c where t.rut = c.rut)  html, (ROW_NUMBER() OVER (ORDER BY t.[rut])) + @indice numero 
--FROM #temp t 
--order by CONVERT(int, RUT)
--drop table #temp
	
	
	
	
	-- 20170410
	
--	select DISTINCT CTC_NUMERO RUT,CTC_DIGITO DV , CTC_NOMFANT NOMBRE, 
--	(ROW_NUMBER() OVER (ORDER BY CTC_NUMERO)) + 5800  NUMERO, CTC_CTCID CTCID, 
--	'' ADHERENTE ,
--	'' HTML from  DEUDORES
--where CTC_CODEMP = 1 
--and CTC_CTCID IN (1207955)

	
	-- 20170410
	
--declare @indice int

--  SELECT @indice= IsNull(Max(cbc_numero)+1, 1)+1283 + 2273--5431 - 1513--IsNull(Max(cbc_numero)+1, 1) +1283 + 2273
--    FROM cabacera_comprobantes  
--   WHERE ( cabacera_comprobantes.cbc_codemp = 1 ) AND  
--         ( cabacera_comprobantes.cbc_sucid = 1 ) AND  
--         ( cabacera_comprobantes.cbc_tpcid = 31 ) 
         
--select distinct CTC_NUMERO rut
--      ,CTC_DIGITO dv
--      ,CTC_NOMFANT nombre
--      ,f9 numero
--      ,ctcid ,--(ROW_NUMBER() OVER (ORDER BY [rut])) + @indice numero, 
--      (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 579 and CCB_CTCID = CTCID)) ADHERENTE 
--	   INTO #temp
--from dbo.[AA_CASTIGO_MASIVO_MUTUAL_20170324]	, DEUDORES where CTC_CTCID = ctcid
         
--select distinct [rut]
--      ,[dv]
--      ,[nombre]
     
--      ,ctcid ,--(ROW_NUMBER() OVER (ORDER BY [rut])) + @indice numero, 
--      (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 579 and CCB_CTCID = CTCID)) ADHERENTE 
--	   INTO #temp
--from dbo.[AA_CASTIGO_MASIVO_MUTUAL_20170221]

--order by rut desc
--union
--select distinct [rut]
--      ,[dv]
--      ,[nombre]

--      ,ctcid ,--(ROW_NUMBER() OVER (ORDER BY [rut])) + @indice numero, 
--      (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 579 and CCB_CTCID = CTCID)) ADHERENTE 
--	   --INTO #temp
--from dbo.[AA_CASTIGO_MASIVO_MUTUAL_20170221]


----ALTER TABLE #temp DROP COLUMN html 
--SELECT  t.* ,(select top 1 r.HTML from sii..RUT_CAPTCHA r where r.RUT = t.rut)
-- html --(ROW_NUMBER() OVER (ORDER BY t.[rut])) + @indice numero 
--into #temp2
--FROM #temp t 
----where  ctcid in (1202128,1207365,1209733,7657,9866)
--order by CONVERT(int, RUT)
--drop table #temp

--select *  from #temp2 where html != 'A'
--drop table #te


--20170706

--declare @indice int

--  SELECT @indice= IsNull(Max(cbc_numero)+1, 1) +4000
--    FROM cabacera_comprobantes  
--   WHERE ( cabacera_comprobantes.cbc_codemp = 1 ) AND  
--         ( cabacera_comprobantes.cbc_sucid = 1 ) AND  
--         ( cabacera_comprobantes.cbc_tpcid = 31 ) 	
         
--select distinct [rut]
--      ,[dv]
--      ,[Nombre]

--      ,ctcid ,--(ROW_NUMBER() OVER (ORDER BY [rut])) + @indice numero, 
--      (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 579 and CCB_CTCID = CTCID and CCB_CODID = 3)) ADHERENTE 
--	   INTO #temp
--from dbo.AA_CASTIGO_MASIVO_MUTUAL_20170512
----where nombre_sii is null
----order by rut desc
--union
--select distinct [rut]
--      ,[dv]
--      ,[nombre]

--      ,ctcid ,--(ROW_NUMBER() OVER (ORDER BY [rut])) + @indice numero, 
--      (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 579 and CCB_CTCID = CTCID)) ADHERENTE 
--	   --INTO #temp
--from dbo.AA_CASTIGO_MASIVO_MUTUAL_20170512

----ALTER TABLE #temp DROP COLUMN html

--SELECT  t.* ,(select top 1 html from [SII].[dbo].[RUT_CAPTCHA] c where t.rut = c.rut)  html, (ROW_NUMBER() OVER (ORDER BY t.[rut])) + @indice numero 
--FROM #temp t 
--order by CONVERT(int, RUT)
--drop table #temp
	

end
