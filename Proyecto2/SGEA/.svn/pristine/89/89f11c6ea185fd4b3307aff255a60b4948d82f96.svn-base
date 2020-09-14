-- Drop table

-- DROP TABLE dbo.tipo_item

CREATE TABLE dbo.tipo_item (
	id serial NOT NULL,
	descripcion varchar(50) NOT NULL,
	institucionid int4 NOT NULL,
	CONSTRAINT tipo_item_pk PRIMARY KEY (id),
	CONSTRAINT tipo_item_institucion_fk FOREIGN KEY (institucionid) REFERENCES institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);