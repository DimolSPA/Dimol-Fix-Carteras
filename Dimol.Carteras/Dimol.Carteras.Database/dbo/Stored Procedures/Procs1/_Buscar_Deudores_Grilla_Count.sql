-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Buscar_Deudores_Grilla_Count]
(
@codemp int ,
@usrid int ,
@nombre varchar(400) ,
@paterno varchar(100) ,
@materno varchar(100),
@rut varchar(20),
@nom_fant varchar(600),
@telefono  varchar(20),
@email varchar(300),
@direccion varchar(800),
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
--declare @provcli_consulta int = (Select PCC_PCLID_VER FROM PROVCLI_CONSULTA  where pcc_usrid = @usrid)

  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  (	' 
  
set @query = @query + 'select count(ctc_ctcid) count
						from deudores 
						where ctc_codemp = ' + CONVERT(VARCHAR,@codemp)

if @nombre is not null
begin
set @query = @query + ' and ctc_nombre like ''%'+ @nombre + '%'''
end
if @paterno is not null
begin
set @query = @query + ' and ctc_apepat like ''%'+ @paterno + '%'''
end 
if @materno is not null
begin
set @query = @query + ' and ctc_apemat like ''%'+ @materno + '%'''
end          
if @rut is not null
begin
set @query = @query + ' and ctc_rut like ''%'+ @rut + '%'''
end 
if @nom_fant is not null
begin
set @query = @query + ' and ctc_nomfant like ''%'+ @nom_fant + '%'''
end 

if @telefono is not null
begin
set @query = @query + ' and ctc_ctcid in (select ddt_ctcid from deudores_telefonos where ddt_codemp = " + codemp.ToString
                 and ddt_numero like like ''%'+ @telefono + '%''
                 UNION 
                 select dct_ctcid from deudores_contactos_telefonos where dct_codemp = " + codemp.ToString
                 and dct_numero like like ''%'+ @telefono + '%'''
end 

if @email is not null
begin
set @query = @query + ' and ctc_ctcid in (
                 select ddm_ctcid from deudores_mail where ddm_codemp = " + codemp.ToString
                 and ddm_mail like ''%'+ Lower(@email) + '%'' 
                 UNION "
                 select dcm_ctcid from deudores_contactos_mail where dcm_codemp = " + codemp.ToString
                 and dcm_mail like ''%'+ Lower(@email) + '%''
                 )' 
end 
if @direccion is not null
begin
set @query = @query + ' and ctc_direccion like ''%'+ @direccion + '%'''
end 

  
set @query = @query + ') as tabla  ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
