using NorthwindService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace NorthwindService.Controllers
{
  public class ProductsController : ODataController
  {
    NothwindContext db = new NothwindContext();
    private bool ProductExists(int key)
    {
      return db.Products.Any(p => p.ProductID == key);
    }

    [EnableQuery]
    public IQueryable<Product> Get()
    {
      return db.Products;
    }

    [EnableQuery]
    public SingleResult<Product> Get([FromODataUri] int key)
    {
      IQueryable<Product> result = db.Products.Where(p => p.ProductID == key);
      return SingleResult.Create(result);
    }

    public async Task<IHttpActionResult> Post(Product product)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      db.Products.Add(product);
      await db.SaveChangesAsync();
      return Created(product);
    }

    protected override void Dispose(bool disposing)
    {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}
