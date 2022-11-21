using Microsoft.AspNetCore.Hosting;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using WebApplication1.Models;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using WebApplication1.Data;
//using LinqToDB;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
	public class PlayerController : Controller
	{
		private Timer _timer = null;
		private bool bInttilaze = false;
		private readonly AppDBContext _context;
		private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
		public PlayerController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, AppDBContext context)
		{
			_hostingEnvironment = hostingEnvironment;
			_context = context;
		}

		public IActionResult Index(List<Player> players)
		{

			players = players == null ? new List<Player>() : players;

			return View(players);
		}
		[HttpPost]
		public IActionResult Index(IFormFile file)

		{
			#region Upload CSV
			string fileName = $"{_hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
			using (FileStream fileStream = System.IO.File.Create(fileName))
			{
				file.CopyTo(fileStream);
				fileStream.Flush();
			}

			#endregion
			var player = this.GetPlayerList(file.FileName);


			every15Minte(file.FileName);
			return Index(player);
		}

		bool b = false;
		private List<Player> GetPlayerList(string fileName)
		{

			List<Player> players = new List<Player>();

			#region Read CSV
			var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
			using (var reader = new StreamReader(path))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csv.Read();
				csv.ReadHeader();
				while (csv.Read())
				{
					var player = csv.GetRecord<Player>();

					players.Add(player); 
					

				}
                 
			}

            if (b == false)
            {
				inserDB(players);

			}

            #endregion
            #region Create  CSV 
            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
			using (var write = new StreamWriter(path + "\\NewFile.csv"))
			using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(players);
			}


			#endregion
			return players;

		}

		public IActionResult inserDB(List<Player> p)
        {
			var list = _context.AppUsers.Include(x => x.team).ToList();
			if (list.Count == 0)
          {
              for (int i = 0; i < p.Count; i++)
            {
				b=true;
                _context.AppUsers.Add(p[i]);

				_context.SaveChanges();
			}
		
         
            }
			   return Ok(p);
			
        }


		public async void every15Minte(string file)
		{
			//while (await timer.WaitForNextTickAsync())
			//{

			//get from db

			var list = _context.AppUsers.Include(x => x.team).ToList();			    
			
		
			var timer = new PeriodicTimer(TimeSpan.FromSeconds(3));

			var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file;
			//get read csv
			var players = GetPlayerList(file);
			

			//timer !
			// cheak if is change 
			var b = false;

			for (int i = players.Count; i >=0; i--)
			{
			bool v = list[i].updatebyFile(players[i]);


				            if (v ==true)
			               {
				 	     var index = list[i];
					    //query update this index
				    	 var myplayer = _context.AppUsers.First(g => g.id == players[i].id);
				             myplayer.id=list[i].id;
					         myplayer.first_name = list[i].first_name;
					         myplayer.last_name = list[i].last_name;
					         myplayer.position = list[i].position;
					         myplayer.height_feet = list[i].height_feet;
					         myplayer.height_inches = list[i].height_inches;
					         myplayer.weight_pounds = list[i].weight_pounds;
					         myplayer.team = list[i].team;

					         _context.SaveChanges();
               
				       }

					Console.WriteLine(v);
		      	}

				if (b == true)
				{
					//	HttpContext.Session.SetString("SessionPlayers", JsonConvert.SerializeObject(list));
					Console.WriteLine("הנתונים עודכנו בהצלחה");
				}
				Console.WriteLine(b);


				


				//}







			}




		}
	}

