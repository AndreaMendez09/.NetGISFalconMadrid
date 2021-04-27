CREATE DATABASE "GIS_Falcon";
DROP TABLE IF EXISTS PersonaSistema;
DROP TABLE IF EXISTS Usuarios;
DROP TABLE IF EXISTS Personas;

CREATE TABLE Personas(
    id_persona SERIAL PRIMARY KEY,
    nombre varchar(20) not null,
    apellido varchar(20) not null,
    email varchar(20) not null,
    genero char(5) not null,
    idioma varchar(20) not null,
    contraseña varchar(255)not null,
    foto varchar(20)
    );
CREATE TABLE PersonaSistema(
    rol INT,
    zona VARCHAR(10)
) INHERITS (Personas);

CREATE TABLE Usuarios(
    municipio VARCHAR(20),
    fecha_nacimiento DATE

) INHERITS (Personas);