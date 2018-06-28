-- =============================================
-- Author:		FMO
-- Create date: 23-05-2014
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE _Trae_Subcartera(@codemp int, @sbcid int)
AS
BEGIN
	SET NOCOUNT ON;
declare @comuna int
Declare @ciudad int
declare @region int
declare @pais	int	

SELECT @comuna = [SBC_COMID]
  FROM [SUBCARTERAS]
  where [SBC_CODEMP] = @codemp
      and [SBC_SBCID] = @sbcid
select @ciudad = COM_CIUID 
from COMUNA 
where COM_COMID = @comuna      
select @region = CIU_REGID  
from CIUDAD 
where CIU_CIUID = @ciudad
select @pais = REG_PAIID
from REGION 
where REG_REGID = @region

/****** Script para el comando SelectTopNRows de SSMS  ******/
SELECT [SBC_CODEMP] Codemp
      ,[SBC_SBCID] Sbcid
      ,[SBC_RUT] Rut
      ,[SBC_NOMBRE] Nombre
      ,[SBC_COMID] Comuna
      ,[SBC_DIRECCION] Direccion
      ,[SBC_TELEFONO] Telefono
      ,@ciudad Ciudad
      ,@region Region
      ,@pais Pais
  FROM [SUBCARTERAS]
  where [SBC_CODEMP] = @codemp
      and [SBC_SBCID] = @sbcid
      
      
      end
      