.Net
Entity Framework: es la forma moderna de conectar una Base de Datos a una aplicación .NET usando la librería ADO .NET. Simplifica la forma de construcción del backend y sus objetos para interactuar con la bd.

ADO .NET: Es un conjunto de librerías para acceder a datos y servicios de base de datos. 
Proporciona una serie de clases y componentes que permiten a las aplicaciones realizar tareas como:
Conectarse a una base de datos
Ejecutar comandos SQL 
Leer y escribir datos mediante objetos como DataSet (llena datos que traemos de la bd en un componente genérico), Linq (conecta clases y componentes con tablas de bd), EntityFramework.
Realizar transacciones.



Hay varias formas de conectarse a una base de datos, dependiendo del sistema de gestión de bases de datos (DBMS, por sus siglas en inglés) que se esté utilizando.

ODBC
OLEDB
SQL Server
Conexion Azure SQL



Un ORM Object Relational Mapping es una herramienta que realiza un mapeo que nos permite transformar los objetos de la base de datos como tablas y esquemas a clases con atributos en código de programación para poder manipular la información de una forma más fácil sin requerir de SQL. Dapper y Entity Framework son ORM’s.

Entity Framework
Es un marco de trabajo de mapeo objeto-relacional (ORM) para Microsoft .NET que permite a los desarrolladores trabajar con bases de datos relacionales mediante objetos y entidades en lugar de tablas y columnas.

Con Entity Framework, los desarrolladores pueden crear modelos de datos lógicos que representan las entidades y las relaciones entre ellas, y luego mapearlos a la estructura de la base de datos relacional. Esto significa que los desarrolladores pueden escribir código en términos de objetos y entidades, en lugar de escribir consultas SQL complejas.

Ventajas
Hay varias ventajas en el uso de Entity Framework como ORM para trabajar con bases de datos relacionales:
El tiempo de desarrollo, ahorro en esa transformación de objetos a tablas o campos de bd, facilidad de conexión, etc.
Permite manejar un solo repositorio para el backend y la base de datos.
Mejora la seguridad: evita el sql inyection, la integridad de datos alineada al tipado de objetos y datos.
Programacion mas facil y amigable, permite a los desarrolladores trabajar con objetos y entidades en lugar de tablas y columnas
Fácil control de los cambios de la base de datos. Usa migraciones.
Optimización de rendimiento: incluye un sistema de caché incorporado que permite optimizar el rendimiento de las consultas y mejorar la eficiencia de la aplicación.



Práctica

si quiero verificar las versiones de sdk instaladas: dotnet --list-sdks ó dotnet --version

crear el directorio de la practica
abrir una consola y ejecutar: dotnet new web
para ver las versiones de sdk de .net se revisa el archivo .csproj, el program.cs  solo tiene el hola mundo.
luego se agregan las librerías, para esto abrimos:  https://www.nuget.org/ 
buscamos entity framework y seleccionamos:  Microsoft.EntityFrameworkCore
seleccionarlo y descargar la version compatible con la de nuestro proyecto.
copiamos la linea de comando generada, en este caso para la prueba: dotnet add package Microsoft.EntityFrameworkCore --version 7.0.0
ejecutamos el comando en un cmd y luego verificamos que el archivo .csproj tenga la nueva dependencia instalada.
ahora buscamos el nuget entity framework in memory (para bd en memoria)
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 7.0.0
en caso de necesitar conexion con alguna bd como sql server o mysql, descargar e instalar el respectivo nuget.
Creamos el paquete modelos
creamos las clases Categoria, Tarea y un enum Prioridad
añadimos las propiedades.

Durante la configuracion de EF, debemos tener claro lo siguiente:
Contexto: Es donde van a ir todas las relaciones de los modelos que nosotros tenemos para poder transformarlo en colecciones que van a representarse dentro de la base de datos.
DbContext es una clase fundamental en Entity Framework que actúa como el puente entre tu código de aplicación y la base de datos. Proporciona una interfaz para interactuar con las entidades (clases de modelo) y realizar operaciones de lectura y escritura en la base de datos. En esencia, DbContext es una representación del modelo de datos de tu aplicación y contiene conjuntos de entidades que representan las tablas de la base de datos. Cada entidad en el DbContext representa una fila en la tabla correspondiente.
DBSet: Es un set o una asignación de datos del modelo que nosotros hemos creado previamente, básicamente esto va a representar lo que sería una tabla dentro del contexto de entity framework.

