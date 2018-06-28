CREATE Procedure _Update_Cartera_Clientes_Cpbt_Doc_Imagenes_Ruta(@codemp integer, @pclid numeric (15), @ctcid numeric (15), @ccbid integer, @cdid integer, @ruta varchar(1000)) as  
 update [CARTERA_CLIENTES_CPBT_DOC_IMAGENES]
set CDI_RUTA_ARCHIVO = @ruta
where [CDI_CODEMP]=@codemp
and [CDI_PCLID] =@pclid
and [CDI_CTCID] =@ctcid
and [CDI_CCBID] = @ccbid
and [CDI_CDID]=@cdid
 
 
 
