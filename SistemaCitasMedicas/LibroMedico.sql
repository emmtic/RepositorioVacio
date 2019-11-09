/*
* Cita Medica Database
* @author Grupo 8
*/
create database libromedico;
use libromedico; 
set sql_mode='';
create table usuario (
	id_usuario int not null auto_increment primary key,
	nombre_usuario varchar(50),
	nombre varchar(50),
	apellido varchar(50),
	email varchar(255),
	contraseña varchar(60),
    es_activo boolean not null default 1,
	es_admin boolean not null default 0,
	fecha_alta datetime
);

insert into usuario (nombre_usuario,contraseña,es_admin,es_activo,fecha_alta) value ("admin",sha1(md5("admin")),1,1,NOW());


create table paciente (
	id_paciente int not null auto_increment primary key,
	dni varchar(50),
	nombre varchar(50),
	apellido varchar(50),
	genero varchar(1),
	fecha_nac date,
	email varchar(255),
	direccion varchar(255),
	telefono varchar(255),
	enfermedad varchar(500),
	medicamentos varchar(500),
	alergia varchar(500),
	fecha_alta datetime
);

create table especialidad (
	id_especialidad int not null auto_increment primary key,
	especialidad varchar(200)
	);

insert into especialidad (especialidad) value ("ODONTOLOGIA");


create table medico (
	id_medico int not null auto_increment primary key,
	dni varchar(50),
    matricula varchar(50),
	nombre varchar(50),
	apellido varchar(50),
	genero varchar(1),
	fecha_nac date,
	email varchar(255),
	direccion varchar(255),
	telefono varchar(255),
	fecha_alta datetime,
	id_especialidad int,
	foreign key (id_especialidad) references especialidad(id_especialidad)
);



create table estado (
	id_estado int not null auto_increment primary key,
	estado varchar(100)
);

insert into estado (id_estado,estado) values (1,"Pendiente"), (2,"Aplicada"),(3,"No asistio"),(4,"Cancelada");

create table pago (
	id_pago int not null auto_increment primary key,
	estado_pago varchar(100)
);

insert into pago (id_pago,estado_pago) values  (1,"Pendiente"),(2,"Pagado"),(3,"Anulado");

create table reservacion(
	id_reservacion int not null auto_increment primary key,
	asunto_cita varchar(100),
	observaciones text,
	mensaje text,
	fecha_cita varchar(50),
	hora_cita varchar(50),
	fecha_alta datetime,
	id_paciente int,
	sintomas text,
	enfermedad text,
	medicamentos text,
	id_usuario int,
	id_medico int,
	precio double,
	id_pago int not null default 1,
	foreign key (id_pago) references pago(id_pago),
	id_estado int not null default 1,
	foreign key (id_estado) references estado(id_estado),
	foreign key (id_usuario) references usuario(id_usuario),
	foreign key (id_paciente) references paciente(id_paciente),
	foreign key (id_medico) references medico(id_medico)
);