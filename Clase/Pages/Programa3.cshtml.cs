using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clase.Pages
{
    [BindProperties]
    public class Programa3Model : PageModel


    {

        public int Noches { get; set; }

        public int TipoHuesped { get; set; }

        public int Huespedes { get; set; }

        public bool ChkDesayuno { get; set; }

        public bool ChkEstacionamiento { get; set; }

        public bool ChkWifi { get; set; }

        public bool ChkSpa { get; set; }

        public int IVA { get; set; }

        public string? DescuentoTexto { get; set; }

        public string? ResultadoOK { get; set; }

        public string? ResultadoNoOK { get; set; }
        public int TipoHabitacion { get; set; }

        public List<SelectListItem> ddlTipoHabitacion { get; set; } = new();


        public void CargarDropDownList()
        {
            ddlTipoHabitacion = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Seleccione" },
                new SelectListItem { Value = "1", Text = "Sencilla ($800/noche)" },
                new SelectListItem { Value = "2", Text = "Doble ($1,200/noche)" },
                new SelectListItem { Value = "3", Text = "Suite ($1,800/noche)" },
                new SelectListItem { Value = "4", Text = "Master Suite ($2,500/noche)" }
            };
        }


        public void OnGet()
        {
            CargarDropDownList();
        }


        public void OnPostCalcular()
        {
            CargarDropDownList();
        }
    }
}