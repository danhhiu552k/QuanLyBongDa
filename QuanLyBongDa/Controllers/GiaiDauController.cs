using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuanLyBongDa.Controllers
{
    public class GiaiDauController : ApiController
    {
        /*// GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }*/
        DBGiaiDauDataContext db = new DBGiaiDauDataContext();
        
        
        [HttpGet]
        //[Route("api/giaidau")]
        public IEnumerable<GiaiDau> GetAll()
        {
            return db.GiaiDaus.ToList();
        }


        [HttpGet]
        public IEnumerable<DoiBong> GetDoi(string MaGiaiDau)
        {
            return db.DoiBongs.Where(a => a.MaGiaiDau == MaGiaiDau).ToList();
        }


        public IEnumerable<CauThu> GetCauThu(string MaDoiBong)
        {
            return db.CauThus.Where(a => a.MaDoiBong == MaDoiBong).ToList();
        }



        [HttpPost]
        public bool ThemGiaiDau(string MaGiaiDau,string TenGiaiDau, int SoLuongDoi,string TheThucThiDau, bool GioiTinh, int TheLoaiSan)
        {
            try
            {
                GiaiDau GD = new GiaiDau();
                GD.MaGiaiDau = MaGiaiDau;
                GD.TenGiaiDau = TenGiaiDau;
                GD.SoLuongDoi = SoLuongDoi;
                GD.TheLoaiSan = TheLoaiSan;
                GD.TheThucThiDau = TheThucThiDau;
                GD.GioiTinh = GioiTinh;
                db.GiaiDaus.InsertOnSubmit(GD);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPut]
        public bool CapNhatGiaiDau(string MaGiaiDau, string TenGiaiDau, int SoLuongDoi, string TheThucThiDau, bool GioiTinh, int TheLoaiSan)
        {
            try
            {
                GiaiDau GD = db.GiaiDaus.FirstOrDefault(x => x.MaGiaiDau == MaGiaiDau);
                if (GD == null) return false;
                GD.MaGiaiDau = MaGiaiDau;
                GD.TenGiaiDau = TenGiaiDau;
                GD.SoLuongDoi = SoLuongDoi;
                GD.TheLoaiSan = TheLoaiSan;
                GD.TheThucThiDau = TheThucThiDau;
                GD.GioiTinh = GioiTinh;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpDelete]
        public bool XoaGiaiDau(string MaGiaiDau)
        {
            try
            {
                GiaiDau GD = db.GiaiDaus.FirstOrDefault(x => x.MaGiaiDau == MaGiaiDau);
                if (GD == null) return false;
                db.GiaiDaus.DeleteOnSubmit(GD);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
