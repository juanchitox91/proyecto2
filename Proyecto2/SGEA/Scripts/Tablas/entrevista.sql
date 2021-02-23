-- Drop table

-- DROP TABLE dbo.entrevista

CREATE TABLE entrevista (
	id serial NOT NULL,
	idinscripcion int4 NOT NULL,
	motivo varchar(100) NOT NULL,
	acuerdo varchar(500) NOT NULL,
	sugerencia varchar(500) NULL,
	fecha date NOT NULL DEFAULT now(),
	CONSTRAINT entrevista_pk PRIMARY KEY (id),
	CONSTRAINT entrevista_alumno_fk FOREIGN KEY (id) REFERENCES alumno(id),
	CONSTRAINT entrevista_inscripcion_fk FOREIGN KEY (idinscripcion) REFERENCES inscripcion(id)
);
