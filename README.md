<h1>Стартиране на приложението</h1>
<ol>
  <li>
    Изтеглете или клонирайте solution-a (кода на приложението);
  </li>
  <li>
    В <i>appsettings.json</i>, който се намира в <i>Scolly/Scolly/appsettings.json</i> , въведете Вашият connection string в кавичките на <i>DefaultConnection</i> (може базата Ви данни да се казва "<i>Scolly</i>"): <strong> "DefaultConnection": "Вашият Connection String"</strong>
  </li>
  <li>
    В <i>Package Manager Console</i>, задайте <i>Default project</i> на <strong><i>Scolly.Infrastructure</i></strong>, напишете командата <strong>Update-Database</strong>, за да се приложат миграциите към базата данни. Приложението има предварително създадени примерни данни в базата данни - Регистрации, Курсове и други.
  </li>
  <li>
    Стартирайте проекта (Build).
  </li>
</ol>
