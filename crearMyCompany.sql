create database MyCompany
go
use MyCompany
go

create table Users(
UserID int identity (1,1) primary key,
LoginName nvarchar (100) unique not null,
Password nvarchar (100)  not null,
FirstName nvarchar (100)  not null,
LastName nvarchar (100)  not null,
Position nvarchar (100)  not null,
Email nvarchar (150)  not null,
)

insert into Users values ('OscarPollo','ppoolloo','Oscar','Lopez','ADM','A325490@alumnos.uaslp.mx')
insert into Users values ('Marivi','DF','Maria','Vicela','DIB','A329104@alumnos.uaslp.mx')
insert into Users values ('Sofos','amogus','Sofia','Lopez','DIB','A324654@alumnos.uaslp.mx')
insert into Users values ('Anasof','larisa','Sofia','Pereda','ADM','A324046@alumnos.uaslp.mx')
insert into Users values ('Benavides','wosito','Jose','Salas','PIH','A324046@alumnos.uaslp.mx')


use MyCompany
go
create table disp
(
NumSerie int unique not null,
Nombre varchar(50),
AreaHosp varchar(50),
Modelo varchar(50),
Marca varchar(50),
Proveedor varchar(50),
VerSoft varchar(50),
MttoCorrectivo bit,
ProxMttoCorr varchar(50),
ProxMttoPrev varchar(50)
)

use MyCompany
go
create table eventos
(
NoEvento int identity (1,1) primary key,
TipoEvento varchar(12),
Fecha varchar(50),
NumSerie int,
Completado bit,
Reporte varchar(100),
Nombre varchar(50),
Modelo varchar(50),
Marca varchar(50)
)

delete disp
delete eventos

insert into disp values (12345678,'Nombre','Area','Modelo','Marca','Proveedor','1',0,'YYYY/MM/DD','2023/12/12')
insert into disp values (74566943,'Máquina de anestesia','Quirofano','Atlan A300','Drager','Drager','1.12.2',0,'YYYY/MM/DD','2023/05/29')
insert into disp values (39984347,'Máquina de anestesia','Quirofano','Atlan A300','Drager','Drager','1.12.2',0, 'YYYY/MM/DD','2023/05/29')
insert into disp values (87722263,'Máquina de anestesia','Quirofano','Atlan A300 XL','Drager','Drager','1.12.1',0, 'YYYY/MM/DD','2023/05/29')

insert into disp values (26877655,'Electrocauterio','Quirofano','System 5000 60-805','Conmed','Conmed','3.2',0, 'YYYY/MM/DD','2023/06/19')
insert into disp values (73425459,'Coagulador Monopolar-Bipolar','Quirofano','Force','Valley Lab','Proveedores MX','1.1',0, 'YYYY/MM/DD','2023/06/19')
insert into disp values (68884988, 'Coagulador Monopolar-Bipolar','Quirofano','Force','Valley Lab','Proveedores MX','1.1',0, 'YYYY/MM/DD','2023/06/19') 
insert into disp values (77522985, 'Monitor de signos vitales','Quirofano','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/06/19')

insert into disp values (88755272, 'Monitor de signos vitales','Quirofano','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/08/22')
insert into disp values (66984664, 'Monitor de signos vitales','Quirofano','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/08/22')

insert into disp values (52224894,'Desfibrilador','Quirofano','Tec-5631','Nihon-Kohden','Proved','2.12',0, 'YYYY/MM/DD','2023/09/11')
insert into disp values (47684996, 'Desfibrilador','Quirofano','Tec-5631','Nihon-Kohden','Proved','2.12',0, 'YYYY/MM/DD','2023/09/11')
insert into disp values (52758785, 'Desfibrilador','Quirofano','Tec-5631','Nihon-Kohden','Proved','2.12',0, 'YYYY/MM/DD','2023/09/11')
insert into disp values (86247555, 'Monitor de signos vitales','Urgencias','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/09/11')
insert into disp values (95224676, 'Monitor de signos vitales','Urgencias','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/09/11')

