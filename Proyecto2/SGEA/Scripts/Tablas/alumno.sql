-- Drop table

-- DROP TABLE dbo.alumno

CREATE TABLE dbo.alumno (
	id serial NOT NULL,
	cedula int4 NOT NULL,
	nombre varchar(50) NOT NULL,
	apellido varchar(50) NOT NULL,
	telefono varchar(20) NOT NULL,
	persona_contacto1 varchar(70) NOT NULL,
	telefono_contacto1 varchar(20) NOT NULL,
	persona_contacto2 varchar(70) NULL,
	telefono_contacto2 varchar(20) NULL,
	direccion varchar(100) NOT NULL,
	observacion varchar(250) NULL,
	idgrupofamiliar int4 NULL,
	fecha_alta date NOT NULL,
	ult_modificacion date NOT NULL,
	idinstitucion int4 NOT NULL DEFAULT 1, -- id de la institución
	CONSTRAINT alumno_pk PRIMARY KEY (id),
	CONSTRAINT alumno_un UNIQUE (cedula),
	CONSTRAINT alumno_grupo_familiar_fk FOREIGN KEY (idgrupofamiliar) REFERENCES dbo.grupo_familiar(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT alumno_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES dbo.institucion(id) ON DELETE CASCADE
);
CREATE UNIQUE INDEX alumno_cedula_idx ON dbo.alumno USING btree (cedula);

-- Column comments

COMMENT ON COLUMN dbo.alumno.idinstitucion IS 'id de la institución';

-- Permissions

ALTER TABLE dbo.alumno OWNER TO postgres;
GRANT ALL ON TABLE dbo.alumno TO postgres;
