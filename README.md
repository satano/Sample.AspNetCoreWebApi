# Simple example of .net core web api with KORM

>Toto repository obsahuje vzorový príklad web api využitím KORMu.

## Prehľad
Zjednodušený popis api, ktoré obsahuje tento príklad.
API|Description|Request body|Response body
-|-|-|-
GET /api/people|Get all people for authorized user.|None|Array of people
GET /api/people{id}|Get person by id|None|Person
POST /api/people|Create new person on server.|New person|New person id
PUT /api/people{id}|Update existing person|New person info|None
DELETE /api/people{id}|Delete person by id|None|None
GET /token|Creating authorization token|User for authentification.|None

Lepší popis nájdete po spustení aplikácie na addrese `localhost:5000/swagger/`, alebo na [apiary](https://sampleaspnetcorewebapi.docs.apiary.io).


## Steps to run
1. Stiahnite (naklonujte).
2. Obnovte si zálohu databázy `Data\People.bak` do vášho SQL Servera.
3. Zmente si connection string v `appsettings.json`.
4. Nastavte sa do adresára `src` a spustite `dotnet run`.
5. Spustí sa Vám webový server.

Jedná sa len o backend bez klienta. Takže vyskúšať si to môžte napríklad cez Postman-a.

`PeopleController` vyžaduje autorizáciu. Takže najskôr si musíte vyžiadať autorizačný token cez api `/token`.

## Čo si môžete pozrieť
- Vytvorenie vlastného middleware-u
    - `LoggingMiddleware` obsahuje jednoduchý príklad vlastného middleware-u, ktorý loguje requesty. Jeho registrovanie je podľa konvencie v triede `MiddlewareExtensions`.
- Využitie KORMu ako ORM na prístup k dátam
    - V `ServiceCollectionExtensions.AddKorm` je vidieť ako je možné zaregistrovať KORM do DI kontajnera.
- Validácia
    - Dáta, ktoré posielam na server je potrebné validovať.
    - Na validáciu využívam klasický `ModelState` validátor, ktorý validuje dáta na základe data anotations.
    - Aby som nemusel v každej `POST` respektíve `PUT` metóde opakovať ten istý kód, pripravil som ukážku vlastného action filtra `ModelStateValidationFilterAttribute`.
    - Jeho použitie je vidieť v `PeopleController`.
    - Dobrou praxou je, že pri týchto metódach neposielam na server priamo model, ale view model. Práve tento view model je odekorovaný validačnými atribútmy. `PersonViewModel` je príklad takéhoto view modelu.
- Autorizácia a authentifikácia
    - `TokenController` obsahuje ukážku vystavovania autorizačného tokenu.
    - Pre ukážku sú použité dva typy používateľov. Admin a bežný User.
    - Pre ukážku sú dáta viazané na prihláseného používateľa. Id prihláseného používateľa nám poskytuje rozhranie `IActiveUser` a implementované je v triede `HttpContextUser`.
- Práca so statickými súbormi
    - Pre ukážku je tu aj zopár statických súborov a v triede `Startup` je ukážka ako je možné nakonfigurovať asp.net core tak aby vám tieto súbory vedel poskytnúť.
- Dokumentácia
    - Využil som swagger na generovanie api dokumentácie.
    - Jeho konfiguráciu si môžte pozrieť v triede `SwaggerServiceExtension`.
    - Vygenerovanú dokumentáciu môžte vydieť po spustení služby na adrese  [localhost:5000/swagger/](localhost:5000/swagger/)


Je to len prehľad toho čo tento príklad obsahuje. Teraz sa mi naozaj nechce písať viac. Ak máte záujem o to aby som bližšie popísal (vysvetlil) niektorú z týchto častí, tak mi napíšte. Alebo zaraďte issue.

V budúcnosti chcem spraviť aj vzorového Angular 5 klienta. Ak má niekto záujem mi s tým pomôcť tak píšte.
