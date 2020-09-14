-- Drop table

-- DROP TABLE dbo.institucion

CREATE TABLE institucion (
	id int8 NOT NULL,
	nombre varchar(50) NULL,
	direccion varchar(50) NULL,
	ciudad varchar(50) NULL,
	departamento varchar(50) NULL,
	telefono varchar(20) NULL,
	CONSTRAINT "PK_dbo.Institucion" PRIMARY KEY (id)
);
