CREATE PROCEDURE [dbo].[_Insertar_Panel_Quiebra](
@codemp int,
@rolId int,
@rolNumero varchar(20),
@sbcid int,
@cuantia decimal(15,2),
@user int)
	
AS
BEGIN

declare
	@panelId int = 0, @query nvarchar(1000), @count int, @existQuiebra int

	SET @query = 'select @cnt=quiebra_id from panel_quiebra where codemp = @codemp and rolid = @rolId and rolnumero = @rolNumero'
	if @sbcid is null
	begin
	set @query = @query + ' and sbcid is null';
	end
	else
	begin  
	set @query = @query + ' and sbcid = @sbcid';
	end

	EXECUTE sp_executesql @query, N'@codemp int, @rolId int, @rolNumero varchar(20), @sbcid int, @cnt int OUTPUT', @codemp = @codemp, @rolId = @rolId, @rolNumero=@rolNumero, @sbcid=@sbcid, @cnt=@count OUTPUT
	SET @existQuiebra = (select ISNULL(@count,0) as Counts)

	IF (@existQuiebra = 0)
	BEGIN
		SET @panelId = (SELECT IsNull(Max(QUIEBRA_ID)+1, 1)
							FROM PANEL_QUIEBRA)
					
		INSERT INTO PANEL_QUIEBRA(
		QUIEBRA_ID,
		CODEMP,
		ROLID,
		ROLNUMERO,
		SBCID,
		CUANTIA,
		USRID_REGISTRO)	
		VALUES(@panelId, @codemp, @rolId, @rolNumero, @sbcid,@cuantia, @user)
		select @panelId panelId
	END
	ELSE
	BEGIN
		select @existQuiebra panelId
	END
END
