-- Drop table

-- DROP TABLE dbo.accionrol

CREATE TABLE accionrol (
	id serial NOT NULL,
	idaccion int4 NOT NULL,
	idrol int4 NOT NULL,
	fechaalta timestamp NOT NULL,
	ultmodificacion timestamp NOT NULL,
	activo bool NOT NULL DEFAULT true,
	CONSTRAINT accionrol_pkey PRIMARY KEY (id),
	CONSTRAINT accionrol_accion_fkey FOREIGN KEY (idaccion) REFERENCES dbo.accion(id),
	CONSTRAINT accionrol_rol_fkey FOREIGN KEY (idrol) REFERENCES dbo.rol(id)
);
