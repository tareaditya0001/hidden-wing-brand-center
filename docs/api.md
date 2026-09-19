# API

Base path: `/api/v1`

Admin responses use a consistent envelope:

```json
{
  "success": true,
  "data": {}
}
```

Errors:

```json
{
  "success": false,
  "message": "Brand not found.",
  "errorCode": "BRAND_NOT_FOUND"
}
```

The public branding endpoint returns the branding DTO directly so consuming apps can read `product`, `theme`, and `contact` without an extra wrapper.

## Auth

```http
POST /api/v1/auth/login
```

## Brands

```http
GET    /api/v1/brands
GET    /api/v1/brands/{id}
POST   /api/v1/brands
PUT    /api/v1/brands/{id}
DELETE /api/v1/brands/{id}
```

## Products

```http
GET    /api/v1/products
GET    /api/v1/products/{id}
POST   /api/v1/products
PUT    /api/v1/products/{id}
DELETE /api/v1/products/{id}
GET    /api/v1/products/{id}/branding
PUT    /api/v1/products/{id}/branding
```

## Themes

```http
GET    /api/v1/themes
GET    /api/v1/themes/{id}
POST   /api/v1/themes
PUT    /api/v1/themes/{id}
DELETE /api/v1/themes/{id}
```

## Assets

```http
GET    /api/v1/assets
GET    /api/v1/assets/{id}
POST   /api/v1/assets
POST   /api/v1/assets/upload
PUT    /api/v1/assets/{id}
DELETE /api/v1/assets/{id}
```

## Public branding

```http
GET /api/v1/public/branding/{productSlug}
```

## Health

```http
GET /health
GET /health/ready
```

Interactive docs are available at `/swagger` in Development.
