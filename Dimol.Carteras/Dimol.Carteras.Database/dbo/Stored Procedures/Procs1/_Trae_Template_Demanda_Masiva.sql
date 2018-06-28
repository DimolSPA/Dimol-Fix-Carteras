create procedure [dbo].[_Trae_Template_Demanda_Masiva] (@pdmcid integer) as
  select [dbo].[PANEL_DEMANDA_MASIVA_CONFECCION_OLD].PDMC_TEMPLATE 
  FROM PANEL_DEMANDA_MASIVA_CONFECCION_OLD
  WHERE [dbo].[PANEL_DEMANDA_MASIVA_CONFECCION_OLD].PDMC_ID = @pdmcid
