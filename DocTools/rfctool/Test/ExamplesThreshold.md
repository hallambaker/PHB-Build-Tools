
## Threshold Key Generation

### X25519

The key parameters of the first key contribution are:

~~~~
X25519Key1 (X25519)
    UDF:        ZAAA-CTKG-X255-XXKE-YX
    Scalar:     56751742936444772792970879017152360515706108153669948486190735258502824077920
    Encoded Private
  60 2A E2 12  AC 8E C8 86  A1 79 51 7E  79 90 5E C2
  9B AD 10 01  B9 2D 51 33  65 DB F4 9E  23 59 78 7D
    U: 25222393324990721517739552691612440154338285166262054281502859684220669343438
    V: 15622452724514925334849257786951944861130311422605147559630230860481236780294
    Encoded Public
  CE 36 B9 F1  56 BD 92 5C  F4 B6 F5 E1  E0 BA CA 6A
  9B 7C 37 7D  F8 DC 39 CC  12 2E A6 8F  64 5E C3 37
  00
~~~~

The key parameters of the second key contribution are:

~~~~
X25519Key2 (X25519)
    UDF:        ZAAA-CTKG-X255-XXKE-Y2
    Scalar:     30800688691513612134093999707357841640579640775881469593062950189697563564400
    Encoded Private
  70 19 5B 38  A4 46 21 79  31 AC 48 83  60 C9 BD F8
  E1 EE 04 53  67 F2 B5 D8  9E 42 53 66  6F 92 18 44
    U: 35108630063567318397224393939085269372284744000330218923799041589332061533992
    V: 13827314478911339710714490558315610168380330915483870499348836357802235649136
    Encoded Public
  28 37 F5 39  16 C6 10 C6  8A AC 75 E9  20 EF 67 6D
  C2 6C AF 2C  E4 F6 4F C9  E9 30 6C BD  C9 C7 9E 4D
  00
~~~~

The aggregate private key is:

~~~~
Scalar_A = (Scalar_1 + Scalar_2) mod L
  = 708364699971238359386639967994271266000352616992526807230274188774936630452

Encoded Aggrgate Private Key:

  B4 54 B7 EF  13 30 0D DF  C6 CB FE 5D  6A A3 A8 C0
  7C 9C 15 54  20 20 07 0C  04 1E 48 05  93 EB 90 01
~~~~

The aggregate public key is:

~~~~
Point_A = Point_1 + Point_2

U: 41645493613991421877170472401490489168274208680761359909716597934846285027335
V: 47340023312676432136363965264534933360110310079062150811084144252099552212729

Encoded Public
  07 98 75 38  67 9C 66 21  A3 0A D1 06  CF F5 81 04
  94 C0 52 C9  9C FD AE 4E  13 3B 43 9D  9A 83 12 5C
~~~~

Note that in this case, the unsigned representation of the key is used as
the aggregate key is intended for unsigned CurveX key agreement. If the
result is intended for use as a key contribution, the signed representation
is required.



### X448

The key parameters of the first key contribution are:

~~~~
X448Key1 (X448)
    UDF:        ZAAA-ETKG-X44X-KEYX
    Scalar:     681654152294348434876407549748279373112143225581269788055715553507401814865302008262214951100710804646043741434925630887320553400661768
    Encoded Private
  08 77 91 25  66 19 C6 1A  03 C7 60 9A  8C C8 10 9D
  DE F5 20 E1  A7 7F 3E 83  56 57 FE A6  C9 97 79 FB
  DC 85 55 6F  CE 17 79 70  CA 3E B5 D1  6A B0 50 6A
  60 F6 BF 3A  88 E5 15 F0
    U: 60697849609835675975297341597995979787516605306209816088918249829345395334584666059402047242453606517328394767062378040850523120715561
    V: 608135004941470494173649782645868772784563155814448853373618285076034450004202627339591608123302429557097118744860203117206220854848663
    Encoded Public
  29 C7 E7 1A  ED 85 B5 66  F4 CA 8F 4D  07 72 EC 4B
  15 42 FA 95  4D A3 25 F6  D2 BF C0 5E  11 C4 27 D3
  A1 43 D8 74  B6 4C C8 22  7D 64 56 58  A4 8C C6 5D
  DA F2 AA 75  DE DE 60 15  80
~~~~

The key parameters of the second key contribution are:

