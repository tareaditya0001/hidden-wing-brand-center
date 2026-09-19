# Database

PostgreSQL is the source of truth for dynamic branding.

Schema is managed with EF Core migrations in `src/backend/HiddenWing.BrandCenter.Infrastructure/Persistence/Migrations`.

## Tables

- `brands`
- `products`
- `product_brandings`
- `themes`
- `brand_assets`

## Relationships

- Product belongs to one brand.
- Product branding is 1:1 with product.
- Product branding may reference a theme.
- Assets belong to a brand and optionally a product.

## Seeded V1 data

Brand: Hidden Wing (`hidden-wing`)

Products:

- Hidden Wing HQ (`hq`)
- Hidden Wing Store (`store`)
- Hidden Wing Projects (`projects`)
- Hidden Wing Data (`data`)
- Hidden Wing Admin (`admin`)

Theme: Hidden Wing Default (`hidden-wing-default`)

SQL helpers:

- `database/scripts/seed.sql`
- `database/scripts/cleanup.sql`
