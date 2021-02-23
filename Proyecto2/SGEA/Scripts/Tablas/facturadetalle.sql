-- Drop table

-- DROP TABLE dbo.facturadetalle

CREATE TABLE dbo.facturadetalle (
	id int8 NOT NULL DEFAULT 'facturadetalle_id_seq'::regclass,
	id_facturacabecera int8 NOT NULL,
	id_pagare int8 NOT NULL,
	monto float8 NOT NULL,
	descripcion varchar(50) NOT NULL,
	CONSTRAINT facturadetalle_pk PRIMARY KEY (id),
	CONSTRAINT facturadetalle_factura_cabecera_fk FOREIGN KEY (id_facturacabecera) REFERENCES factura_cabecera(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT facturadetalle_pagare_fk FOREIGN KEY (id_pagare) REFERENCES pagare(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);

-- Permissions

ALTER TABLE dbo.facturadetalle OWNER TO postgres;
GRANT ALL ON TABLE dbo.facturadetalle TO postgres;