insert into disp values (98992955, 'Desfibrilador','Urgencias','Tec-5631','Nihon-Kohden','Proved','2.12',0, 'YYYY/MM/DD','2023/11/25')
insert into disp values (83249333, 'Desfibrilador','Urgencias','Tec-5631','Nihon-Kohden','Proved','2.12',0, 'YYYY/MM/DD','2023/11/25')
insert into disp values (22888599,'Desfibrilador','Urgencias','Tec-5631','Nihon-Kohden','Proved','2-12',0, 'YYYY/MM/DD','2023/11/25')
insert into disp values (43568672,'Ventilador de cuidados críticos','Cuidados Intensivos','Infinity C500','Drager','Drager','1.1.11',0, 'YYYY/MM/DD','2023/11/25')
insert into disp values (66463362, 'Ventilador de cuidados críticos','Cuidados Intensivos','Infinity C500','Drager','Drager','1.1.11',0, 'YYYY/MM/DD','2023/11/25')

insert into disp values (32648342,'Ultrasonido','Urgencias','HD7XE','Philips','Philips','2.14',0, 'YYYY/MM/DD','2023/07/04')
insert into disp values (57386749, 'Monitor de signos vitales','Cuidados Intensivos','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/07/04')
insert into disp values (57743396, 'Monitor de signos vitales','Cuidados Intensivos','Delta XL','Drager','Drager','1.17',0, 'YYYY/MM/DD','2023/07/04')
insert into disp values (59283324,'Monitor de signos vitales','Medicina Interna','Scholar III 507EL','Criticare','Empresau','2.2',0, 'YYYY/MM/DD','2023/07/04')
insert into disp values (53863397,'Monitor de signos vitales','Medicina Interna','Scholar III 507EL','Criticare','Empresau','2.2',0, 'YYYY/MM/DD','2023/07/04')

insert into disp values (43957473,'Máquina de anestesia','Medicina Interna','13000','Plarre','Proved','1.14',0, 'YYYY/MM/DD','2023/08/19')
insert into disp values (29623534,'Ventilador neonatal pediátrico','Neonatologia','CV2','Biomed','MexProv','1.13.2',0, 'YYYY/MM/DD','2023/08/19')
insert into disp values (86556573, 'Ventilador neonatal pediátrico','Neonatologia', 'CV2','Biomed','MexProv','1.13.2',0, 'YYYY/MM/DD','2023/06/19')
insert into disp values (23227539, 'Ventilador neonatal pediátrico','Neonatologia', 'CV2','Biomed','MexProv','1.13.2',0, 'YYYY/MM/DD','2023/08/19')
insert into disp values (52595628,'Incubadora de Cuidados Intensivos','Neonatologia','Caleo','Caleo','Caleo','2.34',0, 'YYYY/MM/DD','2023/08/19')

insert into disp values (82389478, 'Incubadora de Cuidados Intensivos','Neonatologia', 'Caleo','Caleo','Caleo','2.34',0, 'YYYY/MM/DD','2023/12/02')
insert into disp values (55442756,'Cuna/Incubadora','Neonatologia','Babyleo TN500','Drager','Drager','9',0, 'YYYY/MM/DD','2023/12/02')
insert into disp values (44772638,'Cuna térmica','Neonatologia','Atom Intant','Atom','MedicalCare','1.11.8',0, 'YYYY/MM/DD','2023/12/02')
insert into disp values (75255967,'Cuna de calor radiante','Neonatologia','Baby Therm 8010','Drager','Drager','3.33',0, 'YYYY/MM/DD','2023/12/02')
insert into disp values (79326488,'Oxímetro de pulso','Cuidados Intensivos','504','Criticare','Empresau','4',0, 'YYYY/MM/DD','2023/12/02')

insert into disp values (71476185,'Unidad radiográfica rodable digital','Cuidados Intensivos','Mobilett XP','Siemens','Siemens','2.14',0, 'YYYY/MM/DD','2023/07/11')
insert into disp values (11559802, 'Oxímetro de pulso','Cuidados Intensivos', '504','Criticare','Empresau','4',0, 'YYYY/MM/DD','2023/07/11')
insert into disp values (26885107,'Oxímetro de pulso','Cuidados Intensivos', '504','Criticare','Empresau','4',0, 'YYYY/MM/DD','2023/07/11') 
insert into disp values (52494415,'Bomba de infusión','Urgencias','Kon-V10','KONTROLab','LabMX','1.42',0, 'YYYY/MM/DD','2023/07/11')
insert into disp values (88362599, 'Bomba de infusión','Urgencias', 'Kon-V10','KONTROLab','LabMX','1.42',0, 'YYYY/MM/DD','2023/07/11')

