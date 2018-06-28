-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar documentos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Cartera_Email]
(
@codemp int,
@pclid integer ,  
@estid char(1000),
@tipo_cartera int ,
@gestor int ,
@sucid int ,
@lista_gestores char(1000)
)
AS
BEGIN
	SET NOCOUNT ON;
	
	--Dimol.Email.dao.EnvioEmail.ListarCarteraEmail(1,22, 45,1,282,1,6)

declare @query varchar(7000)='';
declare @query2 varchar(7000)='';

-------------------Reviso cuantos Casos Son-----------------
set @query = 'SELECT  Distinct cartera_clientes_cpbt_doc.ccb_ctcid FROM cartera_clientes_cpbt_doc, estados_cartera
            WHERE  cartera_clientes_cpbt_doc.ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ' and cartera_clientes_cpbt_doc.ccb_pclid = '+ CONVERT(VARCHAR,@pclid)+ '
            and ccb_codemp = ect_codemp and ccb_estid = ect_estid and cartera_clientes_cpbt_doc.ccb_estcpbt not in (''X'',''F'',''J'') '

if @tipo_cartera = 3
begin
	set @query = @query +' and cartera_clientes_cpbt_doc.ccb_tipcart in (1,2) '
end
else
begin
	set @query = @query +' and cartera_clientes_cpbt_doc.ccb_tipcart = ' + CONVERT(VARCHAR,@tipo_cartera)
end
---------------Reviso cuales no estan incluidos y dejo fuera del envio------------------
if @estid != '' and @estid is not null
begin
	set @query2 = ' SELECT distinct cartera_clientes_cpbt_doc.ccb_ctcid FROM cartera_clientes_cpbt_doc, deudores, estados_cartera
                WHERE  cartera_clientes_cpbt_doc.ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ' and cartera_clientes_cpbt_doc.ccb_pclid = '+ CONVERT(VARCHAR,@pclid)+ '
                and ccb_codemp = ect_codemp and ccb_estid = ect_estid and cartera_clientes_cpbt_doc.ccb_estcpbt not in (''X'',''F'',''J'') 
                and ccb_codemp = ctc_codemp and ccb_ctcid = ctc_ctcid '
    if @tipo_cartera = 3
	begin
		set @query2 = @query2 +' and cartera_clientes_cpbt_doc.ccb_tipcart in (1,2) '
	end
	else
	begin
		set @query2 = @query2 +' and cartera_clientes_cpbt_doc.ccb_tipcart = ' + CONVERT(VARCHAR,@tipo_cartera)
	end


    set @query2 = @query2 +' and ccb_estid not in (' + @estid + ')'
end

if @gestor > 0 and @gestor != 282
begin
    set @query = @query +' and cartera_clientes_cpbt_doc.ccb_ctcid in   (SELECT gestor_cartera.gsc_ctcid  FROM gestor_cartera
						WHERE  gestor_cartera.gsc_codemp ='+ CONVERT(VARCHAR,@codemp)+ ' and gsc_gesid ='+ CONVERT(VARCHAR,@gestor)+ '
						and gestor_cartera.gsc_sucid ='+ CONVERT(VARCHAR,@sucid)+')' 
end

If @query2 <> '' begin
	set @query = @query +' and ccb_ctcid not in(' + @query2 + ')'
End

If @gestor = 0 begin
	If @lista_gestores != '' and @lista_gestores is not null begin
		set @query = @query +' and ccb_ctcid in ( SELECT gestor_cartera.gsc_ctcid FROM gestor_cartera
                    WHERE  gestor_cartera.gsc_codemp ='+ CONVERT(VARCHAR,@codemp)+ ' and gsc_gesid in (' +@lista_gestores+ ')
                    and gestor_cartera.gsc_sucid ='+ CONVERT(VARCHAR,@sucid) + ')'
                End
            End



--select @query
--select @query2
exec(@query)	
	

END
