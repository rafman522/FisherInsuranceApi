using Microsoft.AspNetCore.Mvc;

[RouteAttribute("api/home/quotes")]

public class HomeController : Controller

{
// POST api/auto/quotes

    [HttpPost]
    public IActionResult Post([FromBodyAttribute]string value)
    {
        return Created("", value);
    }

// GET api/auto/quotes/5
    [HttpGetAttribute("{id}")]
    public IActionResult Get(int id)
    {
        return Ok("The id is: " + id);
    }

// PUT api/auto/quotes/id
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBodyAttribute]string value)
    {
        return NoContent();
    }

// DELETE api/auto/quotes/id

    [HttpDeleteAttribute("{id}")]
    public IActionResult Delete(int id)
    {
        return Delete(id);
    }

}
