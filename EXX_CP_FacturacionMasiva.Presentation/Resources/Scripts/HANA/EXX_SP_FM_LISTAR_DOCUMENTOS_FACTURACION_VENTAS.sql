CREATE PROCEDURE EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_VENTAS
(
	IN fechaIni date,
	IN fechaFin date,
	IN cardCode varchar(50),
	IN slpCode varchar(50),
	IN DocBase varchar(50),
	IN TipoDoc varchar(50)
)
LANGUAGE SQLSCRIPT
AS
BEGIN

TMP_0=(
SELECT DISTINCT T3."DocNum" as "DocNum",'0' "LineNum",T1."DocEntry" , T3."DocTotal"
FROM ODPI T0  
INNER JOIN DPI1 T1 ON T0."DocEntry" = T1."DocEntry"
INNER JOIN RDR1 T2 ON T1."BaseEntry" = T2."DocEntry" AND T1."BaseType"=T2."ObjType" AND T2."U_EXC_FACTURA" ='Y' AND T2."LineStatus"='O'
INNER JOIN ORDR T3 ON T2."DocEntry"=T3."DocEntry"
UNION 
SELECT DISTINCT  T4."DocNum" as  "DocNum",'0' "LineNum",T1."DocEntry" , T4."DocTotal"
FROM ODPI T0  
INNER JOIN DPI1 T1 ON T0."DocEntry" = T1."DocEntry"
INNER JOIN QUT1 T3 ON T1."BaseEntry" = T3."DocEntry" AND T1."BaseType"=T3."ObjType"
INNER JOIN RDR1 T2 ON T2."BaseEntry"=T3."DocEntry" AND T2."BaseType"=T3."ObjType" AND T2."U_EXC_FACTURA" ='Y' AND T2."LineStatus"='O'
INNER JOIN ORDR T4 ON T2."DocEntry"=T4."DocEntry"
);


TMP=(
SELECT 
T0."DocEntry",
T0."DocNum", 
ROW_NUMBER ( ) OVER( PARTITION BY T0."DocNum" ORDER BY 
(CASE WHEN IFNULL(T1."U_EXC_FACTURA",'N')='Y' AND IFNULL(T1."U_EXC_CONFI",'N')='N' THEN 'Y' ELSE '' END) DESC) AS "ROW_ID",
CASE WHEN T0."Indicator"='01' THEN 
(SELECT TA."Series" FROM NNM1 TA WHERE TA."ObjectCode" ='13' AND TA."Locked"='N' AND  TA."DocSubType" ='--' AND  TA."U_FE_TipoEmision" ='FE' AND RIGHT(TA."SeriesName",2)= T0."U_EXC_TVENTA") 
ELSE 
(SELECT TA."Series" FROM NNM1 TA WHERE TA."ObjectCode" ='13' AND TA."Locked"='N' AND  TA."DocSubType" ='--' AND  TA."U_FE_TipoEmision" ='FE' AND RIGHT(TA."SeriesName",2)= 'TT') 
END AS "Series" ,
CASE WHEN T0."Indicator"='01' THEN 
(SELECT TA."SeriesName" FROM NNM1 TA WHERE TA."ObjectCode" ='13' AND TA."Locked"='N' AND  TA."DocSubType" ='--' AND  TA."U_FE_TipoEmision" ='FE' AND RIGHT(TA."SeriesName",2)= T0."U_EXC_TVENTA") 
ELSE 
(SELECT TA."SeriesName" FROM NNM1 TA WHERE TA."ObjectCode" ='13' AND TA."Locked"='N' AND  TA."DocSubType" ='--' AND  TA."U_FE_TipoEmision" ='FE' AND RIGHT(TA."SeriesName",2)= 'TT') 
END AS "SeriesName" ,
T0."CardCode", 
T0."CardName",
CASE T0."DocType" WHEN 'S' THEN 'dDocument_Service' ELSE 'dDocument_Items' END AS "DocType",
CAST(YEAR(CURRENT_DATE)||RIGHT(('0'||MONTH(CURRENT_DATE)),2)||RIGHT(CURRENT_DATE,2) AS NVARCHAR(100)) AS "Fecha de contabilización", --T0."TaxDate", T0."DocDueDate", 
T0."NumAtCard" as "N° OC", 
T0."DocCur" as "Moneda", 
T0."Indicator", 
T0."CtlAccount", 
T0."GroupNum" as "Condicion Pago",
T0."Comments",
T0."SlpCode" as "Vendedor", 
T0."U_EXC_TVENTA", 
T0."U_EXC_AUTOD",
(CASE WHEN IFNULL(T1."U_EXC_FACTURA",'N')='Y' AND IFNULL(T1."U_EXC_CONFI",'N')='N' THEN 'Y' ELSE '' END) AS "FT Reserva",
CASE WHEN T3."PymntGroup" like '%CONTADO%' THEN 1 ELSE 2 END as "FEX Forma Pago",
CASE "EXC_CP_TipoOperacion" (T0."DocNum") WHEN 0 THEN '0101' ELSE '1001' END AS "FEX Tipo Operacion", T0."DocDate",
CASE WHEN IFNULL(T4."DocNum",0)=0 THEN 
(CASE T0."DocCur" WHEN 'SOL' THEN SUM(T1."LineTotal")+SUM(T1."VatSum") ELSE SUM(T1."TotalFrgn")+SUM(T1."VatSumFrgn") END)
ELSE
0
END
 AS "DocTotal"

FROM ORDR T0  
INNER JOIN RDR1 T1 ON T0."DocEntry" = T1."DocEntry" 
INNER JOIN OACT T2 ON T1."AcctCode"=T2."AcctCode" 
INNER JOIN "OCTG" T3 ON T0."GroupNum" = T3."GroupNum"
LEFT JOIN :TMP_0 T4 ON T0."DocNum"=T4."DocNum"
WHERE IFNULL(T1."U_EXC_FACTURA",'N') ='Y' AND T1."LineStatus"='O' AND "U_EXD_FACTURADO" = 'N' AND 
		T0."DocDate" Between :fechaIni AND fechaFin
GROUP BY T0."DocNum", T0."CardCode", T0."DocDate", T0."TaxDate", T0."DocDueDate", T0."DocType",T0."NumAtCard", T0."DocCur", T0."Indicator", T0."CtlAccount", T0."GroupNum",T0."Comments",T0."SlpCode",T0."U_EXC_TVENTA", T0."U_EXC_AUTOD",T3."PymntGroup",
T1."U_EXC_FACTURA",T1."U_EXC_CONFI", T4."DocNum",T0."CardName",T0."DocEntry"
);

