-- Drop table

-- DROP TABLE dbo.docente

CREATE TABLE docente (
	id serial NOT NULL,
	cedula int4 NOT NULL,
	nombre varchar(50) NOT NULL,
	apellido varchar(50) NOT NULL,
	telefono varchar(50) NOT NULL,
	direccion varchar(100) NOT NULL,
	observacion varchar(150) NULL,
	idinstitucion int4 NOT NULL DEFAULT 1,
	CONSTRAINT docente_pk PRIMARY KEY (id),
	CONSTRAINT docente_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);
