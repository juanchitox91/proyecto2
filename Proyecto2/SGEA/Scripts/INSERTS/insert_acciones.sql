﻿--OJO! AGREGAR SIEMPRE LOS INSERTS EN ORDEN NUMERICO
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(1,'Administrador',now(),now(),'Sección Administrador',0);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(2, 'verRoles',now(),now(),'Ver Roles',1);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(3,'crearRol',now(),now(),'Crear Roles',2);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(4,'editarRol',now(),now(),'Editar Roles',2);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(5,'eliminarRol',now(),now(),'Eliminar Roles',2);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(6,'detalleRol',now(),now(),'Ver Detalle Rol',2);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(7, 'asignarAccion',now(),now(),'Asignar Acción a Rol',1);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(8,'verUsuarios',now(),now(),'Ver Usuarios',1);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(9,'crearUsuario',now(),now(),'Crear Usuarios',8);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(10,'editarUsuario',now(),now(),'Editar Usuarios',8);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(11,'eliminarUsuario',now(),now(),'Elimiar Usuarios',8);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(12,'detalleUsuario',now(),now(),'Ver Detalle Usuario',8);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(13,'Datos',now(),now(),'Datos',0);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(14,'verAlumnos',now(),now(),'Ver Alumnos',13);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(15,'crearAlumno',now(),now(),'Crear Alumnos',14);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(16,'editarAlumno',now(),now(),'Editar Alumnos',14);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(17,'eliminarAlumno',now(),now(),'Eliminar Alumnos',14);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(18,'detalleAlumno',now(),now(),'Ver Detalle Alumno',14);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(19,'verAranceles',now(),now(),'Ver Aranceles',13);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(20,'crearArancel',now(),now(),'Crear Alumnos',19);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(21,'editarArancel',now(),now(),'Editar Alumnos',19);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(22,'eliminarArancel',now(),now(),'Eliminar Alumnos',19);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(23,'detalleArancel',now(),now(),'Ver Detalle Alumno',19);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(24,'verCursos',now(),now(),'Ver Cursos',13);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(25,'crearCurso',now(),now(),'Crear Cursos',24);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(26,'editarCurso',now(),now(),'Editar Cursos',24);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(27,'eliminarCurso',now(),now(),'Eliminar Cursos',24);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(28,'detalleCurso',now(),now(),'Ver Detalle Curso',24);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(29,'verMaterias',now(),now(),'Ver Materias',13);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(30,'crearMateria',now(),now(),'Crear Materia',29);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(31,'editarMateria',now(),now(),'Editar Materia',29);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(32,'eliminarMateria',now(),now(),'Eliminar Materia',29);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(33,'detalleMateria',now(),now(),'Ver Detalle Materia',29);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(34,'verDocentes',now(),now(),'Ver Docentes',13);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(35,'crearDocente',now(),now(),'Crear Docente',34);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(36,'editarDocente',now(),now(),'Editar Docente',34);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(37,'eliminarDocente',now(),now(),'Eliminar Docente',34);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(38,'detalleDocente',now(),now(),'Ver Detalle Docente',34);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(39,'verGrupoFamiliar',now(),now(),'Ver Docentes',13);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(40,'crearGrupoFamiliar',now(),now(),'Crear Docente',39);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(41,'editarGrupoFamiliar',now(),now(),'Editar Docente',39);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(42,'eliminarGrupoFamiliar',now(),now(),'Eliminar Docente',39);
INSERT INTO dbo.accion (id,nombre,fechaalta,ultmodificacion,descripcion,parent_id) VALUES 
(43,'detalleGrupoFamiliar',now(),now(),'Ver Detalle Docente',39);