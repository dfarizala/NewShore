
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

# Pasos para ejecutar la solucion

1. compilar la solucion completa y ejecutarla por separado tanto el Api como el Web
2. Solo es necesario compilar y ejecutar ya que el archivo de proyecto esta configurado para instalar los paquetes nuget durante la compilacion.
3. Para configurar la ruta del servicio solo basta abrir el archivo https://github.com/dfarizala/NewShore/blob/main/NewshoreTest.Web/Controllers/HomeController.cs y cambiar la variable `_baseUrl`  por la url correcta del servicio

       [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Browse(IFormCollection collection)
        {
            var _baseUrl = "http://localhost:5210";
    
            RequestViewModel FlightOBject = new RequestViewModel
            {
                Destino = collection["Destino"].ToString(),
                Moneda = collection["Moneda"].ToString(),
                Origen = collection["Origen"].ToString()
            };
    
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                //HttpContent _Content = new JsonContent(_Request, );
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsJsonAsync("api/GetTravel", FlightOBject);
                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;
                    var _Result = JsonConvert.DeserializeObject<GetTravelResponse>(UserResponse);
    
                    if (_Result.Status != "OK")
                        throw new Exception("Error retreiving flights");
                }
    
            }
    
            return RedirectToAction(nameof(Index));
        }

