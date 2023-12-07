using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();

            ML.Result resultObjects = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5290/api/Usuario/");

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                        usuario.Usuarios.Add(resultItemList);
                    }
                }
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Form(int? IdUsuario) 
        {
            ML.Usuario usuario = new ML.Usuario();

            if(IdUsuario != 0) //update
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5290/api/Usuario/");

                    var responseTask = client.GetAsync("GetById/"+ IdUsuario);
                    responseTask.Wait();

                    var result =  responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Usuario readItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        usuario = readItemList;
                    }
                }
            }
            else //add
            {

            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Form(ML.Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5290/api/Usuario/");

                if (usuario.IdUsuario == 0) //add
                {
                    var responsTask = client.PostAsJsonAsync<ML.Usuario>("", usuario);
                    responsTask.Wait();

                    var result = responsTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se incerto correctamente";
                    }
                    else
                    {
                        ViewBag.Error = "Error al incertar";
                    }
                }
                else //update
                {
                    var responsTask = client.PutAsJsonAsync<ML.Usuario>("" + usuario.IdUsuario , usuario);
                    responsTask.Wait();

                    var result = responsTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se actualizo correctamente";
                    }
                    else
                    {
                        ViewBag.Error = "Error al actualizar";
                    }
                }
            }
            return PartialView("Modal");
        }

        public IActionResult Delete(int IdUsuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5290/api/Usuario/");

                var readTask = client.DeleteAsync("" + IdUsuario);
                readTask.Wait();

                var result = readTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se elimino correctamente";
                }
                else
                {
                    ViewBag.Error = "Error al eliminar";
                }
            }
            return PartialView("Modal");
        }
    }
}
