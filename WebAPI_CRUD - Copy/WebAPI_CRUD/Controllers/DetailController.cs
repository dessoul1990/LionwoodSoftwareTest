using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Controllers
{
    public class DetailController : ApiController
    {
        //GET - Retrieve data
        public IHttpActionResult GetAllDetail()
        {
            IList<DetailViewModel> details = null;
            using (var x = new BicycleDetailsEntities())
            {
                details = x.Details
                           .Select(c => new DetailViewModel()
                           {
                               Id = c.id,
                               Name = c.name,
                               Color = c.color,
                               Brand = c.brand,
                               Country = c.country,

                           }).ToList<DetailViewModel>();
            }
            if (details.Count == 0)
                return NotFound();

            return Ok(details);
        }
        //POST - Insert data
        public IHttpActionResult PostNewDetail(DetailViewModel detail)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data, Please double check");

            using (var x = new BicycleDetailsEntities())
            {
                x.Details.Add(new Detail()
                {
                    name = detail.Name,
                    color = detail.Color,
                    brand = detail.Brand,
                    country = detail.Country,
                });
                x.SaveChanges();
            }
            return Ok();
        }
        //PUT - Update data
        public IHttpActionResult PutDetail(DetailViewModel detail)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is invalid, Please double check");
            using (var x = new BicycleDetailsEntities())
            {
                var checkExistingDetail = x.Details.Where(c => c.id == detail.Id)
                                                     .FirstOrDefault<Detail>();
                if(checkExistingDetail != null)
                {
                    checkExistingDetail.name = detail.Name;
                    checkExistingDetail.color = detail.Color;
                    checkExistingDetail.brand = detail.Brand;
                    checkExistingDetail.country = detail.Country;

                    x.SaveChanges();

                }
                else
                    return NotFound();
            }
            return Ok();
        }
        //DELETE - Delete record
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Please enter a valid Id");

            using (var x = new BicycleDetailsEntities())
            {
                var detail = x.Details
                    .Where(c => c.id == id)
                    .FirstOrDefault();

                x.Entry(detail).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }
    }
}
