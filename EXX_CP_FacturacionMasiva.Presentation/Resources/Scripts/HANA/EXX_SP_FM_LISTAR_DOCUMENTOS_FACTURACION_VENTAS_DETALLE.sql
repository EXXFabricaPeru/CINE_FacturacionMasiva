CREATE PROCEDURE EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_VENTAS_DETALLE
(
	IN KeyDoc varchar(50),
	IN TipoDoc varchar(50)
)
LANGUAGE SQLSCRIPT
AS
BEGIN

SELECT T1."DocNum", 
CAST (IFNULL(T0."VisOrder",'0') AS NVARCHAR(10)) AS "Linea", 
T0."ItemCode",
T0."Dscription",
T0."Quantity",
T0."Price",
T0."DiscPrcnt",
T0."PriceAfVAT",
CASE T1."DocCur" WHEN 'SOL' THEN T0."LineTotal" ELSE T0."TotalFrgn" END  as "LineTotal",
CASE T1."DocCur" WHEN 'SOL' THEN T0."VatSum" ELSE T0."VatSumFrgn" END as "VatSum",  
T0."AcctCode",
 T0."TaxCode",
 CASE T1."Indicator" WHEN '01' THEN T0."U_EXX_GRUPODET" ELSE '999' END AS "GrupoDetraccion",
'01' AS "FEXTipoVenta", 
CASE T1."U_EXC_TVENTA" WHEN 'EX' THEN '40' ELSE '10' END AS "FEXTipoAfectacion",
T0."OcrCode" as "Complejo",
T0."OcrCode2" as "Area",
T0."OcrCode3" as "LineaProducto", 
T0."U_EXC_CONFI",
 T0."U_EXC_FACTURA",
 T0."U_EXC_TARPRO",
T0."DocEntry" as "DocEntryBase",
 CAST (IFNULL(T0."LineNum",'0') AS NVARCHAR(10)) AS "LineaBase",
T0."ObjType" as "ObjBase",
T0."U_EXX_SERVENTA",
TO_NVARCHAR(TO_DATE(T1."DocDate"),'YYYYMMDD') AS "DocDate",
T0."U_EXX_FFNC",
T0."U_EXX_FINC"
--CASE T0."U_EXC_CONFI" WHEN 'N' THEN 'O' ELSE 'C' END AS "LineStatus"
FROM RDR1 T0  
INNER JOIN ORDR T1 ON T0."DocEntry" = T1."DocEntry" 
WHERE T0."U_EXC_FACTURA" ='Y' AND T0."LineStatus"='O' AND T0."DocEntry"=:KeyDoc
ORDER BY T1."DocNum", T0."LineNum" ASC;

END;