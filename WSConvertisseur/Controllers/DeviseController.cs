using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviseController : ControllerBase
    {
        public List<Devise> listDevises =  new List<Devise>();      

        public DeviseController()
        {
            listDevises.Add(new Devise(1, "Dollar", 1.08));
            listDevises.Add(new Devise(2, "Franc Suisse", 1.07));
            listDevises.Add(new Devise(3, "Yen", 120));
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the Devise list is found</response>
        /// <response code="404">When the Devise list is not found</response>
        // GET: api/<DeviseController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return listDevises;
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DeviseController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = listDevises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        /// <summary>
        /// Post a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">new Devise</param>
        /// <response code="200">When the devise is valid</response>
        /// <response code="404">When the devise is not valid</response>
        // POST api/<DeviseController>
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            listDevises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Update a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">new Devise</param>
        /// <response code="200">When the devise is valid</response>
        /// <response code="404">When the devise is not valid</response>
        // PUT api/<DeviseController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = listDevises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            listDevises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Delete a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">devise</param>
        /// <response code="200">When the devise is found</response>
        /// <response code="404">When the devise is not found</response>
        // DELETE api/<DeviseController>/5
        [HttpDelete("{id}", Name = "GetDevise")]
        public ActionResult<Devise> Delete(int id)
        {
            Devise? devise = listDevises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            listDevises.Remove(devise);
            return devise;
        }
    }
}
