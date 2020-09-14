-- Drop table

-- DROP TABLE dbo.item_planilla

CREATE TABLE itemplanilla (
	id serial NOT NULL,
	idtipoitem int4 NOT NULL,
	idsubunidad int4 NOT NULL,
	descripcion varchar(250) NOT NULL,
	puntajemaximo int4 NOT NULL,
	fechaevaluacion timestamp NOT NULL,
	idinstitucion int4 NOT NULL,
	CONSTRAINT item_planilla_pk PRIMARY KEY (id),
	CONSTRAINT item_planilla_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES dbo.institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT item_planilla_subunidad_fk FOREIGN KEY (idsubunidad) REFERENCES dbo.subunidad(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT item_planilla_tipo_item_fk FOREIGN KEY (idtipoitem) REFERENCES dbo.tipo_item(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);