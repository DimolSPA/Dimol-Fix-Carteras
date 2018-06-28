

CREATE FUNCTION [dbo].[_Arbol_Judicial_Logica] 
(@valido int,
@cliente varchar(400),
@fecasig datetime,
@cuenta int,
@categoria varchar(16)
)
RETURNS char(1)
AS
BEGIN

declare 
@logica char(1);

if @valido = 1 
	begin
		if @cliente = 'COOPEUCH LTDA' and datediff(dd, @fecasig, getdate()) > 59 and @cuenta > 1
			begin
				set @logica = 'C'	
			end
		else
			begin
				if @cliente = 'COOPEUCH LTDA' and 60 > datediff(dd, @fecasig, getdate()) and @cuenta > 0
					begin
						set @logica = 'C'	
					end
				else
					begin
						if @categoria = 'BUENO' and @cuenta > 1 
							begin
								set @logica = 'C'
							end
						else
							begin
								if @categoria = 'REGULAR' and @cuenta > 1 
									begin
										set @logica = 'C'
									end
								else
									begin
										if @categoria = 'MALO' and @cuenta > 0 
											begin
												set @logica = 'C'
											end
										else
											begin
												if @categoria = 'SIN CATEGORIA' and @cuenta > 1
													begin
														set @logica = 'C'
													end
												else
													begin
														set @logica = 'S'
													end
											end
									end
							end
					end
			end
	end	
else
	begin	
		set @logica = ''
	end

RETURN @logica
END

