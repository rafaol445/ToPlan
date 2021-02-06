﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ToPlan.Models
{
    public class EventsRepository
    {
        internal void Save(Event e)
        {
            ToPlanContext context = new ToPlanContext();
            try
            {
                context.Events.Add(e);
                context.SaveChanges();
            }
            catch (Exception a)
            {
                Debug.WriteLine("Error de conexion");
            }
        }

        internal void UpdateEvent(int id, string f, string c, string p, string d, int max)
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conéxion");
                return false;
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

        internal void InsertUser(int id, string n)
        {
            ToPlanContext context = new ToPlanContext();
            Event u;
            try
            {
                u = context.Events.Single(p => p.EventId == id);
                if (u.ListMembers == "")
                {
                    u.ListMembers = n.ToLower();
                }
                else
                {
                    u.ListMembers = u.ListMembers + ";" + n.ToLower();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");

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
                u = context.Users.Single(p => p.UserId == x.CreatorEmail);
                return new EventDTO(x.City, x.EventDate, t.Name, t.Subtype, u.Name, u.Surname);
            }
            catch(Exception e)
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
                u = context.Users.Single(p => p.UserId == x.CreatorEmail);
                if (!x.ListMembers.Equals(""))
                {
                    lista = x.ListMembers.Split(';');
                    aux = lista.Length;
                }
                return new EventDTO2(x.City, x.EventDate, t.Name, t.Subtype, u.Name, u.Surname,aux,x.MaxMembers);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion:");
                return null;
            }
        }



    }
}