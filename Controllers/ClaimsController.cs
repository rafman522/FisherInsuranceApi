using Microsoft.AspNetCore.Mvc;
using FisherInsuranceApi.Models;
using FisherInsuranceApi.Data;

[RouteAttribute("api/claims")]

public class ClaimsController : Controller

{

    private readonly FisherContext db

    public ClaimsController(FisherContext context)
    {
        db=context;
    }

    [HttpGet]
    public IActionResult GetClaims()
    {
        return Ok(db.Claims;
    }


    [HttpGetAttribute("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(db.Claims.Find(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody]Claim claim)
    {
        var newClaim = db.Claims.Add(claim);
        db.SaveChanges();

        return CreatedAtRoute("GetClaim", new {id=claim.Id}, claim);
    }

    

    // PUT api/auto/quotes/id
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBodyAttribute]Claim claim)
    {
        var newClaim = db.Claims.Find(id);

        if (newClaim == null)
        {
            return NotFound();
        }
        newClaim = claim;
        db.SaveChanges();
        return Ok(newClaim);
    }

// DELETE api/auto/quotes/id

    [HttpDeleteAttribute("{id}")]
    public IActionResult Delete(int id)
    {
        var claimToDelete = db.Claims.Find(id);
        if (claimToDelete == null)
        {
            return NotFound();
        }
        db.Claims.Remove(claimToDelete);
        db.SaveChangesAsync();

        return NoContent();
    }

}
