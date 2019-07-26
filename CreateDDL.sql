/*
Created		22.9.2017
Modified		22.9.2017
Project		
Model		
Company		
Author		
Version		
Database		MS SQL 7 
*/

CREATE DATABASE KRVODAJALSKA_AKCIJA
go
USE KRVODAJALSKA_AKCIJA
go

/*Drop table [AKCIJA] 
go
Drop table [ENOTA] 
go
Drop table [POSTA] 
go
Drop table [ODDAJA_KRVI] 
go
Drop table [KRVODAJALEC] 
go
Drop table [TELEFON] 
go*/


Create table [TELEFON] (
	[sifra_krvodajalca] Integer NOT NULL,
	[stevilka] Nvarchar(11) NOT NULL,
Primary Key  ([sifra_krvodajalca],[stevilka])
) 
go

Create table [KRVODAJALEC] (
	[sifra] Integer NOT NULL IDENTITY(1,1),
	[ime] Nvarchar(30) NOT NULL,
	[priimek] Nvarchar(30) NOT NULL,
	[EMSO] Char(13) NOT NULL,
	[spol] Char(1) NOT NULL,
	[naslov] Nvarchar(30) NOT NULL,
	[krvna_skupina] Varchar(3) NOT NULL,
	[posta] Char(4) NOT NULL,
Primary Key  ([sifra])
) 
go

Create table [ODDAJA_KRVI] (
	[datum] Datetime NOT NULL,
	[sifra_krvodajalca] Integer NOT NULL,
	[kolicina] Integer NOT NULL,
	[sifra_akcije] Integer NOT NULL,
Primary Key  ([datum],[sifra_krvodajalca])
) 
go

Create table [POSTA] (
	[postna_st] Char(4) NOT NULL,
	[kraj] Nvarchar(30) NOT NULL,
Primary Key  ([postna_st])
) 
go

Create table [ENOTA] (
	[sifra] Integer NOT NULL,
	[naziv] Nvarchar(50) NOT NULL,
	[naslov] Nvarchar(30) NOT NULL,
	[telefonska_st] Nvarchar(11) NOT NULL,
	[posta] Char(4) NOT NULL,
Primary Key  ([sifra])
) 
go

Create table [AKCIJA] (
	[sifra] Integer NOT NULL IDENTITY(0,1),
	[datum_z] Datetime NOT NULL,
	[datum_k] Datetime NOT NULL,
	[vrsta] Nvarchar(7) NOT NULL,
	[sifra_enote] Integer NOT NULL,
Primary Key  ([sifra])
) 
go

Create table [ADMINI]
(
	uporabnisko_ime Nvarchar(50) PRIMARY KEY NOT NULL,
	kriptirano_geslo Nvarchar(100) NOT NULL
)


Alter table [TELEFON] add  foreign key([sifra_krvodajalca]) references [KRVODAJALEC] ([sifra]) 
go
Alter table [ODDAJA_KRVI] add  foreign key([sifra_krvodajalca]) references [KRVODAJALEC] ([sifra]) 
go
Alter table [KRVODAJALEC] add  foreign key([posta]) references [POSTA] ([postna_st]) 
go
Alter table [ENOTA] add  foreign key([posta]) references [POSTA] ([postna_st]) 
go
Alter table [AKCIJA] add  foreign key([sifra_enote]) references [ENOTA] ([sifra]) 
go
Alter table [ODDAJA_KRVI] add  foreign key([sifra_akcije]) references [AKCIJA] ([sifra]) 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


/* Roles permissions */


/* Users permissions */
