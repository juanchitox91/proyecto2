-- Drop table

-- DROP TABLE dbo.arancel

CREATE TABLE dbo.arancel (
	id serial NOT NULL,
	monto_inscripcion float8 NOT NULL,
	matricula_anual float8 NOT NULL,
	anho_lectivo int4 NOT NULL,
	observacion varchar(100) NULL,
	nombre_arancel varchar(50) NOT NULL,
	CONSTRAINT aracel_pk PRIMARY KEY (id)
);
COMMENT ON TABLE dbo.arancel IS 'Tabla de aranceles de una institucion';

-- Permissions

ALTER TABLE dbo.arancel OWNER TO postgres;
GRANT ALL ON TABLE dbo.arancel TO postgres;
