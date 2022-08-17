# WeatherApp
#### Time spent on this excercise: 1 day

Toss an apikey in the appsettings.json. Or just copy mine from a previous commit cos I totally forgot not to push that.

Admin login:
email: admin@admin.admin
passsword: admin@admin.admin


# Feladat leírás:

Időjárás előre jelző alkalmazás MVC alapokon
Feladat egy időjárás előre jelző alkalmazás készítése, melynek segítségével a felhasználó be tud
jelentkezni (egyszerűsített, megengedett a felhasználó és jelszó egyszerű tárolása), meg tudja adni az
általa megjeleníteni kívánt városokat, amelyeknek megjelennek az aktuális és az 1-2 napra előre
jelzett időjárás adataik (pl. hőmérséklet, légnyomás, stb.) egy külön oldalon. Új város megadása
mellett legyen lehetőség a meglévők szerkesztésére és törlésére is.
Az alkalmazás egy tetszőleges időjárás adatokat biztosító szolgáltatás adatait kell, hogy felhasználja.
Javasolt: https://www.weatherbit.io/ (regisztráció után ingyenesen használható).
Opcionális feladatok:
1. offline működés támogatása (időjárás adatok adatbázisból olvasása amennyiben az adatokat
biztosító szolgáltatás nem elérhető, vagy az utolsó lekérdezés óta nem telt el legalább 2 óra)
2. város kedvencnek jelölése (a kedvencnek jelölt városok a lista elején, kiemelten jelenjenek
meg)
3. időjárás adatok időzített frissítése a háttérben
4. több felhasználós kialakítás (regisztráció, belépés), adminisztrátor szerepkör és
adminisztrációs oldal kialakítása, ahol lehetőség van más felhasználók által felvett városok
módosítására, törlésére
5. AJAX hívások alkalmazása (az alkalmazás bármely pontján)

Architektúra: ASP.Net MVC
Adattárolás: SQL server
Fejlesztő eszköz: Visual Studio 201x
A megoldáshoz kérünk üzemesítés dokumentációt készíteni, melynek segítségével házon belül be
tudjuk üzemelni a megoldást.