~~~~
X448Key2 (X448)
    UDF:        ZAAA-ETKG-X44X-KEY2
    Scalar:     678248814117618497981950831216283788356233701710889826937962011129206719268741815680700006802689991287015918654801310197484516725932432
    Encoded Private
  90 C1 CE 67  A2 88 20 95  B9 A8 8A E7  5A 12 73 C6
  4C E3 B0 0E  3A A4 1A 72  03 39 FC 9B  47 D9 6A E0
  A2 81 63 57  77 EB 97 E5  CE 05 2C CB  EE D7 64 F6
  51 C1 42 E7  FE D9 E2 EE
    U: 323959121868429818009225364153826014340692824647932840393706241565981551756628007189667971676177150776689230729229736979561639842244556
    V: 180562679449983428509213021388324448264772110005414594603016054105989977716699286334066341414636204710801397092415122728296636211077711
    Encoded Public
  CC 67 05 A8  AE D3 8C 6E  17 F8 7F 66  77 14 7F 32
  D3 F6 12 1C  E2 80 A9 BF  A9 AA 41 FC  88 EF E3 F9
  38 C7 1C AA  1A 14 54 EC  F0 4D 6D 20  ED 4F 63 24
  F2 A0 68 F5  1C 09 1A 72  80
~~~~

The aggregate private key is:

~~~~
Scalar_A = (Scalar_1 + Scalar_2) mod L
  = 87935198894654874397041717160555226349504546089353009501069716070586506403266723929544670861554164189887604126085304951388779109045747

Encoded Aggrgate Private Key:

  F3 55 F6 DD  05 50 99 B7  68 84 84 A1  C5 89 8A 79
  3A 5B F6 27  DE 24 31 97  F5 94 73 D9  14 71 E4 DB
  7F 07 B9 C6  45 03 11 56  99 44 E1 9C  59 88 B5 60
  B2 B7 02 22  87 BF F8 1E
~~~~

The aggregate public key is:

~~~~
Point_A = Point_1 + Point_2

U: 61163463447953667798490081919599863789437140567696403693973648136688134799342739585406562158256601376457049422599663606975867088547575
V: 547531628982729065710146631050685048629332114514125362339393102647611348032713305801879333956525397915473191145951077541388024189524364

Encoded Public
  F7 2E 68 4B  64 DC 2E 24  61 B9 28 14  2E 1D D9 41
  6A 29 4F A2  5F F1 AF 07  24 6C 9B 8A  9E C0 E5 58
~~~~

Note that in this case, the unsigned representation of the key is used as
the aggregate key is intended for unsigned CurveX key agreement. If the
result is intended for use as a key contribution, the signed representation
is required.


### Ed25519

The key parameters of the first key contribution are:

~~~~
ED25519Key1 (ED25519)
    UDF:        ZAAA-GTKG-ED25-5XXK-EYX
    Scalar:     39507802390720856312219571924476007168388547774368948368537778683821975155688
    Encoded Private
  1C C7 DE DF  19 7B 39 5F  82 98 26 62  AA DE 6C 66
  04 C3 E3 A2  C8 3D 18 58  06 2C 3E EC  7C D4 B4 F2
    X: 423537218415611592437715742009460965794047152767248386881172483158919506245796568293273125470706294881250827210288815928170449575540044968280671060652600
    Y: 1445324880829144568739937263922000707044256444511826775194220808375795010367943148297413308441540334412518101352213402681367161993702790547954006637246092
    Encoded Public
  6D F1 94 33  33 CC 66 4D  93 89 E2 FB  38 61 21 D5
  C5 6B 29 0F  5C 12 A8 4D  99 06 31 2D  35 32 22 A5
~~~~

The key parameters of the second key contribution are:

~~~~
ED25519Key2 (ED25519)
    UDF:        ZAAA-GTKG-ED25-5XXK-EY2
    Scalar:     39507802390720856312219571924476007168388547774368948368537778683821975155688
    Encoded Private
  1C C7 DE DF  19 7B 39 5F  82 98 26 62  AA DE 6C 66
  04 C3 E3 A2  C8 3D 18 58  06 2C 3E EC  7C D4 B4 F2
    X: 423537218415611592437715742009460965794047152767248386881172483158919506245796568293273125470706294881250827210288815928170449575540044968280671060652600
    Y: 1445324880829144568739937263922000707044256444511826775194220808375795010367943148297413308441540334412518101352213402681367161993702790547954006637246092
    Encoded Public
  6D F1 94 33  33 CC 66 4D  93 89 E2 FB  38 61 21 D5
  C5 6B 29 0F  5C 12 A8 4D  99 06 31 2D  35 32 22 A5
