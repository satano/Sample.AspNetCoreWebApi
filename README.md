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

- info o tom čo obsahuje tento repositár
    - Vzorový príklad na Asp.net Core webapi s použitím KORMu
    - Validácia
    - Ukážka ako vytvárať vlastne middlewares
    - Ukážka ako vytvárať vlastné filtre
    - Príklad authorizacie
    - Ukážka ako pracovať so statickými súbormi
    - ukážaka ako pridať local settings
    -
-
-
- swager