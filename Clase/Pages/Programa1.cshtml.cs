using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBehind.Pages
{
    // Obtiene los valores directamente de los controles de HTML
    [BindProperties]
    public class Programa1Model : PageModel
    {
        // Mapear todos los controles para obtener sus propiedades
        #region Entidad
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public int TipoCliente { get; set; }
        public int MetodoEnvio { get; set; }
        public int Descuento { get; set; }
        public int IVA { get; set; }

        public string? ResultadoOK { get; set; }
        public string? ResultadoNoOK { get; set; }
        #endregion

        public void OnGet()
        {
            // Automaticamente se ejecuta al abrir la página
        }

        // Acción del botón Guardar
        public void OnPostGuardar()
        {
            // Lógica del botón
            try
            {
                // Validación de entrada de datos
                List<string> lstErrores = new List<string>();

                if (Producto == 0)
                    lstErrores.Add("Falta seleccionar un producto.");
                if (Cantidad < 1 || Cantidad > 100)
                    lstErrores.Add("Falta ingresar una cantidad entre 1 y 100.");
                if (TipoCliente == 0)
                    lstErrores.Add("Falta seleccionar un tipo de cliente.");
                if (MetodoEnvio == 0)
                    lstErrores.Add("Falta seleccionar un método de envío.");
                if (Descuento < 1 || Descuento > 50)
                    lstErrores.Add("Falta ingresar un descuento entre 0% y 50%");

                // Validar que no haya errores
                if (lstErrores.Count == 0)
                {
                    ResultadoOK = "PRUEBITA JIJI";
                }
                else
                {
                    ResultadoNoOK = string.Join("<br>", lstErrores);
                }
            }
            catch (Exception e)
            {
                ResultadoNoOK = "Esto es un error, no se porque paso..." + e;
            }
        }

        // Acción del botón Limpiar
        public void OnPostLimpiar()
        {
            ModelState.Clear();
            Producto = 0;
            Cantidad = 0;
            TipoCliente = 0;
            MetodoEnvio = 0;
            Descuento = 0;
            ResultadoOK = string.Empty;
            ResultadoNoOK = string.Empty;
        }
    }
}