~~~~

The aggregate private key is:

~~~~
Scalar_A = (Scalar_1 + Scalar_2) mod L
  = 6645549008119090484707278218522071928205931954938820677056047984789407801486

Encoded Aggrgate Private Key:

  8E 20 46 06  EE 61 70 82  FA 37 43 E2  5A 68 E7 3C
  73 4A 36 B7  AC A4 DF 68  A7 95 5C 8E  58 3F B1 0E
~~~~

The aggregate public key is:

~~~~
Point_A = Point_1 + Point_2

X: -78285292761951767745666894197721180606214882184104422609189932681698965580880441653555514710017439512396957380471064585175456019164578961762059345997440
Y: 260549350612676062448188625658154114443427558625932490186172625180161797877734777066051008765362716939491746548095031028764807202440555166017893772852160

Encoded Public
  8E 89 98 D0  2D 7F 76 C3  A7 FF B3 1D  2B 41 7E E9
  51 6B 51 B5  F2 84 8D 17  6F 59 9B 5B  6F 01 CF 73
~~~~


### Ed448

The key parameters of the first key contribution are:

~~~~
ED448Key1 (ED448)
    UDF:        ZAAA-ITKG-ED44-XKEY-X
    Scalar:     681070423797938453360619484430252305967614234980136707892691573619227377289077600094508827508414971288486032661275761963605715031718704
    Encoded Private
  E5 0F 73 50  27 0A 2F 7D  FD D0 96 E5  03 D3 35 2C
  99 CB 71 7C  0B D9 49 E0  40 5E C7 FB  D1 F5 05 18
    X: 21110522768058701369078562241515359390938392915945705092166970243980644943466929750417082411867587890134927373108964014300862325057090
    Y: 562827776121868557079403760935244976768826037290102287130444895358482487063409857262483973700689741501700215513985669123529544543795102
    Encoded Public
  83 C0 19 E3  07 2A C3 E6  3F 97 DD C2  4A 72 D7 AF
  A8 4B 69 8B  07 A3 4F 45  84 B2 BD 4B  10 B7 44 73
  43 79 89 4B  AA C6 03 25  1F 50 F1 3B  95 A2 4A 81
  13 51 2B 01  D0 2D E3 8C  80
~~~~

The key parameters of the second key contribution are:

~~~~
ED448Key2 (ED448)
    UDF:        ZAAA-ITKG-ED44-XKEY-2
    Scalar:     681070423797938453360619484430252305967614234980136707892691573619227377289077600094508827508414971288486032661275761963605715031718704
    Encoded Private
  E5 0F 73 50  27 0A 2F 7D  FD D0 96 E5  03 D3 35 2C
  99 CB 71 7C  0B D9 49 E0  40 5E C7 FB  D1 F5 05 18
    X: 21110522768058701369078562241515359390938392915945705092166970243980644943466929750417082411867587890134927373108964014300862325057090
    Y: 562827776121868557079403760935244976768826037290102287130444895358482487063409857262483973700689741501700215513985669123529544543795102
    Encoded Public
  83 C0 19 E3  07 2A C3 E6  3F 97 DD C2  4A 72 D7 AF
  A8 4B 69 8B  07 A3 4F 45  84 B2 BD 4B  10 B7 44 73
  43 79 89 4B  AA C6 03 25  1F 50 F1 3B  95 A2 4A 81
  13 51 2B 01  D0 2D E3 8C  80
~~~~

The aggregate private key is:

~~~~
Scalar_A = (Scalar_1 + Scalar_2) mod L
  = 90173080078564848259922305056496676816356088757466810292775298672432726847378100175647367974983310833800009358909887793795139045888955

Encoded Aggrgate Private Key:

  BB 13 DF EE  6E 99 08 2F  3A 48 78 E0  1D 73 3E 8A
  9C 7E 8D 50  1F 20 00 A3  D0 1B 05 BF  00 41 0C 6E
  19 AA 40 21  C4 EC 1D 82  22 81 0F EE  4E B4 CD 08
  3C 09 43 F8  4C 87 C2 1F
