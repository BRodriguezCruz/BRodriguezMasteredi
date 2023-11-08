using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class EmisorController : Controller
    {
        /*
        public IActionResult GetAll()
        {
            BL.Emisor emisor = BL.Emisor.GetAllEF();

            return View(emisor);
        }*/

        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Emisor emisor = new BL.Emisor();
            emisor.Emisores = new List<object>();

            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5162/api/");
                var respuesta = cliente.GetAsync("emisor");
                respuesta.Wait();

                var resultadoServicio = respuesta.Result;   

                if (resultadoServicio.IsSuccessStatusCode)
                {
                    var leerTarea = resultadoServicio.Content.ReadAsAsync<BL.Emisor>();
                    leerTarea.Wait();

                    foreach (var resultEmisores in leerTarea.Result.Emisores)
                    {
                        BL.Emisor resultItemsList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Emisor>(resultEmisores.ToString());
                        emisor.Emisores.Add(resultItemsList);
                    }
                }
            }
            return View(emisor);
        }

        [HttpGet]
        public IActionResult Form(string idEmisor)
        {
            BL.Emisor emisor = new BL.Emisor();

            if (idEmisor != null)
            {
                emisor.Bandera = "update";

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5162/api/");
                    var respuesta = cliente.GetAsync("emisor/" + idEmisor);
                    respuesta.Wait();

                    var resultadoServico = respuesta.Result;

                    if (resultadoServico.IsSuccessStatusCode)
                    {
                        var leerTarea = resultadoServico.Content.ReadAsAsync<BL.Emisor>();
                        leerTarea.Wait();

                        var objeto = leerTarea.Result;

                        //BL.Emisor resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Emisor>(leerTarea.Result.ToString());
                        emisor = objeto;
                    }
                }
            }
            else
            {
                emisor.Bandera = "add";
                //form vacio
            }
            return View(emisor);
        }

        [HttpPost]
        public IActionResult Form(BL.Emisor emisor)
        {
            emisor.Correct = true;

            if (emisor.Bandera == "add")
            {
                using(HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5162/api/");
                    var postTask = cliente.PostAsJsonAsync("emisor", emisor);
                    postTask.Wait();

                    var resultService = postTask.Result;

                    if(resultService.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "EMISOR AGREGADO CON EXITO, SU ID ES: " + emisor.IdEmisor ;
                    }
                    else
                    {
                        ViewBag.Message = "ERROR, NO SE AGREGO AL EMISOR";
                    }
                }
            }
            else
            {
                emisor.Bandera = "update";
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri("http://localhost:5162/api/");
                    var putTask = cliente.PutAsJsonAsync("emisor/" +  emisor.IdEmisor, emisor);
                    putTask.Wait();

                    var resultService = putTask.Result;

                    if (resultService.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "EMISOR ACTUALIZADO CON EXITO, ID: " + emisor.IdEmisor;
                    }
                    else
                    {
                        ViewBag.Message = "ERROR, NO SE ACTUALIZO AL EMISOR";
                    }
                }
            }
            return PartialView("Modal");
        }

        public IActionResult Delete(string idEmisor)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5162/api/");
                var deleteTask = cliente.DeleteAsync("emisor/" + idEmisor);
                deleteTask.Wait();

                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)

                {
                    ViewBag.Message = "Se elimino correctamente al emisor";
                }
                else
                {
                    ViewBag.Message = "No se elimino al emisor";
                }
                return PartialView("Modal");
            }
        }
    }
}

