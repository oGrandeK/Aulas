using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Infra.Contexts.AccountContext.UseCases.Create;
using MediatR;

namespace JwtStore.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAcountContext(this WebApplicationBuilder builder) {
        #region Create 

        builder.Services.AddTransient<IRepository, Repository>();
        builder.Services.AddTransient<IService, Service>();

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app) {
        #region Create

        app.MapPost("api/v1/users", async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Response> handler) =>
                {
                    var result = await handler.Handle(request, new CancellationToken());
                    return result.IsSuccess ? Results.Created("", result) : Results.Json(result, statusCode: result.Status);
                });

        #endregion
    }
}