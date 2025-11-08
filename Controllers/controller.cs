using Microsoft.AspNetCore.Mvc;

public class User
{
    public string? userName { get; set; }
    public required int? userId { get; set; }
    public string? userDetails { get; set; }
}



[ApiController]
[Route("api/[controller]")]
public class TechHiveController : ControllerBase
{
    List<User> users = new List<User>
    {
        new User {userName = "Minseok Kim", userId = 1, userDetails = "This is Minseok"},
        new User {userName = "Yuta Bordes", userId = 2, userDetails = "This is Yuta"},
        new User {userName = "Ian", userId = 3, userDetails = "This is Ian"}
    };

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUserList()
    {
        return Ok(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUser(int id)
    {
        return Ok(users.Single(user => user.userId == id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddUser(User user)
    {
        if (user.userId is not null && user.userName is not null)
        {
            users.Add(user);
            return Ok(users);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateUser(int id, User userToUpdate)
    {
        if (userToUpdate == null || userToUpdate.userId == null) return BadRequest();

        var currentUserToUpdate = users.Single(user => user.userId == id);
        if (currentUserToUpdate == null) return BadRequest();
        currentUserToUpdate.userDetails = userToUpdate.userDetails;
        return Ok(currentUserToUpdate);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteUser(int id)
    {
        var userToDelete = users.Single(user => user.userId == id);
        if (userToDelete == null) return BadRequest();
        users.Remove(userToDelete);
        return Ok(users);
    }

}

