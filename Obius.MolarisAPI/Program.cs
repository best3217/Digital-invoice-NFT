var AllowedOrigins = "_allowedOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowedOrigins,
                    policy =>
                    {
                        //http://testnet2.digital-invoice.io/

#if DEBUG
                        policy.WithOrigins("https://localhost:7091")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
#else
policy.WithOrigins("https://testnet2.digital-invoice.io")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
#endif
                    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(AllowedOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Moralis.MoralisClient.ConnectionData = new Moralis.Models.ServerConnectionData()
{
    ApiKey = "k33i0l2zg4anDtEUiWdW8ltTlOfsTpcW4MaoUCLJweIceI8XgXnoyYpLlWXF2MYd"
};

app.Run();
