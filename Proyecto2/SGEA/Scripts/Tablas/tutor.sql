-- Drop table

-- DROP TABLE dbo.tutor

CREATE TABLE tutor (
	id serial NOT NULL,
	cedula int4 NOT NULL,
	nombre varchar NOT NULL,
	apellido varchar(50) NOT NULL,
	telefono varchar(20) NOT NULL,
	telefono_2 varchar(20) NOT NULL,
	direccion varchar NOT NULL,
	observacion varchar(250) NULL,
	fecha_alta date NULL,
	ult_modificacion date NOT NULL,
	idinstitucion int4 NOT NULL,
	CONSTRAINT tutor_pk PRIMARY KEY (id),
	CONSTRAINT tutor_un UNIQUE (cedula),
	CONSTRAINT tutor_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES institucion(id)
);
CREATE INDEX tutor_cedula_idx ON dbo.tutor USING btree (cedula);
