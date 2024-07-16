/*Хэрэгжүүлж буй төсөл, хөтөлбөр*/
create table project
(
id int not null, 
programid int ,/*Хөтөлбөр*/
name varchar(200),/*Төслийн нэр*/
funder varchar(200),/*Санхүүжүүлэгч*/
note varchar(2000),/*Төслийн товч мэдээлэл*/
results varchar(2000),/*Хүрсэн үр дүн*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Хөтөлбөр*/
create table program
(
id int not null, 
name varchar(200),
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
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Насны ангилал*/
create table agegroup
(
id int not null, 
name varchar(200),
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

/*Орон нутгийн талаарх мэдээлэл*/
create table localinfo
(
id int not null, 
committeeid int not null, 
c1_1  varchar(200),/*Аймаг/Дүүргийн нэр*/
c1_2 varchar(200), /*Сум/Хорооны тоо*/	
c1_3 varchar(200), /*Нийслэлээс алслагдсан байдал /км/*/
c1_4 varchar(200), /*Хүн амын тоо*/
c1_4_1 varchar(200), /*Хүн амын тоо /0-5 нас/	*/
c1_4_2 varchar(200), /*Хүн амын тоо /6-17 нас/*/	
c1_4_3 varchar(200), /*Хүн амын тоо /18-25 нас/*/	
c1_4_4 varchar(200), /*Хүн амын тоо /26-45 нас/*/	
c1_4_5 varchar(200), /*Хүн амын тоо /46-60 нас/*/	
c1_4_6 varchar(200), /*Хүн амын тоо /61+ нас/*/
c1_5 varchar(200), /*Хөдөлмөр эрхлэгч насны хүн амын тоо*/
c1_6 varchar(200), /*Зорилтот бүлгийн хүн амын тоо*/
c1_6_1 varchar(200), /*Өрх толгойлсон эрэгтэй*/
c1_6_2 varchar(200), /*Өрх толгойлсон эмэгтэй*/	
c1_6_3 varchar(200), /*Хөгжлийн бэрхшээлтэй иргэн*/
c1_6_4 varchar(200), /*Өндөр настан*/
c1_6_5 varchar(200), /*Хагас өнчин хүүхдийн тоо*/
c1_6_6 varchar(200), /*Бүтэн өнчин хүүхдийн тоо*/
c1_7 varchar(200), /*Малчин өрхийн тоо*/
c1_8 varchar(200), /*Малын тоо, толгой*/
c1_8_1 varchar(200), /*Адуу*/
c1_8_2 varchar(200), /*Тэмээ*/
c1_8_3 varchar(200), /*Үхэр*/
c1_8_4 varchar(200), /*Хонь*/
c1_8_5 varchar(200), /*Ямаа*/
c1_9 varchar(200), /*Төрийн өмчит аж ахуй нэгж байгууллагын тоо*/
c1_10 varchar(200), /*Хувийн өмчит аж ахуй нэгж байгууллагын тоо*/
c1_11 varchar(200), /*Орон нутагт үйл ажиллагаа явуулж буй Олон улсын байгууллагын тоо*/
c1_12 varchar(200), /*Орон нутагт үйл ажиллагаа явуулж буй хүмүүнлэгийн байгууллагын тоо*/
c1_13 varchar(200), /*Их, дээд сургуулийн тоо*/
c1_14 varchar(200), /*Ерөнхий боловсролын сургуулийн тоо*/
c1_15 varchar(200), /*Сургуулийн өмнөх боловсролын байгууллагын тоо*/
c1_16 varchar(200), /*Жолооны курсын тоо*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);

/*ДУНД ШАТНЫ ХОРООНЫ ТАЛААРХ МЭДЭЭЛЭЛ*/
create table committeeinfo
(
id int not null, 
committeeid int not null, 
c2_1 varchar(200), /*Дунд шатны хорооны нэр*/
c2_2 varchar(200), /*Байгуулагдсан он, сар, өдөр*/
c2_3 varchar(200), /*Хаяг, байршил*/
c2_4 varchar(200), /*Утасны дугаар*/
c2_5 varchar(200), /*И-мэйл хаяг*/
c2_6 varchar(200), /*Үндсэн ажилтаны тоо*/
c2_7 varchar(200), /*Гэрээт ажилтаны тоо*/
c2_8 varchar(200), /*Бүртгэлтэй Сайн дурын идэвхтний тоо*/
c2_9 varchar(200), /*Гишүүнчлэл*/
c2_9_1  varchar(200), /*Гишүүн*/
c2_9_2  varchar(200), /*Онцгой гишүүн*/
c2_9_3  varchar(200), /*Мөнгөн гишүүн*/
c2_9_4  varchar(200), /*Алтан гишүүн*/
c2_10 varchar(200), /*Хүмүүнлэгийн гишүүн байгууллагын тоо*/
c2_11 varchar(200), /*Хүүхэд залуучуудын хөдөлгөөний гишүүний тоо*/
c2_11_1  varchar(200), /*Багачуудын улаан загалмайч*/
c2_11_2  varchar(200), /*Өсвөрийн улаан загалмайч*/
c2_11_3  varchar(200), /*Залуучуудын улаан загалмайч*/
c2_11_4  varchar(200), /*Идэр улаан загалмайч*/
c2_12_1  varchar(200), /*ДШХ-ны эзэмшлийн газартай эсэх*/
c2_12_2  varchar(200), /*Эзэмшлийн газрын зориулалт*/
c2_12_3  varchar(200), /*Талбайн хэмжээ*/
c2_12_4  varchar(200), /*Хэрэв үгүй бол Газрын зориулалт*/
c2_13_1  varchar(200), /*ДШХ-ны эзэмшлийн байртай эсэх*/
c2_13_2  varchar(200), /*Эзэмшлийн байрны тоо*/
c2_13_3  varchar(200), /*Талбайн хэмжээ*/
c2_13_4  varchar(200), /*Өрөөний тоо*/
c2_13_5  varchar(200), /*Хэрэв үгүй бол байрны зориулалт*/
c2_14_1  varchar(200), /*ДШХ-ны эзэмшлийн агуулахтай эсэх*/
c2_14_2  varchar(200), /*Ашиглалтанд орсон огноо*/
c2_14_3  varchar(200), /*Талбайн хэмжээ*/
c2_15_1  varchar(200), /*ДШХ-ны эзэмшлийн тээврийн хэрэгсэлтэй эсэх*/
c2_15_2  varchar(200), /*Тээврийн хэрэгслийн тоо*/
c2_15_3  varchar(200), /*Тээврийн хэрэгслийн тайлбар /Марк, ашигласан хугацаа, Монголд орж ирсэн огноо/*/
c2_16 varchar(200), /*Бусад хөрөнгө /зөөврийн болон суурийн компьютер, принтер гэх мэтийг дурдах/*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*ДУНД ШАТНЫ ХОРООНЫ ТАЛААРХ МЭДЭЭЛЭЛ*/
create table committeeinfodtl
(
id int not null, 
committeeid int not null, 
name varchar(200), /*Сум/Хороо дахь анхан шатны хорооны нэр*/
isnote bool,/*Тэмдэг*/
isbank bool,/*Банкны данс*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*ҮЙЛ АЖИЛЛАГААНЫ ТАЛААРХ МЭДЭЭЛЭЛ*/
create table committeeactivity
(
id int not null, 
committeeid int not null, 
c3_3 varchar(2000), /*Хэрэгжүүлж байсан төслийн нэр, хугацаа, үндсэн үйл ажиллагаа, үр дүнгийн тухай 2-3 өгүүлбэрт багтаах /2020 оноос хойшхи/*/
c3_4 varchar(2000),/*Нөөц хөгжүүлэх, орлого нэмэгдүүлэх чиглэлээр хийгддэг үйл ажиллагаа /Үйл ажиллагааг жагсааж оруулах/*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*ҮЙЛ АЖИЛЛАГААНЫ ТАЛААРХ МЭДЭЭЛЭЛ*/
create table committeeactivitydtl
(
id int not null, 
committeeid int not null, 
name varchar(200), /*Овог, нэр*/
job varchar(200),/*Албан тушаал*/
type bool,/*Гишүүний төрөл*/
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
committeeid int,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сум, дүүрэг*/
create table district
(
id int not null, 
divisionid int,
committeeid int,
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
joindate date,/*Элссэн огноо /улаан загалмайд/*/
isvolunteer bool,/*Сайн дурын идэвхтэн эсэх*/
isblooddonor bool,/*Цусны доонор эсэх*/
bloodgroupid int,/*Цусны бүлэг*/
educationlevelid int,/*Боловсролын түвшин*/
countryid int,/*Улс*/
divisionid int,/*Аймаг, хот*/
districtid int,/*Сум, дүүрэг*/
address varchar(2000),/*гудамж, байр, орц*/
birthplace varchar(2000),/*Төрсөн газар*/
facebook varchar(300),/*facebook*/
jobname varchar(200),/*Мэргэжил*/
employment varchar(200),/*Одоо эрхлэж буй ажил*/
isdisabled bool,/*Хөгжлийн бэрхшээлтэй иргэн эсэх*/
committeeid int,/*Дунд шатны хороо*/
type int,/*төрөл*/
status int,/*Төлөв*/
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
/*Сайн дурын идэвхтэн Зураг*/
create table volunteerimage(
volunteerid int not null, 
image BLOB,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (volunteerid)
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
name varchar(300),
status int,/*Төлөв*/
duration float,/*Хугацаа*/
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
name varchar(200),/*Сургалтын нэр*/
organizer varchar(200),/*Зохион байгуулагч*/
begindate date,/*Сургалт эхэлсэн огноо*/
enddate date,/*Сургалт дууссан огноо*/
location varchar(200),/*Сургалтын байршил*/
iscertificate bool,/*Гэрчилгээтэй эсэх*/
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
committeeid int,
volunteerid int,
updated timestamp default current_timestamp,
updatedby int,
PRIMARY KEY (userid)
);