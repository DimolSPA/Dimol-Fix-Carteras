create PROCEDURE [dbo].[_Buscar_Deudor_Cli]
(
@codemp int ,
@sucid int ,
@usrid int ,
@rut varchar(20), 
@cli int 
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
declare @provcli_consulta int = (Select PCC_PCLID_VER FROM PROVCLI_CONSULTA  where pcc_usrid = @usrid)

  
set @query = 'SELECT provcli.pcl_pclid pclid,
	   provcli.pcl_nomfant NombreCliente,
	   deudores.ctc_ctcid ctcid,  
	   deudores.ctc_rut Rut,  
	   deudores.ctc_nomfant NombreFantasia,  
	   Gestor.ges_nombre Gestor, 
	   Gestor.ges_gesid gesid,
	   '''' as Rol
FROM {oj cartera_clientes 
LEFT OUTER JOIN gestor_cartera 
ON cartera_clientes.ctc_codemp = gestor_cartera.gsc_codemp 
AND cartera_clientes.ctc_pclid = gestor_cartera.gsc_pclid 
AND cartera_clientes.ctc_ctcid = gestor_cartera.gsc_ctcid 
LEFT OUTER JOIN gestor 
ON gestor_cartera.gsc_codemp = gestor.ges_codemp 
AND gestor_cartera.gsc_sucid = gestor.ges_sucid 
AND gestor_cartera.gsc_gesid = gestor.ges_gesid}, 
	provcli,
	deudores
WHERE  cartera_clientes.ctc_codemp = provcli.pcl_codemp  
	and cartera_clientes.ctc_pclid = provcli.pcl_pclid  
	and deudores.ctc_codemp = cartera_clientes.ctc_codemp  
	and deudores.ctc_ctcid = cartera_clientes.ctc_ctcid  
	and cartera_clientes.ctc_codemp = ' + CONVERT(VARCHAR,@codemp) 
	-- Cliente
		if @provcli_consulta is not null
		begin
		set @query = @query + ' and (pcl_pclid = ' +  CONVERT(VARCHAR,@provcli_consulta) + ' or pcl_pclid = ' + CONVERT(VARCHAR,@cli) + ') '
		end
		else
		begin
		set @query = @query + ' and pcl_pclid = ' + CONVERT(VARCHAR,@cli)
		end

-- RUT
if @rut is not null
begin
set @query = @query + ' and ctc_rut like ''%' + @rut + '%''';
end

 exec(@query)	
	

END
