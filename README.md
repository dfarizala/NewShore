
# NewShore
Proyecto para la prueba tecnica solicitada por newshore de Diego Fernando Arizala Revelo

# Arquitectura usada en el proyecto

**BackEnd:** Clean architecture
**FrontEnd:** MVC

# Patrones de diseno usados en el proyecto

**Backend:** DDD, CQRS, Repository, Unit of work, Vertical Slicing, Dependency Inyection
**FrontEnd:** MVC

**Uso de principios SOLID:** Si
**Inyeccion de dependencias:** Si

# Base de datos:
La base de datos se encuentra alojada en un servidor de amazaon, es SQL Server version 2019,, la cadena de conexion es la siguiente:

    Server=sagitario.curkguieuauu.us-east-2.rds.amazonaws.com;Initial Catalog=NewshoreTest;Persist Security Info=False;User ID=OdesyApi;Password=4fAi0K%9sj&v;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;

# Conversion de moneda:
La conversion de moneda se encuentra en el archivo de configuracion, inicialmente trae tres denominaciones distintas, se debe solo agregar una o varias dependiendo de las que se quiera, en este caso se debe modificar el JSON de configuracion en la seccion "Currency", junto con su valor nominal 1 a 1 en cuando a dolares se refiere:

      "Currency" : {
        "COP": 4500,
        "USD": 1,
        "MXN": 200
      }

# Api Rest
La Api, se consume como servico rest desde la siguiente ruta

    http://localhost:5210/api/GetTravel

Esta api se compone de un Json de solicitud de la siguiente manera:
```json
{
  "origin": "MZL",
  "destination": "BCN",
  "currency": "COP"
}
```
y se obtiene la siguiente respuesta:

```json
{
  "status": "string",
  "message": "string",
  "journey": {
    "origin": "string",
    "destination": "string",
    "price": 0,
    "flights": [
      {
        "origin": "string",
        "destination": "string",
        "price": 0,
        "transport": {
          "flightCarrier": "string",
          "fLightNumber": "string"
        }
      }
    ]
  }
}
```
# Paquetes NuGet usados en la solucion
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.EntityFrameworkCore.Designer
* MediatR.Extensions.Microsoft.DependencyInjection
* AutoMapper.Extensions.Microsoft.DependencyInjection

