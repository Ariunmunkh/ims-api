/*Хөтөлбөр*/
create table program
(
id int not null, 
name varchar(200),
bossname varchar(200),
phone varchar(200),
location varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Хөтөлбөрийн индикатор*/
create table indicator
(
id int not null, 
headid int,
name varchar(200),
bossname varchar(200),
phone varchar(200),
location varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Насны ангилал*/
create table agegroup
(
id int not null, 
name varchar(200),
bossname varchar(200),
phone varchar(200),
location varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);

/*ДШХ-ны сарын тайлан*/
create table committeereport
(
id int not null, 
committeeid int not null, 
reportdate date not null, 
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*ДШХ-ны сарын тайлан*/
create table committeereportdtl
(
id int not null, 
reportid int not null, 
programid int not null, 
indicatorid int not null, 
agegroupid int not null, 
male int,
female int, 
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);

/*Дунд шатны хорооны бүртгэл*/
create table committee
(
id int not null, 
name varchar(200),
bossname varchar(200),
phone varchar(200),
location varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Боловсролын түвшин*/
create table educationlevel
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Гишүүнчлэл*/
create table membership
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Цусны бүлэг*/
create table bloodgroup
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Улс*/
create table country
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Аймаг, хот*/
create table division
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сум, дүүрэг*/
create table district
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сургалт*/
create table training
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Гадаад хэлний төрөл*/
create table languages
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Ур чадвар*/
create table skills
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Ур чадварын түвшин*/
create table skillslevel
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын ажил*/
create table voluntarywork
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Таны юу болох*/
create table relationship
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Тусламжийн төрөл*/
create table assistance
(
id int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
drop table volunteer;
/*Сайн дурын идэвхтэн*/
create table volunteer
(
id int not null, 
familyname varchar(200),/*Ургийн овог*/
firstname varchar(200),/*Өөрийн нэр*/
lastname varchar(200),/*Эцэг эхийн нэр*/
gender int,/*Хүйс*/
regno varchar(200),/*Регистрийн дугаар*/
birthday date,/*Төрсөн огноо*/
phone varchar(200),/*Утасны дугаар*/
isvolunteer bool,/*Сайн дурын идэвхтэн эсэх*/
isblooddonor bool,/*Цусны доонор эсэх*/
bloodgroupid int,/*Цусны бүлэг*/
educationlevelid int,/*Боловсролын түвшин*/
countryid int,/*Улс*/
divisionid int,/*Аймаг, хот*/
districtid int,/*Сум, дүүрэг*/
address varchar(2000),/*гудамж, байр, орц*/
isdisabled bool,/*Хөгжлийн бэрхшээлтэй иргэн эсэх*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Яаралтаы үед холбоо барих гэр бүлийн гишүүний мэдээлэл*/
create table emergencycontact
(
id int not null, 
volunteerid int,
relationshipid int,/*Таны юу болох*/
firstname varchar(200),/*Овог нэр*/
phone varchar(200),/*Утасны дугаар*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Сайн дурын ажлын мэдээлэл*/
create table volunteervoluntarywork
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
voluntaryworkid int,/*Сайн дурын ажлын төрөл*/
duration int,/*Хугацаа*/
voluntaryworkdate date,/*Огноо*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Сургалтын мэдээлэл*/
create table volunteertraining
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
trainingid int,/*Сургалтын төрөл*/
levelid int,/*Түвшин*/
trainingdate date,/*Огноо*/
location varchar(200),/*Хаана*/
duration int,/*Хугацаа*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Ур чадварын мэдээлэл*/
create table volunteerskills
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
skillsid int,/*Ур чадварын төрөл*/
skillslevelid int,/*Ур чадварын түвшин*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Гишүүнчлэлийн мэдээлэл*/
create table volunteermembership
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
membershipid int,/*Гишүүнчлэлийн төрөл*/
begindate date,/*Эхэлсэн*/
enddate date,/*Дууссан*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Тусламжийн мэдээлэл*/
create table volunteerassistance
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
assistanceid int,/*Тусламжийн төрөл*/
projectname varchar(200),/*Төслийн нэр*/
assistancedate date,/*Огноо*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Боловсролын мэдээлэл*/
create table volunteereducation
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
educationlevelid int,/*Боловсролын түвшин*/
schoolname varchar(200),/*Сургуулийн нэр*/
isend bool,/*Төгссөн эсэх*/
classlevel int,/*Курс/Анги*/
skill varchar(200),/*Мэрэгжил*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Эрхэлсэн ажил*/
create table volunteeremployment
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
employment varchar(200),/*Ажлын салбар*/
company varchar(200),/*Ажлын газар*/
job varchar(200),/*Албан тушаал*/
begindate date,/*Эхэлсэн огноо*/
enddate date,/*Дууссан огноо*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Гадаад хэлний мэдлэг*/
create table volunteerlanguages
(
id int not null, 
volunteerid int,/*Сайн дурын идэвхтэн*/
languageid int,/*Гадаад хэл*/
levelid int,/*Түвшин*/
studyyear int,/*Сурсан хугацаа /Жилээр/*/
isscore bool,/*Түвшин шалгасан оноотой эсэх*/
testname varchar(200),/*Шалгалтын нэр*/
testscore int,/*Шалгалтын оноо*/
note varchar(2000),/*Нэмэлт мэдээлэл*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Хэрэглэгч*/
create table tbluser(
userid int not null, 
username varchar(200) not null,
password varchar(500) not null,
email varchar(200),
roleid int,
volunteerid int,
updated timestamp default current_timestamp,
updatedby int,
PRIMARY KEY (userid)
);