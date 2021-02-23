-- Drop table

-- DROP TABLE dbo.evaluacion

CREATE TABLE evaluacion (
	id serial NOT NULL,
	iditem int4 NOT NULL,
	idalumno int4 NOT NULL,
	puntajealcanzado int4 NOT NULL,
	CONSTRAINT evaluacion_pk PRIMARY KEY (id),
	CONSTRAINT evaluacion_alumno_fk FOREIGN KEY (idalumno) REFERENCES alumno(id) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT evaluacion_itemplanilla_fk FOREIGN KEY (iditem) REFERENCES itemplanilla(id) ON UPDATE RESTRICT ON DELETE RESTRICT
);