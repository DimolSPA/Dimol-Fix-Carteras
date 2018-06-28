CREATE PROCEDURE [dbo].[ST_Listar_Direcciones_Deudor_Grilla]
(
@codemp int,
@ctcid integer, 
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;
	
declare @query varchar(7000);
declare @pclid int

SET @pclid = (SELECT top 1
				   CTC_PCLID
				FROM CARTERA_CLIENTES
				WHERE CTC_CTCID = @ctcid
				AND CTC_CODEMP = @codemp)

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

if (@pclid = 522)
begin

	set @query = @query +'SELECT [CTCID] Ctcid
								  ,[DIRECCION] Calle
								  ,isnull([COMID],0) IdComuna
								  ,[TIPO] TipoDireccion
								  ,isnull((select COM_NOMBRE from COMUNA where COM_COMID = comid),( select nombre from SITREL_COMUNA where CODIGO = comid)) as Comuna
								  ,(select COM_CIUID from COMUNA where COM_COMID = comid) as Ciudad
								  ,(select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = comid)) as Region
								  ,(select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = comid))) as Pais   
								  ,[ORIGEN]
								  ,[ENVIADO]
							  FROM [DEUDORES_DIRECCION_SITREL] 
							  where [CODEMP] =' + CONVERT(VARCHAR,@codemp)+ '
							  and [CTCID]=' + CONVERT(VARCHAR,@ctcid ) + '
							UNION
							SELECT [CTC_CTCID] Ctcid
								,[CTC_DIRECCION] Calle
								,isnull([CTC_COMID],0) IdComuna
								,'''' TipoDireccion
								,isnull((select COM_NOMBRE from COMUNA where COM_COMID = CTC_COMID),( select nombre from SITREL_COMUNA where CODIGO = CTC_COMID)) as Comuna
								,(select COM_CIUID from COMUNA where COM_COMID = CTC_COMID) as Ciudad
								,(select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = CTC_COMID)) as Region
								,(select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = CTC_COMID))) as Pais   
								,'''' ORIGEN
								,'''' ENVIADO
							FROM [DEUDORES] 
							where [CTC_CODEMP] =' + CONVERT(VARCHAR,@codemp) + '
							and [CTC_CTCID]=' + CONVERT(VARCHAR,@ctcid )+'
							and [CTC_DIRECCION] != '''''
end
else
begin
	set @query = @query +'SELECT [DDC_CTCID] Ctcid
							,[DDC_DIRECCION] Calle
							,isnull([DDC_COMID],0) IdComuna
							,'''' TipoDireccion
							,isnull((select COM_NOMBRE from COMUNA where COM_COMID = DDC_COMID),( select nombre from SITREL_COMUNA where CODIGO = DDC_COMID)) as Comuna
							,(select COM_CIUID from COMUNA where COM_COMID = DDC_COMID) as Ciudad
							,(select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = DDC_COMID)) as Region
							,(select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = DDC_COMID))) as Pais   
							, '''' ORIGEN
							, '''' ENVIADO
						FROM [DEUDORES_CONTACTOS] 
						where [DDC_CODEMP] =' + CONVERT(VARCHAR,@codemp)+ '
						and [DDC_CTCID]=' + CONVERT(VARCHAR,@ctcid ) + '
						and [DDC_DIRECCION] != ''''
						UNION
						SELECT [CTC_CTCID] Ctcid
							,[CTC_DIRECCION] Calle
							,isnull([CTC_COMID],0) IdComuna
							,'''' TipoDireccion
							,isnull((select COM_NOMBRE from COMUNA where COM_COMID = CTC_COMID),( select nombre from SITREL_COMUNA where CODIGO = CTC_COMID)) as Comuna
							,(select COM_CIUID from COMUNA where COM_COMID = CTC_COMID) as Ciudad
							,(select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = CTC_COMID)) as Region
							,(select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = CTC_COMID))) as Pais   
							,'''' ORIGEN
							,'''' ENVIADO
						FROM [DEUDORES] 
						where [CTC_CODEMP] =' + CONVERT(VARCHAR,@codemp) + '
						and [CTC_CTCID]=' + CONVERT(VARCHAR,@ctcid ) +'
						and [CTC_DIRECCION] != '''''
end
set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
