-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar deudores para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Deudor_Grilla_Count]
(
@codemp int ,
@sucid int ,
@usrid int ,
@pclid int ,
@nombre varchar(400) ,
@paterno varchar(100) ,
@materno varchar(100),
@rut varchar(20),
@nom_fant varchar(600),
@telefono  varchar(20),
@email varchar(300),
@direccion varchar(800),
@gestor  varchar(20),
@rol varchar(20), 
@estado varchar(2),
@num_CPBT varchar(30),
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
declare @provcli_consulta int = (Select PCC_PCLID_VER FROM PROVCLI_CONSULTA  where pcc_usrid = @usrid)

  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ctcid asc ) as row from    
  (	' 
  
set @query = @query + 'SELECT count(deudores.ctc_ctcid ) ctcid
FROM {oj cartera_clientes  with (nolock)
LEFT OUTER JOIN gestor_cartera  with (nolock)
ON cartera_clientes.ctc_codemp = gestor_cartera.gsc_codemp 
AND cartera_clientes.ctc_pclid = gestor_cartera.gsc_pclid 
AND cartera_clientes.ctc_ctcid = gestor_cartera.gsc_ctcid 
LEFT OUTER JOIN gestor  with (nolock)
ON gestor_cartera.gsc_codemp = gestor.ges_codemp 
AND gestor_cartera.gsc_sucid = gestor.ges_sucid 
AND gestor_cartera.gsc_gesid = gestor.ges_gesid}, 
	provcli with (nolock),
	deudores with (nolock)
WHERE  cartera_clientes.ctc_codemp = provcli.pcl_codemp  
	and cartera_clientes.ctc_pclid = provcli.pcl_pclid  
	and deudores.ctc_codemp = cartera_clientes.ctc_codemp  
	and deudores.ctc_ctcid = cartera_clientes.ctc_ctcid  
	and cartera_clientes.ctc_codemp = ' + CONVERT(VARCHAR,@codemp) 
	-- Cliente
if @pclid is not null
	begin
		set @query = @query + ' and pcl_pclid = ' +  CONVERT(VARCHAR,@pclid)
	end
	else
		if @provcli_consulta is not null
		begin
		set @query = @query + ' and pcl_pclid = ' +  CONVERT(VARCHAR,@provcli_consulta)
		end
	-- Nombre
if @nombre is not null
begin
	set @query = @query + ' and ctc_nombre like ''%' + @nombre+ '%''';
end
-- Paterno
if @paterno is not null
begin
set @query = @query + ' and ctc_apepat like ''%'+ @paterno + '%''';
end
-- Materno
if @materno is not null
begin
set @query = @query + ' and ctc_apemat like ''%'+ @materno + '%''';
end
-- RUT
if @rut is not null
begin
set @query = @query + ' and ctc_rut like ''%' + @rut + '%''';
end
-- Nombre fantasia
if @nom_fant is not null
begin
set @query = @query + ' and ctc_nomfant like ''%' + @nom_fant + '%''';
end
-- Telefono
if @telefono is not null
begin
set @query = @query + ' and deudores.ctc_ctcid in (select ddt_ctcid 
													from deudores_telefonos 
													where ddt_codemp = '+  CONVERT(VARCHAR,@codemp) +
													' and ddt_numero like ''%' + CONVERT(VARCHAR,@telefono) + '%''
													UNION
													select dct_ctcid 
													from deudores_contactos_telefonos 
													where dct_codemp = ' +  CONVERT(VARCHAR,@codemp) +
													'  and dct_numero like ''%' + CONVERT(VARCHAR,@telefono) + '%'') ';
end
-- eMail
if @email is not null
begin
set @query = @query + ' and deudores.ctc_ctcid in (select ddm_ctcid 
													from deudores_mail 
													where ddm_codemp = '+  CONVERT(VARCHAR,@codemp) +
													' and ddm_mail like ''%' + lower(@email) + '%''
													UNION
													select dcm_ctcid 
													from deudores_contactos_mail 
													where dcm_codemp = '+  CONVERT(VARCHAR,@codemp) +
													' and dcm_mail like ''%' + lower(@email) + '%'')';
end
-- Direccion
if @direccion is not null
begin
set @query = @query + ' and ctc_direccion like ''%' +@direccion + '%''';
end
-- Gestor
if @gestor is not null
begin
set @query = @query + ' and deudores.ctc_ctcid in (Select cartera_clientes.ctc_ctcid
													FROM cartera_clientes
													WHERE  cartera_clientes.ctc_codemp = '+  CONVERT(VARCHAR,@codemp)
					  if @pclid is not null
					  begin
						set @query = @query +		' and cartera_clientes.ctc_pclid = ' +CONVERT(VARCHAR,@pclid)
					  end
						set @query = @query + 		' and cartera_clientes.ctc_ctcid in (SELECT gestor_cartera.gsc_ctcid 
													FROM gestor_cartera
													WHERE  gestor_cartera.gsc_codemp = ' +CONVERT(VARCHAR,@pclid) +
													' and gestor_cartera.gsc_sucid = ' +CONVERT(VARCHAR,@sucid) +
													' and gestor_cartera.gsc_gesid = ' +CONVERT(VARCHAR,@gestor)+'))';
end
-- Rol
if @rol is not null
begin
set @query = ' select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ctcid asc ) as row from    
  (	SELECT count(view_rol.rol_pclid) as ctcid
                   FROM view_rol
                   WHERE view_rol.rol_codemp = '+  CONVERT(VARCHAR,@codemp)+
                   ' and view_rol.rol_numero like ''%'+ @rol +'%''';
end
-- Estado
if @estado is not null
begin
set @query = @query + ' and deudores.ctc_ctcid in (Select cartera_clientes_cpbt_doc.ccb_ctcid
													FROM cartera_clientes_cpbt_doc
													WHERE  cartera_clientes_cpbt_doc.ccb_codemp = '+  CONVERT(VARCHAR,@codemp)+
													' and cartera_clientes_cpbt_doc.ccb_estcpbt = '''+ @estado + ''')'
end
-- CPBT
if @num_CPBT is not null
begin
set @query = @query + ' and deudores.ctc_ctcid in (Select cartera_clientes_cpbt_doc.ccb_ctcid
													FROM cartera_clientes_cpbt_doc
													WHERE  cartera_clientes_cpbt_doc.ccb_codemp = '+  CONVERT(VARCHAR,@codemp)+
													' and cartera_clientes_cpbt_doc.ccb_numero like ''%' + @num_CPBT + '%'')';
end

  
set @query = @query + ') as tabla  ) as t
  where  row >= 0 and row <= 1000000000'

if @where is not null
begin
set @query = @query + @where;
end

 --select @query
 exec(@query)	
	

END
