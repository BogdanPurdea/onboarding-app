# Meridian Onboarding App

## Local Setup & Run Instructions

Follow these instructions to run the database, backend server, frontend client, and the local LLM chatbot locally.

---

### Quick Start – Docker (Recommended)

If you have [**Docker Desktop**](https://www.docker.com/products/docker-desktop/) installed, a single command starts all three services (PostgreSQL, .NET API, and React client) with the database auto-migrated and seeded:

```bash
docker compose up --build
```

The application will be available at **`http://localhost:3000`** once all containers are healthy. No other installs required.

> **Note**: The chat assistant requires Ollama to be running separately on the host machine (see [Setup Local Ollama Chatbot](#4-setup-local-ollama-chatbot-optional) below). All other features work inside Docker.

---

### Manual Setup Prerequisites
If you prefer to run services natively, make sure you have the following installed:
1. [**.NET 10 SDK**](https://dotnet.microsoft.com/en-us/download) (to compile and run the backend)
2. [**Node.js**](https://nodejs.org/en) (v18+ recommended) and [**npm**](https://www.npmjs.com/) (to run the React client)
3. [**PostgreSQL**](https://www.postgresql.org/download/) Database Server (running on port `5432` with username `postgres` and password `postgres`)
4. [**Ollama**](https://ollama.com/) (optional, to run the local Chat Assistant)

---


### 1. Database Setup
The C# backend uses Entity Framework Core (Code First) to manage the database schema.

> **Option A – Docker (recommended, no PostgreSQL install required)**
> A `docker-compose.yml` is included at the root of `onboarding_app` that spins up a pre-configured PostgreSQL 16 container with the correct credentials:
> ```bash
> docker compose up -d
> ```
> This starts a `postgres` container on port `5432` with `POSTGRES_USER=postgres`, `POSTGRES_PASSWORD=postgres`, and `POSTGRES_DB=onboardingapp` — matching `appsettings.json` exactly. No further configuration is needed.

> **Option B – Native PostgreSQL**
> If you already have PostgreSQL installed locally, make sure the server is active. The default connection string is configured in `appsettings.json`:
> ```json
> "DefaultConnection": "Host=localhost;Database=onboardingapp;Username=postgres;Password=postgres;"
> ```
> If your local PostgreSQL setup uses a different username or password, update this connection string before continuing.

1. **Install EF Core Tool**: (If you don't have it installed globally)
   ```bash
   dotnet tool install --global dotnet-ef
   ```
2. **Apply Migrations**: Execute the following command from the root of `onboarding_app` to create the `onboardingapp` database, create all tables, and seed the initial dataset:
   ```bash
   dotnet ef database update --project server/OnboardingApp.Api.csproj
   ```

---

### 2. Run Backend API Server
1. Navigate to the `onboarding_app` workspace root.
2. Run the C# Web API using the HTTP profile:
   ```bash
   dotnet run --project server/OnboardingApp.Api.csproj --launch-profile http
   ```
3. The server will start and listen on **`http://localhost:5245`**.

---

### 3. Run React Frontend Client
1. Navigate to the `client/` folder:
   ```bash
   cd client
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the Vite development server:
   ```bash
   npm run dev
   ```
4. The React application will start and be accessible at **`http://localhost:5173`**.

---

### 4. Setup Local Ollama Chatbot (Optional)
The chat assistant queries a local LLM to support natural language questions and onboarding task tool execution.

1. **Download & Install Ollama** from [ollama.com](https://ollama.com).
2. **Download Model**: Pull the `gemma4:latest` model locally:
   ```bash
   ollama pull gemma4
   ```
3. **Run Ollama with CORS enabled**: Start the Ollama daemon allowing cross-origin requests from the browser:
   ```bash
   OLLAMA_ORIGINS="*" ollama serve
   ```
4. Refresh the client page. The chat drawer status will change to **Navigator Bot • Active**, allowing chat interaction. If Ollama is shut down, the chat falls back to local rule-based keyword mocked replies.
