using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : Controller
    {
        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        // GET: api/Quotes
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_quotesDbContext.Quotes);
        }

        // GET: api/Quotes/1
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Quote> Get(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);

            if (quote == null)
            {
                return NotFound("No record with that ID");
            }

            return Ok(quote);
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var entity = _quotesDbContext.Quotes.Find(id);

            if (entity == null)
            {
                return NotFound("No record with that ID");
            }

            entity.Title = quote.Title;
            entity.Description = quote.Description;
            entity.Author = quote.Author;
            _quotesDbContext.SaveChanges();

            return Ok("Record updated Successfully...");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            
            if (quote == null)
            {
                return NotFound("No record with that ID");
            }
            
            _quotesDbContext.Quotes.Remove(quote);
            _quotesDbContext.SaveChanges();

            return Ok("Deleted...");
        }
    }
}