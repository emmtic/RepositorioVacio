/*
* Cita Medica Database
* @author Grupo 8
*/
create database libromedico1;
use libromedico1; 
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

insert into usuario (nombre_usuario,nombre,apellido,email,contraseña,es_admin,es_activo,fecha_alta) value ("admin","Emmanuel","Gimenez","Lemmanuel.gimenez@gmail.com","admin",1,1,NOW());


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
	fecha_alta datetime,
    es_activo boolean not null default 1
);

create table especialidad (
	id_especialidad int not null auto_increment primary key,
	especialidad varchar(200)
	);

insert into especialidad (especialidad) value ("ODONTOLOGIA");
insert into especialidad (especialidad) value ("ONCOLOGIA");
insert into especialidad (especialidad) value ("TRAUMATOLOGIA");


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
    es_activo boolean not null default 1,
	foreign key (id_especialidad) references especialidad(id_especialidad)
);

create table horariomedico(
	id_horariomedico int not null auto_increment primary key,
	id_medico int,
	id_dia int,
	horainicio_a varchar(100),
	horafin_a varchar(100),
    horainicio_b varchar(100),
	horafin_b varchar(100),
    duracion_turnos int,
	foreign key (id_dia) references dia(id_dia),
	foreign key (id_medico) references medico(id_medico)
);
create table dia(
	id_dia int not null auto_increment primary key,
	dia varchar (50)
);
insert into dia (dia) values ("Monday");
insert into dia (dia) values ("Tuesday");
insert into dia (dia) values ("Wednesday");
insert into dia (dia) values ("Thursday");
insert into dia (dia) values ("Friday");
insert into dia (dia) values ("Saturday");

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
    id_estado int not null default 1,
    es_activo boolean not null default 1,
	foreign key (id_pago) references pago(id_pago),
	foreign key (id_estado) references estado(id_estado),
	foreign key (id_usuario) references usuario(id_usuario),
	foreign key (id_paciente) references paciente(id_paciente),
	foreign key (id_medico) references medico(id_medico)
);