insert into disp values (49917985, 'Bomba de infusión','Cuidados Intensivos', 'Kon-V10','KONTROLab','LabMX','1.42',0, 'YYYY/MM/DD','2023/07/19')
insert into disp values (95285492, 'Bomba de infusión','Cuidados Intensivos', 'Kon-V10','KONTROLab','LabMX','1.42',0, 'YYYY/MM/DD','2023/07/19') 
insert into disp values (30762097,'Desfibrilador','Cuidados Intensivos','Heart Star','Philips','Philips','4',0, 'YYYY/MM/DD','2023/07/19')
insert into disp values (52906003,'Tomografía','Imagenologia','Brillance 64','Philips','Philips','3',0, 'YYYY/MM/DD','2023/07/19')
insert into disp values (53618708,'Resonancia Magnética','Imagenologia','RM90','ClearImage','NS','1',0, 'YYYY/MM/DD','2023/07/19')

insert into disp values (81002482,'Rayos X','Imagenologia','Ray-4000','ClearImage','Empresau','2',0, 'YYYY/MM/DD','2023/08/03')
insert into disp values (84638638,'Mastografo','Imagenologia','AVIA 3000','Selenia','Krom','1.1',0, 'YYYY/MM/DD','2023/08/03')
insert into disp values (35135797,'Arco en C','Imagenologia','CSharp','Selenia','Krom','2.3',0, 'YYYY/MM/DD','2023/08/03')
insert into disp values (94303064,'Cámara Gamma','Imagenologia','GC2010','Duck','Tape','1',0, 'YYYY/MM/DD','2023/08/03')
insert into disp values (96319334,'PET/CT','Imagenologia','Tauro-6000','ClearImage','Empresau','1',0, 'YYYY/MM/DD','2023/08/03')

insert into disp values (78660742,'Ecografo','Imagenologia','EcoGlance V1','GL','Proveedores MX','1.23',0, 'YYYY/MM/DD','2023/08/22')
insert into disp values (35025867,'Angiografo','Imagenologia','ANG1','GL','Proveedores MX','1.9',0, 'YYYY/MM/DD','2023/08/22')
insert into disp values (81932080,'Autoclave','Medicina Interna','E28','Binder','MexProv','0',0, 'YYYY/MM/DD','2023/08/22')
insert into disp values (63659518,'Esterilizador','Medicina Interna','1906','Aesculap','MexProv','0',0, 'YYYY/MM/DD','2023/08/22')
insert into disp values (75690885,'Esterilizador','Medicina Interna', '1906','Aesculap','MexProv','0',0, 'YYYY/MM/DD','2023/08/22')

insert into disp values (43992043,'Ventilador de alta frecuencia','Medicina Interna','3100A','Sensor Medics','LabMX','2',0, 'YYYY/MM/DD','2023/10/20')
insert into disp values (20650083,'Generador de electrocirugía','Quirofano','Surgistat','Valley Lab','Proveedores MX','1.4',0, 'YYYY/MM/DD','2023/10/20')
insert into disp values (44239296,'Generador de electrocirugía','Medicina Interna', 'Surgistat','Valley Lab','Proveedores MX','1.4',0, 'YYYY/MM/DD','2023/10/20')





insert into eventos values ('Preventivo', '2023/05/29',74566943,0,'Preventivo_74566943_2023/05/29','Máquina de anestesia','Atlan A300','Drager')
insert into eventos values ('Preventivo', '2023/05/29',39984347,0,'Preventivo_39984347_2023/05/29','Máquina de anestesia','Atlan A300','Drager')
insert into eventos values ('Preventivo', '2023/05/29',87722263,0,'Preventivo_87722263_2023/05/29','Máquina de anestesia','Atlan A300 XL','Drager')

insert into eventos values ('Preventivo', '2023/06/19',26877655,0,'Preventivo_26877655_2023/06/19','Electrocauterio','System 5000 60-805','Conmed')
insert into eventos values ('Preventivo', '2023/06/19',73425459,0,'Preventivo_73425459_2023/06/19','Coagulador Monopolar-Bipolar','Force','Valley Lab')
insert into eventos values ('Preventivo', '2023/06/19',68884988,0,'Preventivo_68884988_2023/06/19','Coagulador Monopolar-Bipolar','Force','Valley Lab')
insert into eventos values ('Preventivo', '2023/06/19',77522985,0,'Preventivo_77522985_2023/06/19','Monitor de signos vitales','Delta XL','Drager')

