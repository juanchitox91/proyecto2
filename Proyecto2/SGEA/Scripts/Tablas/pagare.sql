-- Drop table

-- DROP TABLE dbo.pagare

CREATE TABLE pagare (
	id serial NOT NULL,
	idinscripcion int4 NOT NULL,
	tipopagare varchar NULL,
	fechavencimiento date NULL,
	estado varchar NOT NULL,
	monto float8 NOT NULL,
	fechapago date NULL,
	descripcion varchar NOT NULL,
	CONSTRAINT newtable_pk PRIMARY KEY (id),
	CONSTRAINT newtable_inscripcion_fk FOREIGN KEY (idinscripcion) REFERENCES inscripcion(id)
);
