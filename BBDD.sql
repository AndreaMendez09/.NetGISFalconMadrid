DROP TABLE IF EXISTS PersonaSistema CASCADE;
DROP TABLE IF EXISTS Usuarios CASCADE;
DROP TABLE IF EXISTS Personas CASCADE;
DROP TABLE IF EXISTS Zona CASCADE;
DROP TABLE IF EXISTS Operadores_zona CASCADE;
DROP TABLE IF EXISTS Peticion CASCADE;
DROP TABLE IF EXISTS Peticion_operadores CASCADE;
DROP TABLE IF EXISTS Estado CASCADE;
DROP TABLE IF EXISTS Historico_estado CASCADE;
DROP TABLE IF EXISTS Direccion CASCADE;
DROP TABLE IF EXISTS Nivel CASCADE;
DROP TABLE IF EXISTS Respuesta CASCADE;


CREATE TABLE Personas(
    nombre varchar(20) not null,
    apellido varchar(20) not null,
    email varchar(20) not null unique,
    genero char(5) not null,
    idioma varchar(20) not null,
    contraseña varchar(255)not null,
    foto varchar(20)
    );
CREATE TABLE PersonaSistema(
	id_personasistema SERIAL primary key,
    rol INT,
    zona VARCHAR(10)
) INHERITS (Personas);

CREATE TABLE Usuarios(
	id_usuario SERIAL primary key,
    municipio VARCHAR(20),
    fecha_nacimiento DATE
) INHERITS (Personas);

CREATE TABLE Zona(
    id_zona SERIAL PRIMARY KEY,
    nombre_zona varchar(20) not null,
    descripcion_zona varchar (100) not null,
    geometria_zona varchar(255) not null
);

CREATE TABLE Operadores_zona (
    operador int not null REFERENCES PersonaSistema(id_personasistema),
    zona int not null REFERENCES Zona(id_zona),
    PRIMARY KEY (operador,zona)
);

CREATE TABLE Peticion (
    id_peticion SERIAL PRIMARY KEY,
    fecha_creacion date not null, 
    localizacion_peticion varchar(255) not null,
    usuario int not null, 
    FOREIGN KEY (usuario) REFERENCES Usuarios(id_usuario)
);

CREATE TABLE Peticion_operadores (
    operador int not null,
    peticion int not null, 
    PRIMARY KEY (operador,peticion),
    FOREIGN KEY (operador) REFERENCES PersonaSistema(id_personasistema),
    FOREIGN KEY (peticion) REFERENCES Peticion(id_peticion)
);

CREATE TABLE Estado(
    id_estado SERIAL PRIMARY KEY, 
    nombre_estado varchar(20) not null, 
    EsFinal boolean not null,
    color_estado varchar(5)
);

CREATE TABLE Historico_estado (
    fecha_modificacion date not null, 
    estado int not null, 
    peticion int not null,
    operador int not null, 
    PRIMARY KEY (operador,peticion,estado),
    FOREIGN KEY (operador) REFERENCES PersonaSistema(id_personasistema),
    FOREIGN KEY (peticion) REFERENCES Peticion(id_peticion),
    FOREIGN KEY (estado) REFERENCES Estado(id_estado)
);

CREATE TABLE Direccion (
    id_direccion SERIAL PRIMARY KEY, 
    tipo_via varchar(50),
    nombre_via varchar (70) not null,
    ciudad varchar(25) not null,
    piso varchar(25),
    escalera varchar(25),
    numero varchar(25) not null,
    cod_postal varchar(15),
    pais varchar(25) not null, 
    notas varchar(255)
);

CREATE TABLE Nivel (
    id_pregunta SERIAL PRIMARY KEY, 
    descripcion_pregunta varchar (100) not null, 
    nivel smallint not null
);

CREATE TABLE Respuesta (
    id_respuesta SERIAL PRIMARY KEY,
    cuerpo_respuesta varchar(25) not null,
    principal boolean not null, 
    nivel int not null,
    FOREIGN KEY (nivel) REFERENCES Nivel(id_pregunta)
);
