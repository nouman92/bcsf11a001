using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.Models
{
    public interface UserInterface
    {
        user validate(string username, string pass);
        user getuser(string username);
        string getpass(string username);
        user adduser(string username,string password,string email,string name,string country,string state,string city,string phone);
        ad getad(int id,string username);
        int newad(string user, string title, string catagory, string description,string path);
        int edit(string user, string id , string title, string catagory, string description);
        void ad_deactivate(int id, string username);
        void ad_activate(int id, string username);
        void ad_delet(int id, string username);
        void message_delet(int id, string username);
        void message_Archive(int id, string username);
        void message_inbox(int id, string username);
        message get_message(int id, string username);
        void change_email(string user,string email);
        void change_pass(string user,string pass);
        void personal_details(string username,string name,string phone,string city,string state,string country);
        List<city> get_cities();
        List<catagory> get_catagries();
        List<Country> get_countries();
        List<city> get_cities(int mState);
        List<state> get_states(int mCountry);
        List<catagory> get_catagories();
        List<ad> get_ads();
        void send_email(string user, string email, string message);
       
    }
}
