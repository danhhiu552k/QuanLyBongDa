using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBongDa.Models;

namespace QuanLyBongDa.Controllers
{
    [RoutePrefix("League")]
    public class EPLController : Controller
    {
        DBGiaiDauDataContext db = new DBGiaiDauDataContext();
        // GET: EPL
        public ActionResult DetailEPL(string idLeague)
        {
            idLeague id = new idLeague() { idLeagueN = idLeague };
            return View(id);
        }

        public ActionResult DetailTeam(string idTeam)
        {
            idTeams id = new idTeams() { idTeam = idTeam };
            return View(id);
        }
        

        public ActionResult DetailPlayer(string idPlayer)
        {
            idPlayer id = new idPlayer() { IdPlayer = idPlayer };
            return View(id);
        }
        
    }
}