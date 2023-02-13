Create Database PruebaTecnica;
Create Table Quizzez(
	Id uuid primary key not null,
	Description varchar(50)
);
Create Table Quiz_Questions(
	Id serial primary key not null ,
	IdQuiz uuid,
	NumberQuestion double precision,
	LetterAnswer varchar(6)
);
Create Table Questions(
	Id serial primary key not null,
	NumberQuestion DOUBLE PRECISION,
	Description varchar(150),
	Type varchar(30)
);
Create Table Answers(
	Id serial primary key not null,
	Description Varchar(200),
	Letter char,
	NumberQuestion double precision,
	NextQuestion double precision
);



insert into Questions (numberquestion, description,type ) Values
(1,'Contestaré esta encuesta respecto a la experiencia de la empresa que represento:','bool'),
(2,'¿A qué sector se dedica la empresa?','single'),
(3, 'Número de empleados:','single'),
(4,'¿Este año habrá entrega de reparto de utilidades?','bool'),
(5,'En comparación con el año anterior ¿Cómo será el monto repartido de PTU?','single'),
(5.1,'¿Cuáles fueron las razones por las que el monto de reparto de utilidades fue mayor?','multiple'),
(5.2,'¿Cuáles fueron las razones por las que habrá menor reparto de utilidades?','multiple'),
(6,'Días de salario estimado a pagar por PTU ejercicio 2021','single'),
(7,'¿Cuáles fueron las razones por las que no hubo reparto de utilidades?','multiple'),
(8,'El año anterior, ¿hubo reparto de PTU?','bool');

insert into answers(description, letter, numberquestion,nextquestion) values
('Acepto','a',1,2),
('No Acepto','b',1,null),
('Agricultura, cría y explotación de animales, aprovechamiento forestal, pesca y caza','a',2,3),
('Minería','b',2,3),
('Generación, transmisión, distribución y comercialización de energía eléctrica, suministro de agua y de gas natural por ductos al consumidor final','c',2,3),
('Construcción','d',2,3),
('Industrias manufactureras','e',2,3),
('Comercio al por mayor','f',2,3),
('Comercio al por menor','g',2,3),
('Transportes, correos y almacenamiento','h',2,3),
('Información en medios masivos','i',2,3),
('Servicios financieros y de seguros','j',2,3),
('Servicios inmobiliarios y de alquiler de bienes muebles e intangibles','k',2,3),
('Servicios profesionales, científicos y técnicos','l',2,3),
('Corporativos','m',2,3),
('Servicios de apoyo a los negocios y manejo de residuos, y servicios de remediación','n',2,3),
('Servicios educativos','o',2,3),
('Servicios de salud y de asistencia social','p',2,3),
('Servicios de esparcimiento culturales y deportivos, y otros servicios recreativos','q',2,3),
('Servicios de alojamiento temporal y de preparación de alimentos y bebidas','r',2,3),
('Otros servicios excepto actividades gubernamentales','s',2,3),
('1 a 10','a',3,4),
('11 a 50','b',3,4),
('51 a 250','c',3,4),
('251 o más','d',3,4),
('Si','a',4,5),
('No','b',4,7),
('Mayor','a',5,5.1),
('Igual','b',5,6),
('Menor','c',5,5.2),
('Aumento en la productividad de las y los colaboradores','a',5.1,6),
('Aumento de ventas','b',5.1,6),
('Entorno económico favorable','c',5.1,6),
('Nuevas oportunidades de negocio','d',5.1,6),
('Disminución en la productividad de las y los colaboradores','a',5.2,6),
('Afectaciones por inseguridad','b',5.2,6),
('Afectaciones por el entorno político y económico','c',5.2,6),
('Continuación de la pandemia COVID-19','d',5.2,6),
('Problemas para mejorar la innovación y los procesos al interior de la empresa','e',5.2,6),
('Complicaciones por la inflación','f',5.2,6),
('Pregunta abierta (condicionado a número decimal)','g',5.2,6),
('Disminución en la productividad de las y los colaboradores','a',7,8),
('Afectaciones por inseguridad','b',7,8),
('Afectaciones por el entorno político y económico','c',7,8),
('Continuación de la pandemia COVID-19','d',7,8),
('Problemas para mejorar la innovación y los procesos al interior de la empresa','e',7,8),
('Complicaciones por la inflación','f',7,8),
('Si','a',8,null),
('No','b',8,null)


Select * from quiz_questions
Select * from quizzez