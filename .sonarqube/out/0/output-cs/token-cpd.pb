á
8S:\in≈ºynierka\api\es_webapi\Authorization\Extensions.cs
	namespace 	
ExpertalSystem
 
. 
Authorization &
{ 
public		 

static		 
class		 

Extensions		 "
{

 
public 
static 
IServiceCollection ("
AddCustomAuthorization) ?
(? @
this@ D
IServiceCollectionE W
servicesX `
,` a
IConfigurationb p
configurationq ~
)~ 
{ 	
services 
. 
AddAuthentication &
(& '
options' .
=>/ 1
{ 
options 
. %
DefaultAuthenticateScheme 1
=2 3
JwtBearerDefaults4 E
.E F 
AuthenticationSchemeF Z
;Z [
options 
. "
DefaultChallengeScheme .
=/ 0
JwtBearerDefaults1 B
.B C 
AuthenticationSchemeC W
;W X
} 
) 
. 
AddJwtBearer 
( 
options #
=>$ &
{ 
var 
key 
= 
Convert !
.! "
FromBase64String" 2
(2 3
configuration3 @
.@ A
GetValueA I
<I J
stringJ P
>P Q
(Q R
$strR Z
)Z [
)[ \
;\ ]
options 
.  
RequireHttpsMetadata ,
=- .
false/ 4
;4 5
options 
. 
	SaveToken !
=" #
true$ (
;( )
options 
. %
TokenValidationParameters 1
=2 3
new4 7%
TokenValidationParameters8 Q
{ 
ValidateIssuer "
=# $
true% )
,) *
ValidIssuer 
=  !
configuration" /
./ 0
GetValue0 8
<8 9
string9 ?
>? @
(@ A
$strA I
)I J
,J K$
ValidateIssuerSigningKey ,
=- .
true/ 3
,3 4
ValidateAudience $
=% &
false' ,
,, -
IssuerSigningKey $
=% &
new' * 
SymmetricSecurityKey+ ?
(? @
key@ C
)C D
,D E
} 
; 
} 
) 
; 
return 
services 
; 
}   	
}!! 
}"" ¢
4S:\in≈ºynierka\api\es_webapi\Authorization\Hasher.cs
	namespace 	
ExpertalSystem
 
. 
Authorization &
{ 
public 

static 
class 
Hasher 
{ 
public 
static 
string 
HashPassword )
() *
string* 0
password1 9
)9 :
{		 	
byte

 
[

 
]

 
salt

 
=

 
new

 
byte

 "
[

" #
$num

# %
]

% &
;

& '
new $
RNGCryptoServiceProvider (
(( )
)) *
.* +
GetBytes+ 3
(3 4
salt4 8
)8 9
;9 :
var 
pbkdf2 
= 
new 
Rfc2898DeriveBytes /
(/ 0
password0 8
,8 9
salt: >
,> ?
$num@ E
)E F
;F G
byte 
[ 
] 
hash 
= 
pbkdf2  
.  !
GetBytes! )
() *
$num* ,
), -
;- .
byte 
[ 
] 
	hashBytes 
= 
new "
byte# '
[' (
$num( *
]* +
;+ ,
Array 
. 
Copy 
( 
salt 
, 
$num 
, 
	hashBytes  )
,) *
$num+ ,
,, -
$num. 0
)0 1
;1 2
Array 
. 
Copy 
( 
hash 
, 
$num 
, 
	hashBytes  )
,) *
$num+ -
,- .
$num/ 1
)1 2
;2 3
return 
Convert 
. 
ToBase64String )
() *
	hashBytes* 3
)3 4
;4 5
} 	
public 
static 
bool 
Encrypt "
(" #
string# )
password* 2
,2 3
string4 :
savedPasswordHash; L
)L M
{ 	
byte 
[ 
] 
	hashBytes 
= 
Convert &
.& '
FromBase64String' 7
(7 8
savedPasswordHash8 I
)I J
;J K
byte 
[ 
] 
salt 
= 
new 
byte "
[" #
$num# %
]% &
;& '
Array 
. 
Copy 
( 
	hashBytes  
,  !
$num" #
,# $
salt% )
,) *
$num+ ,
,, -
$num. 0
)0 1
;1 2
var 
pbkdf2 
= 
new 
Rfc2898DeriveBytes /
(/ 0
password0 8
,8 9
salt: >
,> ?
$num@ E
)E F
;F G
byte 
[ 
] 
hash 
= 
pbkdf2  
.  !
GetBytes! )
() *
$num* ,
), -
;- .
for   
(   
int   
i   
=   
$num   
;   
i   
<   
$num    "
;  " #
i  $ %
++  % '
)  ' (
if!! 
(!! 
	hashBytes!! 
[!! 
i!! 
+!!  !
$num!!" $
]!!$ %
!=!!& (
hash!!) -
[!!- .
i!!. /
]!!/ 0
)!!0 1
return"" 
false""  
;""  !
return## 
true## 
;## 
}$$ 	
}%% 
}&& ß
9S:\in≈ºynierka\api\es_webapi\Authorization\IJWTManager.cs
	namespace 	
ExpertalSystem
 
