-- Drop table

-- DROP TABLE dbo.tipopago

CREATE TABLE dbo.tipopago (
	id int8 NOT NULL,
	descripcion varchar(30) NOT NULL,
	observacion varchar(50) NULL,
	CONSTRAINT tipopago_pk PRIMARY KEY (id)
);

-- Permissions

ALTER TABLE dbo.tipopago OWNER TO postgres;
GRANT ALL ON TABLE dbo.tipopago TO postgres;
