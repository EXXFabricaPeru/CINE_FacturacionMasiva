CREATE PROCEDURE EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_COMPRAS
(
	cardCode varchar(50),
	fechaIniFunc date,
	fechaFinFunc date,
	fechaIniCont date,
	fechaFinCont date,
	codComplejo varchar(50),
	codSala varchar(50),
	codPelicula varchar(50),
	agrupaPelicula varchar(1),
	agrupaComplejo varchar(1),
	agrupaSala varchar(1),
	tipoFacturacion varchar(1)
)
AS
BEGIN
	IF :tipoFacturacion = 'D'
	THEN	
		select distinct
			'N' 				as "Slc"
			,T0."CardCode"		as "CardCode"
			,T0."CardName"		as "CardName"
			,T0."ObjType"		as "TipoDoc"
			,T0."DocEntry"		as "KeyDoc"
			,T1."LineNum"		as "LineaDoc"
			,T0."DocNum"		as "NroDoc"
			,T1."WhsCode"		as "CodComplejo"
			,T2."WhsName"		as "NomComplejo"
			,ifnull(T1."U_EXC_FFUNC",now())	as "FechaFuncion"
			,T1."U_EXC_PELIC"	as "CodPelicula"
			,T3."Name"			as "NomPelicula"
			,T1."U_EXC_NSALA"	as "Sala"
			,T1."ItemCode"		as "CodArticulo"
			,T1."Quantity"		as "Cantidad"
			,T1."PriceBefDi"	as "PrecioUnitario"
			,T1."LineTotal"		as "Total"
			,replicate(' ', 20) as "NroFactura"
			,0.00 				as "Ajuste"
			,replicate(' ', 20)	as "Glosa"
			,replicate(' ', 20)	as "Complejo"
			,replicate(' ', 20)	as "Area"
			,replicate(' ', 20)	as "LineaProducto"
			,replicate(' ', 20)	as "TipoGasto"
		from OPDN T0 
		inner join PDN1 		T1 	on T0."DocEntry" 	= T1."DocEntry"
		inner join OWHS 		T2 	on T1."WhsCode"		= T2."WhsCode"
		left join  "@EXC_TPELI"	T3	on T3."Code"		= T1."U_EXC_PELIC"
		where
		T0."DocStatus" = 'O' and T1."LineStatus" = 'O'
		and ifnull(T1."U_EXD_FACTURADO",'') = 'N'
		and T0."CardCode" like ifnull(:cardCode,'%')
		and ifnull(T1."U_EXC_FFUNC",now()) between :fechaIniFunc and :fechaFinFunc
		and ifnull(T0."DocDate",now()) between :fechaIniCont and fechaFinCont
		and ifnull(T1."WhsCode",'') like ifnull(:codComplejo,'%')	
		and ifnull(T1."U_EXC_PELIC",'') like ifnull(:codPelicula,'%')
		and T1."U_EXC_NSALA" = case when :codSala = -1 then T1."U_EXC_NSALA" else :codSala end 
		and ifnull(T1."U_EXC_PELIC",'') <>  ''
		order by 
		 case when ifnull(:agrupaPelicula,'') = 'Y' then T1."U_EXC_PELIC" else 2 end
		,case when ifnull(:agrupaComplejo,'') = 'Y' then T1."WhsCode" else 2 end
		,case when ifnull(:agrupaSala,'') = 'Y' then T1."U_EXC_NSALA" else 2 end;
	ELSE
		select distinct
			'N' 				as "Slc"
			,T0."CardCode"		as "CardCode"
			,T0."CardName"		as "CardName"
			,T0."ObjType"		as "TipoDoc"
			,T0."DocEntry"		as "KeyDoc"
			,T1."LineNum"		as "LineaDoc"
			,T0."DocNum"		as "NroDoc"
			,T1."WhsCode"		as "CodComplejo"
			,T2."WhsName"		as "NomComplejo"
			,T1."DocDate"		as "FechaDocumento"
			,T1."BaseEntry"		as "KeyOC"
			,T0."NumAtCard"		as "ReferenciaEM"
			,T0."Comments"		as "Comentarios"
			,T1."LineTotal"		as "ImporteBase"
			,T0."DocRate"		as "PrecioSistemas"
			,T0."DocTotal"		as "Total"
			,T0."DocTotalFC"	as "TotalME"
			,replicate(' ',13)  as "NroFactura"
			,replicate('',10)	as "TipoDocFactura"
			,now()				as "FechaDocFactura"
			,now() 				as "FechaContFactura"
			,replicate('',50)	as "GlosaFactura"
			,replicate('',10)	as "GrupoDETFactura"
			,replicate('',20)	as "CtaAsocFactura"
		from OPDN T0 
		inner join PDN1 		T1 	on T0."DocEntry" 	= T1."DocEntry"
		inner join OWHS 		T2 	on T1."WhsCode"		= T2."WhsCode"
		left join  "@EXC_TPELI"	T3	on T3."Code"		= T1."U_EXC_PELIC"
		where 
		T0."DocStatus" = 'O' and T1."LineStatus" = 'O'
		and T1."BaseType" = '22'
		and ifnull(T1."U_EXD_FACTURADO",'') = 'N'
		and T0."CardCode" like ifnull(:cardCode,'%')
		--and ifnull(T1."U_EXC_FFUNC",now()) between :fechaIniFunc and :fechaFinFunc
		and ifnull(T0."DocDate",now()) between :fechaIniCont and fechaFinCont
		and ifnull(T1."WhsCode",'') like ifnull(:codComplejo,'%')
		--and ifnull(T1."U_EXC_NSALA",'0') like ifnull(:codSala,'%')
		--and ifnull(T1."U_EXC_PELIC",'') like ifnull(:codPelicula,'%');
		and ifnull(T1."U_EXC_PELIC",'') =  '';
	END IF;
END;