using Microsoft.AspNetCore.Mvc;
using FisherInsuranceApi.Models;
using FisherInsuranceApi.Data;

[RouteAttribute("api/claims")]

public class ClaimsController : Controller

{

    private IMemoryStore db;

    public ClaimsController(IMemoryStore repo)
    {
        db=repo;
    }

    [HttpGet]
    public IActionResult GetClaims()
    {
        return Ok(db.RetrieveAllClaims);
    }


    [HttpGetAttribute("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(db.RetrieveClaim(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody]Claim claim)
    {
        return Ok(db.CreateClaim(claim));
    }


// PUT api/auto/quotes/id
    [HttpPut("{id}")]
    public IActionResult Put([FromBodyAttribute]Claim claim)
    {
        return Ok(db.UpdateClaim(claim));
    }

// DELETE api/auto/quotes/id

    [HttpDeleteAttribute("{id}")]
    public IActionResult Delete([FromBodyAttribute]Claim claim)
    {
        db.DeleteClaim(claim.Id);
        return Ok();
    }

}
