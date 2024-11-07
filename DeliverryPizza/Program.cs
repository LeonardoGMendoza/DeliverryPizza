using DeliverryPizza.Business;
using DeliverryPizza.Business.Interfaces;
using DeliverryPizza.Business.RabbitMQClient;
using DeliverryPizza.Repository;
using DeliverryPizza.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com string de conexão
builder.Services.AddDbContext<PedidoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeliverryPizzaConnection")));

// Registro de serviços para injeção de dependências
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<PedidoService>();

// Configuração do RabbitMQClient com a interface IRabbitMQService
var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddSingleton<IRabbitMQService>(sp =>
    new RabbitMQService(
        rabbitMQSettings["HostName"] ?? "localhost",
        rabbitMQSettings["QueueName"] ?? "pedido_queue"
    ));

// Configuração do CORS para permitir requisições de outros domínios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        // Adicione o endereço do seu frontend aqui
        policy.WithOrigins("http://192.168.1.2:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuração para controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração para o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar o CORS antes de outros middlewares
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();




//using DeliverryPizza.Business;
//using DeliverryPizza.Business.Interfaces;
//using DeliverryPizza.Repository;
//using DeliverryPizza.Repository.Interface;
//using DeliverryPizza.Business.RabbitMQClient;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Configuração do DbContext com string de conexão
//builder.Services.AddDbContext<PedidoContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DeliverryPizzaConnection")));

//// Registro de serviços para injeção de dependências
//builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
//builder.Services.AddScoped<PedidoService>();

//// Configuração do RabbitMQClient com a interface IRabbitMQService
//var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
//builder.Services.AddSingleton<IRabbitMQService>(sp =>
//    new RabbitMQService(
//        rabbitMQSettings["HostName"] ?? "localhost",
//        rabbitMQSettings["QueueName"] ?? "pedido_queue"
//    ));

//// Configurações para controllers e Swagger
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configuração para o ambiente de desenvolvimento
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();



//using DeliverryPizza.Repository;
//using DeliverryPizza.Business;
//using DeliverryPizza.Business.Interfaces;
//using DeliverryPizza.Business.RabbitMQClient;
//using Microsoft.EntityFrameworkCore;
//using DeliverryPizza.Repository.Interface;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PedidoContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DeliverryPizzaConnection")));


//builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
//builder.Services.AddScoped<PedidoService>();
//builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();


//// Configuração do RabbitMQClient com a interface IRabbitMQService
//var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
//builder.Services.AddSingleton<IRabbitMQService>(sp =>
//{
//    return new RabbitMQService(rabbitMQSettings["HostName"], rabbitMQSettings["QueueName"]);
//});

//// Configurações para controllers e Swagger
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configuração para o ambiente de desenvolvimento
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
