CREATE PROCEDURE [dbo].[_Listar_ClausulasContratoCartera_PorId]
(
@codemp int,
@id int
)
AS
BEGIN
	select clc_clcid, clc_nombre 
	  ,case [clc_tipo] when '1' then 'Sin Etiqueta'
                when '2' then 'Sin Etiqueta'
                when '3' then 'Sin Etiqueta'
                when '4' then 'Sin Etiqueta'
                when '5' then 'Sin Etiqueta'
				when '6' then 'Sin Etiqueta' end as Tipo, clc_tipo
	  ,case [clc_prejud] when'P' then 'Prejudicial'
                when '' then 'Judicial'
                when 'A' then 'Ambas' end as Area
	  from clausulas_contcart 
	  where clc_codemp = @codemp
	  and clc_clcid = @id
		
   END
