using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefBox.Cooking.Dto.Recipe;
using ChefBox.Cooking.IData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChefBox.ServiceExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IRecipeRepository RecipeRepository { get; }
        public ValuesController(IRecipeRepository recipeRepository)
        {
            RecipeRepository = recipeRepository;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> Get()
        {
            //return RecipeRepository.GetAllRecipes(null).ToArray();
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
