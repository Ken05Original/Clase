using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBehind.Pages
{
    //D
    // Creamos la clase producto , y creamos sus atributos
    public class Producto
    {
        public int Id {get ; set;}
        public string? Nombre {get ; set;}
        public string? Categoria {get ; set;} // si valor puede ser nulo ponemos signo ?
        public decimal Precio {get ; set;}
        public int Stock {get ; set;}
        public string? ImagenUrl {get ; set;}
    }

    public class Programa4Model : PageModel
    {
        // creo lista privada estatica porque no podremos modificarla con el tipo de objeto de la clase que creamos
        private static List<Producto> lstProducto = new List<Producto>
        {
            new Producto {Id = 1, Nombre="GTA VI", Categoria="Categoria 1", Precio = 49.99m, Stock=5, ImagenUrl="https://m.media-amazon.com/images/I/8193phjuCML._AC_UF1000,1000_QL80_.jpg"},
            new Producto {Id = 2, Nombre="ZELDA OCARINA OF TIME", Categoria="Categoria 1", Precio = 149.99m, Stock=5, ImagenUrl="https://http2.mlstatic.com/D_NQ_NP_763449-MLM115906328382_092026-O.webp"},
            new Producto {Id = 3, Nombre="Producto C", Categoria="Categoria 1", Precio = 79.99m, Stock=5, ImagenUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTjKyi3LUVhheP3okEbhtlXFYQ8y1QVTPvVviTIoub8NsAFfMgT7gBXK7I&s=10"},
            new Producto {Id = 4, Nombre="Producto D", Categoria="Categoria 1", Precio = 129.99m, Stock=5, ImagenUrl=""},
            new Producto {Id = 5, Nombre="Producto E", Categoria="Categoria 1", Precio = 9.99m, Stock=5, ImagenUrl=""}

        };

        // Es el Acceso a directo a la interfaz conecta el modelo con HTML
        public List<Producto> Productos {get; set;} = new List<Producto>();

        public String? Mensaje {get; set;}
        public void OnGet()
        {
            // Imprimir los datos de la lista al abrir el navegador es lo primero que se ejecuta
            Productos = lstProducto;
        }

        public void OnPost(int idProducto) // ocupara un parametro
        {
            // busca en la lista para simular la baja de elementos de la lista
            var item = lstProducto.FirstOrDefault(p => p.Id == idProducto);

            // VALIDA SI ENCUENTRA UNA coincidencia
            if(item != null && item.Stock > 0)
            {
                item.Stock = item.Stock - 1;
                Mensaje = $"Compraste {item.Nombre}, quedan: {item.Stock} en Stock";
            }
            else
            {
                Mensaje = "Producto agotado, vuelve pronto";
            }

            // Volver a llenar los elementos, recargar la página 
            Productos = lstProducto;

        }
        
    }
}