Siguiendo con la práctica, se debe crear una clase para definir el contexto. En nuestro caso la llamamos ProyectoContext.cs, ahi añadimos los DBSet para que EF entienda qué modelos van a ser representados en bd y añadimos un constructor para inicializar el contexto.
Podemos usar data annotations para validar los campos de nuestro modelo: requerido, key, maxLength, ForeingKey, etc.
Para poder validar la solución con bd en memoria necesitamos configurar EF en memoria en el archivo Program.cs
añadimos: 
builder.Services.AddDbContext<ProyectoContext>(p => p.UseInMemoryDatabase("MiBaseEnMemoria"));
en caso de querer usar una bd real: 
builder.Services.AddSqlServer<ProyectoContext>("Data Source=DANNY-MEJIA;Initial Catalog=MiBaseSqlServer;user id=sa;password=12345678;TrustServerCertificate=True");
para no dejar los datos quemados en el Program.cs se usa el archivo appsettings.json
añadimos el string conexion en el appsettings.json:
"ConnectionStrings": {
    "conexionBd": "Data Source=DANNY-MEJIA;Initial Catalog=MiBaseSqlServer;user id=sa;password=12345678;TrustServerCertificate=True"
  }
cambiamos la forma de crear la conexion desde el Program.cs builder.Services.AddSqlServer<ProyectoContext>(builder.Configuration.GetConnectionString("conexionBd"));

Fluent API es la forma avanzada de configuración sin utilizar atributos o data-annotations, o detalles de restricción/configuración que no son soportados, usando funciones de extensión anidadas en objetos de tabla (con un builder), columnas durante el mapeo de los datos.

Esta configuración se debe aplicar en el Context.
se sobreescribe el método OnModelCreating(ModelBuilder modelBuilder)
Por ejemplo:
modelBuilder.Entity<Tarea>(tarea=> {
   tarea.ToTable("Tarea”);
   tarea.HasKey(tarea => tarea.Id);
   tarea.HasOne(tarea => tarea.Categoria).WithMany(tarea =>  tarea.Tareas).HasForeignKey(tarea => tarea.CategoriaId);
   tarea.Property(tarea =>   tarea.Titulo).IsRequired().HasMaxLength(200);
   tarea.Property(tarea => tarea.Descripcion);
   tarea.Property(tarea => tarea.Prioridad);
   tarea.Property(tarea => tarea.FechaCreacion);
});

Migraciones
Es una funcionalidad de EF que nos permite guardar de manera incremental los cambios realizados en la base de datos. Nos permite versionar la base de datos, cambio a cambio.

se deben inicializar las migraciones
verificamos si esta instalada:
dotnet ef
si no esta instalada podemos ejecutar:
dotnet tool install --global dotnet-ef
Ahora inicializamos, normalmente la migración inicial se conoce como initdatabase, initialcreate.
 dotnet ef migrations add InitialCreate 
en caso de error en el anterior comando instalar:
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.0
cada vez que se agrega una migración hay que ejecutar, antes de ejecutar debemos eliminar la bd
la inicialización de los migrations debe hacerse desde un inicio para evitar borrar la bd.
dotnet ef database update 
cada vez que se crea algún cambio de la bd hay que crear una migración y ejecutar el update.

Datos iniciales o datos semilla

Simplemente se debe crear una lista de objetos que se quieran crear como datos iniciales y luego dentro del model builder, al final, se añade la instrucción:
	categoria.HasData(categoriasIniciales); ó
tarea.HasData(tareasIniciales);

Recordar que siempre que se hagan modificaciones en el modelo, debemos crear una migracion para poder tener la traza de los cambios. Para esto ejecutamos los comandos que ya conocemos:

dotnet ef migrations add DatosIniciales
dotnet ef database update 
