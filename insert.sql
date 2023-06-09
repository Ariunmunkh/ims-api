

INSERT INTO tbluser (userid, username, email, password) values (@userid, @username, @email, @password)
ON DUPLICATE KEY
UPDATE username = @username,
         email    = @email,
         password = @password;
         
insert into household
  (householdid ,
numberof ,
name ,
district ,
section ,
address ,
phone ,
coachid ,
updatedby  )
values
  (@householdid ,
@numberof ,
@name ,
@district ,
@section ,
@address ,
@phone ,
@coachid ,
@updatedby) 
on duplicate key update 
numberof =@numberof,
name=@name ,
district=@district ,
section=@section ,
address=@address ,
phone=@phone ,
coachid=@coachid ,
updated = current_timestamp,
updatedby =@updatedby;


insert into householdmember
(memberid,
householdid,
name,
relative,
birthdate,
gender,
istogether,
updatedby )
values
(@memberid,
@householdid,
@name,
@relative,
@birthdate,
@gender,
@istogether,
@updatedby) 
on duplicate key update 
householdid=@householdid,
name=@name,
relative=@relative,
birthdate=@birthdate,
gender=@gender,
istogether=@istogether,
updated=current_timestamp,
updatedby=@updatedby;

SELECT 
    householdid,
    numberof,
    name,
    district,
    section,
    address,
    phone,
    coachid,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    household;



    
insert into district (districtid , name) values(	1, 'Багануур дүүрэг'),
	(	4, 'Багахагай дүүрэг'),
	(	7, 'Баянгол дүүрэг'),
	(	10, 'Баянзүрх дүүрэг'),
	(	13, 'Налайх дүүрэг'),
	(	16, 'Сонгинохайрхан дүүрэг'),
	(	19, 'Сүхбаатар дүүрэг'),
	(	22, 'Хан-Уул дүүрэг'),
	(	25, 'Чингэлтэй дүүрэг');

insert into relationship (  name,relationshipid, ishead) values
('ӨРХИЙН ТЭРГҮҮН', 	01,true),
('ЭХНЭР/ НӨХӨР', 	02,false),
('ХҮҮ/ ОХИН', 	03,false),
('ЭЦЭГ/ ЭХ',	04,false),
('АХ/ ЭГЧ/ ДҮҮ',	05,false),
('ХАДАМ ЭЦЭГ/ ХАДАМ ЭХ',	06,false),
('ХҮРГЭН/ БЭР',	07,false),
('ӨВӨГ ЭЦЭГ/ ЭМЭГ ЭХ',	08,false),
('АЧ/ ЗЭЭ/ ГУЧ',	09,false),
('БУСАД ТӨРӨЛ ТӨРӨГСӨД',	10,false),
('ХАМААРАЛГҮЙ',	11,false);

insert into coach (coachid,name) values(1,'coach 01'),
(2,'coach 02'),
(3,'coach 03'),
(4,'coach 04'),
(5,'coach 05'),
(6,'coach 06'),
(7,'coach 07'),
(8,'coach 08'),
(9,'coach 09'),
(10,'coach 10'),
(11,'coach 11'),
(12,'coach 12'),
(13,'coach 13'),
(14,'coach 14'),
(15,'coach 15'),
(16,'coach 16'),
(17,'coach 17'),
(18,'coach 18'),
(19,'coach 19'),
(20,'coach 20'),
(21,'coach 21'),
(22,'coach 22'),
(23,'coach 23'),
(24,'coach 24'),
(25,'coach 25'),
(26,'coach 26'),
(27,'coach 27'),
(28,'coach 28'),
(29,'coach 29'),
(30,'coach 30'),
(31,'coach 31'),
(32,'coach 32'),
(33,'coach 33'),
(34,'coach 34'),
(35,'coach 35');

select * from tbluser;

insert into tbluser(
email ,
password ,
userid , 
username ,
roleid ,
coachid )values('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',101,'coach01',3,1),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',102,'coach02',3,2),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',103,'coach03',3,3),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',104,'coach04',3,4),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',105,'coach05',3,5),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',106,'coach06',3,6),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',107,'coach07',3,7),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',108,'coach08',3,8),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',109,'coach09',3,9),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',110,'coach10',3,10),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',111,'coach11',3,11),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',112,'coach12',3,12),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',113,'coach13',3,13),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',114,'coach14',3,14),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',115,'coach15',3,15),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',116,'coach16',3,16),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',117,'coach17',3,17),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',118,'coach18',3,18),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',119,'coach19',3,19),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',120,'coach20',3,20),

('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',121,'coach21',3,21),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',122,'coach22',3,22),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',123,'coach23',3,23),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',124,'coach24',3,24),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',125,'coach25',3,25),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',126,'coach26',3,26),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',127,'coach27',3,27),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',128,'coach28',3,28),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',129,'coach29',3,29),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',130,'coach30',3,30),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',131,'coach31',3,31),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',132,'coach32',3,32),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',133,'coach33',3,33),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',134,'coach34',3,34),
('coach@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',135,'coach35',3,35);


insert into householdmember (memberid , 
householdid ,
name ,
relationshipid ,
isparticipant ,
istogether )
select ROW_NUMBER() OVER (ORDER BY householdid) , 
householdid ,
name ,
1 ,
true ,
true from household;

insert into tbluser(email,password,userid,username,roleid,coachid )values('admin@mail.mn','?-}?{?+??aE?Ka?"2{.?????Ud???',1,'admin',1,null);
insert into householdstatus (id,name) values(0,'Хяналтын өрх'),(1,'Хөтөлбөрийн өрх');

