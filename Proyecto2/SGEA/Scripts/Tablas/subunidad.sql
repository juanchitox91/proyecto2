-- Drop table

-- DROP TABLE dbo.subunidad

CREATE TABLE subunidad (
	id serial NOT NULL,
	idunidad int4 NOT NULL,
	titulo varchar(50) NOT NULL,
	descripcion varchar(250) NOT NULL,
	idinstitucion serial NOT NULL,
	CONSTRAINT subunidad_pk PRIMARY KEY (id),
	CONSTRAINT subunidad_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES dbo.institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT subunidad_unidad_fk FOREIGN KEY (idunidad) REFERENCES dbo.unidad(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);