insert into eventos values ('Preventivo', '2023/08/22',88755272,0,'Preventivo_88755272_2023/08/22','Monitor de signos vitales','Delta XL','Drager')
insert into eventos values ('Preventivo', '2023/08/22',66984664,0,'Preventivo_66984664_2023/08/22','Monitor de signos vitales','Delta XL','Drager')

insert into eventos values ('Preventivo', '2023/09/11',52224894,0,'Preventivo_52224894_2023/09/11','Desfibrilador','Tec-5631','Nihon-Kohden')
insert into eventos values ('Preventivo', '2023/09/11',47684996,0,'Preventivo_47684996_2023/09/11','Desfibrilador','Tec-5631','Nihon-Kohden')
insert into eventos values ('Preventivo', '2023/09/11',52758785,0,'Preventivo_52758785_2023/09/11','Desfibrilador','Tec-5631','Nihon-Kohden')
insert into eventos values ('Preventivo', '2023/09/11',86247555,0,'Preventivo_86247555_2023/09/11','Monitor de signos vitales','Delta XL','Drager')
insert into eventos values ('Preventivo', '2023/09/11',95224676,0,'Preventivo_95224676_2023/09/11','Monitor de signos vitales','Delta XL','Drager')

insert into eventos values ('Preventivo', '2023/11/25',98992955,0,'Preventivo_98992955_2023/11/25','Desfibrilador','Tec-5631','Nihon-Kohden')
insert into eventos values ('Preventivo', '2023/11/25',83249333,0,'Preventivo_83249333_2023/11/25','Desfibrilador','Tec-5631','Nihon-Kohden')
insert into eventos values ('Preventivo', '2023/11/25',22888599,0,'Preventivo_22888599_2023/11/25','Desfibrilador','Tec-5631','Nihon-Kohden')
insert into eventos values ('Preventivo', '2023/11/25',43568672,0,'Preventivo_43568672_2023/11/25','Ventilador de cuidados críticos','Infinity C500','Drager') 
insert into eventos values ('Preventivo', '2023/11/25',66463362,0,'Preventivo_66463362_2023/11/25','Ventilador de cuidados críticos','Infinity C500','Drager')

insert into eventos values ('Preventivo', '2023/07/04',32648342,0,'Preventivo_32648342_2023/07/04','Ultrasonido','HD7XE','Philips')
insert into eventos values ('Preventivo', '2023/07/04',57386749,0,'Preventivo_57386749_2023/07/04','Monitor de signos vitales','Delta XL','Drager')
insert into eventos values ('Preventivo', '2023/07/04',57743396,0,'Preventivo_57743396_2023/07/04','Monitor de signos vitales','Delta XL','Drager')
insert into eventos values ('Preventivo', '2023/07/04',59283324,0,'Preventivo_59283324_2023/07/04','Monitor de signos vitales','Scholar III 507EL','Criticare')
insert into eventos values ('Preventivo', '2023/07/04',53863397,0,'Preventivo_53863397_2023/07/04','Monitor de signos vitales','Scholar III 507EL','Criticare')

insert into eventos values ('Preventivo', '2023/08/19',43957473,0,'Preventivo_43957473_2023/08/19','Máquina de anestesia','13000','Plarre')
insert into eventos values ('Preventivo', '2023/08/19',29623534,0,'Preventivo_29623534_2023/08/19','Ventilador neonatal pediátrico','CV2','Biomed')
insert into eventos values ('Preventivo', '2023/08/19',86556573,0,'Preventivo_86556573_2023/08/19','Ventilador neonatal pediátrico', 'CV2','Biomed')
insert into eventos values ('Preventivo', '2023/08/19',23227539,0,'Preventivo_23227539_2023/08/19','Ventilador neonatal pediátrico', 'CV2','Biomed')
insert into eventos values ('Preventivo', '2023/08/19',52595628,0,'Preventivo_52595628_2023/08/19','Incubadora de Cuidados Intensivos','Caleo','Caleo')

