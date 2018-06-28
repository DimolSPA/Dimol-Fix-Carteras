

Create Procedure Trae_Empresa_Datos_Todos(@codemp integer, @codsuc integer) as
  SELECT empresa.emp_rut,   
         empresa.emp_nombre,   
         empresa.emp_rutrepleg,   
         empresa.emp_replegal,   
         empresa.emp_giro, 
        empresa.emp_logo,  
         empresa_sucursal.esu_nombre,   
         pais.pai_nombre,   
         pais.pai_codtel,   
         region.reg_nombre,   
         ciudad.ciu_nombre,   
         ciudad.ciu_codarea,   
         comuna.com_nombre,   
         comuna.com_codpost,   
         empresa_sucursal.esu_direccion,   
         empresa_sucursal.esu_telefono,   
         empresa_sucursal.esu_fax,   
         empresa_sucursal.esu_mail,
         empresa.emp_giro  
    FROM empresa,   
         empresa_sucursal,   
         comuna,   
         ciudad,   
         region,   
         pais  
   WHERE ( empresa_sucursal.esu_codemp = empresa.emp_codemp ) and  
         ( comuna.com_comid = empresa_sucursal.esu_comid ) and  
         ( ciudad.ciu_ciuid = comuna.com_ciuid ) and  
         ( region.reg_regid = ciudad.ciu_regid ) and  
         ( pais.pai_paiid = region.reg_paiid ) and  
         ( ( empresa.emp_codemp = @codemp )   and (empresa_sucursal.esu_sucid = @codsuc)
         )
