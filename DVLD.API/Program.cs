using DVLD.Business;
using FluentValidation.AspNetCore;
// using DVLD.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 1.  ”ÃÌ· Œœ„«  Swagger (»œ·« „‰ OpenApi «·ﬁœÌ„…)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddProjectDependencies(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DVLD API V1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();