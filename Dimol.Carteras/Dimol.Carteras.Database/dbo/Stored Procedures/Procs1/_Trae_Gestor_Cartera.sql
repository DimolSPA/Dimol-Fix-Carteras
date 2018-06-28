CREATE Procedure [dbo].[_Trae_Gestor_Cartera](@codemp integer, @sucid integer, @ctcid integer) as
  Select distinct gestor_cartera.gsc_gesid
FROM gestor_cartera
WHERE  gestor_cartera.gsc_codemp = @codemp
and gestor_cartera.gsc_sucid = @sucid
and gestor_cartera.gsc_ctcid = @ctcid
