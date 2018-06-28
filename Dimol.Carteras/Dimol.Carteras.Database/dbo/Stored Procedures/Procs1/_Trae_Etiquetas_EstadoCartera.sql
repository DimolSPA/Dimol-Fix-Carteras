CREATE Procedure [dbo].[_Trae_Etiquetas_EstadoCartera]
(
		@idioma integer,
		@permiso int)
		 as  

      
DECLARE @Table TABLE (Id INT, Descripcion varchar(100) )     
      
DECLARE @cnt INT = 1;
DECLARE @cntf INT = 8;
		 
IF (@permiso =3)
begin
	set @cnt = 2;
	set @cntf = 5;

         
END

WHILE @cnt < @cntf
BEGIN
	insert into @Table
	 select @cnt,ETQ_DESCRIPCION from  etiquetas where etq_codigo like 'AgrEst0' + CONVERT(varchar,@cnt)

   SET @cnt = @cnt + 1;
END;


select * from @Table