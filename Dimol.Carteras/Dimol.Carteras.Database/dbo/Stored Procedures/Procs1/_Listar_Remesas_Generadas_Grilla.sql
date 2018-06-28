CREATE PROCEDURE [dbo].[_Listar_Remesas_Generadas_Grilla]
(
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'SELECT 
	Id,
	Cliente,
	CapitalRecuperado,
	InteresRecuperado,
	HonorarioRecuperado,
	Capital,
	Interes,
	Honorario,
	(FacturaCliente - Anticipo) TotalFactura,
	(FacturaDimol - Anticipo) TotalDimol,
	FechaRemesa
FROM(select r.REMESA_ID Id,
			p.PCL_NOMFANT Cliente,
			sum(rd.CAPITALRECUPERADO) CapitalRecuperado,
			sum(rd.INTERESRECUPERADO) InteresRecuperado,
			sum(rd.HONORARIORECUPERADO) HonorarioRecuperado,
			sum(rd.CAPITALGANANCIA) Capital,
			sum(rd.INTERESGANANCIA) Interes,
			sum(rd.HONORARIOGANANCIA) Honorario,
			(Select ISNULL(SUM(DEBITADO),0) from REMESA_ANTICIPO where REMESA_ID = r.REMESA_ID ) Anticipo,
			case when rd.PORINTERES = 100 and rd.PORHONORARIO = 100
			then (sum(rd.CAPITALGANANCIA))
				else 
					case when rd.PORINTERES = 100 then (sum(rd.CAPITALGANANCIA) + sum(rd.HONORARIOGANANCIA)) 
					else 
						case when rd.PORHONORARIO = 100 then (sum(rd.CAPITALGANANCIA) + sum(rd.INTERESGANANCIA))
						else 
							(sum(rd.CAPITALGANANCIA) + sum(rd.INTERESGANANCIA) + sum(rd.HONORARIOGANANCIA)) 
					end
				end
			end as FacturaCliente,
			(sum(rd.CAPITALGANANCIA) + sum(rd.INTERESGANANCIA) + sum(rd.HONORARIOGANANCIA)) FacturaDimol,
			r.FEC_REGISTRO FechaRemesa
		from REMESA r
		JOIN REMESA_DETALLE rd
		on r.REMESA_ID = rd.REMESA_ID
		join PROVCLI p
		on rd.PCLID = P.PCL_PCLID
		AND RD.CODEMP = P.PCL_CODEMP
		group by r.REMESA_ID, p.PCL_NOMFANT,rd.PORINTERES, rd.PORHONORARIO, r.FEC_REGISTRO) remesas'


set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
