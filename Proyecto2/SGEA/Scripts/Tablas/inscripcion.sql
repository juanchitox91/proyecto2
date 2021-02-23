-- Drop table

-- DROP TABLE dbo.inscripcion

CREATE TABLE inscripcion (
	id serial NOT NULL,
	idalumno int4 NOT NULL,
	idcurso int4 NOT NULL,
	anho varchar NOT NULL,
	mesdesde int4 NOT NULL,
	meshasta int4 NOT NULL,
	fecha_inscripcion date NOT NULL DEFAULT now(),
	estado varchar NOT NULL DEFAULT 'A'::character varying,
	cantidad_cuotas int4 NOT NULL,
	idarancel int4 NOT NULL,
	idinstitucion int4 NOT NULL,
	nrocomprobante varchar NOT NULL,
	CONSTRAINT inscripcion_check PRIMARY KEY (id),
	CONSTRAINT inscripcion_check1 CHECK (((mesdesde > 0) AND (mesdesde < 13))),
	CONSTRAINT inscripcion_check2 CHECK (((meshasta > 0) AND (meshasta < 13))),
	CONSTRAINT inscripcion_check3 CHECK (((cantidad_cuotas > 0) AND (cantidad_cuotas < 11))),
	CONSTRAINT inscripcion_un UNIQUE (idalumno, idcurso, anho),
	CONSTRAINT inscripcion_alumno_fk FOREIGN KEY (idalumno) REFERENCES alumno(id),
	CONSTRAINT inscripcion_arancel_fk FOREIGN KEY (idarancel) REFERENCES arancel(id),
	CONSTRAINT inscripcion_curso_fk FOREIGN KEY (idcurso) REFERENCES curso(id),
	CONSTRAINT inscripcion_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES institucion(id)
);
