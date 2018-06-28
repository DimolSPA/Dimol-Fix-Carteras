-- =============================================  
-- Author:  FM  
-- Create date: 12-05-2014  
-- Description: Lista regiones segun pais  
-- =============================================  
CREATE PROCEDURE [dbo].[_Trae_Comunas]
AS  
BEGIN  
 SET NOCOUNT ON;  
 Select com_comid, com_nombre from comuna 
 order by com_nombre  
END
