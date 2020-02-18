using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Initiate a dal
        Dal db = new Dal();

        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            try
            {
                DataTable dt = db.GetData("uspGetProductAll3d", "<parameters></parameters>");
                var result = (from rw in dt.Select()
                              select new Product
                              {
                                  ID = Convert.ToInt32(rw["ID"]),
                                  Content = Convert.ToString(rw["Content"]),
                                  OwnerID = Convert.ToString(rw["LogonID"])
                              }).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                DataTable dt = db.GetData("uspGetProductByProductID", "<parameters><productID>" + id + "</productID></parameters>");
                var result = new Product
                {
                    ID = Convert.ToInt32(dt.Rows[0]["ProductID"]),
                    Content = Convert.ToString(dt.Rows[0]["Content"]),
                    OwnerID = Convert.ToString(dt.Rows[0]["LogonID"])
                };
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: api/Product
        [HttpPost]
        public void Post(string value)
        {
            string preParm = value;
            try
            {
                db.SetData("uspCreateProductByLogonID", preParm);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
            string preParm = value;
            try
            {
                db.SetData("uspUpdateProductByID", preParm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string preParm = "<parameters><ID>" + id + "</ID></parameters>";
            try
            {
                db.SetData("uspDeleteProductByID", preParm);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
