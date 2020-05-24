using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Models;
using System;
using System.Linq;

namespace Project.Data
{
	public class DbInitializer
	{
		public static void Initialize(MvcBookContext context)
		{
			//using var context = new MvcBookContext(serviceProvider.GetRequiredService<DbContextOptions<MvcBookContext>>());
			context.Database.EnsureCreated();

			if (context.Books.Any())
			{
				return;
			}


			var Authors = new Models.Author[]
			{
				  new Author{Name="Джордж Р. Р. Мартин"},
				  new Author{Name="Милко Палангурски"},
				  new Author{Name="Пламен Павлов"},
				  new Author{Name="Светлана Алексиевич"},
				  new Author{Name="Христо Стоичков"},
				  new Author{Name="Владимир Памуков"},
				  new Author{Name="Анджей Сапковски"},
				  new Author{Name="Робърт Кийосаки"},
				  new Author{Name="Никола Бьогле"},
				  new Author{Name="Дж. К. Роулинг"},
				  new Author{Name="Димитър Бербатов"},
				  new Author{Name="Ашли Ванс"},
				  new Author{Name="Стивън Кинг"},
				  new Author{Name="Мишел Обама"}
			};

			foreach (Author author in Authors)
			{
				context.Authors.Add(author);
			}
			context.SaveChanges();
			 
			var Genres = new Models.Genre[]
			{
				new Genre{Name="Приключенски"},
				new Genre{Name="Фентъзи"},
				new Genre{Name="История"},
				new Genre{Name="Съвременни"},
				new Genre{Name="Спорт"},
				new Genre{Name="Финанси"},
				new Genre{Name="Трилъри"},
				new Genre{Name="Технологии"},
				new Genre{Name="Хорър"},
				new Genre{Name="Политика и политология"}
			};

			foreach (Genre genre in Genres)
			{
				context.Genres.Add(genre);
			}
			context.SaveChanges();

			var Books = new Models.Book[]
			{
				new Book{ISBN=9789546559197, Title="Огън и кръв",ReleaseYear=2019, GenreId=Genres.Single(i => i.Name=="Фентъзи").ID, Price= 24.99M, Pages=592,
					Info="Върнете се 300 години назад, когато драконите владееха Вестерос. „Огън и кръв“ разказва историята на рода Таргариен. Пригответе се за финалната битка и последния сезон на култовия сериал „Игра на тронове“.",
					Cover="ogan-i-krav-30.jpg"
				},
				new Book{ISBN=9786191883219, Title="Фермата - Алманах. История на българщината", ReleaseYear=2019, GenreId=Genres.Single(i=>i.Name=="История").ID, Price=34.90M, Pages=376,
					Info="Алманахът на Фермата излиза под името „Алманах. История на българщината“ и представя историята на България така, както не сте я виждали до момента. Изчерпателен и безценен за всеки, който иска да пътува назад във времето и да разбере защо и как стигнахме до мястото, където сме днес.",
					Cover="fermata---almanah-istoriya-na-balgarshtinata-30.jpg"
				},
				new Book{ISBN=9789545532252, Title="Чернобилска молитва", ReleaseYear=2017, GenreId=Genres.Single(i => i.Name=="Съвременни").ID, Price=18M, Pages=308,
					Info="Светлана Алексиевич е Нобеловият лауреат за литература за 2015 г. Отличието се присъжда на белоруската писателка за нейното 'многозвучно писане, монумент на страданието и смелостта в нашето време'.",
					Cover="chernobilska-molitva-30.jpg"
				},
				new Book{ISBN=9789545852930, Title="Игра на тронове", ReleaseYear=2001,  GenreId=Genres.Single(i => i.Name=="Фентъзи").ID, Price=19.99M, Pages=704,
					Info="'Песен за огън и лед' се завръща в нови дрехи. Ожесточената 'Игра на тронове' си е все така кървава и драматична, нова е само визията. Издателство 'Бард' представя на всички фентъзи фенове класиката на Мартин с нови корици.",
					Cover="igra-na-tronove-pesen-za-ogan-i-led-1-31.jpg"
				},
				new Book{ISBN=9789545852992, Title="Сблъсък на крале", ReleaseYear=2001, GenreId=Genres.Single(i => i.Name=="Фентъзи").ID, Price=19.99M, Pages=768,
					Info="'Песен за огън и лед' се завръща в нови дрехи. Предстои 'Сблъсък на крале', който ще разтрепери цялата земя. Издателство 'Бард' представя на всички фентъзи фенове класиката на Мартин с нови корици.",
					Cover="sblasak-na-krale-pesen-za-ogan-i-led-2-31.jpg"
				},
				new Book{ISBN=9789545853104, Title="Вихър от мечове", ReleaseYear=2002,  GenreId=Genres.Single(i => i.Name=="Фентъзи").ID, Price=19.99M, Pages=924 ,
					Info="'Песен за огън и лед' се завръща в нови дрехи. Насред 'Вихър от мечове' няма невинни. Издателство 'Бард' представя на всички фентъзи фенове класиката на Мартин с нови корици.",
					Cover="vihar-ot-mechove-pesen-za-ogan-i-led-3-31.jpg"
				},
				new Book{ISBN=9789545859649, Title="Пир за врани", ReleaseYear=2006, GenreId=Genres.Single(i => i.Name=="Фентъзи").ID, Price=19.99M, Pages=832,
					Info="'Песен за огън и лед' се завръща в нови дрехи. Битките са затихнали... Победители и победени са поканени на 'При за врани'. Издателство 'Бард' представя на всички фентъзи фенове класиката на Мартин с нови корици.",
					Cover="pir-za-vrani-pesen-za-ogan-i-led-4-31.jpg"
				},
				new Book{ISBN=9789546552655, Title="Танц с дракони", ReleaseYear=2011, GenreId=Genres.Single(i => i.Name=="Фентъзи").ID, Price=19.99M, Pages=960,
					Info="'Песен за огън и лед' се завръща в нови дрехи. Във времена на нарастващ смут, готови ли сте за 'Танц с дракони'? Издателство 'Бард' представя на всички фентъзи фенове класиката на Мартин с нови корици.",
					Cover="tants-s-drakoni-pesen-za-ogan-i-led-5-31.jpg"
				},
				new Book{ISBN=9786191514496, Title="Христо Стоичков. Историята", ReleaseYear=2018, GenreId=Genres.Single(i=> i.Name=="Спорт").ID, Price=24.99M, Pages=400,
					Info="Официалната биография на Христо Стоичков, написана в съавторство с Владимир Памуков. Над 400 страници лични истории за живота и кариерата на най-големия български футболист – с твърди корици и цветни снимки.",
					Cover="hristo-stoichkov-istoriyata-30.jpg"
				},
				new Book{ISBN=9789542821601, Title="Вещерът 1: Последното желание", ReleaseYear=2016, GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=13M, Pages=316,
					Info="Гералт от Ривия е легенда и това е началото на неговата история! Той не е герой. Не е рицарят на бял кон, който спасява девойката в последния момент. Той е професионалист, който убива чудовища срещу заплащане.",
					Cover="veshterat-1--poslednoto-zhelanie-31.jpg"
				},
				new Book{ISBN=9789542822608, Title="Вещерът 2: Меч на съдбата", ReleaseYear=2017,  GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=13M, Pages=384,
					Info="Гералт от Ривия, един от последните представители на вещерската каста – мутанти, създадени с алхимия и магия, крачи по мрачните пътеки между света на хората и чудовищата. Шест нови истории във втората книга от поредицата на Анджей Сапковски „Вещерът“.",
					Cover="veshterat-2--mech-na-sadbata-31.jpg"
				},
				new Book{ISBN=9789542824190, Title="Вещерът 3: Кръвта на елфите", ReleaseYear=2017, GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=14M, Pages=308,
					Info="Третата книга от бестселър поредицата „Вещерът“, която издигна сагата за Гералт от Ривия до ново ниво се завръща на българския пазар с обновен превод.",
					Cover="veshterat-3--kravta-na-elfite-31.jpg"
				},
				new Book{ISBN=9789542824435, Title="Вещерът 4: Време на презрение", ReleaseYear=2017, GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=14M, Pages=340,
					Info="Войната е надвиснала над света. И докато хаосът се надига навсякъде, заплашвайки да погълне и унищожи всичко, Гералт от Ривия, Белия вълк остава непоклатим, като скала сред бурята, като меч, издигнат в защита на невинните.‌",
					Cover="veshterat-4--vreme-na-prezrenie-31.jpg"
				},
				new Book{ISBN=9789542825401, Title="Вещерът 5: Огнено кръщение", ReleaseYear=2018, GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=14M, Pages=344,
					Info="Войната е в разгара си. Армиите на Северните кралства отстъпват пред настъплението на Нилфгард. Сключват се съюзи, извършват се предателства. Светът изгаря и изглежда, че никой не може да го спаси. Загърбил унищожението около себе си и излекувал раните си, Гералт от Ривия, Белия вълк, се отправя към Нилфгард. По пътя ще срещне неочаквани съюзници и ще бъде принуден да мине през огъня, за да намери Цири.",
					Cover="veshterat-5--ogneno-krashtenie-30.jpg"
				},
				new Book{ISBN=9789542826200, Title="Вещерът 6: Кулата на лястовицата", ReleaseYear=2018,  GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=16M, Pages=440,
					Info="‌Гералт от Ривия продължава своя път, но вече не е сам. Спътниците, които откри в „Огнено кръщение” продължават да бъдат с него в търсенето на Цири. А злото дебне. В мрака и което е по-страшно – в дълбините на човешките сърца. Алчността, фанатизма и садизма пускат своите корени, а срещу тях вещерския меч е безсилен.",
					Cover="veshterat-6--kulata-na-lyastovitsata-31.jpg"
				},
				new Book{ISBN=9789542826729, Title="Вещерът 7: Господарката на езерото", ReleaseYear=2018,  GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=16M, Pages=540,
					Info="‌Всяка история си има край и за съжаление и тази на Гералт не прави изключение. Нишките на съдбата се заплитат, докато белокосият вещер крачи към своето Предопределение.",
					Cover="veshterat-7--gospodarkata-na-ezeroto-30.jpg"
				},
				new Book{ISBN=9786190200666, Title="Вещерът: Сезонът на бурите", ReleaseYear=2017, GenreId=Genres.Single(i=> i.Name=="Фентъзи").ID, Price=16M, Pages=400,
					Info="'Сезонът на бурите' излиза за пръв път на български език. Това не е предистория, нито продължение на поредицата, а напълно самостоятелен роман.Гералт от Ривия унищожава злите свръхестествени чудовища с помощта на магически Знаци, вълшебни еликсири и гордостта на всеки вещер - два специални меча, които пази като зениците на очите си. Но някой ги открадва с хитрост...",
					Cover="veshterat--sezonat-na-burite-31.jpg"
				},
				new Book{ISBN=9789542929550, Title="Богат татко, беден татко", ReleaseYear=2018, GenreId=Genres.Single(i => i.Name=="Финанси").ID, Price=15.95M, Pages=360,
					Info="Уроците за парите, на които богатите учат децата си, а бедните и средната класа - не. Робърт Кийосаки промени начина, по който милиони хора по света мислят за парите. Той притежава репутация на човек, чиито възгледи често противоречат на конвенционалното мислене със своята смелост и безкомпромисна откровеност. Той е световно признат експерт по финансова грамотност.",
					Cover="bogat-tatko--beden-tatko-30.jpg"
				},
				new Book{ISBN=9786191642977, Title="Пациент 488", ReleaseYear=2019, GenreId=Genres.Single(i => i.Name=="Трилъри").ID, Price=20M, Pages=392,
					Info="Защо жертвата има белег с номер 488 на челото? Какво означават тайнствените рисунки на стената на стаята му? Защо болничният персонал отказва да разкрие истинската самоличност на покойника, настанен в „Гаустад“ преди повече от тридесет години?",
					Cover="patsient-488-30.jpg"
				},
				new Book{ISBN=9789544464684, Title="Хари Потър и Философският камък", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=19.90M, Pages=280,
					Info="Представете си, че до 10-та си години сте живели под стълбите при семейство, което ви мрази. И на 11-я си рожден ден откривате, че сте истински магьосник! Запознайте се Хари Потър... Едно удивително приключение е на път да започне.",
					Cover="hari-potar-i-filosofskiyat-kamak-31.jpg"
				},
				new Book{ISBN=9789544460501, Title="Хари Потър и Стаята на тайните", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=19.90M, Pages=304,
					Info="Хари прекарва най-ужасната лятна ваканция. А втората година във „Хогуортс“ го чакат нови изпитания. Заплаха дебне из коридорите на училището и напада деца на не-магьосници. Кой е зловещия Наследника на Слидерин?",
					Cover="hari-potar-i-stayata-na-taynite-31.jpg"
				},
				new Book{ISBN=9789544465575, Title="Хари Потър и Затворникът от Азкабан", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=19.90M, Pages=384,
					Info="В третата книга неприятностите започват още преди началото на учебната година. Хари прави непозволена магия, преследва го жесток убиец, избягал от магьосническия затвор, появяват се ужасяващите дементори... Нещата се заплитат, а накрая се оказва, че нищо не такова, каквото изглежда.",
					Cover="hari-potar-i-zatvornikat-ot-azkaban-31.jpg"
				},
				new Book{ISBN=9789544466213, Title="Хари Потър и Огненият бокал", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=24.90M, Pages=632,
					Info="В четвъртата книга от поредицата за Хари Потър всичко е много по-дълбоко, по-вълнуващо, по-драматично. Финал на Световното първенство по куидич, „Хогуортс“ е домакин на Тримагическия турнир. Синьо-белите пламъци на Огнения бокал ще определят съдбата на Хари.",
					Cover="hari-potar-i-ogneniyat-bokal-31.jpg"
				},
				new Book{ISBN=9789544467616, Title="Хари Потър и Орденът на феникса", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=24.90M, Pages=864,
					Info="Стената между вълшебния и мъгълския свят се пропуква. Смъртножадни нападат обикновени хора и внасят паника. Ордена на феникса се събира, за да даде отпор на Волдемор. Къде води зловещия коридор от сънищата Хари и какво гласи едно пророчество?",
					Cover="hari-potar-i-ordenat-na-feniksa-31.jpg"
				},
				new Book{ISBN=9789544469306, Title="Хари Потър и Нечистокръвния принц", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=24.90M, Pages=576,
					Info="Злото се въздига, започва втората война в магьосническия свят и Хари Потър естествено е в центъра ѝ. Тайната на могъществото на Волдемор ще бъде разкрита и пътят за разгрома му - очертан. Хари ще трбява да реши дали ще го поеме, защото веднъж тръгнал по него, връщане назад няма.",
					Cover="hari-potar-i-nechistokravniya-prints-31.jpg"
				},
				new Book{ISBN=9789542701514, Title="Хари Потър и Даровете на Смъртта", ReleaseYear=2016, GenreId=Genres.Single(i=>i.Name=="Приключенски").ID, Price=24.90M, Pages=680,
					Info="Загадъчен снич, множество съмнителни истини и едно тежко обещание - това е завещанието на Дъмбълдор. Хари ще трябва да открие и унищожи хоркруксите, пазещи душата на Волдемор. Истина ли е легендата за Даровете на смъртта и ще помогнат ли те на Хари да спази обещанието си.",
					Cover="hari-potar-i-darovete-na-smartta-31.jpg"
				},
				new Book{ISBN=9789542827375, Title="Димитър Бербатов. По моя начин", ReleaseYear=2018, GenreId=Genres.Single(i => i.Name=="Спорт").ID, Price=25M, Pages=488,
					Info="Сензационната, официална автобиография на Димитър Бербатов, голмайстор на националния отбор за всички времена, шампион на Англия през 2009 и 2011 г. и голмайстор на английската Висша лига за сезон 2010/2011, излиза с логото на ИК „Сиела“.",
					Cover="dimitar-berbatov-po-moya-nachin-30.jpg"
				},
				new Book{ISBN=9789547713611, Title="Илън Мъск: PayPal, Tesla, SpaceX и походът към невероятното бъдеще", ReleaseYear=2016, GenreId=Genres.Single(i => i.Name=="Технологии").ID, Price=25M, Pages=408,
					Info="ИК 'Кръгозор' представя единствената официална биография на Илън Мъск, човекът зад PayPal, Tesla, SpaceX.",
					Cover="ilan-mask--paypal--tesla--spacex-i-pohodat-kam-neveroyatnoto-badeshte-36.jpg"
				},
				new Book{ISBN=9789546559647, Title="Институтът", ReleaseYear=2019, GenreId=Genres.Single(i => i.Name=="Хорър").ID, Price=25M, Pages=544,
					Info="Свръхестествено страховит като „Живата факла“ и с невероятната детска сила на „То“, новият роман на Стивън Кинг „Институтът“ е напрегната история за борбата на доброто срещу злото в свят, в който добрите невинаги побеждават.",
					Cover="institutat-meki-koritsi-30.jpg"
				},
				new Book{ISBN=9786191515011, Title="Мишел Обама. Моята история", ReleaseYear=2019, GenreId=Genres.Single(i=>i.Name=="Политика и политология").ID, Price=29.99M, Pages=416,
					Info="Топла, мъдра и откровена, „Моята история“ е необикновено личната равносметка на една жена с голямо сърце и твърд характер, която надскача всички очаквания и чиято история ни вдъхновява да направим същото.",
					Cover="mishel-obama-moyata-istoriya-30.jpg"
				},
				new Book{ISBN=9789542816355, Title="Светът на огън и лед", ReleaseYear=2014, GenreId=Genres.Single(i=>i.Name=="Фентъзи").ID, Price=39M, Pages=336,
					Info="Светът на огън и лед – неразказаната история на Вестерос и Песен за огън и лед! Задължително четиво за всички почитатели на творчество на Джордж Мартин. Над 170 цветни илюстрации и тонове нова информация за Седемте кралства и Земите отвъд.",
					Cover="svetat-na-ogan-i-led-31.jpg"
				}
			};

			foreach (Book book in Books)
			{
				context.Books.Add(book);
			}
			context.SaveChanges();

			var BookAuthors = new Written_By[]
			{
				new Written_By{ISBN=Books.Single(i=>i.Title=="Огън и кръв").ISBN , AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID },
				new Written_By{ISBN=Books.Single(i=>i.Title=="Фермата - Алманах. История на българщината").ISBN, AuthorID=Authors.Single(i=>i.Name=="Милко Палангурски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Фермата - Алманах. История на българщината").ISBN, AuthorID=Authors.Single(i=>i.Name=="Пламен Павлов").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Чернобилска молитва").ISBN, AuthorID=Authors.Single(i=>i.Name=="Светлана Алексиевич").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Игра на тронове").ISBN, AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Сблъсък на крале").ISBN, AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вихър от мечове").ISBN, AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Пир за врани").ISBN, AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Танц с дракони").ISBN, AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Христо Стоичков. Историята").ISBN, AuthorID=Authors.Single(i=>i.Name=="Христо Стоичков").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Христо Стоичков. Историята").ISBN, AuthorID=Authors.Single(i=>i.Name=="Владимир Памуков").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 1: Последното желание").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 2: Меч на съдбата").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 3: Кръвта на елфите").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 4: Време на презрение").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 5: Огнено кръщение").ISBN,AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 6: Кулата на лястовицата").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът 7: Господарката на езерото").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Вещерът: Сезонът на бурите").ISBN, AuthorID=Authors.Single(i=>i.Name=="Анджей Сапковски").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Богат татко, беден татко").ISBN, AuthorID=Authors.Single(i=>i.Name=="Робърт Кийосаки").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Пациент 488").ISBN,AuthorID=Authors.Single(i=>i.Name=="Никола Бьогле").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Философският камък").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Стаята на тайните").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Затворникът от Азкабан").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Огненият бокал").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Орденът на феникса").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Нечистокръвния принц").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Хари Потър и Даровете на Смъртта").ISBN, AuthorID=Authors.Single(i=>i.Name=="Дж. К. Роулинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Димитър Бербатов. По моя начин").ISBN, AuthorID=Authors.Single(i=>i.Name=="Димитър Бербатов").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Илън Мъск: PayPal, Tesla, SpaceX и походът към невероятното бъдеще").ISBN, AuthorID=Authors.Single(i=>i.Name=="Ашли Ванс").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Институтът").ISBN, AuthorID=Authors.Single(i=>i.Name=="Стивън Кинг").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Мишел Обама. Моята история").ISBN, AuthorID=Authors.Single(i=>i.Name=="Мишел Обама").ID},
				new Written_By{ISBN=Books.Single(i=>i.Title=="Светът на огън и лед").ISBN, AuthorID=Authors.Single(i=>i.Name=="Джордж Р. Р. Мартин").ID}
			};

			foreach (Written_By bookAuthor in BookAuthors)
			{
				var bookAuthorsInDataBase = context.BookAuthors.Where(
						s => s.ISBN == bookAuthor.ISBN && s.AuthorID == bookAuthor.AuthorID
					).SingleOrDefault();
				if (bookAuthorsInDataBase == null)
				{
					context.BookAuthors.Add(bookAuthor);
				}
			}
			context.SaveChanges();

			var Stockpiles = new Stockpile[]
			{
				new Stockpile{BookID=Books.Single(i=>i.Title=="Огън и кръв").ISBN , Volume=10},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Фермата - Алманах. История на българщината").ISBN,Volume=2},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Чернобилска молитва").ISBN, Volume=1},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Игра на тронове").ISBN,Volume=10},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Сблъсък на крале").ISBN,Volume=10},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вихър от мечове").ISBN,Volume=10},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Пир за врани").ISBN,Volume=10},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Танц с дракони").ISBN,Volume=10},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Христо Стоичков. Историята").ISBN,Volume=3},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 1: Последното желание").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 2: Меч на съдбата").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 3: Кръвта на елфите").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 4: Време на презрение").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 5: Огнено кръщение").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 6: Кулата на лястовицата").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът 7: Господарката на езерото").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Вещерът: Сезонът на бурите").ISBN,Volume=20},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Богат татко, беден татко").ISBN,Volume=1},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Пациент 488").ISBN,Volume=4},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Философският камък").ISBN,Volume=15},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Стаята на тайните").ISBN,Volume= 15},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Затворникът от Азкабан").ISBN,Volume=15},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Огненият бокал").ISBN,Volume=15},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Орденът на феникса").ISBN,Volume=15, },
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Нечистокръвния принц").ISBN,Volume=15},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Хари Потър и Даровете на Смъртта").ISBN,Volume=15},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Димитър Бербатов. По моя начин").ISBN,Volume=1},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Илън Мъск: PayPal, Tesla, SpaceX и походът към невероятното бъдеще").ISBN,Volume=6},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Институтът").ISBN,Volume=2},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Мишел Обама. Моята история").ISBN,Volume=4},
				new Stockpile{BookID=Books.Single(i=>i.Title=="Светът на огън и лед").ISBN,Volume=12}
			};

			foreach (Stockpile stockpile in Stockpiles)
			{
				context.Stockpiles.Add(stockpile);
			}
			context.SaveChanges();

			var Customers= new Customer[]
			{
					new Customer{Name="Customer1", Email="Customer1@gmail.com",TelephoneNumber="123456"}
			};

			foreach(Customer customer in Customers)
			{
				context.Customers.Add(customer);
			}
			context.SaveChanges();

			var Carts= new Cart[]
			{
				new Cart{Customer_ID=1, ISBN=9786191642977,Volume=1}
			};

			foreach(Cart cart in Carts)
			{
				context.Carts.Add(cart);
			}
			context.SaveChanges();
		}
	}
}
