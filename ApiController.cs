using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml.Linq;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public User[] Users = new User[5];

        public ApiController() {
            //Fill array so there's data to be seen
            Users[0] = new User(0, "User1");
            Users[1] = new User(1, "User2");
            Users[2] = new User(2, "User3");
            Users[3] = new User(3, "User4");
            Users[4] = new User(4, "User5");

        }


        //Get all Users
        [Route("get")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //returns the array as-is
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get user by id
        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id) 
        {
            try
            {
                //returns the array at the id specified
                return Ok(Users[id]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Add user to array
        [Route("post")]
        [HttpPost]
        public IActionResult Post([FromBody] User name)
        {
            //check for valid input
            if (name.Id >= Users.Length) {
                return BadRequest("Id exceeds capacity of storage");
            }
            try
            {
                //sets request body as the id specified
                Users[name.Id] = name;
                //Return full array to show change in place
                //Will not save, because we're using an array rather than something good
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //Edit name of user
        [Route("put/{id}")]
        [HttpPut]
        public IActionResult Put(int id, [FromBody] string newName)
        {
            //check for valid input
            if (id >= Users.Length)
            {
                return BadRequest("Id exceeds capacity of storage");
            }
            try
            {
                //Sets new name at specified id
                Users[id].Name = newName;
                //return full array to show changes
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Clear user name from array
        //Leaves a blank, but they asked for a simple api. If it were something more serious, I wouldn't use an array.
        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                Users[id].Name = null;
                //Return the full array so changes can be seen
                return Ok(Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}