. 
Authorization &
{ 
public 

	interface 
IJwtManager  
{ 
string 
GenerateToken 
( 
string #
id$ &
,& '
string( .
username/ 7
,7 8
int9 <
expireMinutes= J
=K L
$numM O
)O P
;P Q
} 
} µ
8S:\in≈ºynierka\api\es_webapi\Authorization\JwtManager.cs
	namespace 	
ExpertalSystem
 
. 
Authorization &
{ 
public		 

class		 

JwtManager		 
:		 
IJwtManager		 )
{

 
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
public 

JwtManager 
( 
IConfiguration (
configuration) 6
)6 7
{ 	
_configuration 
= 
configuration *
;* +
} 	
public 
string 
GenerateToken #
(# $
string$ *
id+ -
,- .
string/ 5
username6 >
,> ?
int@ C
expireMinutesD Q
=R S
$numT V
)V W
{ 	
var 
symmetricKey 
= 
Convert &
.& '
FromBase64String' 7
(7 8
_configuration8 F
.F G
GetValueG O
<O P
stringP V
>V W
(W X
$strX `
)` a
)a b
;b c
var 
tokenHandler 
= 
new "#
JwtSecurityTokenHandler# :
(: ;
); <
;< =
var 
now 
= 
DateTime 
. 
UtcNow %
;% &
var 
tokenDescriptor 
=  !
new" %#
SecurityTokenDescriptor& =
{ 
Subject 
= 
new 
ClaimsIdentity ,
(, -
new- 0
[0 1
]1 2
{ 
new 
Claim 
( 

ClaimTypes %
.% &
Name& *
,* +
username, 4
)4 5
,5 6
new 
Claim 
( 

ClaimTypes %
.% &
NameIdentifier& 4
,4 5
id6 8
)8 9
} 
) 
, 
Expires 
= 
now 
. 

AddMinutes (
(( )
Convert) 0
.0 1
ToInt321 8
(8 9
expireMinutes9 F
)F G
)G H
,H I
Issuer 
= 
_configuration '
.' (
GetValue( 0
<0 1
string1 7
>7 8
(8 9
$str9 A
)A B
,B C
SigningCredentials "
=# $
new% (
SigningCredentials) ;
(; <
new    
SymmetricSecurityKey   ,
(  , -
symmetricKey  - 9
)  9 :
,  : ;
SecurityAlgorithms!! &
.!!& '
HmacSha256Signature!!' :
)!!: ;
}"" 
;"" 
var$$ 
stoken$$ 
=$$ 
tokenHandler$$ %
.$$% &
CreateToken$$& 1
($$1 2
tokenDescriptor$$2 A
)$$A B
;$$B C
var%% 
token%% 
=%% 
tokenHandler%% $
.%%$ %

WriteToken%%% /
(%%/ 0
stoken%%0 6
)%%6 7
;%%7 8
return'' 
token'' 
;'' 
}(( 	
})) 
}** π
:S:\in≈ºynierka\api\es_webapi\Controllers\BaseController.cs
	namespace 	
ExpertalSystem
 
. 
Controllers $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
[ 
Produces 
( 
$str  
)  !
]! "
public 

class 
BaseController 
:  !
ControllerBase" 0
{		 
}

 
} Ã

?S:\in≈ºynierka\api\es_webapi\Controllers\ConcludingControler.cs
	namespace 	
ExpertalSystem
 
. 
Controllers $
{ 
public 

class 
ConcludingControler $
:% &
BaseController' 5
{		 
private

 
readonly

 
IQuestionService

 )
_questionService

* :
;

: ;
public 
ConcludingControler "
(" #
IQuestionService# 3
questionService4 C
)C D
{ 	
_questionService 
= 
questionService .
;. /
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
object' -
>- .
>. /
Conclude0 8
(8 9
ConcludeRequest9 H
requestI P
)P Q
{ 	
var 
result 
= 
await 
_questionService /
./ 0
Conclude0 8
(8 9
request9 @
)@ A
;A B
return 
Ok 
( 
result 
) 
; 
} 	
} 
} ’A
=S:\in≈ºynierka\api\es_webapi\Controllers\ProblemController.cs
	namespace

 	
ExpertalSystem


 
.

 
Controllers

 $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
ProblemController "
:# $
BaseController% 3
{ 
private 
readonly 
IProblemRepository +
_problemRepository, >
;> ?
private 
readonly 
IQuestionRepository ,
_questionRepository- @
;@ A
public 
ProblemController  
(  !
IProblemRepository! 3
problemRepository4 E
,E F
IQuestionRepository 
questionRepository  2
)2 3
{ 	
_problemRepository 
=  
problemRepository! 2
;2 3
_questionRepository 
=  !
questionRepository" 4
;4 5
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
<& '
IEnumerable' 2
<2 3
Domain3 9
.9 :
Problem: A
>A B
>B C
>C D
GetAllProblemsE S
(S T
[T U
	FromQueryU ^
]^ _!
GetAllProblemsRequest` u
requestv }
)} ~
{ 	
var 
	questions 
= 
await !
_problemRepository" 4
.4 5
	FindAsync5 >
(> ?
p? @
=>@ B
pB C
.C D
	IssueTypeD M
==N P
requestQ X
.X Y
	IssueTypeY b
)b c
;c d
return 
Ok 
( 
	questions 
)  
;  !
} 	
[   	
HttpGet  	 
(   
$str   
)   
]   
public!! 
async!! 
Task!! 
<!! 
ActionResult!! &
<!!& '
IEnumerable!!' 2
<!!2 3
Domain!!3 9
.!!9 :
Problem!!: A
>!!A B
>!!B C
>!!C D
GetAllProblems!!E S
(!!S T
[!!T U
	FromRoute!!U ^
]!!^ _
Guid!!` d
id!!e g
)!!g h
{"" 	
var## 
	questions## 
=## 
await## !
_problemRepository##" 4
.##4 5
GetAsync##5 =
(##= >
id##> @
)##@ A
;##A B
return$$ 
Ok$$ 
($$ 
	questions$$ 
)$$  
;$$  !
}%% 	
[,, 	
	Authorize,,	 
],, 
[-- 	
HttpPost--	 
]-- 
public.. 
async.. 
Task.. 
<.. 
ActionResult.. &
<..& '
Question..' /
>../ 0
>..0 1
CreateProblem..2 ?
(..? @
[..@ A
FromBody..A I
]..I J 
CreateProblemRequest..K _
request..` g
)..g h
{// 	
if00 
(00 
await00 
_problemRepository00 (
.00( )
GetAsync00) 1
(001 2
p002 3
=>004 6
p007 8
.008 9
ProblemName009 D
.00D E
Equals00E K
(00K L
request00L S
.00S T
ProblemName00T _
)00_ `
)00` a
!=00b d
null00e i
)00i j
return11 
Conflict11 
(11  
$str11  P
)11P Q
;11Q R
var33 

newProblem33 
=33 
new33  
Domain33! '
.33' (
Problem33( /
(33/ 0
)330 1
{44 
Id55 
=55 
Guid55 
.55 
NewGuid55 !
(55! "
)55" #
,55# $
ProblemName66 
=66 
request66 %
.66% &
ProblemName66& 1
,661 2
Solution77 
=77 
request77 "
.77" #
Solution77# +
,77+ ,
	IssueType88 
=88 
request88 #
.88# $
	IssueType88$ -
}99 
;99 
foreach;; 
(;; 
var;; 
question;;  
in;;! #
request;;$ +
.;;+ ,
	Questions;;, 5
);;5 6
{<< 
var== 
newQuestion== 
===  !
new==" %
Question==& .
(==. /
)==/ 0
{>> 
Id?? 
=?? 
Guid?? 
.?? 
NewGuid?? %
(??% &
)??& '
,??' (
Answers@@ 
=@@ 
new@@ !
List@@" &
<@@& '
string@@' -
>@@- .
(@@. /
)@@/ 0
{@@1 2
questionAA  
.AA  !
AnswerAA! '
}BB 
,BB 
QuestionNameCC  
=CC! "
questionCC# +
.CC+ ,
QuestionNameCC, 8
}DD 
;DD 
varFF 

dbQuestionFF 
=FF  
awaitFF! &
_questionRepositoryFF' :
.FF: ;
GetAsyncFF; C
(FFC D
pFFD E
=>FFF H
pFFI J
.FFJ K
QuestionNameFFK W
.FFW X
EqualsFFX ^
(FF^ _
questionFF_ g
.FFg h
QuestionNameFFh t
)FFt u
)FFu v
;FFv w
ifHH 
(HH 

dbQuestionHH 
!=HH !
nullHH" &
)HH& '
{II 

newProblemJJ 
.JJ 
	QuestionsJJ (
.JJ( )
AddJJ) ,
(JJ, -
newJJ- 0
QuestionBasicJJ1 >
{KK 
IdLL 
=LL 

dbQuestionLL '
.LL' (
IdLL( *
,LL* +
AnswerMM 
=MM  
questionMM! )
.MM) *
AnswerMM* 0
,MM0 1
QuestionNameNN $
=NN% &

dbQuestionNN' 1
.NN1 2
QuestionNameNN2 >
}OO 
)OO 
;OO 
ifPP 
(PP 
!PP 

dbQuestionPP #
.PP# $
AnswersPP$ +
.PP+ ,
ContainsPP, 4
(PP4 5
questionPP5 =
.PP= >
AnswerPP> D
)PPD E
)PPE F
{QQ 

dbQuestionRR "
.RR" #
AnswersRR# *
.RR* +
AddRR+ .
(RR. /
questionRR/ 7
.RR7 8
AnswerRR8 >
)RR> ?
;RR? @
awaitSS 
_questionRepositorySS 1
.SS1 2
UpdateAsyncSS2 =
(SS= >

dbQuestionSS> H
)SSH I
;SSI J
}TT 
}UU 
elseVV 
{WW 
awaitXX 
_questionRepositoryXX -
.XX- .
AddAsyncXX. 6
(XX6 7
newQuestionXX7 B
)XXB C
;XXC D

newProblemYY 
.YY 
	QuestionsYY (
.YY( )
AddYY) ,
(YY, -
newYY- 0
QuestionBasicYY1 >
{ZZ 
Id[[ 
=[[ 
newQuestion[[ (
.[[( )
Id[[) +
,[[+ ,
Answer\\ 
=\\  
question\\! )
.\\) *
Answer\\* 0
,\\0 1
QuestionName]] $
=]]% &
newQuestion]]' 2
.]]2 3
QuestionName]]3 ?
}^^ 
)^^ 
;^^ 
}__ 
}`` 
awaitaa 
_problemRepositoryaa $
.aa$ %
AddAsyncaa% -
(aa- .

newProblemaa. 8
)aa8 9
;aa9 :
returnbb 
Createdbb 
(bb 
$"bb 
{bb 
Requestbb %
.bb% &
Pathbb& *
.bb* +
Valuebb+ 0
}bb0 1
/bb1 2
{bb2 3

newProblembb3 =
.bb= >
Idbb> @
}bb@ A
"bbA B
,bbB C

newProblembbD N
)bbN O
;bbO P
}cc 	
}ee 
}ff ≥&
;S:\in≈ºynierka\api\es_webapi\Controllers\UsersController.cs
	namespace

 	
ExpertalSystem


 
.

 
Controllers

 $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
UsersController  
:! "
BaseController# 1
{ 
private 
readonly 
IUserRepository (
_userRepository) 8
;8 9
private 
readonly 
IJwtManager $
_jWTManager% 0
;0 1
public 
UsersController 
( 
IUserRepository .
userRepository/ =
,= >
IJwtManager? J

jWTManagerK U
)U V
{ 	
_userRepository 
= 
userRepository ,
;, -
_jWTManager 
= 

jWTManager $
;$ %
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (

CreateUser) 3
(3 4
[4 5
FromBody5 =
]= >
CreateUserRequest? P
requestQ X
)X Y
{   	
if!! 
(!! 
(!! 
await!! 
_userRepository!! &
.!!& '
GetAsync!!' /
(!!/ 0
p!!0 1
=>!!1 3
p!!3 4
.!!4 5
Name!!5 9
.!!9 :
Equals!!: @
(!!@ A
request!!A H
.!!H I
Name!!I M
)!!M N
)!!N O
!=!!P R
null!!S W
)!!W X
)!!X Y
return"" 

BadRequest"" !
(""! "
$str""" F
)""F G
;""G H
var$$ 
user$$ 
=$$ 
new$$ 
User$$ 
($$  
)$$  !
{%% 
Name&& 
=&& 
request&& 
.&& 
Name&& #
,&&# $
Password'' 
='' 
Hasher'' !
.''! "
HashPassword''" .
(''. /
request''/ 6
.''6 7
Password''7 ?
)''? @
,''@ A
Id(( 
=(( 
Guid(( 
.(( 
NewGuid(( !
(((! "
)((" #
})) 
;)) 
await** 
_userRepository** !
.**! "
AddAsync**" *
(*** +
user**+ /
)**/ 0
;**0 1
return++ 
Created++ 
(++ 
$str++ %
,++% &
user++' +
)+++ ,
;++, -
},, 	
[33 	
HttpPost33	 
(33 
$str33 
)33 
]33 
public44 
async44 
Task44 
<44 
IActionResult44 '
>44' (
Auth44) -
(44- .
[44. /
FromBody44/ 7
]447 8
AuthenticateRequest449 L
authenticateRequest44M `
)44` a
{55 	
var66 
fetchedUser66 
=66 
await66 #
_userRepository66$ 3
.663 4
GetAsync664 <
(66< =
x66= >
=>66> @
x66@ A
.66A B
Name66B F
.66F G
Equals66G M
(66M N
authenticateRequest66N a
.66a b
Login66b g
)66g h
)66h i
;66i j
if77 
(77 
fetchedUser77 
is77 
null77 #
)77# $
return77% +
NotFound77, 4
(774 5
$str775 X
)77X Y
;77Y Z
if99 
(99 
Hasher99 
.99 
Encrypt99 
(99 
authenticateRequest99 2
.992 3
Password993 ;
,99; <
fetchedUser99= H
.99H I
Password99I Q
)99Q R
)99R S
{:: 
var;; 
token;; 
=;; 
_jWTManager;; '
.;;' (
GenerateToken;;( 5
(;;5 6
fetchedUser;;6 A
.;;A B
Id;;B D
.;;D E
ToString;;E M
(;;M N
);;N O
,;;O P
fetchedUser;;Q \
.;;\ ]
Name;;] a
,;;a b
$num;;c h
);;h i
;;;i j
return<< 
Ok<< 
(<< 
new<< 
JwtToken<< &
{<<' (
Token<<) .
=<</ 0
token<<1 6
}<<7 8
)<<8 9
;<<9 :
}== 
return>> 

BadRequest>> 
(>> 
$str>> .
)>>. /
;>>/ 0
}?? 	
}@@ 
}AA ¥
1S:\in≈ºynierka\api\es_webapi\Domain\BaseEntity.cs
	namespace 	
ExpertalSystem
 
. 
Domain 
{ 
public 

class 

BaseEntity 
: 
IBase #
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
}		 
}

 ≈
0S:\in≈ºynierka\api\es_webapi\Domain\IssueType.cs
	namespace 	
ExpertalSystem
 
. 
Domain 
{ 
public 

enum 
	IssueType 
{ 
ScreenIssue 
= 
$num 
, 
HardwareIssue 
= 
$num 
, 
IOIssue 
= 
$num 
} 
}		 ©
.S:\in≈ºynierka\api\es_webapi\Domain\Problem.cs
	namespace 	
ExpertalSystem
 
. 
Domain 
{ 
public 

class 
Problem 
: 

BaseEntity %
{		 
public

 
string

 
ProblemName

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 
List 
< 
QuestionBasic !
>! "
	Questions# ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
=; <
new= @
ListA E
<E F
QuestionBasicF S
>S T
(T U
)U V
;V W
[ 	
BsonRepresentation	 
( 
BsonType $
.$ %
String% +
)+ ,
], -
public 
	IssueType 
	IssueType "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
Solution 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public 

class 
QuestionBasic 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Answer 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
QuestionName "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} ù
/S:\in≈ºynierka\api\es_webapi\Domain\Question.cs
	namespace 	
ExpertalSystem
 
. 
Domain 
{ 
public 

class 
Question 
: 

BaseEntity &
{ 
public 
string 
QuestionName 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 
string 
> 
Answers  
{! "
get# &
;& '
set( +
;+ ,
}- .
=/ 0
new1 4
List5 9
<9 :
string: @
>@ A
(A B
)B C
;C D
}		 
}

 Õ
+S:\in≈ºynierka\api\es_webapi\Domain\User.cs
	namespace 	
ExpertalSystem
 
. 
Domain 
{ 
public 

class 
User 
: 

BaseEntity "
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} è
-S:\in≈ºynierka\api\es_webapi\Dtos\JwtToken.cs
	namespace 	
ExpertalSystem
 
. 
Dtos 
{ 
public 

class 
JwtToken 
{ 
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
} 
} µ$
=S:\in≈ºynierka\api\es_webapi\LogicServices\EngineGenerator.cs
	namespace 	
ExpertalSystem
 
. 
LogicServices &
{ 
public 

class 
EngineGenerator  
:! "
IEngineGenerator# 3
{		 
private

 
readonly

 
IProblemRepository

 +
_problemRepository

, >
;

> ?
public 
EngineGenerator 
( 
IProblemRepository 1
problemRepository2 C
)C D
{ 	
_problemRepository 
=  
problemRepository! 2
;2 3
} 	
public 
async 
Task 
< (
RuleInferenceEngineExtension 6
>6 7
CreateEngine8 D
(D E
)E F
{ 	
var 
engine 
= 
new (
RuleInferenceEngineExtension 9
(9 :
): ;
;; <
var 
problems 
= 
await  
_problemRepository! 3
.3 4
	FindAsync4 =
(= >
)> ?
;? @
foreach 
( 
var 
problem 
in  "
problems# +
)+ ,
{ 
Rule 
rule 
= 
new 
Rule  $
($ %
problem% ,
., -
ProblemName- 8
)8 9
;9 :
foreach 
( 
var 
question $
in% '
problem( /
./ 0
	Questions0 9
)9 :
{ 
rule 
. 
AddAntecedent &
(& '
new' *
IsClause+ 3
(3 4
question4 <
.< =
QuestionName= I
,I J
questionK S
.S T
AnswerT Z
)Z [
)[ \
;\ ]
} 
rule 
. 
setConsequent "
(" #
new# &
IsClause' /
(/ 0
nameof0 6
(6 7
problem7 >
.> ?
Solution? G
)G H
,H I
problemJ Q
.Q R
SolutionR Z
)Z [
)[ \
;\ ]
engine 
. 
AddRule 
( 
rule #
)# $
;$ %
} 
return 
engine 
; 
}   	
public!! 
async!! 
Task!! 
<!! (
RuleInferenceEngineExtension!! 6
>!!6 7
CreateEngine!!8 D
(!!D E
	IssueType!!E N
	issueType!!O X
)!!X Y
{"" 	
var## 
engine## 
=## 
new## (
RuleInferenceEngineExtension## 9
(##9 :
)##: ;
;##; <
var$$ 
problems$$ 
=$$ 
await$$  
_problemRepository$$! 3
.$$3 4
	FindAsync$$4 =
($$= >
	issueType$$> G
)$$G H
;$$H I
foreach&& 
(&& 
var&& 
problem&&  
in&&! #
problems&&$ ,
)&&, -
{'' 
Rule(( 
rule(( 
=(( 
new(( 
Rule((  $
((($ %
problem((% ,
.((, -
ProblemName((- 8
)((8 9
;((9 :
foreach)) 
()) 
var)) 
question)) %
in))& (
problem))) 0
.))0 1
	Questions))1 :
))): ;
{** 
rule++ 
.++ 
AddAntecedent++ &
(++& '
new++' *
IsClause+++ 3
(++3 4
question++4 <
.++< =
QuestionName++= I
,++I J
question++K S
.++S T
Answer++T Z
)++Z [
)++[ \
;++\ ]
},, 
rule-- 
.-- 
setConsequent-- "
(--" #
new--# &
IsClause--' /
(--/ 0
nameof--0 6
(--6 7
problem--7 >
.--> ?
Solution--? G
)--G H
,--H I
problem--J Q
.--Q R
Solution--R Z
)--Z [
)--[ \
;--\ ]
engine.. 
... 
AddRule.. 
(.. 
rule.. #
)..# $
;..$ %
}// 
return11 
engine11 
;11 
}22 	
}33 
}44 ä
<S:\in≈ºynierka\api\es_webapi\LogicServices\FactsExtension.cs
	namespace 	
ExpertalSystem
 
. 
LogicServices &
{ 
public 

class 
FactsExtension 
:  !
WorkingMemory" /
{ 
public 
object 
GetFacts 
( 
WorkingMemory ,
wm- /
)/ 0
=>1 3
wm4 6
.6 7
GetType7 >
(> ?
)? @
.@ A
GetFieldA I
(I J
$strJ R
,R S
BindingFlagsT `
.` a
	NonPublica j
|k l
BindingFlagsm y
.y z
Instance	z Ç
)
Ç É
.
É Ñ
GetValue
Ñ å
(
å ç
wm
ç è
)
è ê
;
ê ë
}		 
}

 ¥
>S:\in≈ºynierka\api\es_webapi\LogicServices\IEngineGenerator.cs
	namespace 	
ExpertalSystem
 
. 
LogicServices &
{ 
public 

	interface 
IEngineGenerator %
{ 
public 
Task 
< (
RuleInferenceEngineExtension 0
>0 1
CreateEngine2 >
(> ?
)? @
;@ A
public		 
Task		 
<		 (
RuleInferenceEngineExtension		 0
>		0 1
CreateEngine		2 >
(		> ?
	IssueType		? H
	issueType		I R
)		R S
;		S T
}

 
} È
>S:\in≈ºynierka\api\es_webapi\LogicServices\IQuestionService.cs
	namespace 	
ExpertalSystem
 
. 
LogicServices &
{ 
public 

	interface 
IQuestionService %
{ 
public 
Task 
< 
object 
> 
Conclude $
($ %
ConcludeRequest% 4
request5 <
)< =
;= >
}		 
}

 ¶(
=S:\in≈ºynierka\api\es_webapi\LogicServices\QuestionService.cs
	namespace 	
ExpertalSystem
 
. 
LogicServices &
{ 
public		 

class		 
QuestionService		  
:		! "
IQuestionService		# 3
{

 
private 
readonly 
IEngineGenerator )

_generator* 4
;4 5
private 
readonly 
IQuestionRepository ,
_questionRepository- @
;@ A
public 
QuestionService 
( 
IEngineGenerator /
	generator0 9
,9 :
IQuestionRepository; N
questionRepositoryO a
)a b
{ 	

_generator 
= 
	generator "
;" #
_questionRepository 
=  !
questionRepository" 4
;4 5
} 	
public 
async 
Task 
< 
object  
>  !
Conclude" *
(* +
ConcludeRequest+ :
request; B
)B C
{ 	
var 
engine 
= 
await 

_generator )
.) *
CreateEngine* 6
(6 7
request7 >
.> ?
Type? C
)C D
;D E
Clause 
currentClause  
=! "
null# '
;' (
List 
< 
string 
> &
answersForCurrentQuestions 3
=4 5
null6 :
;: ;
var 
unprovedConditions "
=# $
new% (
List) -
<- .
Clause. 4
>4 5
(5 6
)6 7
;7 8
if 
( 
request 
. 
Facts 
!=  
null! %
)% &
{' (
foreach 
( 
IncomingClause '
clause( .
in/ 1
request2 9
.9 :
Facts: ?
)? @
{ 
var 
fact 
= 
new "
IsClause# +
(+ ,
clause, 2
.2 3
Variable3 ;
,; <
clause= C
.C D
ValueD I
)I J
;J K
engine 
. 
AddFact "
(" #
fact# '
)' (
;( )
}   
}!! 
if"" 
("" 
request"" 
."" 
Answer"" 
!="" !
null""" &
)""& '
engine## 
.## 
AddFact## 
(## 
new## "
IsClause### +
(##+ ,
request##, 3
.##3 4
PreviousQuestion##4 D
,##D E
request##F M
.##M N
Answer##N T
)##T U
)##U V
;##V W
var%% 

conclusion%% 
=%% 
engine%% #
.%%# $
Infer%%$ )
(%%) *
$str%%* 4
,%%4 5
unprovedConditions%%6 H
)%%H I
;%%I J
if'' 
('' 

conclusion'' 
is'' 
null'' "
)''" #
{(( 
currentClause)) 
=)) 
unprovedConditions))  2
[))2 3
$num))3 4
]))4 5
;))5 6&
answersForCurrentQuestions** *
=**+ ,
await**- 2
_questionRepository**3 F
.**F G
GetQuestionAnswers**G Y
(**Y Z
currentClause**Z g
.**g h
Variable**h p
)**p q
;**q r
}++ 
if-- 
(-- 

conclusion-- 
!=-- 
null-- "
)--" #
return--$ *
new--+ .
{.. 
question// 
=// 
$str// 
,// 
answers00 
=00 
new00 
List00 "
<00" #
string00# )
>00) *
(00* +
)00+ ,
,00, -
facts11 
=11 
$str11 
,11 
consequence22 
=22 
$"22  
{22  !

conclusion22! +
.22+ ,
Value22, 1
}221 2
:222 3
{223 4

conclusion224 >
.22> ?
Value22? D
}22D E
"22E F
}33 
;33 
return55 
new55 
{66 
question77 
=77 
currentClause77 (
.77( )
Variable77) 1
,771 2
answers88 
=88 &
answersForCurrentQuestions88 4
,884 5
facts99 
=99 
engine99 
.99 
Accessor99 '
.99' (
GetFacts99( 0
(990 1
engine991 7
.997 8
Facts998 =
)99= >
,99> ?
consequence:: 
=:: 
$str::  
};; 
;;; 
}<< 	
}== 
}>> Ñ
JS:\in≈ºynierka\api\es_webapi\LogicServices\RuleInferenceEngineExtension.cs
	namespace 	
ExpertalSystem
 
. 
LogicServices &
{ 
public 

class (
RuleInferenceEngineExtension -
:. /
RuleInferenceEngine0 C
{ 
public 
FactsExtension 
Accessor &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
} 
}		 ä
+S:\in≈ºynierka\api\es_webapi\Mongo\IBase.cs
	namespace 	
ExpertalSystem
 
. 
Mongo 
{ 
public 

	interface 
IBase 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
}		 ò
1S:\in≈ºynierka\api\es_webapi\Mongo\IRepository.cs
	namespace 	
ExpertalSystem
 
. 
Mongo 
{ 
public 

	interface 
IRepository  
<  !
TEntity! (
>( )
where* /
TEntity0 7
:8 9
IBase: ?
{		 
Task

 
<

 
TEntity

 
>

 
GetAsync

 
(

 
Guid

 #
id

$ &
)

& '
;

' (
Task 
< 
TEntity 
> 
GetAsync 
( 

Expression )
<) *
Func* .
<. /
TEntity/ 6
,6 7
bool8 <
>< =
>= >

expression? I
)I J
;J K
Task 
< 
IEnumerable 
< 
TEntity  
>  !
>! "
	FindAsync# ,
(, -
)- .
;. /
Task 
< 
IEnumerable 
< 
TEntity  
>  !
>! "
	FindAsync# ,
(, -

Expression- 7
<7 8
Func8 <
<< =
TEntity= D
,D E
boolF J
>J K
>K L

expressionM W
)W X
;X Y
Task 
AddAsync 
( 
TEntity 
entity $
)$ %
;% &
Task 
UpdateAsync 
( 
TEntity  
entity! '
)' (
;( )
} 
} ˜
3S:\in≈ºynierka\api\es_webapi\Mongo\MongoDbSeeder.cs
	namespace 	
ExpertalSystem
 
. 
Mongo 
{ 
public 

static 
class 
MongoDbSeeder %
{ 
public 
static 
void 
Seed 
<  
TEntity  '
>' (
(( )
IMongoDatabase) 7
db8 :
,: ;
string< B

collectionC M
)M N
whereO T
TEntityU \
:] ^
IBase_ d
{ 	
if		 
(		 
db		 
.		 
GetCollection		  
<		  !
TEntity		! (
>		( )
(		) *

collection		* 4
)		4 5
==		6 8
null		9 =
)		= >
db

 
.

 
CreateCollection

 #
(

# $

collection

$ .
)

. /
;

/ 0
} 	
} 
} ‹#
4S:\in≈ºynierka\api\es_webapi\Mongo\MongoExtension.cs
	namespace 	
ExpertalSystem
 
. 
Mongo 
{ 
public 

static 
class 
MongoExtension &
{ 
public		 
static		 
void		 
AddMongo		 #
(		# $
this		$ (
ContainerBuilder		) 9
builder		: A
)		A B
{

 	
builder 
. 
Register 
( 
context $
=>% '
{ 
var 
configuration !
=" #
context$ +
.+ ,
Resolve, 3
<3 4
IConfiguration4 B
>B C
(C D
)D E
;E F
var 
options 
= 
configuration +
.+ ,

GetOptions, 6
<6 7
MongoOptions7 C
>C D
(D E
$strE L
)L M
;M N
return 
options 
; 
} 
) 
; 
builder 
. 
Register 
( 
context $
=>% '
{ 
var 
options 
= 
context %
.% &
Resolve& -
<- .
MongoOptions. :
>: ;
(; <
)< =
;= >
return 
new 
MongoClient &
(& '
options' .
.. /
ConnectionString/ ?
)? @
;@ A
} 
) 
. 
SingleInstance 
( 
) 
;  
builder 
. 
Register 
( 
context $
=>% '
{ 
var 
options 
= 
context %
.% &
Resolve& -
<- .
MongoOptions. :
>: ;
(; <
)< =
;= >
var 
client 
= 
context $
.$ %
Resolve% ,
<, -
MongoClient- 8
>8 9
(9 :
): ;
;; <
return 
client 
. 
GetDatabase )
() *
options* 1
.1 2
DbName2 8
)8 9
;9 :
} 
) 
. $
InstancePerLifetimeScope '
(' (
)( )
;) *
}   	
public## 
static## 
void## 
AddRepository## (
<##( )
TEntity##) 0
>##0 1
(##1 2
this##2 6
ContainerBuilder##7 G
builder##H O
,##O P
string##Q W

collection##X b
)##b c
where##d i
TEntity##j q
:##r s
IBase##t y
{$$ 	
builder%% 
.%% 
Register%% 
(%% 
context%% $
=>%%% '
new&& 

Repository&& 
<&& 
TEntity&& #
>&&# $
(&&$ %
context&&% ,
.&&, -
Resolve&&- 4
<&&4 5
IMongoDatabase&&5 C
>&&C D
(&&D E
)&&E F
,&&F G

collection&&H R
)&&R S
)'' 
.'' 
As'' 
<'' 
IRepository'' 
<'' 
TEntity'' $
>''$ %
>''% &
(''& '
)''' (
.''( )$
InstancePerLifetimeScope'') A
(''A B
)''B C
;''C D
}(( 	
public** 
static** 
TModel** 

GetOptions** '
<**' (
TModel**( .
>**. /
(**/ 0
this**0 4
IConfiguration**5 C
configuration**D Q
,**Q R
string**S Y
section**Z a
)**a b
where**c h
TModel**i o
:**p q
new**r u
(**u v
)**v w
{++ 	
var,, 
model,, 
=,, 
new,, 
TModel,, "
(,," #
),,# $
;,,$ %
configuration-- 
.-- 

GetSection-- $
(--$ %
section--% ,
)--, -
.--- .
Bind--. 2
(--2 3
model--3 8
)--8 9
;--9 :
return// 
model// 
;// 
}00 	
}11 
}22 æ
2S:\in≈ºynierka\api\es_webapi\Mongo\MongoOptions.cs
	namespace 	
ExpertalSystem
 
. 
Mongo 
{ 
public 

class 
MongoOptions 
{ 
public 
string 
ConnectionString &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
DbName 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} Ê!
0S:\in≈ºynierka\api\es_webapi\Mongo\Repository.cs
	namespace 	
ExpertalSystem
 
. 
Mongo 
{ 
public		 

class		 

Repository		 
<		 
TEntity		 #
>		# $
:		% &
IRepository		' 2
<		2 3
TEntity		3 :
>		: ;
where		< A
TEntity		B I
:		J K
IBase		L Q
{

 
private 
readonly 
IMongoCollection )
<) *
TEntity* 1
>1 2
_mongoCollection3 C
;C D
public 

Repository 
( 
IMongoDatabase (
mongoDatabase) 6
,6 7
string8 >

collection? I
)I J
{ 	
_mongoCollection 
= 
mongoDatabase ,
., -
GetCollection- :
<: ;
TEntity; B
>B C
(C D

collectionD N
)N O
;O P
} 	
public 
async 
Task 
AddAsync "
(" #
TEntity# *
entity+ 1
)1 2
=> 
await 
_mongoCollection %
.% &
InsertOneAsync& 4
(4 5
entity5 ;
,; <
new= @
InsertOneOptionsA Q
(Q R
)R S
)S T
;T U
public 
async 
Task 
< 
IEnumerable %
<% &
TEntity& -
>- .
>. /
	FindAsync0 9
(9 :

Expression: D
<D E
FuncE I
<I J
TEntityJ Q
,Q R
boolS W
>W X
>X Y

expressionZ d
)d e
=> 
await 
_mongoCollection &
.& '
Find' +
(+ ,

expression, 6
)6 7
.7 8
ToListAsync8 C
(C D
)D E
;E F
public 
async 
Task 
< 
IEnumerable %
<% &
TEntity& -
>- .
>. /
	FindAsync0 9
(9 :
): ;
=> 
await 
_mongoCollection %
.% &
Find& *
(* +
p+ ,
=>, .
true. 2
)2 3
.3 4
ToListAsync4 ?
(? @
)@ A
;A B
public 
async 
Task 
< 
TEntity !
>! "
GetAsync# +
(+ ,
Guid, 0
id1 3
)3 4
=> 

await 
_mongoCollection !
.! "
Find" &
(& '
p' (
=>) +
p, -
.- .
Id. 0
.0 1
Equals1 7
(7 8
id8 :
): ;
); <
.< = 
SingleOrDefaultAsync= Q
(Q R
)R S
;S T
public 
async 
Task 
< 
TEntity !
>! "
GetAsync# +
(+ ,

Expression, 6
<6 7
Func7 ;
<; <
TEntity< C
,C D
boolE I
>I J
>J K

expressionL V
)V W
=> 

await 
_mongoCollection !
.! "
Find" &
(& '

expression' 1
)1 2
.2 3 
SingleOrDefaultAsync3 G
(G H
)H I
;I J
public   
async   
Task   
UpdateAsync   %
(  % &
TEntity  & -
entity  . 4
)  4 5
{!! 	
await"" 
_mongoCollection"" "
.""" #
ReplaceOneAsync""# 2
<""2 3
TEntity""3 :
>"": ;
(""; <
p""< =
=>""> @
p""A B
.""B C
Id""C E
==""F H
entity""I O
.""O P
Id""P R
,""R S
entity""T Z
)""Z [
;""[ \
}## 	
}$$ 
}%% ≈
'S:\in≈ºynierka\api\es_webapi\Program.cs
	namespace 	
ExpertalSystem
 
{ 
public 

static 
class 
Program 
{ 
public		 
static		 
void		 
Main		 
(		  
string		  &
[		& '
]		' (
args		) -
)		- .
{

 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
.+ ,%
UseServiceProviderFactory, E
(E F
newF I)
AutofacServiceProviderFactoryJ g
(g h
)h i
)i j
.j k$
ConfigureWebHostDefaults (
(( )

webBuilder) 3
=>4 6
{ 

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
; 
} 
} ‚
?S:\in≈ºynierka\api\es_webapi\Repositories\IProblemRepository.cs
	namespace 	
ExpertalSystem
 
. 
Repositories %
{ 
public 

	interface 
IProblemRepository '
:( )
IRepository* 5
<5 6
Problem6 =
>= >
{		 
Task

 
<

 
IEnumerable

 
<

 
Problem

  
>

  !
>

! "
	FindAsync

# ,
(

, -
	IssueType

- 6
type

7 ;
)

; <
;

< =
} 
} ˆ
@S:\in≈ºynierka\api\es_webapi\Repositories\IQuestionRepository.cs
	namespace 	
ExpertalSystem
 
. 
Repositories %
{ 
public		 

	interface		 
IQuestionRepository		 (
:		) *
IRepository		+ 6
<		6 7
Question		7 ?
>		? @
{

 
public 
Task 
< 
List 
< 
string 
>  
>  !
GetQuestionAnswers" 4
(4 5
Guid5 9
id: <
)< =
;= >
public 
Task 
< 
List 
< 
string 
>  
>  !
GetQuestionAnswers" 4
(4 5
string5 ;
questionName< H
)H I
;I J
} 
} ’
<S:\in≈ºynierka\api\es_webapi\Repositories\IUserRepository.cs
	namespace 	
ExpertalSystem
 
. 
Repositories %
{ 
public 

	interface 
IUserRepository $
:% &
IRepository' 2
<2 3
User3 7
>7 8
{ 
Task		 
<		 
User		 
>		 
GetAsync		 
(		 
string		 "
login		# (
,		( )
string		* 0
password		1 9
)		9 :
;		: ;
}

 
} ¯
>S:\in≈ºynierka\api\es_webapi\Repositories\ProblemRepository.cs
	namespace 	
ExpertalSystem
 
. 
Repositories %
{		 
public

 

class

 
ProblemRepository

 "
:

# $
IProblemRepository

% 7
{ 
private 
readonly 
IRepository $
<$ %
Problem% ,
>, -
_repository. 9
;9 :
public 
ProblemRepository  
(  !
IRepository! ,
<, -
Problem- 4
>4 5

repository6 @
)@ A
{ 	
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
AddAsync "
(" #
Problem# *
entity+ 1
)1 2
=> 
await 
_repository  
.  !
AddAsync! )
() *
entity* 0
)0 1
;1 2
public 
async 
Task 
< 
IEnumerable %
<% &
Problem& -
>- .
>. /
	FindAsync0 9
(9 :

Expression: D
<D E
FuncE I
<I J
ProblemJ Q
,Q R
boolS W
>W X
>X Y

expressionZ d
)d e
=> 
await 
_repository  
.  !
	FindAsync! *
(* +

expression+ 5
)5 6
;6 7
public## 
async## 
Task## 
<## 
IEnumerable## %
<##% &
Problem##& -
>##- .
>##. /
	FindAsync##0 9
(##9 :
	IssueType##: C
type##D H
)##H I
=>$$ 
await$$ 
_repository$$  
.$$  !
	FindAsync$$! *
($$* +
p$$+ ,
=>$$- /
p$$0 1
.$$1 2
	IssueType$$2 ;
==$$< >
type$$? C
)$$C D
;$$D E
public&& 
async&& 
Task&& 
<&& 
IEnumerable&& %
<&&% &
Problem&&& -
>&&- .
>&&. /
	FindAsync&&0 9
(&&9 :
)&&: ;
=>'' 
await'' 
_repository''  
.''  !
	FindAsync''! *
(''* +
)''+ ,
;'', -
public)) 
async)) 
Task)) 
<)) 
Problem)) !
>))! "
GetAsync))# +
())+ ,
Guid)), 0
id))1 3
)))3 4
=>** 
await** 
_repository**  
.**  !
GetAsync**! )
(**) *
id*** ,
)**, -
;**- .
public,, 
async,, 
Task,, 
<,, 
Problem,, !
>,,! "
GetAsync,,# +
(,,+ ,

Expression,,, 6
<,,6 7
Func,,7 ;
<,,; <
Problem,,< C
,,,C D
bool,,E I
>,,I J
>,,J K

expression,,L V
),,V W
=>-- 
await-- 
_repository--  
.--  !
GetAsync--! )
(--) *

expression--* 4
)--4 5
;--5 6
public// 
async// 
Task// 
UpdateAsync// %
(//% &
Problem//& -
entity//. 4
)//4 5
=>00 
await00 
_repository00  
.00  !
UpdateAsync00! ,
(00, -
entity00- 3
)003 4
;004 5
}11 
}22 ˘"
?S:\in≈ºynierka\api\es_webapi\Repositories\QuestionRepository.cs
	namespace 	
ExpertalSystem
 
. 
Repositories %
{		 
public

 

class

 
QuestionRepository

 #
:

$ %
IQuestionRepository

& 9
{ 
private 
readonly 
IRepository $
<$ %
Question% -
>- .
_repository/ :
;: ;
public 
QuestionRepository !
(! "
IRepository" -
<- .
Question. 6
>6 7

repository8 B
)B C
{ 	
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
AddAsync "
(" #
Question# +
entity, 2
)2 3
=> 
await 
_repository  
.  !
AddAsync! )
() *
entity* 0
)0 1
;1 2
public 
async 
Task 
< 
IEnumerable %
<% &
Question& .
>. /
>/ 0
	FindAsync1 :
(: ;

Expression; E
<E F
FuncF J
<J K
QuestionK S
,S T
boolU Y
>Y Z
>Z [

expression\ f
)f g
=> 
await 
_repository  
.  !
	FindAsync! *
(* +
p+ ,
=>- /
p0 1
!=2 4
null5 9
)9 :
;: ;
public 
async 
Task 
< 
IEnumerable %
<% &
Question& .
>. /
>/ 0
	FindAsync1 :
(: ;
); <
=> 
await 
_repository  
.  !
	FindAsync! *
(* +
)+ ,
;, -
public 
async 
Task 
< 
Question "
>" #
GetAsync$ ,
(, -
Guid- 1
id2 4
)4 5
=> 
await 
_repository  
.  !
GetAsync! )
() *
id* ,
), -
;- .
public 
async 
Task 
< 
Question "
>" #
GetAsync$ ,
(, -

Expression- 7
<7 8
Func8 <
<< =
Question= E
,E F
boolG K
>K L
>L M

expressionN X
)X Y
=> 
await 
_repository  
.  !
GetAsync! )
() *

expression* 4
)4 5
;5 6
public   
async   
Task   
<   
List   
<   
string   %
>  % &
>  & '
GetQuestionAnswers  ( :
(  : ;
Guid  ; ?
id  @ B
)  B C
=>!! 
(!! 
await!! 
_repository!! !
.!!! "
GetAsync!!" *
(!!* +
p!!+ ,
=>!!- /
p!!0 1
.!!1 2
Id!!2 4
==!!5 7
id!!8 :
)!!: ;
)!!; <
.!!< =
Answers!!= D
;!!D E
public## 
async## 
Task## 
<## 
List## 
<## 
string## %
>##% &
>##& '
GetQuestionAnswers##( :
(##: ;
string##; A
questionName##B N
)##N O
=>$$ 
($$ 
await$$ 
_repository$$ !
.$$! "
GetAsync$$" *
($$* +
p$$+ ,
=>$$- /
p$$0 1
.$$1 2
QuestionName$$2 >
.$$> ?
Equals$$? E
($$E F
questionName$$F R
)$$R S
)$$S T
)$$T U
.$$U V
Answers$$V ]
;$$] ^
public&& 
async&& 
Task&& 
UpdateAsync&& %
(&&% &
Question&&& .
entity&&/ 5
)&&5 6
=>'' 
await'' 
_repository''  
.''  !
UpdateAsync''! ,
('', -
entity''- 3
)''3 4
;''4 5
}(( 
})) •
;S:\in≈ºynierka\api\es_webapi\Repositories\UserRepository.cs
	namespace 	
ExpertalSystem
 
. 
Repositories %
{		 
public

 

class

 
UserRepository

 
:

  !
IUserRepository

" 1
{ 
private 
readonly 
IRepository $
<$ %
User% )
>) *
_repository+ 6
;6 7
public 
UserRepository 
( 
IRepository )
<) *
User* .
>. /

repository0 :
): ;
{ 	
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
AddAsync "
(" #
User# '
entity( .
). /
=> 
await 
_repository  
.  !
AddAsync! )
() *
entity* 0
)0 1
;1 2
public 
async 
Task 
< 
IEnumerable %
<% &
User& *
>* +
>+ ,
	FindAsync- 6
(6 7

Expression7 A
<A B
FuncB F
<F G
UserG K
,K L
boolM Q
>Q R
>R S

expressionT ^
)^ _
=> 
await 
_repository  
.  !
	FindAsync! *
(* +
p+ ,
=>- /
p0 1
!=2 4
null5 9
)9 :
;: ;
public 
async 
Task 
< 
IEnumerable %
<% &
User& *
>* +
>+ ,
	FindAsync- 6
(6 7
)7 8
=> 
await 
_repository  
.  !
	FindAsync! *
(* +
)+ ,
;, -
public 
async 
Task 
< 
User 
> 
GetAsync  (
(( )
string) /
login0 5
,5 6
string7 =
password> F
)F G
=> 
await 
_repository  
.  !
GetAsync! )
() *
x* +
=>+ -
x- .
.. /
Name/ 3
.3 4
Equals4 :
(: ;
login; @
)@ A
&&B D
xE F
.F G
PasswordG O
.O P
EqualsP V
(V W
passwordW _
)_ `
)` a
;a b
public 
async 
Task 
< 
User 
> 
GetAsync  (
(( )

Expression) 3
<3 4
Func4 8
<8 9
User9 =
,= >
bool? C
>C D
>D E

expressionF P
)P Q
=> 
await 
_repository  
.  !
GetAsync! )
() *

expression* 4
)4 5
;5 6
public   
async   
Task   
<   
User   
>   
GetAsync    (
(  ( )
Guid  ) -
id  . 0
)  0 1
=>!! 
await!! 
_repository!!  
.!!  !
GetAsync!!! )
(!!) *
id!!* ,
)!!, -
;!!- .
public## 
Task## 
UpdateAsync## 
(##  
User##  $
entity##% +
)##+ ,
{$$ 	
throw%% 
new%% #
NotImplementedException%% -
(%%- .
)%%. /
;%%/ 0
}&& 	
}'' 
}(( …
<S:\in≈ºynierka\api\es_webapi\Requests\AuthenticateRequest.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class 
AuthenticateRequest $
{ 
public 
string 
Login 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} „
8S:\in≈ºynierka\api\es_webapi\Requests\ConcludeRequest.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class 
ConcludeRequest  
{ 
public		 
string		 
?		 
Answer		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
public

 
string

 
?

 
PreviousQuestion

 '
{

( )
get

* -
;

- .
set

/ 2
;

2 3
}

4 5
public 
List 
< 
IncomingClause "
>" #
?# $
Facts% *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
	IssueType 
Type 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} „

=S:\in≈ºynierka\api\es_webapi\Requests\CreateProblemRequest.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class  
CreateProblemRequest %
{ 
public 
string 
ProblemName !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
List		 
<		 
QuestionRequest		 #
>		# $
	Questions		% .
{		/ 0
get		1 4
;		4 5
set		6 9
;		9 :
}		; <
public

 
	IssueType

 
	IssueType

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
public 
string 
Solution 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public 

class 
QuestionRequest  
{ 
public 
string 
QuestionName "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
Answer 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} ƒ
:S:\in≈ºynierka\api\es_webapi\Requests\CreateUserRequest.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class 
CreateUserRequest "
{ 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ∏
>S:\in≈ºynierka\api\es_webapi\Requests\GetAllProblemsRequest.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class !
GetAllProblemsRequest &
{ 
public 
	IssueType 
	IssueType "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
}		 ≠
:S:\in≈ºynierka\api\es_webapi\Requests\GetProblemRequest.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class 
GetProblemRequest "
{ 
public 
ProblemType 
ProblemType &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
? 
	SessionId  
{! "
get# &
;& '
set( +
;+ ,
}- .
}		 
public 

enum 
ProblemType 
{ 
ScreenQuestion 
, 
HardwareQuestion 
, 

IOQuestion 
} 
} ‹
7S:\in≈ºynierka\api\es_webapi\Requests\IncomingClause.cs
	namespace 	
ExpertalSystem
 
. 
Requests !
{ 
public 

class 
IncomingClause 
{ 
public 
string 
	Condition 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Variable 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Value 
{ 
get 
;  
set! $
;$ %
}& '
} 
}		 ∆6
'S:\in≈ºynierka\api\es_webapi\Startup.cs
	namespace 	
ExpertalSystem
 
{ 
public 

class 
Startup 
{ 
private 
ILifetimeScope 
AutofacContainer /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
services 
. 
AddCors 
( 
o 
=> !
o" #
.# $
AddDefaultPolicy$ 4
(4 5
builder5 <
=>= ?
{   
builder!! 
.!! 
AllowAnyOrigin!! &
(!!& '
)!!' (
."" 
AllowAnyMethod"" &
(""& '
)""' (
.## 
AllowAnyHeader## &
(##& '
)##' (
;##( )
}$$ 
)$$ 
)$$ 
;$$ 
services%% 
.%% "
AddCustomAuthorization%% +
(%%+ ,
Configuration%%, 9
)%%9 :
;%%: ;
services'' 
.'' 
AddControllers'' #
(''# $
)''$ %
;''% &
services(( 
.(( 

AddSwagger(( 
(((  
)((  !
;((! "
services** 
.** 

AddMvcCore** 
(**  
options**  '
=>**( *
{++ 
options,, 
.,, !
EnableEndpointRouting,, -
=,,. /
false,,0 5
;,,5 6
}-- 
)-- 
.-- 
AddJsonOptions-- 
(-- 
options-- %
=>--& (
{.. 
options// 
.// !
JsonSerializerOptions// -
.//- .

Converters//. 8
.//8 9
Add//9 <
(//< =
new//= @
System//A G
.//G H
Text//H L
.//L M
Json//M Q
.//Q R
Serialization//R _
.//_ `#
JsonStringEnumConverter//` w
(//w x
)//x y
)//y z
;//z {
}00 
)00 
.00 
AddNewtonsoftJson00  
(00  !
)00! "
;00" #
services22 
.22 
AddMvc22 
(22 
)22 
;22 
}33 	
public55 
void55 
ConfigureContainer55 &
(55& '
ContainerBuilder55' 7
builder558 ?
)55? @
{66 	
BsonSerializer77 
.77 
RegisterSerializer77 -
<77- .
IBase77. 3
>773 4
(774 5
new775 84
(ImpliedImplementationInterfaceSerializer779 a
<77a b
IBase77b g
,77g h

BaseEntity77i s
>77s t
(77t u
BsonSerializer	77u É
.
77É Ñ
LookupSerializer
77Ñ î
<
77î ï

BaseEntity
77ï ü
>
77ü †
(
77† °
)
77° ¢
)
77¢ £
)
77£ §
;
77§ •
builder88 
.88 !
RegisterAssemblyTypes88 )
(88) *
Assembly88* 2
.882 3
GetEntryAssembly883 C
(88C D
)88D E
)88E F
.88F G#
AsImplementedInterfaces88G ^
(88^ _
)88_ `
;88` a
builder99 
.99 
AddMongo99 
(99 
)99 
;99 
builder:: 
.:: 
AddRepository:: !
<::! "
User::" &
>::& '
(::' (
$str::( /
)::/ 0
;::0 1
builder;; 
.;; 
AddRepository;; !
<;;! "
Problem;;" )
>;;) *
(;;* +
$str;;+ 5
);;5 6
;;;6 7
builder<< 
.<< 
AddRepository<< !
<<<! "
Question<<" *
><<* +
(<<+ ,
$str<<, 7
)<<7 8
;<<8 9
}== 	
public@@ 
void@@ 
	Configure@@ 
