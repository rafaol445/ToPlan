﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ToPlan.Models;

namespace ToPlan.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/User/Check")]
        //Comprueba si existe un Usuario a partir de una id. Devuelve true si no existe y false si existe
        public bool CheckId(string id)
        {
            UsersRepository rep = new UsersRepository();
            bool aux = rep.CheckId(id);
            return aux;
        }

        [Route("api/User/GetPassword")]
        //Devuelve la password de un usuario en especifico a partir de una id
        public string GetPassword(string id)
        {
            UsersRepository rep = new UsersRepository();
            string aux = rep.GetPasswrod(id);
            return aux;
        }

        [Route("api/UserDTO")]
        //Devuelve el nombre, apellidos y fecha de un usuario a partir de su id
        public UserDTO GetUserDTO(string id)
        {
            UsersRepository rep = new UsersRepository();
            UserDTO u = rep.RecoverUserDTO(id);
            return u;
        }
        [Route("api/Users")]
        //Recupera toda la lista de usuarios
        public List<User> GetUser()
        {
            UsersRepository rep = new UsersRepository();
            List<User> lista = rep.RecoverUsers();
            return lista;
        }

        [Route("api/User/Preferences")]
        //Devuelve un array de las preferencias de cada usuario a partir de su id
        public string[] GetPreferences(string id)
        {
            UsersRepository rep = new UsersRepository();
            string[] u = rep.RecoverPreferences(id);
            return u;
        }

        [Route("api/User")]
        //Insertar un nuevo usuario 
        public void Post([FromBody] User u)
        {
            UsersRepository rep = new UsersRepository();
            rep.Save(u);
        }

        [Route("api/User/AdminT")]
        //Añade permisos de administrador un usuario
        public void PutAdminT(string id)
        {
            UsersRepository rep = new UsersRepository();
            rep.BecomeAdmin(id);
        }

        [Route("api/User/AdminF")]
        //Elimina permisos de administrador un usuario
        public void PutAdminF(string id)
        {
            UsersRepository rep = new UsersRepository();
            rep.StopAdmin(id);
        }
        [Route("api/User/Password")]
        //Modifica la contraseña de un usuario a partir de su id
        public void PutPassword(string id, string nid)
        {
            UsersRepository rep = new UsersRepository();
            rep.ChangePassword(id,nid);
        }

        [Route("api/User")]
        //Modifica el nombre, apellido, fecha y preferencias de un usuario a partir de su id
        public void PutUser(string id,string n, string s,  string f, string p)
        {
            UsersRepository rep = new UsersRepository();
            rep.ChangeThings(id,n,s,f,p);
        }

        [Route("api/User")]
        //Elimina un usuario a partir de su id
        public void DeleteUser(string id)
        {
            UsersRepository rep = new UsersRepository();
            rep.DeleteUser(id);
        }
    }
}