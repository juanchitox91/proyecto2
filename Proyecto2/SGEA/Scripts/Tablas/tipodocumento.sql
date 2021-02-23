-- Drop table

-- DROP TABLE dbo.tipo_documento

CREATE TABLE dbo.tipo_documento (
	id bigserial NOT NULL,
	nombre_documento varchar(30) NOT NULL,
	observacion varchar(50) NOT NULL,
	CONSTRAINT tipo_documento_pk PRIMARY KEY (id)
);

-- Permissions

ALTER TABLE dbo.tipo_documento OWNER TO postgres;
GRANT ALL ON TABLE dbo.tipo_documento TO postgres;
