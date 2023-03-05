Aby uruchomić program należy pobrać ten folder (folder "adresy") z repozytorium. Można to zrobić w łatwy sposób przy pomocy strony DownGit, lub innego dowolnego sposobu.
Po pobraniu nalerzy upewnić się że na komputerze jest prawidłowo zainstalowane środowisko do pracy z Laravelem(php, composer,serwer mySQL itp.).
Jeśli wszystko jest, to należy otworzyć konsolę i przejść do folderu adresy i wykonać następujące czynności:

1. Zainstalować zależności composera poprzez komendę: **composer install**
2. Zainstalować zależności npm: **npm install**
3. Wygenerować klucz szyfrowania: **php artisan key:generate**
4. Skonfigurować serwer mySQL jeśli to konieczne(w pliku .env)
5. Zmigrować bazę danych: **php artisan migrate**
6. Włączyć serwer: **php artisan serve**
