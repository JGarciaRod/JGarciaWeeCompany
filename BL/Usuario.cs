using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JgarciaWeeCompanyContext context = new DL.JgarciaWeeCompanyContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll");

                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = item.IdUsuario;
                            usuario.Nombre = item.Nombre;
                            usuario.Email = item.Email;
                            usuario.Telefono = item.Telefono;
                            usuario.Empresa = item.Empresa;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMesage = e.Message;
                result.Ex = e;
            }
            return result;
        }

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JgarciaWeeCompanyContext context = new DL.JgarciaWeeCompanyContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.Email = query.Email;
                        usuario.Telefono = query.Telefono;
                        usuario.Empresa = query.Empresa;
                        
                        result.Object = usuario;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMesage = e.Message;
                result.Ex = e;
            }
            return result;
        }


        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JgarciaWeeCompanyContext context = new DL.JgarciaWeeCompanyContext())
                {
                    int rowAffected = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.Email}','{usuario.Telefono}','{usuario.Empresa}'");

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else 
                    { 
                        result.Correct = false; 
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMesage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JgarciaWeeCompanyContext context = new DL.JgarciaWeeCompanyContext())
                {
                    int rowAffected = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario} , '{usuario.Nombre}','{usuario.Email}','{usuario.Telefono}','{usuario.Empresa}'");

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JgarciaWeeCompanyContext context =  new DL.JgarciaWeeCompanyContext())
                {
                    int rowAffected = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");

                    if(rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}