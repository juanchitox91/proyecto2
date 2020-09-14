-- Drop table

-- DROP TABLE dbo.unidad

CREATE TABLE unidad (
	id serial NOT NULL,
	titulo varchar(50) NOT NULL,
	descripcion varchar(250) NOT NULL,
	idplanilla int4 NOT NULL,
	idinstitucion int4 NOT NULL,
	CONSTRAINT unidad_pk PRIMARY KEY (id),
	CONSTRAINT unidad_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES dbo.institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT unidad_planilla_fk FOREIGN KEY (idplanilla) REFERENCES dbo.planilla(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);

