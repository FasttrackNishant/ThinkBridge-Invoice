# ThinkBridge Invoicing System  

## 🔗 URLs  
- **Frontend (Blazor Server UI)**: https://thinkbridge-client.azurewebsites.net/  
- **Backend (Web API + Swagger)**: https://thinkbridge-api.azurewebsites.net/

---

## ▶️ Run Locally  

### Run Backend (API)  
```bash
cd ThinkBridge.Api
dotnet run
```
Runs on ** https://localhost:7283  || http://localhost:5065**  

Swagger: https://localhost:7283/swagger  

---

### Run Frontend (Blazor Server)  
```bash
cd ThinkBridge.Client
dotnet run
```
Runs on ** https://localhost:7024 ||  http://localhost:5225 **  

---

## ⚙️ Configuration  

In **ThinkBridge.Client/Program.cs** set backend API URL:  

```csharp
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:7283/");
});
```

# Connection String

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DB_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
}
```


# Application Image
<img width="1464" height="853" alt="Screenshot 2025-09-19 at 5 02 07 PM" src="https://github.com/user-attachments/assets/80d02535-25b9-4c29-866a-9c0ad90c73f9" />

<img width="1466" height="851" alt="Screenshot 2025-09-19 at 5 12 45 PM" src="https://github.com/user-attachments/assets/3a02000a-a26c-43b9-880d-2030a26bb84d" />