TVENTA=(
SELECT 
    T0."TableID", 
    T0."FieldID", 
    T0."AliasID", 
    T1."FldValue" AS "Valor", 
    T1."Descr" AS "Descripcion"
FROM 
    "CUFD" T0
INNER JOIN 
    "UFD1" T1 
ON 
    T0."TableID" = T1."TableID" 
    AND T0."FieldID" = T1."FieldID"
WHERE 
    T0."TableID" = 'ORDR' -- Tabla en la que está el campo de usuario
    AND T0."AliasID" = 'EXC_TVENTA' 
);

SELECT 
		'N' 	as "Slc",
		T0."DocDate"		as "Fecha",
		T0."Indicator"		as "TipoDoc",
		''		as "NroDoc",
		T0."DocNum" as "NroOV",
		T0."SeriesName"		as "Serie",
		TV."Descripcion"		as "TipoVen",
		TS."SlpName"		as "Vendedor",
		T0."CardCode"		as "RUC",
		T0."CardName"		as "RazonSocial",
		T0."Moneda"		as "Moneda",
		T0."DocTotal"		as "TotalDoc",
		''		as "Anticipo",
		'N'		as "GenAnti",
		T0."Comments"		as "Comentarios",
		''		as "FactReserva",
		''		as "TipoNC",
		''		as "SutentoNC",
		T0."Vendedor"		as "CodVendedor",
		T0."DocEntry"		as "KeyDoc",
		T0."U_EXC_TVENTA"		as "CodTVenta",
		T0."Series" as "CodSerie",
"DocNum",IFNULL("Series",83) AS "Series","DocType","Fecha de contabilización", --T0."TaxDate", T0."DocDueDate", 
"N° OC",  "CtlAccount", "Condicion Pago", 
"U_EXC_TVENTA", "U_EXC_AUTOD","FT Reserva", "FEX Forma Pago", "FEX Tipo Operacion"
FROM :TMP T0
INNER JOIN :TVENTA TV ON T0."U_EXC_TVENTA"=TV."Valor"
INNER JOIN OSLP TS ON T0."Vendedor"=TS."SlpCode"
WHERE "ROW_ID"=1
ORDER BY "DocNum" ASC;

	/*select distinct
		'N' 	as "Slc",
		''		as "Fecha",
		''		as "TipoDoc",
		''		as "NroDoc",
		''		as "NroOV",
		''		as "KeyDoc",
		''		as "Serie",
		''		as "TipoVen",
		''		as "Vendedor",
		''		as "RUC",
		''		as "RazonSocial",
		''		as "Moneda",
		''		as "TotalDoc",
		''		as "Anticipo",
		''		as "GenAnti",
		''		as "Comentarios",
		''		as "FactReserva",
		''		as "TipoNC",
		replicate(' ', 20) as "SutentoNC",
		''		as "CodVendedor"
	from
	dummy;
	/*select distinct
		'N' 				as "Slc"
		,T0."CardCode"		as "Fecha"
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
	and ifnull(T1."U_EXC_PELIC",'') like ifnull(:codPelicula,'%');*/
END;