~~~~

The aggregate public key is:

~~~~
Point_A = Point_1 + Point_2

X: 117970703860677873857394241859792339756031627299029461971946613964970547220649014069518272088071719887448971094010337125923569243159171
Y: 679430910062962953304601927238939723027714017166671976282539499239309676358730709148741795472033434901956727650426535076863107329148849

Encoded Public
  9A 6C C7 FF  5A AE B0 8B  EB 3D B6 C3  55 82 A7 B6
  4F 9C EC 91  F8 B0 F3 53  51 37 BB 9D  7F F6 63 90
  FE B0 15 3C  D4 35 F3 B9  01 FE 80 BE  B9 F2 2E DF
  FA 8B 15 E0  82 E4 F3 1B  80
~~~~


## Threshold Decryption

### Key Splitting X25519


The encryption key pair is

~~~~
X25519KeyA (X25519)
    UDF:        ZAAA-CTHD-X255-XXKE-YA
    Scalar:     36212799908425711450656372795692477094724455418915704216848228525958587810064
    Encoded Private
  10 01 D5 D1  E2 D3 DB 42  9E 40 5F D9  DB AE E8 09
  DE 43 C3 E6  D1 4F 3A 31  92 BF 19 8A  E9 B7 0F 50
    U: 14523539712308371644546850739155588238080554014514563739095172886049239361031
    V: 56685060472089790044070522288405984326906734250304251487683593932889808473139
    Encoded Public
  07 66 84 48  25 85 F6 4A  3A EE DF B7  69 1B 57 51
  EC 18 BE AF  08 BA 0D FE  BE F8 74 4E  3C 08 1C 20
~~~~

To create n key shares we first create n-1 key pairs in the normal fashion. Since 
these key pairs are only used for decryption operations, it is not necessary to 
calculate the public components:

~~~~
X25519Key1 (X25519)
    UDF:        ZAAA-CTHD-X255-XXKE-YX
    Scalar:     32951726132685026729149224926255648061071804906258082061427666995947179849152
    Encoded Private
  C0 B5 33 D4  F3 D0 16 4F  96 DF C3 AD  97 93 02 EF
  B4 25 E2 46  A3 69 1D 22  9B 5B A2 78  1C 04 DA 48
~~~~

The secret scalar of the final key share is the secret scalar of the base key minus
the sum of the secret scalars of the other shares modulo the group order:

~~~~
Scalar_2 = (Scalar_A - Scalar_1) mod L
    = 4031475845120378254046918654561176988082213090754617824258337077336679400315
This is encoded as a binary integer in little endian format:

  7B 43 64 61  E9 28 4D 79  AB 9C 6E CC  9F 79 14 3D
  92 69 A5 2D  75 B9 57 53  2D 1B BC 02  06 BC E9 08
~~~~



### Decryption X25519


The means of encryption is unchanged. We begin by generating an ephemeral 
key pair:

~~~~
X25519KeyE (X25519)
    UDF:        ZAAA-CTHD-X255-XXKE-YE
    Scalar:     41955577142906312619127105554814681129195921605852142704362465226652441661496
    Encoded Private
  38 50 3C 88  22 4F 61 D7  9A 2E 1D 71  F0 31 74 44
  A2 3B 2B 35  21 21 CA 19  4B 11 EB F0  DF 03 C2 5C
    U: 10080018124246254127076649374753145019412450363156572968151721892767560820008
    V: 43683938787921854603630290352714276342923724280578266457509078671566344321831
    Encoded Public
  28 E5 5E 1D  DD 1D 93 71  24 53 0A 83  B3 68 0D 28
  8F 37 AC 53  B6 65 97 7E  C1 54 44 41  8C 16 49 16
~~~~

The key agreement result is given by multiplying the public key of the encryption 
pair by the secret scalar of the ephemeral key to obtain the u-coordinate of the result.

~~~~
U: 24735175138889480342644265086752492408614475919483465883032610526622202018180
~~~~

The u-coordinate is encoded in the usual fashion (i.e. without specifying the sign of v).

~~~~

  84 39 A5 21  13 F9 13 F0  7F F4 44 C0  DF 5D 44 DD
  DD F4 9B 87  4C DD E1 AB  64 00 8F A2  ED 9C AF 36
~~~~

The first decryption contribution is generated from the secret scalar of the first key
share and the public key of the ephemeral.

