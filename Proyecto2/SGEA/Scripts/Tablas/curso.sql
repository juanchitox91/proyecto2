-- Drop table

-- DROP TABLE dbo.curso

CREATE TABLE dbo.curso (
	id serial NOT NULL,
	curso int4 NOT null,
	seccion varchar(15) NOT NULL,
	turno varchar(15) NOT NULL,
	nombrecurso varchar(50) NOT NULL,
	observacion varchar(150) NULL,
	fecha_alta timestamp NOT NULL DEFAULT now(),
	idinstitucion int4 NOT NULL DEFAULT 1,
	CONSTRAINT curso_pk PRIMARY KEY (id)
);
