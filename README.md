# Hidden Wing Brand Center

Internal platform for company branding, product branding, and white-label configuration.

Build once. Configure centrally. Reuse everywhere.

## What this is

Hidden Wing Brand Center is the source of truth for:

- Company branding
- Product branding
- Themes, colors, and typography
- Logos, favicons, and asset URLs
- Public branding payloads consumed by other Hidden Wing applications

It is a modular monolith, not a microservice suite.

## Architecture

```text
React admin UI
    ↓
ASP.NET Core Web API
    ↓
Application services
    ↓
Repositories
    ↓
Entity Framework Core
    ↓
PostgreSQL
```

Other applications should not read the database. They call:

```http
GET /api/v1/public/branding/{productSlug}
```

Example: `GET /api/v1/public/branding/store`

## Tech stack

- Backend: C#, .NET 8, ASP.NET Core, EF Core, PostgreSQL, FluentValidation, Swagger
- Frontend: TypeScript, React, Vite, CSS
- Tests: xUnit, Moq, WebApplicationFactory, Vitest
- Delivery: Docker, GitHub Actions

## Local development

### Environment variables

Copy `.env.example` and `src/frontend/hidden-wing-brand-center-web/.env.example`.

Never commit `.env` files or production secrets.

### Database

```bash
docker compose -f docker-compose.dev.yml up postgres
```

Connection string:

```text
Host=localhost;Port=5432;Database=hidden_wing_brand_center;Username=brandcenter;Password=brandcenter_dev
```

EF migrations apply automatically in Development.

Optional SQL scripts live in `database/scripts`.

### Backend

```bash
dotnet restore
dotnet run --project src/backend/HiddenWing.BrandCenter.Api
```

API: `http://localhost:5080`  
Swagger: `http://localhost:5080/swagger`

Default local admin: `admin` / `admin`

### Frontend

```bash
cd src/frontend/hidden-wing-brand-center-web
npm install
npm run dev
```

UI: `http://localhost:5173`

### Docker

```bash
docker compose -f docker-compose.dev.yml up --build
```

Production compose expects `JWT_SECRET`, `ADMIN_USERNAME`, and `ADMIN_PASSWORD` in the environment.

## API documentation

See [docs/api.md](docs/api.md). Public integration is documented in [docs/integration.md](docs/integration.md).

## Deployment

See [docs/deployment.md](docs/deployment.md).

## White-label integration

See [docs/white-label-guide.md](docs/white-label-guide.md).

## Tests

```bash
dotnet test HiddenWing.BrandCenter.sln
cd src/frontend/hidden-wing-brand-center-web
npm test
```
