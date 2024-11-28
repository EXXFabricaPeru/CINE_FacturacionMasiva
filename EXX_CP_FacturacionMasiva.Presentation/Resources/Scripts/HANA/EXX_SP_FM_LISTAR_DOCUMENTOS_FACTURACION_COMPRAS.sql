CREATE PROCEDURE EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_COMPRAS
(
	cardCode varchar(50),
	fechaIniFunc date,
	fechaFinFunc date,
	fechaIniCont date,
	fechaFinCont date,
	codComplejo varchar(50),
	codSala varchar(50),
	codPelicula varchar(50)
)
AS
BEGIN
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
		,T1."U_EXC_FFUNC"	as "FechaFuncion"
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
		,replicate(' ', 20)	as "Area"
	from OPDN T0 
	inner join PDN1 		T1 	on T0."DocEntry" 	= T1."DocEntry"
	inner join OWHS 		T2 	on T1."WhsCode"		= T2."WhsCode"
	left join  "@EXC_TPELI"	T3	on T3."Code"		= T1."U_EXC_PELIC"
	where 
	ifnull(T1."U_EXD_FACTURADO",'') = 'N'
	and T0."CardCode" like ifnull(:cardCode,'%')
	and ifnull(T1."U_EXC_FFUNC",now()) between :fechaIniFunc and :fechaFinFunc
	and ifnull(T0."DocDate",now()) between :fechaIniCont and fechaFinCont
	and ifnull(T1."WhsCode",'') like ifnull(:codComplejo,'%')
	and ifnull(T1."U_EXC_NSALA",'0') like ifnull(:codSala,'%')
	and ifnull(T1."U_EXC_PELIC",'') like ifnull(:codPelicula,'%');
END;