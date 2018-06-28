CREATE Procedure [dbo].[_Update_Empleados_Imagenes_Ruta](@codemp integer, @emplid int, @ruta varchar(1000)) as 

update [Iluvatar_test].[dbo].[EMPLEADOS]
  set [EPL_URL_FOTO] = @ruta
  where [EPL_CODEMP] = @codemp
  and [EPL_EMPLID] = @emplid
