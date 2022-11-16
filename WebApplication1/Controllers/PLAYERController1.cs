using Microsoft.AspNetCore.Hosting;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;


using System;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Controllers
{
	public class PlayerController : Controller
	{
		private Timer _timer = null;
		private bool bInttilaze = false;

		private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
		public PlayerController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		//[HttpGet]
		//[Route("[controller]/[action]")]
		//public IActionResult Test()
		//{
		//	var root = $"{_hostingEnvironment.WebRootPath}";
		//	return Ok();
		//}
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
			
			//HttpContext.Session.SetString("SessionPlayers", JsonConvert.SerializeObject(player));
			every15Minte(file.FileName);
			return Index(player);
		}

            bool b=false;
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

    //        if (b == false)
    //        {
    //          HttpContext.Session.SetString("SessionPlayers", JsonConvert.SerializeObject(players));
				//b=true;
    //        }

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



	
		public async void every15Minte(string file)
		{
			var timer = new PeriodicTimer(TimeSpan.FromSeconds(3));
		
			 var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file;

			var players = GetPlayerList(file);
			
            
          // HttpContext.Session.Clear();
           

			//players[0].height_feet = "789";
		 	  
			   
		 	var list = JsonConvert.DeserializeObject<List<Player>>(HttpContext.Session.GetString("SessionPlayers"));
			
			var b=false;
            for (int i = 0; i < players.Count; i++)
            {
          Dictionary<string,string> v= list[i].updatebyFile(players[i]);
                if (v.Count > 0)
                {
					HttpContext.Session.Clear();
					HttpContext.Session.SetString("SessionPlayers", JsonConvert.SerializeObject(list));
				}
				Console.WriteLine(v);
			}
            if (b == true)
            {
			//	HttpContext.Session.SetString("SessionPlayers", JsonConvert.SerializeObject(list));
				Console.WriteLine("הנתונים עודכנו בהצלחה");
            }
			Console.WriteLine(b);
		

			//while (await timer.WaitForNextTickAsync())
			//{


			//	Console.WriteLine(players);
			//	Console.WriteLine(list);

			//	count++;
			//	Console.WriteLine(DateTime.Now);
			//	if (count > 10)
			//	{
			//		timer.Dispose();
			//	}
			//}







		}

	
	}
}
