-- DROP TABLE dbo.factura_cabecera

CREATE TABLE dbo.factura_cabecera (
	id bigserial NOT NULL,
	razon_social varchar(50) NOT NULL,
	nro_factura varchar(20) NOT NULL,
	fecha date NOT NULL DEFAULT now(),
	nro_documento varchar(20) NOT NULL,
	id_tipodocumento int8 NOT NULL,
	id_tipopago int8 NOT NULL,
	CONSTRAINT factura_cabecera_pk PRIMARY KEY (id),
	CONSTRAINT factura_cabecera_tipo_documento_fk FOREIGN KEY (id_tipodocumento) REFERENCES tipo_documento(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT factura_cabecera_tipopago_fk FOREIGN KEY (id_tipopago) REFERENCES tipopago(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);

-- Permissions

ALTER TABLE dbo.factura_cabecera OWNER TO postgres;
GRANT ALL ON TABLE dbo.factura_cabecera TO postgres;
