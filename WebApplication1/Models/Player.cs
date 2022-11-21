using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace WebApplication1.Models
{
    //:IEquatable<Player>
  
    public class Player

    {
        private bool isupdate=false;

        [Index(0)]
        public uint  id { get; set; }
       [Index(1)]
        public string? first_name { get; set; } = "";
        [Index(2)]
        public string? last_name { get; set; }
       [Index(3)]
        public string? position { get; set; }
        [Index(4)]
        public int? height_feet { get; set; }
       [Index(5)]

        public int height_inches { get; set; }
        [Index(6)]
       public int? weight_pounds { get; set; }
        [Index(7)]
        [ForeignKey("AppTeam")]
        public AppTeam team { get; set; }


        //cheak if is change from db to read csv 
        public bool updatebyFile (object other)
        {
            var dict = new Dictionary<string, string>();

            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo pi in props)
            {
                var l1 = pi.GetValue(other);
                var l2 = pi.GetValue(this);

                if (pi.Name == "team")
                { 
                isupdate= team.updatebyFile(l1);
                }
               else if (!l1.Equals(l2))
                {
                    
                    pi.SetValue(this, l2);
                    isupdate=true;
                     //string  key = pi.Name;
                   //var value = (string?)l2;

                  //  dict.Add(key:pi.Name, value:value);


                }
             

            }
            return isupdate;
        }

        
    }

//public class Team
//{
//        private bool isupdate=false;
//    public int team_id { get; set; }
//    public string team_abbreviation { get; set; }

//    public string team_city { get; set; }
//    public string team_conference { get; set; }
//    public string team_division { get; set; }
//    public string team_full_name { get; set; }
//    public string team_name { get; set; }

      
//    }

}