The outputs from the Montgomery Ladder are:

~~~~
x_2 57800249527850149046770413207257250301842844049677844025524059085132359257003
z_2 37229326806761131733056994095424883574786241198535734197174081138402379671391
x_3 30722194817314627970562030033494699359853137448471883846088158083361967945513
z_3 29143359268139878301695995826542801325089258636824690596939399658126254126746
~~~~

The coordinates of the corresponding point are:

~~~~
u 26252004436924590849672630346501225836719120282448901501615216776458728744244
v 23403392496099289678702686304896871239416248574944871213406041948857707717709
~~~~

The encoding of this point specifies the u coordinate and the sign (oddness) of the 
v coordinate:

~~~~

  28 E5 5E 1D  DD 1D 93 71  24 53 0A 83  B3 68 0D 28
  8F 37 AC 53  B6 65 97 7E  C1 54 44 41  8C 16 49 16
~~~~

The second decryption contribution is generated from the secret scalar of the second key
share and the public key of the ephemeral in the same way:

~~~~
u 25681807750768643008939672211197487679310559285918558512272983019789028635830
v 52376245356417565100774234298065960285268351486530966017774030988054910628425
~~~~

~~~~

  28 E5 5E 1D  DD 1D 93 71  24 53 0A 83  B3 68 0D 28
  8F 37 AC 53  B6 65 97 7E  C1 54 44 41  8C 16 49 16
~~~~

To obtain the key agreement value, we add the two decryption contributions:

~~~~
u 53638091939023843532448425374574271509377559762011846300201437672880976727749
v 25762387779482158521025954468700106943716042880666532610246614075543602367190
~~~~

This returns the same u coordinate value as before, allowing us to obtain the encoding 
of the key agreement value and decrypt the message.



### Key Splitting X448


The encryption key pair is

~~~~
X448KeyA (X448)
    UDF:        ZAAA-ETHD-X44X-KEYA
    Scalar:     705967891238294807334857303861745653390131856473630286777277621057939099785091228353248522408450794128800398810019569879502484967206280
    Encoded Private
  88 2D AF 58  10 66 9E 1E  F9 F2 C5 76  A2 00 86 F5
  B0 B9 C6 B9  E6 34 12 57  64 E3 63 B7  99 48 01 77
  9B A3 49 2D  7C B8 80 D7  63 44 6B C9  CB 83 F0 01
  B6 55 E0 92  1C 2A A6 F8
    U: 54256629638851994806054576189463839532492460394052748417730874429953350260100190689466093860782780520056908859392703589108528218439174
    V: 126404941983047578037139936243515739368042620857955185710610450631383333635306581037675004961525698205648857075020359124084524068583614
    Encoded Public
  06 FE 38 7A  1B 1E 99 D4  89 00 07 B9  88 6F 97 01
  BD 88 BB 9D  A9 31 30 CC  47 E6 2F 9C  44 35 AF A4
~~~~

To create n key shares we first create n-1 key pairs in the normal fashion. Since 
these key pairs are only used for decryption operations, it is not necessary to 
calculate the public components:

~~~~
X448Key1 (X448)
    UDF:        ZAAA-ETHD-X44X-KEYX
    Scalar:     630662656726684233432914381478400571720353379363734733594300758463732521976388313294665447253881782852832499090049354258188417511652528
    Encoded Private
  B0 FC CE 55  87 AA A5 36  D2 5B E5 F2  5C 1B F7 9A
  5A 3D 97 D8  BB C0 81 84  98 3B 7C 29  C3 02 FC AE
  91 1B EA 67  68 C5 5E 87  7A ED 16 1F  CB D0 20 9D
  C0 D6 62 BD  0F 35 20 DE
~~~~

The secret scalar of the final key share is the secret scalar of the base key minus
the sum of the secret scalars of the other shares modulo the group order:

~~~~
Scalar_2 = (Scalar_A - Scalar_1) mod L
    = 64662780447666982306455021536138289991612854634558414878970530955649594030702441634543682620485945238460841936427288004274611461310355
This is encoded as a binary integer in little endian format:

  93 47 14 FF  94 BE F6 5C  77 63 44 89  DD CA 83 A6
  1A 79 82 CA  9E F6 6B 7D  98 23 59 77  60 4B FD 25
  2D BF 33 95  E4 7D DF 5E  DE 31 82 E8  96 54 11 9F
  76 2C 43 50  2C 5F C6 16
