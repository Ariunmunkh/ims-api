create table tbluser(
userid int not null, 
username varchar(200) not null,
email varchar(200) not null,
password varchar(500) not null,
roleid int,
coachid int,
updated timestamp default current_timestamp,
updatedby int,
PRIMARY KEY (userid)
);
create table household
(
householdid int not null, 
status int default 0,
numberof int,
name varchar(200),
householdgroupid int,
districtid int,
section int,
registeredaddress varchar(200),
address varchar(200),
phone varchar(200),
latitude varchar(20),
longitude  varchar(20),
coachid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (householdid),
INDEX ind_household_status (status),  
CONSTRAINT fk_household_status FOREIGN KEY (status) REFERENCES householdstatus(ID),
INDEX ind_household_householdgroupid (householdgroupid),  
CONSTRAINT fk_household_householdgroupid FOREIGN KEY (householdgroupid) REFERENCES householdgroup(ID),
INDEX ind_household_districtid (districtid),  
CONSTRAINT fk_household_districtid FOREIGN KEY (districtid) REFERENCES district(districtid),
INDEX ind_household_coachid (coachid)
);
create table householdmember
(
memberid int not null, 
householdid int,
name varchar(200),
relationshipid int,
birthdate datetime,
gender int,
isparticipant bool,
istogether bool,
educationdegreeid int,
employmentstatusid int,
healthconditionid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (memberid),
INDEX ind_householdmember_educationdegreeid (educationdegreeid),  
CONSTRAINT fk_householdmember_educationdegreeid FOREIGN KEY (educationdegreeid) REFERENCES educationdegree(ID),
INDEX ind_householdmember_employmentstatusid (employmentstatusid),  
CONSTRAINT fk_householdmember_employmentstatusid FOREIGN KEY (employmentstatusid) REFERENCES employmentstatus(ID),
INDEX ind_householdmember_healthconditionid (healthconditionid),  
CONSTRAINT fk_householdmember_healthconditionid FOREIGN KEY (healthconditionid) REFERENCES healthcondition(ID)  
);
create table district
(
districtid int not null, 
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (districtid)
);
create table relationship
(
relationshipid int not null,
name varchar(200),
ishead bool,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (relationshipid)
);
create table project
(
id int not null,
name varchar(200),
leadername varchar(200),
leaderphone varchar(200),
location varchar(200),
implementation varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table coach
(
coachid int not null,
name varchar(200),
phone varchar(20),
projectid int,
districtid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (coachid),
INDEX ind_coach_projectid (projectid),  
CONSTRAINT fk_coach_projectid FOREIGN KEY (projectid) REFERENCES project(id)
);
create table householdvisit
(
visitid int not null,
householdid int,
visitdate datetime,
memberid int,
coachid int,
note varchar(2000),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (visitid),
INDEX ind_householdvisit_memberid (memberid),  
CONSTRAINT fk_householdvisit_memberid FOREIGN KEY (memberid) REFERENCES householdmember(memberid),
INDEX ind_householdvisit_coachid (coachid),  
CONSTRAINT fk_householdvisit_coachid FOREIGN KEY (coachid) REFERENCES coach(coachid)
);
create table meetingattendance
(
entryid int not null,
householdid int,
meetingdate datetime,
isjoin bool,
quantity int,
amount decimal,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_meetingattendance_householdid (householdid),  
CONSTRAINT fk_meetingattendance_householdid FOREIGN KEY (householdid) REFERENCES household(householdid)
);
create table loan
(
entryid int not null,
householdid int,
loandate datetime,
amount decimal,
loanpurposeid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_loan_householdid (householdid),  
CONSTRAINT fk_loan_householdid FOREIGN KEY (householdid) REFERENCES household(householdid),
INDEX ind_loan_loanpurposeid (loanpurposeid),  
CONSTRAINT fk_loan_loanpurposeid FOREIGN KEY (loanpurposeid) REFERENCES loanpurpose(id)
);
create table loanrepayment
(
entryid int not null,
householdid int,
repaymentdate datetime,
amount decimal,
balance decimal,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_loanrepayment_householdid (householdid),  
CONSTRAINT fk_loanrepayment_householdid FOREIGN KEY (householdid) REFERENCES household(householdid)
);
create table training
(
entryid int not null,
householdid int,
trainingdate datetime,
trainingtypeid int,
trainingandactivityid int,
organizationid int,
duration decimal,
isjoin bool,
memberid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_training_householdid (householdid),  
CONSTRAINT fk_training_householdid FOREIGN KEY (householdid) REFERENCES household(householdid),
INDEX ind_training_memberid (memberid),  
CONSTRAINT fk_training_memberid FOREIGN KEY (memberid) REFERENCES householdmember(memberid),
INDEX ind_training_trainingtypeid (trainingtypeid),  
CONSTRAINT fk_training_trainingtypeid FOREIGN KEY (trainingtypeid) REFERENCES trainingtype(id),
INDEX ind_training_trainingandactivityid (trainingandactivityid),  
CONSTRAINT fk_training_trainingandactivityid FOREIGN KEY (trainingandactivityid) REFERENCES trainingandactivity(id),
INDEX ind_training_organizationid (organizationid),  
CONSTRAINT fk_training_organizationid FOREIGN KEY (organizationid) REFERENCES organization(id)
);
create table improvement
(
entryid int not null,
householdid int,
plandate datetime,
selectedfarm varchar(200),
subbranchid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_improvement_householdid (householdid),  
CONSTRAINT fk_improvement_householdid FOREIGN KEY (householdid) REFERENCES household(householdid),
INDEX ind_improvement_subbranchid (subbranchid),  
CONSTRAINT fk_improvement_subbranchid FOREIGN KEY (subbranchid) REFERENCES subbranch(id)
);
create table investment
(
entryid int not null,
householdid int,
investmentdate datetime,
assetreceivedtypeid int,
assetreceivedid int,
quantity decimal,
unitprice decimal,
totalprice decimal,
note varchar(2000),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_investment_householdid (householdid),  
CONSTRAINT fk_investment_householdid FOREIGN KEY (householdid) REFERENCES household(householdid),
INDEX ind_improvement_assetreceivedtypeid (assetreceivedtypeid),  
CONSTRAINT fk_investment_assetreceivedtypeid FOREIGN KEY (assetreceivedtypeid) REFERENCES assetreceivedtype(id),
INDEX ind_improvement_assetreceivedid (assetreceivedid),  
CONSTRAINT fk_investment_assetreceivedid FOREIGN KEY (assetreceivedid) REFERENCES assetreceived(id)
);
create table othersupport
(
entryid int not null,
householdid int,
supportdate datetime,
supportreceivedtypeid int,
name varchar(200),
quantity  decimal,
unitprice  decimal,
totalprice decimal,
sponsoringorganizationid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_othersupport_householdid (householdid),  
CONSTRAINT fk_othersupport_householdid FOREIGN KEY (householdid) 
REFERENCES household(householdid),
INDEX ind_othersupport_supportreceivedtypeid (supportreceivedtypeid),  
CONSTRAINT fk_othersupport_supportreceivedtypeid FOREIGN KEY (supportreceivedtypeid) REFERENCES supportreceivedtype(id),
INDEX ind_othersupport_sponsoringorganizationid (sponsoringorganizationid),  
CONSTRAINT fk_othersupport_sponsoringorganizationid FOREIGN KEY (sponsoringorganizationid) REFERENCES sponsoringorganization(id)
);
create table mediatedactivity
(
entryid int not null,
householdid int,
mediateddate datetime,
mediatedservicetypeid int,
intermediaryorganizationid int,
proxyserviceid int,
memberid int,
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (entryid),
INDEX ind_mediatedactivity_householdid (householdid),  
CONSTRAINT fk_mediatedactivity_householdid FOREIGN KEY (householdid) REFERENCES household(householdid),
INDEX ind_mediatedactivity_memberid (memberid),  
CONSTRAINT fk_mediatedactivity_memberid FOREIGN KEY (memberid) REFERENCES householdmember(memberid),
INDEX ind_mediatedactivity_mediatedservicetypeid (mediatedservicetypeid),  
CONSTRAINT fk_mediatedactivity_mediatedservicetypeid FOREIGN KEY (mediatedservicetypeid) REFERENCES mediatedservicetype(id),
INDEX ind_mediatedactivity_intermediaryorganizationid (intermediaryorganizationid),  
CONSTRAINT fk_mediatedactivity_intermediaryorganizationid FOREIGN KEY (intermediaryorganizationid) REFERENCES intermediaryorganization(id),
INDEX ind_mediatedactivity_proxyserviceid (proxyserviceid),  
CONSTRAINT fk_mediatedactivity_proxyserviceid FOREIGN KEY (proxyserviceid) REFERENCES proxyservice(id)
);
create table householdstatus
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table educationdegree
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table employmentstatus
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table healthcondition
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table loanpurpose
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table trainingtype
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table trainingandactivity
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table organization
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table subbranch
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table assetreceivedtype
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table assetreceived
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table supportreceivedtype
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table sponsoringorganization
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table mediatedservicetype
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table intermediaryorganization
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table proxyservice
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);
create table householdgroup
(
id int not null,
name varchar(200),
updated timestamp default current_timestamp,
updatedby int,  
PRIMARY KEY (id)
);

