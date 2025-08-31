using System.Net.Http.Json;
using System.Text.Json;

var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7041");
bool disponible = false;
bool stockCheckeado= false;
int idProducto;
int cantidad=0;
bool productoEncontrado = false;

Console.Write("Ingrese el ID del producto:");
idProducto=Convert.ToInt16(Console.ReadLine());

Console.Write("Ingrese la cantidad del producto:");
cantidad = Convert.ToInt16(Console.ReadLine());

// Obtener el producto
try
{
    var checkResponse = await client.GetFromJsonAsync<JsonElement>($"api/producto/{idProducto}");
    Console.WriteLine($"Nombre del producto: {checkResponse.GetProperty("nombre").GetString()}" );
    Console.WriteLine($"Precio: {checkResponse.GetProperty("precio").GetString()}");

    productoEncontrado = true;
}
catch (Exception ex)
{
    Console.WriteLine($"Error al buscar el producto: {ex.Message}");
}

if (productoEncontrado)
{// Check stock
try
{
    var checkResponse = await client.GetFromJsonAsync<JsonElement>($"api/producto/check/{idProducto}/{cantidad}");
    disponible = checkResponse.GetProperty("disponible").GetBoolean();
    stockCheckeado = true;
}
catch (Exception ex)
{
    Console.WriteLine($"Error checkeando el stock : {ex.Message}");
}

    if (stockCheckeado)
    {
        if (disponible)
        {
            Console.WriteLine("La cantidad de producto solicitada está disponible en el stock.");
            // Crear pedido
            try
            {
                var orderResponse = await client.PostAsJsonAsync("api/producto/order", new { IdProducto = idProducto, cantidad = cantidad });
                var result = await orderResponse.Content.ReadFromJsonAsync<JsonElement>();
                Console.WriteLine(result.GetProperty("mensaje").GetString());

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ordenando : {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("La cantidad de producto solicitada NO está disponible en el stock.");
        }
    }
}

Console.WriteLine("Presione una tecla para finalizar");
Console.ReadKey();

