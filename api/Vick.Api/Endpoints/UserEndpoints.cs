using Microsoft.AspNetCore.Mvc;
using Vick.Api.Endpoints.Internal;
using Vick.Api.Endpoints;
using Vick.Core.Interfaces;
using Vick.Core.Models;
using System.Data.SqlClient;

public class UserEndpoints : IEndpoints
{
    private const string BasePath = "/user";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(BasePath, GetUser);
        app.MapPost($"{BasePath}/update", UpdateUser);
        app.MapPost($"{BasePath}/create", CreateUser);
        app.MapGet($"{BasePath}/all", GetAllUsers);
        app.MapPost($"{BasePath}/login", LoginUser);
        app.MapDelete($"{BasePath}/delete/{{id}}", DeleteUser);
    }

    private static IResult GetUser([FromServices] IUserService userService)
    {
        var user = userService.GetUser();
        return user != null ? Results.Ok(user) : Results.NotFound("Active user not found.");
    }

    private static IResult UpdateUser(User user, [FromServices] IUserService userService)
    {
        try
        {
            userService.UpdateUser(user);
            return Results.Ok();
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static IResult CreateUser(User user, [FromServices] IUserService userService)
    {
        var createdUser = userService.CreateUser(user.Name, user.Email, user.Password);
        return Results.Ok(createdUser);
    }

    private static IResult GetAllUsers([FromServices] IUserService userService)
    {
        var users = userService.GetAllUsers();
        return Results.Ok(users);
    }

    private static IResult LoginUser(LoginModel loginModel, [FromServices] IUserService userService)
    {
        if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
        {
            return Results.BadRequest("Email and password are required for login.");
        }

        var user = userService.GetUserByEmail(loginModel.Email);
        if (user == null || user.Password != loginModel.Password)
        {
            return Results.BadRequest("Invalid email or password.");
        }

        return Results.Ok(user);
    }

    private static IResult DeleteUser(int id, [FromServices] IUserService userService)
    {
        try
        {
            userService.DeleteUser(id);
            return Results.Ok();
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
