-- Drop table

-- DROP TABLE dbo.grupofamiliar

CREATE TABLE grupofamiliar (
	id serial NOT NULL,
	apellidos varchar(50) NOT NULL,
	observacion varchar(150) NULL,
	fecha_alta timestamp NOT NULL DEFAULT now(),
	idinstitucion int4 NOT NULL DEFAULT 1,
	CONSTRAINT grupofamiliar_pk PRIMARY KEY (id),
	CONSTRAINT grupofamiliar_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);
