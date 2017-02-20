using Microsoft.AspNetCore.Mvc;
using FisherInsuranceApi.Data;
using FisherInsuranceApi.Models;

[RouteAttribute("api/auto/quotes")]
public class AutoController : Controller

{
    private IMemoryStore db;

    public AutoController(IMemoryStore repo)
    {
        db=repo;
    }
    
// POST api/auto/quotes

    [HttpGet]
    public IActionResult GetQuotes()
    {
        return Ok(db.RetrieveAllQuotes);
    }


    [HttpGetAttribute("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(db.RetrieveQuote(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody]Quote quote)
    {
        return Ok(db.CreateQuote(quote));
    }

    [HttpPost]
    


// PUT api/auto/quotes/id
    [HttpPut("{id}")]
    public IActionResult Put([FromBodyAttribute]Quote quote)
    {
        return Ok(db.UpdateQuote(quote));
    }

// DELETE api/auto/quotes/id

    [HttpDeleteAttribute("{id}")]
    public IActionResult Delete([FromBodyAttribute]Quote quote)
    {
        db.DeleteQuote(quote.Id);
        return Ok();
    }

}
