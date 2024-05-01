using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Vick.Api.Endpoints.Internal;
using Vick.Core.Interfaces;
using Vick.Core.Models;

namespace Vick.Api.Endpoints
{
    public class UserEndpoints : IEndpoints
    {
        private const string BasePath = "/user";

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet($"{BasePath}", GetUser);
            app.MapPost($"{BasePath}/update", UpdateUser);
            app.MapPost($"{BasePath}/create", CreateUser);
            app.MapGet($"{BasePath}/all", GetAllUsers);
            app.MapPost($"{BasePath}/login", LoginUser);
            app.MapDelete($"{BasePath}/delete/{{id}}", DeleteUser);


        }

        private static async Task<IResult> GetUser(IUserService userService)
        {
            var user = userService.GetUser();
            return Results.Ok(user);
        }

        private static async Task<IResult> UpdateUser(User user, IUserService userService)
        {
            userService.UpdateUser(user);
            return Results.Ok();
        }

        private static async Task<IResult> CreateUser(User user, IUserService userService)
        {
            var name = user.Name;
            var email = user.Email; 
            var password = user.Password;
            var phoneNumber = user.PhoneNumber;
            var timeSlots = user.TimeSlots;

            userService.CreateUser(name, email, password, phoneNumber, timeSlots);
            return Results.Ok();
        }


        private static async Task<IResult> GetAllUsers(IUserService userService)
        {
            var users = userService.GetAllUsers();
            return Results.Ok(users);
        }

        private static async Task<IResult> LoginUser(LoginModel loginModel, IUserService userService)
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

        private static async Task<IResult> DeleteUser(int id, IUserService userService)
        {
            userService.DeleteUser(id);
            return Results.Ok();
        }



    }
}
