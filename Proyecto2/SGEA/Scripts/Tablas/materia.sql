-- Drop table

-- DROP TABLE dbo.materia

CREATE TABLE materia (
	id serial NOT NULL,
	nombre_materia varchar(100) NOT NULL,
	observacion varchar(150) NULL,
	fecha_alta timestamp NOT NULL DEFAULT now(),
	idinstitucion int4 NOT NULL DEFAULT 1,
	CONSTRAINT materia_pk PRIMARY KEY (id)
);
