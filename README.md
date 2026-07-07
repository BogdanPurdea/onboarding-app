# Meridian Onboarding App

Welcome to the Meridian Onboarding App! This application helps new hires navigate their onboarding tracks, complete role-specific tasks, view department information, connect with team buddies, and sync progress anonymously across sessions.

---

## Local Setup & Run Instructions

Follow these instructions to run the database, backend server, frontend client, and the local LLM chatbot on your local machine.

### Prerequisites
Make sure you have the following installed:
1. **.NET 10 SDK** (to compile and run the backend)
2. **Node.js** (v18+ recommended) and **npm** (to run the React client)
3. **PostgreSQL** Database Server (running on port `5432` with username `postgres` and password `postgres`)
4. **Ollama** (optional, to run the local AI Chat Assistant)

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
The "Ask Meridian" chat assistant queries a local LLM to support natural language questions and onboarding task tool execution.

1. **Download & Install Ollama** from [ollama.com](https://ollama.com).
2. **Download Model**: Pull the `gemma4:latest` model locally:
   ```bash
   ollama pull gemma4
   ```
3. **Run Ollama with CORS enabled**: Start the Ollama daemon allowing cross-origin requests from the browser:
   ```bash
   OLLAMA_ORIGINS="*" ollama serve
   ```
4. Refresh your client page. The chat drawer status will change to **Navigator Bot • Active**, enabling interactive chat with tool execution loops (fetching tasks, schedules, buddies). If Ollama is shut down, the chat gracefully falls back to local rule-based keyword replies.
