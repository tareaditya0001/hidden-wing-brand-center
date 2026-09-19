# Deployment

## Requirements

- PostgreSQL 16
- .NET 8 runtime or the backend container
- Nginx or the frontend container
- Environment variables for connection string, JWT secret, and admin credentials

## Environment

Required:

```env
ConnectionStrings__DefaultConnection=
Security__JwtSecret=
Security__AdminUsername=
Security__AdminPassword=
CORS__AllowedOrigins__0=
Database__ApplyMigrations=true
```

Frontend build argument:

```env
VITE_API_BASE_URL=
```

## Docker

From the repository root:

```bash
export JWT_SECRET=your-32-character-or-longer-secret
export ADMIN_USERNAME=admin
export ADMIN_PASSWORD=choose-a-strong-password
docker compose up --build
```

Do not put production secrets in compose files.

## CI

GitHub Actions runs restore, build, test, typecheck, frontend build, and Docker image builds. Production image publishing only runs if CI succeeds.
