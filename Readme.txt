Программа служит для отображения списка существующих баз данных SQL server, имён таблиц, содержащихся в них и вывода имён колонок и их типов в формате Name |.NET type | SQL type.

Для построения дерева таблиц в программе необходимо задать:
-Имя сервера;
-Имя базы данных (по умолчанию "master");
-имя зарегистрированного пользователя и его пароль;

Важно:
Имя базы данных не влияет на отображение списка существующих баз данных, однако необходимо, чтобы база данных с выбранным именем существовала. Параметр по умолчанию (databasename = "master") выбран из соображения, что при нормальной работе SQL server, база данных master существует.

Для проверки работы программы использовались:
-MS SQL Server v14.0;
-MS SQL Server Management Studio v17.9;
