using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class UserClass : UserInterface
    {
        Entities db = new Entities();
        public user validate(string username, string pass)
        {
           user u = db.users.Find(username);
            if (u != null && u.password.Equals(pass))
            {
                return db.users.Find(username);
            }
            else
                return null;
        }
        public user getuser(string username)
        {
            return db.users.Find(username);
        }
        public message get_message(int id, string username)
        {
            return db.messages.Find(id);
        }
        public ad getad(int id,string username)
        {
            List<ad> lsads=db.users.Find(username).ads.ToList();
            foreach(ad a in lsads)
            {
                if (a.Id == id)
                    return a;
            }
            return null;
        }
        public void ad_deactivate(int id, string username)
        {
            ad a = db.ads.Find(id);
            a.activity = false;
            db.SaveChanges();
        }
        public void ad_activate(int id, string username)
        {
            ad a = db.ads.Find(id);
            a.activity = true;
            db.SaveChanges();
        }

        public int newad(string user, string title, string catagory, string description, string path)
        {
            ad a = new ad();
            a.title = title;
            a.catagory = int.Parse(catagory);
            a.description = description;
            a.activity = true;
            a.user = user;
            a.date = DateTime.Now;
            a.image = path;
            db.ads.Add(a);
            db.SaveChanges();
            return a.Id;
        }
        public void ad_delet(int id, string username)
        {
            ad a = db.ads.Find(id);
            db.ads.Remove(a);
            db.SaveChanges();
        }
        public int edit(string user, string id, string title, string catagory, string description)
        {
            ad a = db.ads.Find(int.Parse(id));
            a.title = title;
            a.catagory = int.Parse(catagory);
            a.description = description;
            a.activity = true;
            a.user = user;
            a.date = DateTime.Now;
            db.SaveChanges();
            return db.users.Find(user).ads.Last().Id;
        }

        public List<Country> get_countries()
        {
            return db.Countries.ToList();
        }

        public List< catagory> get_catagories()
        {
            return db.catagories.ToList();
        }

        public List<city> get_cities(int mState)
        {
            return db.states.Find(mState).cities.ToList();
        }

        public List<catagory> get_catagries()
        {
            List<catagory> lst =db.catagories.ToList();
            lst.Insert(0,new catagory { type="All",Id=0});
            return lst;
        }
        public List<city> get_cities()
        {
            List<city> lst=db.cities.ToList();
            lst.Insert(0,new city {name="All",Id=0});
            return lst;
        }

        public List<state> get_states(int mCountry)
        {
            return db.Countries.Find(mCountry).states.ToList();
        }

        public void message_delet(int id, string username)
        {
            message m = db.messages.Find(id);
            db.messages.Remove(m);
            db.SaveChanges();
        }
        public void message_Archive(int id, string username)
        {
            message m = db.messages.Find(id);
            m.deleted = true;
            db.SaveChanges();
        }
        public void message_inbox(int id, string username)
        {
            message m = db.messages.Find(id);
            m.deleted = false;
            db.SaveChanges();
        }
        public user adduser(string username, string password, string email, string name, string country, string state, string city, string phone)
        {
            user u = new user();
            u.name = name;
            u.username = username;
            u.password = password;
            u.email = email;
            u.phone = phone;
            u.password2 = "123456";
            addre a = new addre();
            a.city  = Int32.Parse(city);
            a.state = Int32.Parse(state);
            a.country = Int32.Parse(country);
            a.street="";
            db.addres.Add(a);
            db.SaveChanges();
            u.addre = a;
            db.users.Add(u);
            db.SaveChanges();
            return u;
        }
        public void change_email(string user,string email)
        {
            user u = db.users.Find(user);
            u.email = email;
            db.SaveChanges();
        }
        public void change_pass(string user, string pass)
        {
            user u = db.users.Find(user);
            if (u != null)
            {
                u.password2 = "123456";
                u.password = pass;
                db.SaveChanges();
            }
        }
        public void personal_details(string username, string name, string phone, string city, string state, string country)
        {
            user u = db.users.Find(username);
            u.name = name;
            u.phone = phone;
            u.addre.city = Int32.Parse(city);
            u.addre.state = Int32.Parse(state);
            u.addre.country = Int32.Parse(country);
            u.password2 = "1234567";
            db.SaveChanges();
        }
        public string getpass(string username)
        {
            return db.users.Find(username).password;
        }
        public List<ad> get_ads()
        {
            return db.ads.ToList();
        }
        public void send_email(string user,string email,string message)
        {
            message m = new message();
            m.deleted = false;
            m.from = email;
            m.message1 = message;
            m.user = user;
            db.messages.Add(m);
            db.SaveChanges();
        }
    }
}