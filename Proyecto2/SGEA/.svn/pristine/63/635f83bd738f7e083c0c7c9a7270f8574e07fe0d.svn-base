
-- DROP TABLE dbo.rol

CREATE TABLE rol (
	id bigserial NOT NULL,
	nombre varchar(50) NULL,
	idinstitucion int8 NOT NULL DEFAULT 1,
	fechaalta timestamp NOT NULL DEFAULT now(),
	ultmodificacion timestamp NOT NULL DEFAULT now(),
	esadministrador int4 NOT NULL DEFAULT 0,
	CONSTRAINT "PK_dbo.Rol" PRIMARY KEY (id),
	CONSTRAINT "FK_dbo.Rol_dbo.Institucion_InstitucionID" FOREIGN KEY (idinstitucion) REFERENCES institucion(id) ON DELETE CASCADE
);
CREATE INDEX "Rol_IX_InstitucionID" ON dbo.rol USING btree (idinstitucion);