~~~~



### Decryption X448


The means of encryption is unchanged. We begin by generating an ephemeral 
key pair:

~~~~
X448KeyE (X448)
    UDF:        ZAAA-ETHD-X44X-KEYE
    Scalar:     408315028877728409011067152704680093281167013402289194447950742749557088789408677311466089336893170031425082958041776608657845012501716
    Encoded Private
  D4 94 79 EE  56 3A 43 D5  FC EB 88 3E  F0 63 EF 2F
  B0 92 B2 9D  FD E1 43 8F  67 70 2A FC  2A AB A3 8B
  40 5A C6 D8  DE 8E B8 81  BF AD 17 BA  14 7F A4 B0
  D4 B1 9F CE  D3 0D D0 8F
    U: 419025428575826447105014420878768465513515839475066853199754172921931242764163121977613761818608927787788470856012050834001655292441835
    V: 402546268886696875921653628965101177018312159976293029229126384107109948063151002535904960734463095918076287222011626898597213849340021
    Encoded Public
  EB 34 D3 9E  92 3E 82 CC  E6 EC 77 9F  3D 11 83 3C
  B6 5B 5C 04  E8 1F D6 E1  07 C0 62 FE  F8 F6 34 BB
~~~~

The key agreement result is given by multiplying the public key of the encryption 
pair by the secret scalar of the ephemeral key to obtain the u-coordinate of the result.

~~~~
U: 414519841929159382730636919036875317796178034011999213235983537052305106787024152424411931078767471781837755233750631283588936679550233
~~~~

The u-coordinate is encoded in the usual fashion (i.e. without specifying the sign of v).

~~~~

  19 ED 3F 7A  63 6D AA 9A  3E 05 29 DE  CC BA C7 F1
  E0 A7 FA C0  C4 70 E0 E1  A5 FC DA 0A  B0 52 EC 8A
~~~~

The first decryption contribution is generated from the secret scalar of the first key
share and the public key of the ephemeral.

The outputs from the Montgomery Ladder are:

~~~~
x_2 600872386577892658745397016758405216143268685802857882865503992322027708113246880087865322820085919620753848185209702446811828858483595
z_2 125731405526490379218908999429194405715025611304963932327166171552449066080557651788133951700597009878477147212706681069479775840967645
x_3 402169111608455555076269384855963062605477430779306047038914755995320723805215287283180373439738964352905750714947142945295511147157394
z_3 486718088237609246336261182214535911051994034174915625598144143593407933626543068597450465569884659930973430555454657130674077038979747
~~~~

The coordinates of the corresponding point are:

~~~~
u 531062708495622613354948037901243914958463817114718742644223562926004186474991811830611467926523417739685244466041108245014409383321437
v 64803849519846106548651324906062943559622281296957012203166811671738963455446043723779520423668998535402850812481731449606392177083381
~~~~

The encoding of this point specifies the u coordinate and the sign (oddness) of the 
v coordinate:

~~~~

  EB 34 D3 9E  92 3E 82 CC  E6 EC 77 9F  3D 11 83 3C
  B6 5B 5C 04  E8 1F D6 E1  07 C0 62 FE  F8 F6 34 BB
~~~~

The second decryption contribution is generated from the secret scalar of the second key
share and the public key of the ephemeral in the same way:

~~~~
u 243202930760601185465195064212706453485841544124003246081712864369102601495764607628214871657677482439232238254450281582409532827123057
v 330423749590904913599918273731429932445442646219893531795513631728240167193898154033571033048069157608137872491595181800632292085611537
~~~~

~~~~

  EB 34 D3 9E  92 3E 82 CC  E6 EC 77 9F  3D 11 83 3C
  B6 5B 5C 04  E8 1F D6 E1  07 C0 62 FE  F8 F6 34 BB
~~~~

To obtain the key agreement value, we add the two decryption contributions:

~~~~
u 225977633657335171209032368400628797937814223167545185526313635163184627019682834233607456823018602790573389381890060345691088913511436
v 335680446543491573899077325249645116819775941371845651975113975292022788339143143140400339843420207702195408750068698590142923542805226
~~~~

This returns the same u coordinate value as before, allowing us to obtain the encoding 
of the key agreement value and decrypt the message.


