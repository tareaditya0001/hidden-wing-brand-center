# Integration

Other Hidden Wing applications should consume branding over HTTP. Do not copy values into frontend constants.

## Endpoint

```http
GET /api/v1/public/branding/store
```

Response:

```json
{
  "company": {
    "name": "Hidden Wing",
    "logo": "/assets/hidden-wing/logo.svg"
  },
  "product": {
    "name": "Hidden Wing Store",
    "slug": "store",
    "logo": "/assets/store/logo.svg",
    "favicon": "/assets/store/favicon.svg"
  },
  "theme": {
    "primaryColor": "#1C3353",
    "secondaryColor": "#3E536B",
    "accentColor": "#C6A15B",
    "backgroundColor": "#F4EFE6",
    "textColor": "#1A1F29"
  },
  "contact": {
    "supportEmail": null
  }
}
```

## TypeScript example

```ts
interface PublicBranding {
  company: { name: string; logo?: string | null };
  product: { name: string; slug: string; logo?: string | null; favicon?: string | null };
  theme: { primaryColor: string; secondaryColor: string; accentColor: string; backgroundColor: string; textColor: string };
  contact: { supportEmail?: string | null };
}

export async function getBranding(productSlug: string): Promise<PublicBranding> {
  const response = await fetch(`${import.meta.env.VITE_BRAND_CENTER_URL}/api/v1/public/branding/${productSlug}`);
  if (!response.ok) {
    throw new Error(`Unable to load branding for ${productSlug}`);
  }

  return response.json() as Promise<PublicBranding>;
}

const branding = await getBranding("store");
document.documentElement.style.setProperty("--hw-primary", branding.theme.primaryColor);
```

A future package can wrap this as `@hidden-wing/brand-client`. V1 uses standard HTTP only.
