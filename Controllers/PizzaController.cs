using webapi.Models;
using webapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

// GET all action

    [HttpGet] // his attribute is used to specify that the action method should respond to HTTP GET requests. In ASP.NET Core, the HTTP verb attribute ([HttpGet], [HttpPost], [HttpPut], [HttpDelete], etc.) is used to define the type of HTTP request that the action method can respond to.
public ActionResult<List<Pizza>> GetAll() => // ActionResult<List<Pizza>>: This is the return type of the method. ActionResult<T> is a generic type where T represents the type of data returned in the HTTP response body
    PizzaService.GetAll();


// GET by Id action
    [HttpGet("{id}")]
public ActionResult<Pizza> Get(int id)
{
    var pizza = PizzaService.Get(id);

    if(pizza == null)
        return NotFound();

    return pizza;
}

    // POST action

[HttpPost]
public IActionResult Create(Pizza pizza)
{            
    // This code will save the pizza and return a result
    PizzaService.Add(pizza);
    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
}

    // PUT action

    [HttpPut("{id}")]
public IActionResult Update(int id, Pizza pizza)
{
    // This code will update the pizza and return a result
     if (id != pizza.Id)
        return BadRequest();
           
    var existingPizza = PizzaService.Get(id);
    if(existingPizza is null)
        return NotFound();
   
    PizzaService.Update(pizza);           
   
    return NoContent();
}

    // DELETE action
    [HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    // This code will delete the pizza and return a result
    var pizza = PizzaService.Get(id);
   
    if (pizza is null)
        return NotFound();
       
    PizzaService.Delete(id);
   
    return NoContent();
}
}