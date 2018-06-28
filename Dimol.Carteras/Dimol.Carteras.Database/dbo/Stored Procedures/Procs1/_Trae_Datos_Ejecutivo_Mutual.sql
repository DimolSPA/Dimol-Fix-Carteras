create procedure [dbo].[_Trae_Datos_Ejecutivo_Mutual] (@cuenta int) as
	 select top 1 cm.CUENTA, b.NOMBRE as BANCO, e.EMAIL as EMAIL_DESTINO 
	 from EJECUTIVO_CUENTA_MUTUAL cm with (nolock), EJECUTIVO_MUTUAL e with (nolock), TIPO_BANCO b with (nolock) 
	 where b.ID_TIPO_BANCO = cm.ID_TIPO_BANCO 
	 and cm.ID_EJECUTIVO = e.ID_EJECUTIVO 
	 and cm.ID_CUENTA_EJECUTIVO = @cuenta