insert into eventos values ('Preventivo', '2023/12/02',82389478,0,'Preventivo_82389478_2023/12/02','Incubadora de Cuidados Intensivos', 'Caleo','Caleo')
insert into eventos values ('Preventivo', '2023/12/02',55442756,0,'Preventivo_55442756_2023/12/02','Cuna/Incubadora','Babyleo TN500','Drager')
insert into eventos values ('Preventivo', '2023/12/02',44772638,0,'Preventivo_44772638_2023/12/02','Cuna térmica','Atom Intant','Atom')
insert into eventos values ('Preventivo', '2023/12/02',75255967,0,'Preventivo_75255967_2023/12/02','Cuna de calor radiante','Baby Therm 8010','Drager')
insert into eventos values ('Preventivo', '2023/12/02',79326488,0,'Preventivo_79326488_2023/12/02','Oxímetro de pulso','504','Criticare')

insert into eventos values ('Preventivo', '2023/07/11',71476185,0,'Preventivo_71476185_2023/07/11','Unidad radiográfica rodable digital','Mobilett XP','Siemens')
insert into eventos values ('Preventivo', '2023/07/11',11559802,0,'Preventivo_11559802_2023/07/11','Oxímetro de pulso','504','Criticare')
insert into eventos values ('Preventivo', '2023/07/11',26885107,0,'Preventivo_26885107_2023/07/11','Oxímetro de pulso','504','Criticare') 
insert into eventos values ('Preventivo', '2023/07/11',52494415,0,'Preventivo_52494415_2023/07/11','Bomba de infusión','Kon-V10','KONTROLab')
insert into eventos values ('Preventivo', '2023/07/11',88362599,0,'Preventivo_88362599_2023/07/11','Bomba de infusión','Kon-V10','KONTROLab')

insert into eventos values ('Preventivo', '2023/07/19',49917985,0,'Preventivo_49917985_2023/07/19','Bomba de infusión','Kon-V10','KONTROLab')
insert into eventos values ('Preventivo', '2023/07/19',95285492,0,'Preventivo_95285492_2023/07/19','Bomba de infusión','Kon-V10','KONTROLab') 
insert into eventos values ('Preventivo', '2023/07/19',30762097,0,'Preventivo_30762097_2023/07/19','Desfibrilador','Heart Star','Philips')
insert into eventos values ('Preventivo', '2023/07/19',52906003,0,'Preventivo_52906003_2023/07/19','Tomografía','Brillance 64','Philips')
insert into eventos values ('Preventivo', '2023/07/19',53618708,0,'Preventivo_53618708_2023/07/19','Resonancia Magnética','RM90','ClearImage')

insert into eventos values ('Preventivo', '2023/08/03',81002482,0,'Preventivo_81002482_2023/08/03','Rayos X','Ray-4000','ClearImage')
insert into eventos values ('Preventivo', '2023/08/03',84638638,0,'Preventivo_84638638_2023/08/03','Mastografo','AVIA 3000','Selenia') 
insert into eventos values ('Preventivo', '2023/08/03',35135797,0,'Preventivo_35135797_2023/08/03','Arco en C','CSharp','Selenia')
insert into eventos values ('Preventivo', '2023/08/03',94303064,0,'Preventivo_94303064_2023/08/03','Cámara Gamma','GC2010','Duck')
insert into eventos values ('Preventivo', '2023/08/03',96319334,0,'Preventivo_96319334_2023/08/03','PET/CT','Tauro-6000','ClearImage')

insert into eventos values ('Preventivo', '2023/08/22',78660742,0,'Preventivo_78660742_2023/08/22','Ecografo','EcoGlance V1','GL')
insert into eventos values ('Preventivo', '2023/08/22',35025867,0,'Preventivo_35025867_2023/08/22','Angiografo','ANG1','GL')
insert into eventos values ('Preventivo', '2023/08/22',81932080,0,'Preventivo_81932080_2023/08/22','Autoclave','E28','Binder')
insert into eventos values ('Preventivo', '2023/08/22',63659518,0,'Preventivo_63659518_2023/08/22','Esterilizador','1906','Aesculap')
insert into eventos values ('Preventivo', '2023/08/22',75690885,0,'Preventivo_75690885_2023/08/22','Esterilizador', '1906','Aesculap')

insert into eventos values ('Preventivo', '2023/10/20',43992043,0,'Preventivo_43992043_2023/10/20','Ventilador de alta frecuencia','3100A','Sensor Medics')
insert into eventos values ('Preventivo', '2023/10/20',20650083,0,'Preventivo_20650083_2023/10/20','Generador de electrocirugía','Surgistat','Valley Lab')
insert into eventos values ('Preventivo', '2023/10/20',44239296,0,'Preventivo_44239296_2023/10/20','Generador de electrocirugía', 'Surgistat','Valley Lab')
