create database PonfeLibrary;
go

use PonfeLibrary;
go

create table Author
(
	Id int primary key identity (1,1),
	[Name] varchar (100) not null,
	Nationality varchar (3) not null,
	constraint UQ_AName unique ([Name])
);


create table Publisher
(
	Id int primary key identity (1,1),
	[Name] varchar (100) not null,
	constraint UQ_PName unique ([Name])
);

create table Book
(
	Id int primary key identity (1,1),
	Title varchar (100) not null,
	Subtitle varchar (100),
	ISBN varchar (13) not null,
	constraint UQ_ISBN unique (ISBN),
	PubId int not null
	constraint FK_Publisher foreign key (PubId)
		references Publisher(Id)
);


create table Genre
(
	Id int primary key identity (1,1),
	[Name] varchar (100) not null,
	[Description] varchar (500) not null,
	constraint UQ_GName unique ([Name])
);

create table Book_Gen
(	
	BookId int not null,
	GenId int not null,
	constraint FK_BookG foreign key (BookId)
		references Book(Id) on delete cascade,
	constraint FK_GenG foreign key (GenId)
		references Genre(Id) on delete cascade,
	constraint PK_Book_Gen primary key (BookId, GenId)
);


create table Book_Auth
(
	BookId int not null,
	AuthId int not null,
	constraint FK_BookA foreign key (BookId)
		references Book(Id) on delete cascade,
	constraint FK_AuthG foreign key (AuthId)
		references Author(Id) on delete cascade,
	constraint PK_Book_Au primary key (BookID,AuthId)

);

insert into Author
([Name], Nationality)
values
('J. K. Rowling','GBR'),
('Issac Asimov','RUS'),
('Jhon Green','USA')

insert into Publisher
([Name])
values
('Bloomsbury Publishing'),
('Bantam Spectra Books'),
('Dutton')

insert into Genre
([Name],[Description])
values
('Novel','A novel is a relatively long work of narrative fiction, normally written in prose form, and which is typically published as a book.'),
('Adventure','An adventure is an event or series of events that happens outside the course of the protagonist''s ordinary life, usually accompanied by danger, often by physical action.'),
('Science Fiction','Science fiction (sometimes called sci-fi or simply SF) is a genre of speculative fiction that typically deals with imaginative and futuristic concepts such as advanced science and technology, space exploration, time travel, parallel universes, and extraterrestrial life.')

insert into Book
([Title],[Subtitle],[ISBN],PubId)
values 
('Harry Potter','And the Philosopher''''s Stone','8745091276435',(select Id from Publisher where Publisher.[Name] = 'Bloomsbury Publishing'
))

insert into Book_Auth
(BookId, AuthId)
select 
	(select Id from Book where Book.Id = 1),
	(select Id from Author where Author.Id = 1)

insert into Book_Gen
(BookId, GenId)
select 
	(select Id from Book where Book.Id = 1),
	(select Id from Genre where Genre.Id = 1)

select * from Genre;
select * from Author;
select * from Publisher;
select * from Book
select *from Book_Auth;
select * from Book_Gen;