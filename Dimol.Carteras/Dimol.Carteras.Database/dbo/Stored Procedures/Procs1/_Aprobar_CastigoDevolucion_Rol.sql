CREATE PROCEDURE [dbo].[_Aprobar_CastigoDevolucion_Rol]
(
@codemp int,
@rolid int,
@nuevoEstado int,
@comentario varchar(100),
@user int,
@ip_red  varchar(30),
@ip_maquina varchar(30)
)
AS
BEGIN
declare  @esjid int
declare @date datetime = getdate()
declare @datejud datetime = dateadd(hour,12, @date)
select @esjid = rol_esjid from  rol where ROL_CODEMP = 1 and ROL_ROLID = @rolid
	INSERT INTO rol_estados  
         ( rle_codemp,   
           rle_rolid,   
           rle_estid,   
           rle_esjid,   
           rle_fecha,   
           rle_usrid,   
           rle_ipred,   
           rle_ipmaquina,   
           rle_comentario,   
           rle_fecjud )  
  VALUES ( @codemp,   
           @rolid,   
           @nuevoEstado,   
           @esjid,   
           @date,   
           @user,   
           @ip_red,   
           @ip_maquina,   
           @comentario,   
           @datejud)  

  UPDATE rol  
     SET rol_estid = @nuevoEstado,   
--         rol_esjid = @esjid,   
         rol_fecjud = @datejud,   
         rol_fecultgest = @date 
   WHERE  rol.rol_codemp = @codemp  AND  
          rol.rol_rolid =  @rolid  	

END
