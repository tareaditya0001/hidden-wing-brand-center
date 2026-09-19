# Hidden Wing Brand Center

Internal white-label and branding configuration platform for Hidden Wing applications.

## Architecture

Modular monolith:

```text
Controller → Service → Repository → BrandDbContext → PostgreSQL
```

- API: HTTP, auth, middleware
- Application: business rules, DTOs, validation, branding inheritance
- Domain: entities and enums
- Infrastructure: EF Core, PostgreSQL, file storage, JWT issuance
- React UI: admin dashboard

Repository interfaces live in Application so services do not depend on Infrastructure.

## Rules

- Database is the source of truth for branding.
- Public branding API is GET-only and unauthenticated.
- Admin APIs require JWT.
- Do not expose entities from controllers.
- Do not introduce extra deployable services.
- Never commit secrets or `.env`.