(@@ 
IApplicationBuilder@@ 1
app@@2 5
,@@5 6
IWebHostEnvironment@@7 J
env@@K N
,@@N O$
IHostApplicationLifetime@@P h
applicationLifetime@@i |
)@@| }
{AA 	
ifBB 
(BB 
envBB 
.BB 
IsDevelopmentBB !
(BB! "
)BB" #
)BB# $
{CC 
appDD 
.DD %
UseDeveloperExceptionPageDD -
(DD- .
)DD. /
;DD/ 0
}EE 
AutofacContainerFF 
=FF 
appFF "
.FF" #
ApplicationServicesFF# 6
.FF6 7
GetAutofacRootFF7 E
(FFE F
)FFF G
;FFG H
appGG 
.GG 
UseCorsGG 
(GG 
)GG 
;GG 
appHH 
.HH 
UseAuthenticationHH !
(HH! "
)HH" #
;HH# $
appII 
.II 
UseAuthorizationII  
(II  !
)II! "
;II" #
appJJ 
.JJ 
UseMvcJJ 
(JJ 
)JJ 
;JJ 
appKK 
.KK 

UseSwaggerKK 
(KK 
)KK 
;KK 
appMM 
.MM 
UseSwaggerUIMM 
(MM 
oMM 
=>MM !
{NN 
oOO 
.OO 
SwaggerEndpointOO !
(OO! "
$strOO" 3
,OO3 4
$strOO5 9
)OO9 :
;OO: ;
}PP 
)PP 
;PP 
appRR 
.RR 

UseRoutingRR 
(RR 
)RR 
;RR 
appSS 
.SS 
UseAuthorizationSS  
(SS  !
)SS! "
;SS" #
appTT 
.TT 
UseEndpointsTT 
(TT 
optionsTT $
=>TT% '
{UU 
optionsVV 
.VV 
MapControllersVV &
(VV& '
)VV' (
;VV( )
}WW 
)WW 
;WW 
applicationLifetimeXX 
.XX  
ApplicationStoppedXX  2
.XX2 3
RegisterXX3 ;
(XX; <
(XX< =
)XX= >
=>XX? A
{YY 
AutofacContainerZZ  
.ZZ  !
DisposeZZ! (
(ZZ( )
)ZZ) *
;ZZ* +
}[[ 
)[[ 
;[[ 
}\\ 	
}]] 
}^^ ∞
2S:\in≈ºynierka\api\es_webapi\Swagger\Extensions.cs
	namespace 	
ExpertalSystem
 
. 
Swagger  
{		 
public

 

static

 
class

 

Extensions

 "
{ 
public 
static 
IServiceCollection (

AddSwagger) 3
(3 4
this4 8
IServiceCollection9 K
servicesL T
)T U
{ 	
services 
. 
AddSwaggerGen "
(" #
o# $
=>% '
{ 
o 
. 

SwaggerDoc 
( 
$str !
,! "
new# &
OpenApiInfo' 2
{ 
Description 
=  !
$str" S
,S T
Title 
= 
$str 1
,1 2
Version 
= 
$str "
," #
Contact 
= 
new !
OpenApiContact" 0
{ 
Email 
= 
$str  8
,8 9
Name 
= 
$str 1
,1 2
Url 
= 
new !
Uri" %
(% &
$str& =
)= >
} 
} 
) 
; 
o 
. !
AddSecurityDefinition '
(' (
$str( 0
,0 1
new2 5!
OpenApiSecurityScheme6 K
{ 
Description 
=  !
$str" 9
,9 :
Name 
= 
$str *
,* +
In   
=   
ParameterLocation   *
.  * +
Header  + 1
,  1 2
Type!! 
=!! 
SecuritySchemeType!! -
.!!- .
ApiKey!!. 4
,!!4 5
Scheme"" 
="" 
$str"" %
}## 
)## 
;## 
o$$ 
.$$ "
AddSecurityRequirement$$ (
($$( )
new$$) ,&
OpenApiSecurityRequirement$$- G
($$G H
)$$H I
{%% 
{&& 
new'' !
OpenApiSecurityScheme'' .
{(( 
	Reference)) !
=))" #
new))$ '
OpenApiReference))( 8
{** 
Type++ 
=++ 
ReferenceType++ ,
.++, -
SecurityScheme++- ;
,++; <
Id,, 
=,, 
$str,, %
}-- 
,-- 
Scheme.. 
=..  
$str..! )
,..) *
Name// 
=// 
$str// '
,//' (
In00 
=00 
ParameterLocation00 .
.00. /
Header00/ 5
,005 6
}11 
,11 
new22 
List22 
<22 
string22 #
>22# $
(22$ %
)22% &
}33 
}44 
)44 
;44 
var66 
xmlFile66 
=66 
$"66  
{66  !
Assembly66! )
.66) * 
GetExecutingAssembly66* >
(66> ?
)66? @
.66@ A
GetName66A H
(66H I
)66I J
.66J K
Name66K O
}66O P
.xml66P T
"66T U
;66U V
var77 
xmlPath77 
=77 
Path77 "
.77" #
Combine77# *
(77* +

AppContext77+ 5
.775 6
BaseDirectory776 C
,77C D
xmlFile77E L
)77L M
;77M N
o88 
.88 
IncludeXmlComments88 $
(88$ %
xmlPath88% ,
)88, -
;88- .
}99 
)99 
;99 
return:: 
services:: 
;:: 
};; 	
}<< 
}== Õ
PC:\Users\lukas\AppData\Local\Temp\.NETCoreApp,Version=v3.1.AssemblyAttributes.cs
[ 
assembly 	
:	 

global 
:: 
System 
. 
Runtime !
.! "

Versioning" ,
., -$
TargetFrameworkAttribute- E
(E F
$strF `
,` a 
FrameworkDisplayNameb v
=w x
$stry {
){ |
]| }ô
fS:\in≈ºynierka\api\es_webapi\obj\Debug\netcoreapp3.1\ExpertalSystem.MvcApplicationPartsAssemblyInfo.cs
[ 
assembly 	
:	 

	Microsoft 
. 

AspNetCore 
.  
Mvc  #
.# $
ApplicationParts$ 4
.4 5$
ApplicationPartAttribute5 M
(M N
$strN q
)q r
]r së
SS:\in≈ºynierka\api\es_webapi\obj\Debug\netcoreapp3.1\ExpertalSystem.AssemblyInfo.cs
[ 
assembly 	
:	 

System 
. 

Reflection 
. $
AssemblyCompanyAttribute 5
(5 6
$str6 F
)F G
]G H
[ 
assembly 	
:	 

System 
. 

Reflection 
. *
AssemblyConfigurationAttribute ;
(; <
$str< C
)C D
]D E
[ 
assembly 	
:	 

System 
. 

Reflection 
. (
AssemblyFileVersionAttribute 9
(9 :
$str: C
)C D
]D E
[ 
assembly 	
:	 

System 
. 

Reflection 
. 1
%AssemblyInformationalVersionAttribute B
(B C
$strC J
)J K
]K L
[ 
assembly 	
:	 

System 
. 

Reflection 
. $
AssemblyProductAttribute 5
(5 6
$str6 F
)F G
]G H
[ 
assembly 	
:	 

System 
. 

Reflection 
. "
AssemblyTitleAttribute 3
(3 4
$str4 D
)D E
]E F
[ 
assembly 	
:	 

System 
. 

Reflection 
. $
AssemblyVersionAttribute 5
(5 6
$str6 ?
)? @
]@ A