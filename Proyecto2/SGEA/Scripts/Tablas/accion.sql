-- Drop table

-- DROP TABLE dbo.accion

CREATE TABLE accion (
	id serial NOT NULL,
	nombre varchar(100) NULL,
	fechaalta timestamp NOT NULL,
	ultmodificacion timestamp NOT NULL,
	descripcion varchar NOT NULL DEFAULT 'text'::character varying,
	parent_id int4 NOT NULL DEFAULT 1,
	CONSTRAINT accion_pkey PRIMARY KEY (id)
);
