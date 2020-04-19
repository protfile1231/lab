using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using lab.Models;
using lab.Storage;

namespace lab.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LabController : ControllerBase
   {
       private static IStorage<labData> _memCache = new MemCache();
       [HttpGet]
       public ActionResult<IEnumerable<labData>> Get()
       {
           return Ok(_memCache.All);
       }
       [HttpGet("{id}")]
       public ActionResult<labData> Get(Guid id)
       {
           if (!_memCache.Has(id)) return NotFound("No such");
           return Ok(_memCache[id]);
       }
       [HttpPost]
       public IActionResult Post([FromBody] labData value)
       {
           var validationResult = value.Validate();
           if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
           _memCache.Add(value);
           return Ok($"{value.ToString()} has been added");
       }
       [HttpPut("{id}")]
       public IActionResult Put(Guid id, [FromBody] labData value)

       {
           if (!_memCache.Has(id)) return NotFound("No such");
           var validationResult = value.Validate();
           if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
           var previousValue = _memCache[id];
           _memCache[id] = value;
           return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");

       }
       [HttpDelete("{id}")]
       public IActionResult Delete(Guid id)
       {
           if (!_memCache.Has(id)) return NotFound("No such");


           var valueToRemove = _memCache[id];

           _memCache.RemoveAt(id);


           return Ok($"{valueToRemove.ToString()} has been removed");
       }
   }
}