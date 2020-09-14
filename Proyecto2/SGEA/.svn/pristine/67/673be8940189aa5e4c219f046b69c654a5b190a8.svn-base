-- Drop table

-- DROP TABLE dbo.planilla

CREATE TABLE planilla (
	id serial NOT NULL,
	idcurso int8 NOT NULL,
	idmateria int4 NOT NULL,
	iddocente int4 NOT NULL,
	estado varchar(1) NOT NULL DEFAULT 'A'::character varying,
	anho_lectivo int4 NOT NULL,
	idinstitucion int4 NOT NULL,
	titulo varchar(50) NOT NULL,
	descripcion varchar(250) NOT NULL,
	CONSTRAINT planilla_check CHECK ((((estado)::text = 'A'::text) OR ((estado)::text = 'I'::text))),
	CONSTRAINT planilla_pk PRIMARY KEY (id),
	CONSTRAINT planilla_curso_fk FOREIGN KEY (idcurso) REFERENCES dbo.curso(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT planilla_docente_fk FOREIGN KEY (iddocente) REFERENCES dbo.docente(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT planilla_institucion_fk FOREIGN KEY (idinstitucion) REFERENCES dbo.institucion(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT planilla_materia_fk FOREIGN KEY (idmateria) REFERENCES dbo.materia(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);