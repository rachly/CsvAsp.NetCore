using System.Reflection;

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AppTeam
    {

       [Key]
        public int team_Id { get; set; } 
        //public int team_id { get; set; }
        public string team_abbreviation { get; set; }

        public string team_city { get; set; }
        public string team_conference { get; set; }
        public string team_division { get; set; }
        public string team_full_name { get; set; }
        public string team_name { get; set; }
        public List<Player> Players { get; set; }

        private bool isupdate = false;

        //cheak if is change from db to read csv 
        public bool updatebyFile(object o)
        {
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo pi in props)
            {
                var l1 = pi.GetValue(o);
                var l2 = pi.GetValue(this);

                if (pi.Name == "team")
                {
                    updatebyFile(l1);
                }
                if (!l1.Equals(l2))
                {

                    pi.SetValue(this, l1);
                    isupdate = true;

                }


            }
            return isupdate;
        }
    }
    }
