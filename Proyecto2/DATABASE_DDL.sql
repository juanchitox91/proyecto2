
--SCRIPT DE CREACION DE TABLAS PARA SGEA

CREATE TABLE grupofamiliar(
 id serial PRIMARY KEY,
 apellidos VARCHAR (50) NOT NULL,  --UNIQUE
 observacion VARCHAR (100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL

);

CREATE TABLE alumno(
 id serial PRIMARY KEY,
 cedula INTEGER NOT NULL,
 nombres VARCHAR (50) NOT NULL, 
 apellidos VARCHAR (50) NOT NULL,
 telefono VARCHAR (50) NOT NULL,
 personacontacto VARCHAR(50) NOT NULL,
 telefonocontacto VARCHAR(50) NOT NULL,
 direccion VARCHAR (100) NOT NULL,
 observacion VARCHAR (100),
 idgrupofamiliar serial ,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT alumno_grupofamiliar_fkey FOREIGN KEY (idgrupofamiliar)
      REFERENCES grupofamiliar (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
);

CREATE TABLE curso(
 id serial PRIMARY KEY,
 curso INTEGER NOT NULL,
 seccion VARCHAR (5) NOT NULL, 
 turno VARCHAR (10) NOT NULL,
 nombrecurso VARCHAR (50) NOT NULL,
 observacion VARCHAR (100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE arancel(
 id serial PRIMARY KEY,
 montoinscripcion BIGINT NOT NULL,
 matriculaanual BIGINT NOT NULL, 
 anholectivo INTEGER NOT NULL,
 observacion VARCHAR (100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE inscripcion(
 id serial PRIMARY KEY,
 nrocomprobante INTEGER NOT NULL,
 idalumno serial NOT NULL, 
 idcurso serial NOT NULL,
 anho smallint NOT NULL,
 mesdesde VARCHAR(15) NOT NULL,
 meshasta VARCHAR(15) NOT NULL,
 fechainscripcion TIMESTAMP NOT NULL,
 estado VARCHAR (5),
 cantidadcuotas smallint NOT NULL,
 idarancel serial NOT NULL,
 --fechaalta TIMESTAMP NOT NULL, /*EN LUGAR DE FECHA ALTA QUEDA FECHA INSCRIPCION*/
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT inscripcion_alumno_fkey FOREIGN KEY (idalumno)
      REFERENCES alumno (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT inscripcion_curso_fkey FOREIGN KEY (idcurso)
      REFERENCES curso (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT inscripcion_arancel_fkey FOREIGN KEY (idarancel)
      REFERENCES arancel (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE docente(
 id serial PRIMARY KEY,
 cedula INTEGER NOT NULL,
 nombres VARCHAR (50),
 apellidos VARCHAR (50),
 telefono VARCHAR (15),
 direccion VARCHAR (100),
 observacion VARCHAR (100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE materia(
 id serial PRIMARY KEY,
 nombremateria VARCHAR (100),
 observacion VARCHAR (100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE planilla(
 id serial PRIMARY KEY,
 idcurso serial NOT NULL,
 iddocente serial NOT NULL,
 idmateria serial NOT NULL, 
 estado VARCHAR(5) NOT NULL,
 anho smallint NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT planilla_materia_fkey FOREIGN KEY (idmateria)
      REFERENCES materia (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT planilla_curso_fkey FOREIGN KEY (idcurso)
      REFERENCES curso (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT planilla_docente_fkey FOREIGN KEY (iddocente)
      REFERENCES docente (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

/*verificar si esto no se puede relacionar con inscripcion en lugar de alumno, para tener mas datos de la entrevista!*/
CREATE TABLE entrevista(
 id serial PRIMARY KEY,
 idalumno serial NOT NULL, 
 motivo VARCHAR(100) NOT NULL,
 acuerdo VARCHAR(500) NOT NULL,
 sugerencia VARCHAR(100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT entrevista_alumno_fkey FOREIGN KEY (idalumno)
      REFERENCES alumno (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE pagare(
 id serial PRIMARY KEY,
 idinscripcion serial NOT NULL,
 tipo VARCHAR(5) NOT NULL,
 fechavencimiento TIMESTAMP NOT NULL,
 estado VARCHAR(5) NOT NULL,
 nrofactura VARCHAR(20) NOT NULL,
 monto BIGINT NOT NULL,
 fechapago TIMESTAMP NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT pagare_inscripcion_fkey FOREIGN KEY (idinscripcion)
      REFERENCES inscripcion (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE asistencia(
 idplanilla serial NOT NULL,
 idalumno serial NOT NULL,
 fecha TIMESTAMP NOT NULL,
 presente BOOLEAN NOT NULL,
 observacion VARCHAR(50) NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT asistencia_pkey PRIMARY KEY (idplanilla, idalumno, fecha),
  CONSTRAINT asistencia_alumno_fkey FOREIGN KEY (idalumno)
      REFERENCES alumno (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT asistencia_planilla_fkey FOREIGN KEY (idplanilla)
      REFERENCES planilla (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE unidad(
 id serial PRIMARY KEY,
 idplanilla serial NOT NULL,
 titulo VARCHAR(20) NOT NULL,
 descripcion VARCHAR(100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT unidad_planilla_fkey FOREIGN KEY (idplanilla)
      REFERENCES planilla (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE subunidad(
 id serial PRIMARY KEY,
 idunidad serial NOT NULL,
 titulo VARCHAR(20) NOT NULL,
 descripcion VARCHAR(100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT subunidad_unidad_fkey FOREIGN KEY (idunidad)
      REFERENCES unidad (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE tipoitem(
 id serial PRIMARY KEY,
 descripcion VARCHAR(100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE itemplanilla(
 id serial PRIMARY KEY,
 idsubunidad serial NOT NULL,
 idtipoitem serial NOT NULL,
 descripcion VARCHAR(100),
 puntajemaximo smallint not null,
 fechaevaluacion TIMESTAMP NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT itmplanilla_subunidad_fkey FOREIGN KEY (idsubunidad)
      REFERENCES subunidad (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT itmeplanilla_tipoitem_fkey FOREIGN KEY (idtipoitem)
      REFERENCES tipoitem (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE programa(
 id serial PRIMARY KEY,
 idsubunidad serial NOT NULL,
 descripcion VARCHAR(100),
 fechaeestimativainicio TIMESTAMP NOT NULL,
 fechaestimativafin TIMESTAMP NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT programa_subunidad_fkey FOREIGN KEY (idsubunidad)
      REFERENCES subunidad (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE rol(
 id serial PRIMARY KEY,
 nombre VARCHAR(100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE accion(
 id serial PRIMARY KEY,
 nombre VARCHAR(100),
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL
);

CREATE TABLE accionrol(
 id serial PRIMARY KEY,
 idaccion serial NOT NULL,
 idrol serial NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT accionrol_accion_fkey FOREIGN KEY (idaccion)
      REFERENCES accion (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT accionrol_rol_fkey FOREIGN KEY (idrol)
      REFERENCES rol (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE accionrol(
 id serial PRIMARY KEY,
 idaccion serial NOT NULL,
 idrol serial NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT accionrol_accion_fkey FOREIGN KEY (idaccion)
      REFERENCES accion (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT accionrol_rol_fkey FOREIGN KEY (idrol)
      REFERENCES rol (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE usuario(
 id serial PRIMARY KEY,
 cedula INTEGER NOT NULL UNIQUE,
 nombre VARCHAR(50) NOT NULL,
 apellido VARCHAR(50) NOT NULL,
 idrol serial NOT NULL,
 fechaalta TIMESTAMP NOT NULL,
 ultmodificacion TIMESTAMP NOT NULL,
  CONSTRAINT usuario_rol_fkey FOREIGN KEY (idrol)
      REFERENCES rol (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);
