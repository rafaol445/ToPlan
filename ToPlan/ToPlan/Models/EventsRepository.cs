﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ToPlan.Models
{
    public class EventsRepository
    {
        private List<string> types = new List<string>{ "food", "leisure", "sport"};
        internal void Save(Event e)
        {
            ToPlanContext context = new ToPlanContext();
            try
            {
                context.Events.Add(new Event(e.EventDate,e.City,e.Province,e.TypePlanId,e.Description,e.MaxMembers,e.UserId, e.Direccion));
                context.SaveChanges();
            }
            catch (Exception a)
            {
                Debug.WriteLine("Error de conexion");
            }
        }

        internal void UpdateEvent(int id, string f, string c, string p, string d, int max, string dir)
        {
            ToPlanContext context = new ToPlanContext();
            Event aux;
            try
            {
                aux = context.Events.Single(b => b.EventId == id);
                aux.EventDate = f;
                aux.City = c;
                aux.Province = p;
                aux.Description = d;
                aux.MaxMembers = max;
                aux.Direccion = dir;
                context.Events.Update(aux);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conéxion");

            }
        }

        internal void UpdateEventType(int id, int t)
        {
            ToPlanContext context = new ToPlanContext();
            Event aux;
            try
            {
                aux = context.Events.Single(b => b.EventId == id);
                aux.TypePlanId = t;
                context.Events.Update(aux);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion");

            }
        }

        internal bool CheckEventId(int id)
        {
            try
            {
                ToPlanContext context = new ToPlanContext();
                Event aux;
                try
                {
                    aux = context.Events.Where(b => b.EventId == id).FirstOrDefault();
                    if (aux == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error");
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conéxion");
                return true;
            }
        }

        internal void DeleteEvent(int id)
        {
            ToPlanContext context = new ToPlanContext();
            Event u;
            try
            {
                u = context.Events.Single(p => p.EventId == id);
                context.Remove(u);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conéxion: " + e);
            }
        }


        internal List<Event> RecoverEvents()
        {
            ToPlanContext context = new ToPlanContext();
            List<Event> u;
            try
            {
                u = context.Events.ToList();
                return u;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conéxion");
                return null;
            }
        }


        internal List<User> GetList(int id)
        {
            ToPlanContext context = new ToPlanContext();
            Event u;
            List<User> final = new List<User>();
            string[] aux;

            try
            {
                u = context.Events.Single(p => p.EventId == id);
                if (u.ListMembers == "")
                {
                    return null;
                }
                else
                {
                    aux = u.ListMembers.Split(';');
                    for (int i = 0; i < aux.Length; i++)
                    {
                        final.Add(context.Users.Single(x => x.UserId.Equals(aux[i].ToLower())));

                    }
                    return final;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;

            }


        }

        internal int GetType(int id)
        {
            ToPlanContext context = new ToPlanContext();
            Event e;
            try
            {
                e = context.Events.Single(p => p.EventId == id);
                return e.TypePlanId;

            }
            catch (Exception a)
            {
                Debug.WriteLine("Error de conexion:");
                return -1;
            }
        }

        internal EventDTO Even1(int id)
        {
            ToPlanContext context = new ToPlanContext();
            Event x;
            TypePlan t;
            User u;
            try
            {
                x = context.Events.Single(p => p.EventId == id);
                t = context.TypePlans.Single(p => p.TypePlanId == x.TypePlanId);
                u = context.Users.Single(p => p.UserId == x.UserId);
                return new EventDTO(x.City, x.EventDate, t.Name, t.Subtype, u.Name, u.Surname);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }
        internal EventDTO2 Even2(int id)
        {
            ToPlanContext context = new ToPlanContext();
            Event x;
            TypePlan t;
            User u;
            string[] lista;
            int aux = 0;
            try
            {
                x = context.Events.Single(p => p.EventId == id);
                t = context.TypePlans.Single(p => p.TypePlanId == x.TypePlanId);
                u = context.Users.Single(p => p.UserId == x.UserId);
                if (!x.ListMembers.Equals(""))
                {
                    lista = x.ListMembers.Split(';');
                    aux = lista.Length;
                }
                return new EventDTO2(x.City, x.EventDate, t.Name, t.Subtype, u.Name, u.Surname, aux, x.MaxMembers);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }

        internal List<EventDTO3> Even3()
        {
            ToPlanContext context = new ToPlanContext();
            List<EventDTO3> final = new List<EventDTO3>();
            DateTime Today = DateTime.Now;
            List<Event> aux2 = new List<Event>();
            int aux = 0;
            DateTime date;
            TypePlan t;
            User u;
            User u2;
            string[] aux3;
            try
            {
                aux2 = context.Events.ToList();
                for (int i = 0; i < aux2.Count; i++)
                {
                    List<string> lista = new List<string>();
                    t = context.TypePlans.Single(p => p.TypePlanId == aux2[i].TypePlanId);
                    u = context.Users.Single(p => p.UserId == aux2[i].UserId);
                    date = DateTime.Parse(aux2[i].EventDate);
                    aux3 = aux2[i].ListMembers.Split(';');
                    if (DateTime.Compare(date, Today) > 0)
                    {
                        for (int j = 0; j < aux3.Length; j++)
                        {
                            u2 = context.Users.Single(p => p.UserId.Equals(aux3[j]));
                            lista.Add(char.ToUpper(u2.Name[0]) + u2.Name.Substring(1) + " " + char.ToUpper(u2.Surname[0]) + u2.Surname.Substring(1));
                        }
                        final.Add(new EventDTO3(aux2[i].City, aux2[i].EventDate, t.Name, t.Subtype, u.Name, u.Surname, aux2[i].Direccion, aux2[i].Description, aux2[i].MaxMembers, lista));
                        aux++;
                    }
                    if (aux == 4)
                    {
                        return final;
                    }
                }
                return final;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }

        internal List<EventDTO3> EventsType(string g)
        {
            ToPlanContext context = new ToPlanContext();
            List<EventDTO3> final = new List<EventDTO3>();
            List<Event> aux2 = new List<Event>();
            DateTime Today = DateTime.Now;
            DateTime date;
            TypePlan t;
            User u;
            User u2;
            string[] aux3;
            try
            {
                aux2 = context.Events.ToList();
                for (int i = 0; i < aux2.Count; i++)
                {
                    List<string> lista = new List<string>();
                    t = context.TypePlans.Single(p => p.TypePlanId == aux2[i].TypePlanId);
                    u = context.Users.Single(p => p.UserId == aux2[i].UserId);
                    date = DateTime.Parse(aux2[i].EventDate);
                    aux3 = aux2[i].ListMembers.Split(';');
                    if (DateTime.Compare(date, Today) >= 0)
                    {
                        for (int j = 0; j < aux3.Length; j++)
                        {
                            u2 = context.Users.Single(p => p.UserId.Equals(aux3[j]));
                            lista.Add(char.ToUpper(u2.Name[0]) + u2.Name.Substring(1) + " " + char.ToUpper(u2.Surname[0]) + u2.Surname.Substring(1));
                        }
                        if (t.Name.Equals(g))
                        {
                            final.Add(new EventDTO3(aux2[i].City, aux2[i].EventDate, t.Name, t.Subtype, u.Name, u.Surname, aux2[i].Direccion, aux2[i].Description, aux2[i].MaxMembers, lista));
                        }
                    }
                }
                return final;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }

        internal List<EventDTO> EventsUser(string id)
        {
            ToPlanContext context = new ToPlanContext();
            List<EventDTO> final = new List<EventDTO>();
            List<Event> aux2 = new List<Event>();
            TypePlan t;
            User u;
            string[] aux;
            try
            {
                aux2 = context.Events.Where(p => p.UserId.Equals(id.ToLower())).ToList();
                for (int i = 0; i < aux2.Count; i++)
                {
                    aux = aux2[i].ListMembers.Split(';');
                    if (Array.Exists(aux, element => element.Equals(id)))
                    {
                        t = context.TypePlans.Single(p => p.TypePlanId == aux2[i].TypePlanId);
                        u = context.Users.Single(p => p.UserId == aux2[i].UserId);
                        final.Add(new EventDTO(aux2[i].City, aux2[i].EventDate, t.Name, t.Subtype, u.Name, u.Surname));
                    }
                }
                return final;


            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }

        internal bool AddUser(int id, string e)
        {
            ToPlanContext context = new ToPlanContext();
            Event e1;
            String[] aux;
            try
            {
                e1 = context.Events.Single(p => p.EventId == id);
                aux = e1.ListMembers.Split(';');
                if ((aux.Length + 1) < e1.MaxMembers)
                {
                    e1.ListMembers = e1.ListMembers + ";" + e;
                    context.Events.Update(e1);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e2)
            {
                Debug.WriteLine("Error de conexion:");
                return false;
            }
        }
        internal bool RemoveUser(int id, string e)
        {
            ToPlanContext context = new ToPlanContext();
            Event e1;
            String[] aux;
            bool aux2 = false;
            try
            {
                e1 = context.Events.Single(p => p.EventId == id);
                aux = e1.ListMembers.Split(';');
                e1.ListMembers = "";
                for (int i = 0; i < aux.Length; i++)
                {
                    if (!aux[i].Equals(e.ToLower()))
                    {
                        if (e1.ListMembers.Equals(""))
                        {
                            e1.ListMembers = aux[i];
                        }
                        else
                        {
                            e1.ListMembers = e1.ListMembers + ";" + aux[i];
                        }
                        context.Events.Update(e1);
                        context.SaveChanges();
                        aux2 = true;
                    }
                }
                return aux2;
            }
            catch (Exception e2)
            {
                Debug.WriteLine("Error de conexion:");
                return false;
            }
        }

        internal bool CheckUserEvent(int id, string n)
        {
            ToPlanContext context = new ToPlanContext();
            Event e1;
            String[] aux;
            bool aux2 = false;
            try
            {
                e1 = context.Events.Single(p => p.EventId == id);
                aux = e1.ListMembers.Split(';');
                for (int i = 0; i < aux.Length; i++)
                {
                    if (aux[i].Equals(n.ToLower()))
                    {
                        aux2 = true;
                    }
                }
                return aux2;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return false;
            }
        }

        internal List<Event> EventsByType(string s)
        {
            ToPlanContext context = new ToPlanContext();
            List<Event> lista;
            List<Event> final = new List<Event>();
            TypePlan t;
            try
            {
                lista = context.Events.ToList();
                for (int i = 0; i < lista.Count; i++)
                {
                    t = context.TypePlans.Single(p => p.TypePlanId == lista[i].TypePlanId);
                    if (t.Subtype.Equals(s.ToLower()))
                    {
                        final.Add(lista[i]);
                    }
                }
                return final;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }

        internal List<Event> EvetsByType2(string ty)
        {
            ToPlanContext context = new ToPlanContext();
            List<Event> lista = new List<Event>();
            List<Event> final = new List<Event>();
            TypePlan t;
            try
            {
                lista = context.Events.ToList();
                for (int i = 0; i < lista.Count; i++)
                {
                    t = context.TypePlans.Single(p => p.TypePlanId == lista[i].TypePlanId);
                    if (t.Name.Equals(ty.ToLower()))
                    {
                        final.Add(lista[i]);
                    }
                }
                return final;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }

        internal bool CheckType(string t)
        {
            bool aux = false;
            for(int i = 0;i< types.Count; i++)
            {
                if (types[i].Equals(t.ToLower()))
                {
                    aux = true;
                }
            }
            return aux;
        }
        internal bool AddType(string t)
        {
            if (CheckType(t))
            {
                return false;
            }
            else
            {
                types.Add(t.ToLower());
                return true;
            }
        }
    }
}