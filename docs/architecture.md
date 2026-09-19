# Architecture

Hidden Wing Brand Center is one deployable backend and one React admin UI.

```text
Hidden Wing Brand Center
├── API
├── Application
├── Domain
├── Infrastructure
└── React UI
```

## Request flow

```text
Controller → Service → Repository → BrandDbContext → PostgreSQL
```

Controllers stay thin. They validate incoming models and return HTTP responses.

Services own business rules:

- Slug uniqueness
- Default theme protection
- Product branding upsert
- Brand → theme → product override inheritance

Repositories only persist and query.

## Branding inheritance

```text
Global Hidden Wing brand
    ↓
Default or assigned theme
    ↓
Product branding overrides
    ↓
Consuming application
```

The first non-empty value wins. This is implemented in `BrandingResolver`, not a rules engine.

## Public vs admin

- `/api/v1/public/branding/{slug}` is anonymous GET.
- All management endpoints require JWT.
- `/health` and `/health/ready` are anonymous.

JWT is a temporary internal mechanism. It can later be replaced by Hidden Wing HQ identity without changing controllers.

## Caching

V1 reads PostgreSQL on every public request through `IPublicBrandingService`. A cache can wrap that service later without changing the public controller.
